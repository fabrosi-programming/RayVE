namespace RayVE

type Vector3D(x: float, y: float, z: float) =
    inherit Vector([| x; y; z; 0.0 |])

    member __.X = x
    member __.Y = y
    member __.Z = z
    member __.W = 0.0

    static member fromVector (vector: Vector) =
        match vector.Values.Length with
        | 3 -> Vector3D(vector.Values.[0], vector.Values.[1], vector.Values.[2])
        | 4 when vector.Values.[3] = 0.0 -> Vector3D(vector.Values.[0], vector.Values.[1], vector.Values.[2])
        | _ -> invalidArg (nameof vector) "Must be a 3D vector or 4D vector with w=0"

    static member (+) (left: Vector3D, right: Vector3D) =
        (left :> Vector) + (right :> Vector) |> Vector3D.fromVector

    static member (-) (left: Vector3D, right: Vector3D) =
        (left :> Vector) - (right :> Vector) |> Vector3D.fromVector
 
    static member (*) (left: Vector3D, right: Vector3D) =
        (left :> Vector) * (right :> Vector)

    static member (*) (scalar: float, vector: Vector3D) =
        scalar * (vector :> Vector) |> Vector3D.fromVector

    static member (*) (vector: Vector3D, scalar: float) =
        scalar * vector

    static member (*) (matrix: Matrix, vector: Vector3D) =
        matrix * (vector :> Vector) |> Vector3D.fromVector

    static member (*) (vector: Vector3D, matrix: Matrix) =
        (vector :> Vector) * matrix |> Vector3D.fromVector

    static member (/) (vector: Vector3D, scalar) =
        (1.0 / scalar) * vector

    static member (~-) (vector: Vector3D) =
        -1.0 * vector

module Vector3D =
    let normalize (vector: Vector3D) =
        let v3d = Vector(vector.Values[..2])
        Vector.normalize v3d
        |> Vector3D.fromVector

    let cross (v1: Vector3D) (v2: Vector3D) =
        let v1_3d = Vector(v1.Values[..2])
        let v2_3d = Vector(v2.Values[..2])
        let c = Vector.cross v1_3d v2_3d
        Vector3D(c.Values[0], c.Values[1], c.Values[2])

    let reflect (vector: Vector3D) (normal: Vector3D) =
        let v3d = Vector(vector.Values[..2])
        let n3d = Vector(normal.Values[..2])
        Vector.reflect v3d n3d
        |> Vector3D.fromVector

    module Create =
        let zero = Vector3D(0.0, 0.0, 0.0)