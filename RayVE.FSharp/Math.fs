namespace RayVE

open System

module Math =
    let roundUp (d: int) (x: float) =
        Math.Round(x + Math.Pow(10., -float(d))/2., d)

    let clamp minValue maxValue value =
        match value with
        | v when v < minValue -> minValue
        | v when v > maxValue -> maxValue
        | _ -> value
    
    let discriminant a b c =
        pown b 2 - 4.0 * a * c