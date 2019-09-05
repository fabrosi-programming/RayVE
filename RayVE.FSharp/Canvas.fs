namespace RayVE

open System.Collections.Generic

type Canvas(width, height) =
    let mutable pixels = Array2D.init width height (fun i j -> Color.Black)
    
    member __.Width
        with get() = width

    member __.Height
        with get() = height

    member __.Item
        with get(x, y) = pixels.[x, y]
        and set (x, y) value = pixels.[x, y] <- value

    member __.Fill color =
        do pixels <- Array2D.init width height (fun i j -> color)

    member __.PPMHeader (maxValue: int) =
        [| "P3";
           __.Width.ToString() + " " + __.Height.ToString()
           maxValue.ToString() |]
        |> String.concat "\r\n"

    member __.ToPPM maxValue = ""