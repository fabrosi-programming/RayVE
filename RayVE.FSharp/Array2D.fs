namespace RayVE

module Array2D =
    let toJagged<'a> (arr: 'a[,]) : 'a [][] = 
        [| for x in 0 .. Array2D.length1 arr - 1 do
               yield [| for y in 0 .. Array2D.length2 arr - 1 -> arr.[x, y] |]
            |]