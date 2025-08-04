open System
open System.IO
open RayVE

let origin = Point3D.Create.zero
let canvasSize = 50
let canvasDepth = 5
let scale = 100. / float canvasSize
let ambientColor = Color(0.1, 0.1, 0.1)

let pattern = Solid(Color.Predefined.Blue, Matrix.Create.identity 4)
let material = Phong(0.1, 0.9, 0.9, 200., pattern)
let sphere = Sphere(material, Matrix.Create.identity 4)

let light = PointLightSource(
    Point3D(-10., 10., -10.),
    Color.Predefined.White)

let rays =
    Array.allPairs
        [| -canvasSize/2 + 1 .. -canvasSize/2 + canvasSize - 1 |]
        [| -canvasSize/2 + 1 .. -canvasSize/2 + canvasSize - 1 |]
    |> Array.map (fun (x, y) ->
    {|
        Index = (x, y)
        Ray = Ray(
            origin,
            Vector3D(float x * scale, float y * scale, canvasDepth))
    |})

let hits =
    rays
    |> Array.map (fun ray ->
        {|
            Index = ray.Index
            Ray = ray
            Hit = Surface.intersect sphere ray.Ray
        |})

let canvas = Canvas(canvasSize, canvasSize, 255)

hits |> Array.iter (fun hit ->
    let xPixel = fst hit.Index + (canvasSize / 2)
    let yPixel = -snd hit.Index + (canvasSize / 2)

    if Array.isEmpty hit.Hit then
        canvas[xPixel, yPixel] <- ambientColor
    else
        let closest = Array.minBy (_.Distance) hit.Hit
        let material =
            match closest.Surface with
            | Sphere (m, _) -> m
            | _ -> failwith "Unexpected surface type"
        let point = Ray.position closest.Ray closest.Distance
        let surface = closest.Surface
        let eye = -closest.Ray.Direction
        let normal = Surface.normal surface point
        canvas[xPixel, yPixel] <-
            Surface.illuminate
                material
                point
                surface
                eye
                normal
                light
                false
    )

let document = { Canvas = canvas; MaxValue = 255 }
let filePath = $"C:\\temp\\RayVE\\SphereDrawing\\{DateTime.Now:yyyyMMdd_HHmmss}.ppm";
File.WriteAllText(filePath, PPMDocument.toPPM document)