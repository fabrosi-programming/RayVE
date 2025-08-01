namespace RayVE

type Ray (origin: Point3D, direction: Vector3D) =
    member __.Origin = origin
    member __.Direction = direction

    static member (*) (ray: Ray, matrix: Matrix) =
        if matrix.ColumnCount <> 4 then
            invalidArg (nameof matrix) "Matrix must have 4 columns."
        (ray.Origin * matrix, ray.Direction * matrix) |> Ray

    static member (*) (matrix: Matrix, ray: Ray) =
        if matrix.ColumnCount <> 4 then
            invalidArg (nameof matrix) "Matrix must have 4 columns."
        (matrix * ray.Origin, matrix * ray.Direction) |> Ray

module Ray =
    let position (ray: Ray) (distance: double) =
        ray.Origin + distance * ray.Direction