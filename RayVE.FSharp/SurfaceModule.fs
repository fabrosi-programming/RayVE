namespace RayVE

module Surface =
    let transform = function
        | Plane transform -> transform
        | Sphere transform -> transform
        | Null -> Matrix.Create.identity 4

    let inverseTransform surface =
        surface
        |> transform
        |> Matrix.invert

    let transposeInverseTransform surface =
        surface
        |> inverseTransform
        |> Matrix.transpose

    let center = function
    | Sphere transform -> transform * Point3D.Create.zero
    | _ -> failwith "Only spheres have a center."

    let normalLocal surface (point: Point3D) =
        match surface with
        | Plane _ -> Vector3D(0.0, 1.0, 0.0)
        | Sphere _ -> point - Point3D.Create.zero
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
            let discriminant = Algebra.discriminant a b c

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