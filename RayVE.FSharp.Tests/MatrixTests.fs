namespace RayVE.FSharp.Tests

open Xunit
open RayVE

module MatrixTests =

    [<Fact>]
    let RowCount_0 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        Assert.Equal(2, m.RowCount)

    [<Fact>]
    let ColumnCount_0 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        Assert.Equal(2, m.ColumnCount)

    [<Fact>]
    let RowVectors_0 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let v = m.RowVectors
        Assert.Equal(Vector(1., 2.), v.[0])
        Assert.Equal(Vector(3., 4.), v.[1])

    [<Fact>]
    let ColumnVectors_0 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let v = m.ColumnVectors
        Assert.Equal(Vector(1., 3.), v.[0])
        Assert.Equal(Vector(2., 4.), v.[1])

    [<Fact>]
    let op_multiply_0 () =
        let m1 = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let m2 = Matrix([| [| 3.; 7. |]; [| 1.; 5. |] |])
        let result = m1 * m2
        Assert.Equal(Matrix([| [| 5.; 17. |]; [| 13.; 41. |] |]), result)

    [<Fact>]
    let op_multiply_exception_0 () =
        let m1 = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let m2 = Matrix([| [| 3.; 7. |]; [| 1.; 5. |]; [| 2.; 4. |] |])
        Assert.Throws<System.ArgumentException>(fun () -> ignore (m1 * m2 : Matrix))

    [<Fact>]
    let op_multiply_exception_1 () =
        let m1 = Matrix([| [| 1.; 2. |]; [| 3.; 4. |]; [| 5.; 6. |] |])
        let m2 = Matrix([| [| 3.; 7. |]; [| 1.; 5. |]; [| 2.; 4. |] |])
        Assert.Throws<System.ArgumentException>(fun () -> ignore (m2 * m1 : Matrix))

    [<Fact>]
    let op_multiply_1 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let v = Vector(2., 3.)
        let result = m * v
        Assert.Equal(Vector(8., 18.), result)

    [<Fact>]
    let op_multiply_2 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let v = Vector(2., 3.)
        let result = v * m
        Assert.Equal(Vector(11., 16.), result)

    [<Fact>]
    let op_multiply_3 () =
        let s = 2.
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let result = s * m
        Assert.Equal(Matrix([| [| 2.; 4. |]; [| 6.; 8. |] |]), result)

    [<Fact>]
    let op_multiply_4 () =
        let s = 2.
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let result = m * s
        Assert.Equal(Matrix([| [| 2.; 4. |]; [| 6.; 8. |] |]), result)

    [<Fact>]
    let op_divide_0 () =
        let s = 2.
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let result = m / s
        Assert.Equal(Matrix([| [| 0.5; 1. |]; [| 1.5; 2. |] |]), result)

    [<Fact>]
    let op_subtract_0 () =
        let m1 = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let m2 = Matrix([| [| 3.; 7. |]; [| 1.; 5. |] |])
        let result = m1 - m2
        Assert.Equal(Matrix([| [| -2.; -5. |]; [| 2.; -1. |] |]), result)
    
    [<Fact>]
    let op_add_0 () =
        let m1 = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let m2 = Matrix([| [| 3.; 7. |]; [| 1.; 5. |] |])
        let result = m1 + m2
        Assert.Equal(Matrix([| [| 4.; 9. |]; [| 4.; 9. |] |]), result)
    
    [<Fact>]
    let op_Inequality_0 () =
        let m1 = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let m2 = Matrix([| [| 3.; 7. |]; [| 1.; 5. |] |])
        let result = m1 <> m2
        Assert.True(result)
    
    [<Fact>]
    let op_Equality_0 () =
        let m1 = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let m2 = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let result = m1 = m2
        Assert.True(result)
    
    [<Fact>]
    let subMatrix_0 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.subMatrix m 1 1
        Assert.Equal(Matrix([| [| 1.; 3. |]; [| 7.; 9. |] |]), result)
    
    [<Fact>]
    let subMatrix_1 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.subMatrix m 0 0
        Assert.Equal(Matrix([| [| 5.; 6. |]; [| 8.; 9. |] |]), result)

    [<Fact>]
    let transpose_0 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.transpose m
        Assert.Equal(Matrix([| [| 1.; 4.; 7. |]; [| 2.; 5.; 8. |]; [| 3.; 6.; 9. |] |]), result)
    
    [<Fact>]
    let transpose_1 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |] |])
        let result = Matrix.transpose m
        Assert.Equal(Matrix([| [| 1.; 4.; |]; [| 2.; 5.;|]; [| 3.; 6.; |] |]), result)
    
    [<Fact>]
    let determinant_0 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let result = Matrix.determinant m
        Assert.Equal(-2., result)
    
    [<Fact>]
    let determinant_1 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.determinant m
        Assert.Equal(0., result) // Singular matrix, determinant should be zero

    [<Fact>]
    let determinant_2 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 0.; 1.; 4. |]; [| 5.; 6.; 0. |] |])
        let result = Matrix.determinant m
        Assert.Equal(1., result) // Non-singular matrix, determinant should be non-zero

    [<Fact>]
    let isInvertible_0 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 0.; 1.; 4. |]; [| 5.; 6.; 0. |] |])
        let result = Matrix.isInvertible m
        Assert.True(result) // Non-singular matrix, determinant should be non-zero

    [<Fact>]
    let isInvertible_1 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.isInvertible m
        Assert.False(result) // Singular matrix, determinant should be zero

    [<Fact>]
    let cofactorMatrix_0 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let result = Matrix.cofactorMatrix m
        Assert.Equal(Matrix([| [| 4.; -3. |]; [| -2.; 1. |] |]), result)

    [<Fact>]
    let cofactorMatrix_1 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.cofactorMatrix m
        Assert.Equal(Matrix([| [| -3.; 6.; -3. |]; [| 6.; -12.; 6. |]; [| -3.; 6.; -3. |] |]), result)
    
    [<Fact>]
    let invert_0 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 0.; 1.; 4. |]; [| 5.; 6.; 0. |] |])
        let result = Matrix.invert m
        Assert.Equal(Matrix([| [| -24.; 18.; 5. |]; [| 20.; -15.; -4. |]; [| -5.; 4.; 1. |] |]), result)
    
    [<Fact>]
    let invert_1 () =
        let m = Matrix([| [| 1.; 2. |]; [| 3.; 4. |] |])
        let result = Matrix.invert m
        Assert.Equal(Matrix([| [| -2.; 1. |]; [| 1.5; -0.5 |] |]), result)
    
    [<Fact>]
    let swapRows_0 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.swapRows m 0 2
        Assert.Equal(Matrix([| [| 7.; 8.; 9. |]; [| 4.; 5.; 6. |]; [| 1.; 2.; 3. |] |]), result)
    
    [<Fact>]
    let scaleColumn_0 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.scaleColumn m 1 2.
        Assert.Equal(Matrix([| [| 1.; 4.; 3. |]; [| 4.; 10.; 6. |]; [| 7.; 16.; 9. |] |]), result)
    
    [<Fact>]
    let transform_0 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.transform m (fun r c -> float (r + c))
        Assert.Equal(Matrix([| [| 0.; 1.; 2. |]; [| 1.; 2.; 3. |]; [| 2.; 3.; 4. |] |]), result)
    
    let transformWhere_0 () =
        let m = Matrix([| [| 1.; 2.; 3. |]; [| 4.; 5.; 6. |]; [| 7.; 8.; 9. |] |])
        let result = Matrix.transformWhere m (fun r c -> float (r * c)) (fun r c -> r = c)
        Assert.Equal(Matrix([| [| 0.; 2.; 3. |]; [| 4.; 1.; 6. |]; [| 7.; 8.; 2. |] |]), result)

    module Create =

        [<Fact>]
        let identity_0 () =
            let m = Matrix.Create.identity 3
            Assert.Equal(Matrix([| [| 1.; 0.; 0. |]; [| 0.; 1.; 0. |]; [| 0.; 0.; 1. |] |]), m)

        [<Fact>]
        let zero_0 () =
            let m = Matrix.Create.zero 2 3
            Assert.Equal(Matrix([| [| 0.; 0.; 0. |]; [| 0.; 0.; 0. |] |]), m)

        [<Fact>]
        let translation_0 () =
            let v = Vector(1., 2.)
            let m = Matrix.Create.translation v
            Assert.Equal(Matrix([| [| 1.; 0.; 1. |]; [| 0.; 1.; 2. |]; [| 0.; 0.; 1. |] |]), m)
        
        [<Fact>]
        let scaling_0 () =
            let v = Vector(2., 3.)
            let m = Matrix.Create.scaling v
            Assert.Equal(Matrix([| [| 2.; 0.; 0. |]; [| 0.; 3.; 0. |]; [| 0.; 0.; 1. |] |]), m)
        
        [<Fact>]
        let rotation_0 () =
            let angle = System.Math.PI / 4.
            let m = Matrix.Create.rotation Dimension.X angle
            let expected = Matrix([| [| 1.; 0.;          0.;         0. |];
                                     [| 0.; cos(angle); -sin(angle); 0. |];
                                     [| 0.; sin(angle);  cos(angle); 0. |];
                                     [| 0.; 0.;          0.;         1. |] |])
            Assert.Equal(expected, m)
                                   
        [<Fact>]
        let rotation_1 () =
            let angle = System.Math.PI / 4.
            let m = Matrix.Create.rotation Dimension.Y angle
            let expected = Matrix([| [| cos(angle); 0.; sin(angle); 0. |];
                                     [| 0.;         1.; 0.;         0. |];
                                     [| -sin(angle); 0.; cos(angle); 0. |];
                                     [| 0.;         0.; 0.;         1. |] |])
            Assert.Equal(expected, m)
        
        [<Fact>]
        let rotation_2 () =
            let angle = System.Math.PI / 4.
            let m = Matrix.Create.rotation Dimension.Z angle
            let expected = Matrix([| [| cos(angle); -sin(angle); 0.; 0. |];
                                     [| sin(angle);  cos(angle); 0.; 0. |];
                                     [| 0.;         0.;         1.; 0. |];
                                     [| 0.;         0.;         0.; 1. |] |])
            Assert.Equal(expected, m)

        [<Fact>]
        let shear_0 () =
            let m = Matrix.Create.shear Dimension.X Dimension.Y 2.0
            let expected = Matrix([| [| 1.; 2.; 0.; 0. |];
                                     [| 0.; 1.; 0.; 0. |];
                                     [| 0.; 0.; 1.; 0. |];
                                     [| 0.; 0.; 0.; 1. |] |])
            Assert.Equal(expected, m)