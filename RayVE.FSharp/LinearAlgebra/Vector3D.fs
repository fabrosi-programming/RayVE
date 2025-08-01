namespace RayVE

type Vector3D(x: float, y: float, z: float) =
    inherit Vector([| x; y; z; 0.0 |])

module Vector3D =
    let cross (v1: Vector3D) (v2: Vector3D) =
        let v1_3d = Vector(v1.Values[..2])
        let v2_3d = Vector(v2.Values[..2])
        let c = Vector.cross v1_3d v2_3d
        Vector3D(c.Values[0], c.Values[1], c.Values[2])