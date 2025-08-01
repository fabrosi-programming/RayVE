namespace RayVE

type Point3D(x: float, y: float, z: float) =
    inherit Vector([| x; y; z; 1.0 |])