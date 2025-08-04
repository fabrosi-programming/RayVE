namespace RayVE

type Canvas(width, height, ppmMaxValue) =
    let mutable pixels = Array2D.init width height (fun _ _ -> Color.Predefined.Black)
    
    member __.Width = width

    member __.Height = height

    member __.Item
        with get(x, y) = pixels.[x, y]
        and set(x, y) value = pixels.[x, y] <- value

    member __.Pixels = Array2D.copy pixels

    member __.Fill color =
        do pixels <- Array2D.init width height (fun i j -> color)