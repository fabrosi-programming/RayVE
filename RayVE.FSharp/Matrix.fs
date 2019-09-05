namespace RayVE

open System
open System.Diagnostics

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

    member __.RowCount
        with get() = values.Length

    member __.ColumnCount
        with get() = values.[0].Length

    member __.RowVectors =
        values
        |> Array.map (fun v -> Vector v)

    member this.ColumnVectors =
        let transpose = this.Transpose()
        transpose.RowVectors
        

    member this.Determinant
        with get() =
            match (this.RowCount, this.ColumnCount) with
            | (2, 2) -> this.[0, 0] * this.[1, 1] - this.[0, 1] * this.[1, 0]
            | _ -> Vector(values.[0]) * Vector(this.GetCofactors 0)

    member this.IsInvertible
        with get() = this.Determinant <> 0.0

    member this.Cofactors =
        let cofactorMatrix = Array2D.init this.RowCount this.ColumnCount this.GetCofactor
                             |> Matrix
        cofactorMatrix.Transpose()

    //TODO: implement
    member this.Inverse
        with get() = this.Cofactors / this.Determinant

    member __.SwapRows row1 row2 =
        let valueSource r c =
            if r = row1 then values.[row2].[c]
            elif r = row2 then values.[row1].[c]
            else values.[r].[c]
        Matrix(__.RowCount, __.ColumnCount, valueSource)

    member __.ScaleColumn column factor =
        let valueSource r c =
            if c = column then values.[r].[c] * factor
            else values.[r].[c]
        Matrix(__.RowCount, __.ColumnCount, valueSource)

    member __.Transform (transform: int -> int -> float) =
        Matrix(__.RowCount, __.ColumnCount, transform)

    member __.Transform (transform: int -> int -> float, predicate: int -> int -> bool) = 
        let valueSource r c =
            if predicate r c then transform r c
            else values.[r].[c]
        Matrix(__.RowCount, __.ColumnCount, valueSource)

    member __.Transpose() : Matrix =
        Matrix(__.ColumnCount, __.RowCount, (fun r c -> values.[c].[r]))

    member this.GetSubMatrix r c : Matrix =
        fun i j ->
            let newRow = if i < r then i else i + 1
            let newColumn = if j < c then j else j + 1
            this.[newRow, newColumn]
        |> Array2D.init (this.RowCount - 1) (this.ColumnCount - 1)
        |> Matrix

    member this.GetMinor row column =
        (this.GetSubMatrix row column).Determinant

    member this.GetCofactor r c : float =
        let sign = match (r + c) % 2 with
                   | 0 -> 1
                   | _ -> -1
        (float sign) * this.GetMinor r c

    member this.GetCofactors r : float[] =
        [| 0 .. (this.ColumnCount - 1) |]
        |> Array.map (fun c -> this.GetCofactor r c)

    override this.Equals other =
        match other with
        | :? Matrix as m ->
            if this.RowCount <> m.RowCount || this.ColumnCount <> m.ColumnCount then
                false
            else
                seq { for r in 0 .. this.RowCount - 1 do
                          for c in 0 .. this.ColumnCount - 1 do
                              if Math.Abs (this.[r, c] - m.[r, c]) > Constants.EPSILON then
                                  yield false }
                |> Seq.forall id
        | _ -> false

    override __.ToString() =
        String.Format("Rows={0}, Columns={1}", values.Length, values.[0].Length)

    static member (*) (left: Matrix, right: Matrix) =
        let leftRows = left.RowVectors
        let rightColumns = right.ColumnVectors
        Array2D.init left.RowCount right.ColumnCount (fun r c -> leftRows.[r] * rightColumns.[c])
        |> Matrix

    static member (*) (matrix: Matrix, vector: Vector) =
        matrix.RowVectors
        |> Array.map (fun v -> v * vector)
        |> Vector

    static member (*) (vector: Vector, matrix: Matrix) =
        matrix.ColumnVectors
        |> Array.map (fun v -> vector * v)
        |> Vector

    static member (*) (scalar: float, matrix: Matrix) =
        Array2D.init matrix.RowCount matrix.ColumnCount (fun r c -> matrix.[r, c] * scalar)
        |> Matrix

    static member (/) (matrix: Matrix, scalar: float) =
        (1.0 / scalar) * matrix

    static member (-) (left: Matrix, right: Matrix) : Matrix =
        Array.map2 (-) left.RowVectors right.RowVectors
        |> Matrix

    static member (+) (left: Matrix, right: Matrix) : Matrix =
        Array.map2 (+) left.RowVectors right.RowVectors
        |> Matrix

    static member op_Inequality (left: Matrix, right: Matrix) =
        not(left.Equals right)

    static member op_Equality (left: Matrix, right: Matrix) =
        left.Equals right

    static member Identity(size: int) =
        fun r c -> if r = c then 1.0
                   else 0.0
        |> Array2D.init size size
        |> Matrix

    static member Zero rows columns =
        Array2D.zeroCreate rows columns
        |> Matrix

    static member Translation (vector: Vector) =
        Matrix.Identity (vector.Length + 1)
        + Matrix(vector.Length + 1, vector.Length + 1, fun r c -> if r <> c && c = vector.Length
                                                                  then vector.[r]
                                                                  else 0.0)

    static member Scale (vector: Vector) =
        Matrix(vector.Length + 1, vector.Length + 1, fun r c -> if r <> c then 0.0
                                                                elif r = vector.Length then 1.0
                                                                else vector.[r])

    static member Rotation (dimension: Dimension) (radians: float) =
        match dimension with
        | Dimension.X -> [| [| 1.0; 0.0;          0.0;           0.0 |];
                            [| 0.0; cos(radians); -sin(radians); 0.0 |];
                            [| 0.0; sin(radians);  cos(radians); 0.0 |];
                            [| 0.0; 0.0;           0.0;          1.0 |] |]
                         |> Matrix

        | Dimension.Y -> [| [| cos(radians);  0.0; sin(radians); 0.0 |];
                            [| 0.0;           1.0; 0.0;          0.0 |];
                            [| -sin(radians); 0.0; cos(radians); 0.0 |];
                            [| 0.0;           0.0; 0.0;          1.0 |] |]
                         |> Matrix
        | Dimension.Z -> [| [| cos(radians); -sin(radians); 0.0; 0.0 |];
                            [| sin(radians); cos(radians);  0.0; 0.0 |];
                            [| 0.0;          0.0;           1.0; 0.0 |];
                            [| 0.0;          0.0;           0.0; 1.0 |] |]
                         |> Matrix

    static member Shear (shearDimension: Dimension) (inProportionTo: Dimension) (amount: float) =
        Matrix(4, 4, fun r c -> if r = (LanguagePrimitives.EnumToValue shearDimension) && c = (LanguagePrimitives.EnumToValue inProportionTo)
                                then amount
                                else Matrix.Identity(4).[r, c])