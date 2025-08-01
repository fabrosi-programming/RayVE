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

    member __.RowCount
        with get() = values.Length

    member __.ColumnCount
        with get() = values.[0].Length

    member __.RowVectors =
        values
        |> Array.map (fun v -> Vector v)

    member private __.Transpose() : Matrix =
        Matrix(__.ColumnCount, __.RowCount, (fun r c -> values.[c].[r]))

    member this.ColumnVectors =
        let transpose = this.Transpose()
        transpose.RowVectors
    
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

    override __.GetHashCode() =
        values.GetHashCode()

    override __.ToString() =
        String.Format("Rows={0}, Columns={1}", values.Length, values.[0].Length)

    override this.Equals other =
        match other with
        | :? Matrix as m ->
            if this.RowCount <> m.RowCount || this.ColumnCount <> m.ColumnCount then
                false
            else
                seq { for r in 0 .. this.RowCount - 1 do
                            for c in 0 .. this.ColumnCount - 1 do
                                Math.Abs (this.[r, c] - m.[r, c]) <= Constants.EPSILON }
                |> Seq.forall id
        | _ -> false

module Matrix =
    let subMatrix (matrix: Matrix) r c : Matrix =
        fun i j ->
            let newRow = if i < r then i else i + 1
            let newColumn = if j < c then j else j + 1
            matrix.[newRow, newColumn]
        |> Array2D.init (matrix.RowCount - 1) (matrix.ColumnCount - 1)
        |> Matrix

    let transpose (matrix: Matrix) =
        Matrix(matrix.ColumnCount, matrix.RowCount, (fun r c -> matrix.Values.[c].[r]))

    let rec determinant (matrix: Matrix) =
        match (matrix.RowCount, matrix.ColumnCount) with
        | (2, 2) -> matrix.[0, 0] * matrix.[1, 1] - matrix.[0, 1] * matrix.[1, 0]
        | _ -> Vector(matrix.Values.[0]) * Vector(cofactors matrix 0)
    and minor (matrix:Matrix) row column =
        determinant (subMatrix matrix row column)
    and cofactor (matrix: Matrix) r c =
        let sign = match (r + c) % 2 with
                   | 0 -> 1
                   | _ -> -1
        (float sign) * minor matrix r c
    and cofactors (matrix: Matrix) r : float[] =
        [| 0 .. (matrix.ColumnCount - 1) |]
        |> Array.map (fun c -> cofactor matrix r c)

    let isInvertible (matrix: Matrix) =
        determinant matrix <> 0.0

    let cofactorMatrix (matrix: Matrix) =
        let cofactorMatrix = Array2D.init matrix.RowCount matrix.ColumnCount (cofactor matrix)
                             |> Matrix
        cofactorMatrix
        |> transpose

    let invert (matrix: Matrix) =
        (cofactorMatrix matrix) / (determinant matrix)

    let swapRows (matrix: Matrix) row1 row2 =
        let valueSource r c =
            if r = row1 then matrix.Values.[row2].[c]
            elif r = row2 then matrix.Values.[row1].[c]
            else matrix.Values.[r].[c]
        Matrix(matrix.RowCount, matrix.ColumnCount, valueSource)

    let scaleColumn (matrix: Matrix) column factor =
        match column with
        | c when c > matrix.ColumnCount -> raise (ArgumentOutOfRangeException("column"))
        | _ -> let valueSource r c =
                   if c = column then matrix.Values.[r].[c] * factor
                   else matrix.Values.[r].[c]
               Matrix(matrix.RowCount, matrix.ColumnCount, valueSource)

    let transform (matrix: Matrix) (transform: int -> int -> float) =
        Matrix(matrix.RowCount, matrix.ColumnCount, transform)

    let transformWhere (matrix: Matrix) (transform: int -> int -> float) (predicate: int -> int -> bool) = 
        let valueSource r c =
            if predicate r c then transform r c
            else matrix.Values.[r].[c]
        Matrix(matrix.RowCount, matrix.ColumnCount, valueSource)

    module Create =
        let identity(size: int) =
            fun r c -> if r = c then 1.0
                        else 0.0
            |> Array2D.init size size
            |> Matrix

        let zero rows columns =
            Array2D.zeroCreate rows columns
            |> Matrix

        let translation (vector: Vector) =
            let l = Vector.length vector
            identity (l + 1)
            + Matrix(l + 1, l + 1, fun r c -> if r <> c && c = l
                                                        then vector.[r]
                                                        else 0.0)

        let scaling (vector: Vector) =
            let l = Vector.length vector
            Matrix(l + 1, l + 1, fun r c -> if r <> c then 0.0
                                                        elif r = l then 1.0
                                                        else vector.[r])

        let rotation (dimension: Dimension) (radians: float) =
            let cosR = cos(radians)
            let sinR = sin(radians)
            match dimension with
            | Dimension.X -> [| [| 1.0; 0.0;   0.0;  0.0 |];
                                [| 0.0; cosR; -sinR; 0.0 |];
                                [| 0.0; sinR;  cosR; 0.0 |];
                                [| 0.0; 0.0;   0.0;  1.0 |] |]
                                |> Matrix

            | Dimension.Y -> [| [|  cosR; 0.0; sinR; 0.0 |];
                                [|  0.0;  1.0; 0.0;  0.0 |];
                                [| -sinR; 0.0; cosR; 0.0 |];
                                [|  0.0;  0.0; 0.0;  1.0 |] |]
                                |> Matrix
            | Dimension.Z -> [| [| cosR; -sinR; 0.0; 0.0 |];
                                [| sinR;  cosR; 0.0; 0.0 |];
                                [| 0.0;   0.0;  1.0; 0.0 |];
                                [| 0.0;   0.0;  0.0; 1.0 |] |]
                                |> Matrix
            | _ -> zero 4 4

        let shear (shearDimension: Dimension) (inProportionTo: Dimension) (amount: float) =
            Matrix(4, 4, fun r c -> if r = (LanguagePrimitives.EnumToValue shearDimension) && c = (LanguagePrimitives.EnumToValue inProportionTo)
                                    then amount
                                    else identity(4).[r, c])