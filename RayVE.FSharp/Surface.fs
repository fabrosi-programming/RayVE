namespace RayVE

type Surface =
    | Plane of transform: Matrix
    | Sphere of transform: Matrix
    | Null