namespace RayVE

type Color(red, green, blue) =
    member __.R with get() = red

    member __.G with get() = green

    member __.B with get() = blue
    
    static member (+) (left: Color, right: Color) = Color.Black

    static member (-) (left: Color, right: Color) = Color.Black

    static member (*) (color: Color, scalar: float) = Color.Black

    static member (*) (left: Color, right: Color) = Color.Black

    static member Black
        with get() = Color(0.0, 0.0, 0.0)

    static member White
        with get() = Color(1.0, 1.0, 1.0)

    static member Red
        with get() = Color(1.0, 0.0, 0.0)

    static member Green
        with get() = Color(0.0, 1.0, 0.0)

    static member Blue
        with get() = Color(0.0, 0.0, 1.0)

    static member Cyan
        with get() = Color(0.0, 1.0, 1.0)

    static member Magenta
        with get() = Color(1.0, 0.0, 1.0)

    static member Yellow
        with get() = Color(1.0, 1.0, 0.0)