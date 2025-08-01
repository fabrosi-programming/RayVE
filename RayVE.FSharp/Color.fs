namespace RayVE

open RayVE.Math
open System

type Color(red, green, blue) =
    let vector = Vector([| red; green; blue |])

    member __.R = red

    member __.G = green

    member __.B = blue

    member __.AsVector = vector

    static member (+) (c1: Color, c2: Color) = 
        Color(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B)

    static member (-) (c1: Color, c2: Color) = 
        Color(c1.R - c2.R, c1.G - c2.G, c1.B - c2.B)

    static member (*) (c1: Color, c2: Color) = 
        Color(c1.R * c2.R, c1.G * c2.G, c1.B * c2.B)

    static member (*) (scalar: float, c: Color) = 
        Color(scalar * c.R, scalar * c.G, scalar * c.B)

    static member (*) (c: Color, scalar: float) = 
        Color(scalar * c.R, scalar * c.G, scalar * c.B)

    static member (/) (c: Color, scalar: float) = 
        Color(c.R / scalar, c.G / scalar, c.B / scalar)

    override this.Equals other =
        match other with
        | :? Color as c -> 
            Math.Abs(this.R - c.R) <= Constants.EPSILON &&
            Math.Abs(this.G - c.G) <= Constants.EPSILON &&
            Math.Abs(this.B - c.B) <= Constants.EPSILON
        | _ -> false
        
    override __.GetHashCode() = 
        hash (red, green, blue)
        
    override __.ToString() = 
        sprintf "Color(%.2f, %.2f, %.2f)" red green blue
        
    static member op_Equality (c1: Color, c2: Color) = c1.Equals c2
    static member op_Inequality (c1: Color, c2: Color) = not (c1.Equals c2)

module Color =
    let PPMValue maxValue rawValue =
        rawValue * double(maxValue)
        |> round
        |> int
        |> clamp 0 maxValue

    let toPPM (color: Color) maxValue =
        [| color.R; color.G; color.B |]
        |> Array.map (PPMValue maxValue)
        |> Array.map (sprintf "%i")

    module Predefined =
        let Black = Color(0.0, 0.0, 0.0)
        let White = Color(1.0, 1.0, 1.0)
        let Red = Color(1.0, 0.0, 0.0)
        let Green = Color(0.0, 1.0, 0.0)
        let Blue = Color(0.0, 0.0, 1.0)
        let Cyan = Color(0.0, 1.0, 1.0)
        let Magenta = Color(1.0, 0.0, 1.0)
        let Yellow = Color(1.0, 1.0, 0.0)
        let Gray = Color(0.5, 0.5, 0.5)
        let DarkGray = Color(0.25, 0.25, 0.25)
        let LightGray = Color(0.75, 0.75, 0.75)
        let Orange = Color(1.0, 0.5, 0.0)
        let Purple = Color(0.5, 0.0, 0.5)
        let Brown = Color(0.6, 0.3, 0.1)
        let Pink = Color(1.0, 0.75, 0.8)
        let DarkRed = Color(0.5, 0.0, 0.0)
        let DarkGreen = Color(0.0, 0.5, 0.0)
        let DarkBlue = Color(0.0, 0.0, 0.5)
        let DarkCyan = Color(0.0, 0.5, 0.5)
        let DarkMagenta = Color(0.5, 0.0, 0.5)
        let DarkYellow = Color(0.5, 0.5, 0.0)
