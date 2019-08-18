namespace RayVE

open System


type Matrix(values: double[][]) =
    do if values = null then raise (ArgumentNullException "values")

    new(rows, columns, valueSource: int -> int -> float) =
        let values = Array2D.init rows columns valueSource
        Matrix(values)

    new(values: float[,]) =
        let jaggedValues = values
                           |> Array2D.toJagged
        Matrix(jaggedValues)

    new(vectors: Vector[]) =
        let array = Array.map (fun (v: Vector) -> v.Values) vectors
        Matrix(array)

    member __.Values
        with get() = values

    member __.Item
        with get(row, column) = values.[row].[column]

    member __.Rows
        with get() = values.Length

    member __.Columns
        with get() = values.[0].Length

    member this.Determinant
        with get() =
            match (this.Rows, this.Columns) with
            | (2, 2) -> this.[0, 0] * this.[1, 1] - this.[0, 1] * this.[1, 0]
            | _ -> Vector(values.[0]) * Vector(this.GetCofactors 0)

    member this.IsInvertible
        with get() =
            this.Determinant <> 0.0

    member this.Cofactors = Array2D.init this.Rows this.Columns this.GetCofactor
                            |> Matrix

    //TODO: implement
    member this.Inverse
        with get() = this.Cofactors / this.Determinant

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

    member this.GetSubMatrix r c : Matrix =
        fun i j ->
            let newRow = if i < r then i else i + 1
            let newColumn = if j < c then j else j + 1
            this.[newRow, newColumn]
        |> Array2D.init (this.Rows - 1) (this.Columns - 1)
        |> Matrix

    member this.GetMinor r c = (this.GetSubMatrix r c).Determinant

    member this.GetCofactor r c : float =
        let sign = match (r + c) % 2 with
                   | 0 -> 1
                   | _ -> -1
        (float sign) * this.GetMinor r c

    member this.GetCofactors r : float[] = [| 0 .. (this.Columns - 1) |]
                                           |> Array.map (fun c -> this.GetCofactor r c)

    override this.Equals other =
        match other with
        | :? Matrix as m ->
            if this.Rows <> m.Rows || this.Columns <> m.Columns then
                false
            else
                seq { for r in 0 .. this.Rows - 1 do
                          for c in 0 .. this.Columns - 1 do
                              if Math.Abs (this.[r, c] - m.[r, c]) > Constants.EPSILON then
                                  yield false }
                |> Seq.forall id
        | _ -> false

    //TODO: implement
    static member (*) (left: Matrix, right: Matrix) = Matrix.Zero left.Rows left.Columns

    //TODO: implement
    static member (*) (left: Matrix, right: Vector) = Matrix.Zero left.Rows left.Columns

    //TODO: implement
    static member (*) (left: Vector, right: Matrix) = Matrix.Zero right.Rows right.Columns

    //TODO: implement
    static member (*) (scalar: float, matrix: Matrix) = Array2D.init matrix.Rows matrix.Columns (fun r c -> matrix.[r, c] * scalar)
                                                        |> Matrix

    static member (/) (matrix: Matrix, scalar: float) = (1.0 / scalar) * matrix

    static member (-) (left: Matrix, right: Matrix) : Matrix =
        Array.map2 (fun (v1: float[]) (v2: float[]) -> Vector(v1) - Vector(v2)) left.Values right.Values
        |> Matrix

    static member op_Inequality (left: Matrix, right: Matrix) = not(left.Equals right)

    static member op_Equality (left: Matrix, right: Matrix) = left.Equals right

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