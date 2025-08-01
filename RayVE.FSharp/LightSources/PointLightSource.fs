namespace RayVE

type PointLightSource(position: Point3D, color: Color) =
    member __.Position
        with get() = position
    member __.Color
        with get() = color