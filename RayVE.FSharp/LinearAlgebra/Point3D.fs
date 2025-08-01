namespace RayVE

type Point3D(x: float, y: float, z: float) =
    inherit Vector([| x; y; z; 1.0 |])

    member __.X = x
    member __.Y = y
    member __.Z = z
    member __.W = 1.0

    static member fromVector (vector: Vector) =
        match vector.Values.Length with
        | 3 -> Point3D(vector.Values.[0], vector.Values.[1], vector.Values.[2])
        | 4 when vector.Values.[3] = 1.0 -> Point3D(vector.Values.[0], vector.Values.[1], vector.Values.[2])
        | _ -> invalidArg (nameof vector) "Must be a 3D vector or 4D vector with w=1"

    static member (+) (point: Point3D, vector: Vector3D) =
        (point :> Vector) + (vector :> Vector) |> Point3D.fromVector

    static member (-) (point: Point3D, vector: Vector3D) =
        (point :> Vector) - (vector :> Vector) |> Point3D.fromVector

    static member (-) (left: Point3D, right: Point3D) =
        (left :> Vector) - (right :> Vector) |> Vector3D.fromVector

    static member (*) (matrix: Matrix, point: Point3D) =
        matrix * (point :> Vector) |> Point3D.fromVector

    static member (*) (point: Point3D, matrix: Matrix) =
        (point :> Vector) * matrix |> Point3D.fromVector

module Point3D =
    module Create =
        let zero = Point3D(0.0, 0.0, 0.0)