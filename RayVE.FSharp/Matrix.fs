namespace RayVE


type Matrix(values: double[][]) =
    new(rows, columns, valueSource: int -> int -> float) =
        let values = Array2D.init rows columns valueSource
        Matrix(values)

    new(values: float[,]) =
        let jaggedValues = values
                           |> Array2D.toJagged
        Matrix(jaggedValues)

    member __.Values
        with get() = values

    member __.Item
        with get(row, column) = values.[row].[column]

    member __.Rows
        with get() = values.Length

    member __.Columns
        with get() = values.[0].Length

    //TODO: implement
    member __.Determinant
        with get() = 0.0

    //TODO: implement
    member __.IsInvertible
        with get() = true

    //TODO: implement
    member __.Inverse
        with get() = Matrix.Zero __.Rows __.Columns

    member __.SwapRows row1 row2 =
        let valueSource r c =
            if r = row1 then values.[row2].[c]
            elif r = row2 then values.[row1].[c]
            else values.[r].[c]
        Matrix(__.Rows, __.Columns, valueSource)

    member __.ScaleColumn column factor =
        let valueSource r c =
            if c = column then values.[r].[c] * factor
            else values.[r].[c]
        Matrix(__.Rows, __.Columns, valueSource)

    member __.Transform (transform: int -> int -> float) = Matrix(__.Rows, __.Columns, transform)

    member __.Transform (transform: int -> int -> float, predicate: int -> int -> bool) = 
        let valueSource r c =
            if predicate r c then transform r c
            else values.[r].[c]
        Matrix(__.Rows, __.Columns, valueSource)

    member __.Transpose() = Matrix(__.Rows, __.Columns, (fun r c -> values.[c].[r]))

    //TODO: implement
    member __.GetSubMatrix r c = Matrix.Zero r c

    //TODO: implement
    member __.GetMinor r c = Matrix.Zero r c

    //TODO: implement
    member __.GetCofactor r c = Matrix.Zero r c

    //TODO: implement
    static member (*) (left: Matrix, right: Matrix) = Matrix.Zero left.Rows left.Columns

    //TODO: implement
    static member (*) (left: Matrix, right: Vector) = Matrix.Zero left.Rows left.Columns

    //TODO: implement
    static member (*) (left: Vector, right: Matrix) = Matrix.Zero right.Rows right.Columns

    static member Identity(size: int) =
        fun r c -> if r = c then 1.0
                   else 0.0
        |> Array2D.init size size
        |> Matrix

    static member Zero rows columns =
        Array2D.zeroCreate rows columns
        |> Matrix

    //TODO: implement
    static member Translation (vector: Vector) =
        Matrix.Zero vector.Values.Length vector.Values.Length

    //TODO: implement
    static member Scale (vector: Vector) =
        Matrix.Zero vector.Values.Length vector.Values.Length

    //TODO: implement
    static member Rotation (dimension: Dimension) (radians: float) =
        Matrix.Zero 4 4

    //TODO: implement
    static member Shear (shearDimension: Dimension) (inProportionTo: Dimension) (amount: float) =
        Matrix.Zero 4 4