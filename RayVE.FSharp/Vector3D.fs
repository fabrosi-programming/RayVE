namespace RayVE

type Vector3D(x: float, y: float, z: float) =
    inherit Vector([| x; y; z; 0.0 |])