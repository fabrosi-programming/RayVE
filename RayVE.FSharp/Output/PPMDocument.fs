namespace RayVE

open System

type PPMDocument = {
    Canvas: Canvas
    MaxValue: int
}

module PPMDocument =
    let header doc =
        [|
            "P3"
            $"{doc.Canvas.Width} {doc.Canvas.Height}"
            $"{doc.MaxValue}"
        |]

    let toPPM doc =
        let maxChunkSize = 70
        let width = doc.Canvas.Width
        let height = doc.Canvas.Height
        let pixels = doc.Canvas.Pixels
        let data = Array2D.init width height (fun i j -> Color.toPPM pixels.[i, j] doc.MaxValue)
                   |> Array2D.toJagged
                   |> Array.transpose
                   |> Array.map Array.concat
                   |> Array.map (String.chunkSplit maxChunkSize " ")
                   |> Seq.collect id
                   |> String.concat Environment.NewLine

        [| yield! header doc; yield data |]
        |> String.concat Environment.NewLine