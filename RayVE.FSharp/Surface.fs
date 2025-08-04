namespace RayVE

type Pattern =
    | Solid of Color * Matrix
    | Stripe of Color * Color * Matrix
    | Ring of Color * Color * Matrix
    | Gradient of Color * Color * Matrix

and Material =
    | Phong of Ambience: float * Diffuse: float * Specular: float * Shininess: float * Pattern: Pattern

and Surface =
    | Plane of Material * transform: Matrix
    | Sphere of Material * transform: Matrix
    | Null

and Intersection = {
    Distance: double
    Surface: Surface
    Ray: Ray
}

module Pattern =
    let inverseTransform pattern =
        match pattern with
            | Solid (_, transform) -> transform
            | Stripe (_, _, transform) ->  transform
            | Ring (_, _, transform) -> transform
            | Gradient (_, _, transform) -> transform
        |> Matrix.invert

    let colorAt pattern (point: Point3D) =
        match pattern with
        | Solid (color, _) -> color
        | Stripe (color1, color2, _) ->
            let checkValue = floor(point.X) % 2.
            if checkValue = 0.0 then
                color1
            else color2
        | Ring (color1, color2, _) ->
            let checkValue = floor(sqrt(point.X ** 2 + point.Y ** 2)) % 2.
            if checkValue = 0.0 then
                color1
            else color2
        | Gradient (color1, color2, _) ->
            let colorDistance = color2 - color1
            let fraction = point.X - floor(point.X)
            color1 + (colorDistance * fraction)
            
module Material =
    let reflectVector normal lightVector =
        Vector3D.reflect -lightVector normal

    let illuminate (material: Material) (point: Point3D) (patternColor: Color) (eye: Vector3D) (normal: Vector3D) (light: LightSource) inShadow =
        let lightSourcePosition =
            match light with
            | PointLightSource (position, _) -> position
        let lightVector = Vector3D.normalize (lightSourcePosition - point)
        let lightSourceColor =
            match light with
            | PointLightSource (_, color) -> color
        let illuminationColor = patternColor * lightSourceColor
        let lightDotNormal = lightVector * normal

        match material with
        | Phong (matAmbience, matDiffuse, matSpecular, matShininess, _) ->
            let diffuse = if lightDotNormal > 0.0
                          then illuminationColor * matDiffuse * lightDotNormal
                          else Color.Predefined.Black

            let specular = if lightDotNormal > 0.0 then
                               let reflectVector = reflectVector normal lightVector
                               let reflectDotEye = reflectVector * eye
                               
                               if reflectDotEye > 0.0 then
                                   let specularFactor = reflectDotEye ** matShininess
                                   lightSourceColor * matSpecular * specularFactor
                               else Color.Predefined.Black
                           else Color.Predefined.Black

            let ambience = illuminationColor * matAmbience
            if inShadow then ambience
            else ambience + diffuse + specular

module Surface =
    let transform = function
        | Plane (_, transform) -> transform
        | Sphere (_, transform) -> transform
        | Null -> Matrix.Create.identity 4

    let inverseTransform surface =
        surface
        |> transform
        |> Matrix.invert

    let transposeInverseTransform surface =
        surface
        |> inverseTransform
        |> Matrix.transpose

    let normalLocal surface (point: Point3D) =
        match surface with
        | Plane _ -> Vector3D(0.0, 1.0, 0.0)
        | Sphere _ -> point - Point3D.Create.zero |> Vector3D.normalize
        | Null -> Vector3D.Create.zero

    let normal surface (point: Point3D) =
        let localizedPoint = inverseTransform surface * point
        transposeInverseTransform surface * normalLocal surface localizedPoint
        |> Vector3D.normalize

    let intersectLocal surface (localRay: Ray) =
        match surface with
        | Plane _ -> if abs(localRay.Direction.Y) < Constants.EPSILON then [||]
                     else [| -localRay.Origin.Y / localRay.Direction.Y |]
        | Sphere _ ->
            let connectOrigins = localRay.Origin - Point3D.Create.zero
            let a = localRay.Direction * localRay.Direction
            let b = 2.0 * localRay.Direction * connectOrigins
            let c = connectOrigins * connectOrigins - 1.0
            let discriminant = Math.discriminant a b c

            if discriminant < 0.0 then [||]
            else
                let sqrtDisc = sqrt discriminant
                let t1 = (-b - sqrtDisc) / (2.0 * a)
                let t2 = (-b + sqrtDisc) / (2.0 * a)
                if t1 > t2 then
                    [| t2; t1 |]
                else
                    [| t1; t2 |]
        | Null -> [| 0.0 |]

    let intersect surface (ray: Ray) =
        let localRay = inverseTransform surface * ray
        intersectLocal surface localRay
        |> Array.map (fun distance -> {
            Distance = distance
            Surface = surface
            Ray = ray
        })

    let colorAt (pattern: Pattern) (point: Point3D) surface =
        let surfacePoint = inverseTransform surface * point
        let patternPoint = Pattern.inverseTransform pattern * surfacePoint
        Pattern.colorAt pattern patternPoint

    let illuminate material point surface eye normal light inShadow =
        let patternColor =
            match material with
            | Phong (_, _, _, _, pattern) -> colorAt pattern point surface
        Material.illuminate material point patternColor eye normal light inShadow

module Intersection =
    let position (intersection: Intersection) =
        Ray.position intersection.Ray intersection.Distance

    let eyeVector intersection =
        -intersection.Ray.Direction

    let normalVector intersection =
        let candidate = Surface.normal intersection.Surface (position intersection)
        let eye = eyeVector intersection
        let dot = eye * candidate
        if dot < 0.0 then -candidate else candidate

    let overPosition intersection =
        position intersection + normalVector intersection * Constants.EPSILON

    let illuminate material (intersection: Intersection) light inShadow =
        let point = Ray.position intersection.Ray intersection.Distance
        let surface = intersection.Surface
        let eye = eyeVector intersection
        let normal = normalVector intersection
        Surface.illuminate material point surface eye normal light inShadow