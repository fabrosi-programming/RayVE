namespace RayVE

module PPMColor =
    let ppmValue raw max =
        Math.clamp(0, max, raw * max)

    let toPPM (color: Color) max =
        [|
            ppmValue color.R max |> string
            ppmValue color.G max |> string
            ppmValue color.B max |> string
        |]