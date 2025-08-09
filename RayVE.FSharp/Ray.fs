namespace RayVE

type Ray (origin: Point3D, direction: Vector3D) =
    member __.Origin = origin
    member __.Direction = Vector3D.normalize direction

    static member (*) (ray: Ray, matrix: Matrix) =
        if matrix.ColumnCount <> 4 then
            invalidArg (nameof matrix) "Matrix must have 4 columns."
        (ray.Origin * matrix, ray.Direction * matrix) |> Ray

    static member (*) (matrix: Matrix, ray: Ray) =
        if matrix.ColumnCount <> 4 then
            invalidArg (nameof matrix) "Matrix must have 4 columns."
        (matrix * ray.Origin, matrix * ray.Direction) |> Ray
    
    override __.Equals (other: obj): bool = 
        match other with
        | :? Ray as r -> 
            Vector3D.magnitude(__.Origin - r.Origin) < Constants.EPSILON
            && Vector3D.magnitude(__.Direction - r.Direction) < Constants.EPSILON
        | _ -> false

    static member op_Equality (ray1: Ray, ray2: Ray) =
        Vector3D.magnitude(ray1.Origin - ray2.Origin) < Constants.EPSILON
        && Vector3D.magnitude(ray1.Direction - ray2.Direction) < Constants.EPSILON

module Ray =
    let position (ray: Ray) (distance: double) =
        ray.Origin + distance * ray.Direction