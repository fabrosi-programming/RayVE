namespace RayVE.FSharp.Tests

open Xunit
open RayVE

module VectorTests =

    [<Fact>]
    let op_add () =
        let v1 = Vector([| 1.; 2.; 3. |])
        let v2 = Vector([| 4.; 5.; 6. |])
        let result = v1 + v2
        Assert.Equal(Vector([| 5.; 7.; 9. |]), result)

    [<Fact>]
    let op_subtract () =
        let v1 = Vector([| 1.; 2.; 3. |])
        let v2 = Vector([| 4.; 6.; 8. |])
        let result = v2 - v1
        Assert.Equal(Vector([| 3.; 4.; 5. |]), result)

    [<Fact>]
    let op_multiply_0 () =
        let v1 = Vector([| 1.; 2.; 3. |])
        let v2 = Vector([| 4.; 5.; 6. |])
        let result = v1 * v2
        Assert.Equal(32., result)

    [<Fact>]
    let op_multiply_1 () =
        let s = 2.
        let v = Vector([| 4.; 5.; 6. |])
        let result = s * v
        Assert.Equal(Vector([| 8.; 10.; 12. |]), result)

    [<Fact>]
    let op_multiply_2 () =
        let s = 2.
        let v = Vector([| 4.; 5.; 6. |])
        let result = v * s
        Assert.Equal(Vector([| 8.; 10.; 12. |]), result)

    [<Fact>]
    let op_divide () =
        let s = 2.
        let v = Vector([| 4.; 5.; 6. |])
        let result = v / s
        Assert.Equal(Vector([| 2.; 2.5; 3. |]), result)

    [<Fact>]
    let op_negate () =
        let v = Vector([| 4.; 5.; 6. |])
        let result = -v
        Assert.Equal(Vector([| -4.; -5.; -6. |]), result)

    [<Fact>]
    let op_Inequality_0 () =
        let v1 = Vector([| 1.; 2.; 3. |])
        let v2 = Vector([| 4.; 5.; 6. |])
        let result = v1 <> v2
        Assert.True(result)

    [<Fact>]
    let op_Inequality_1 () =
        let v1 = Vector([| 1.; 2.; 3. |])
        let v2 = Vector([| 1.; 2.; 3. |])
        let result = v1 <> v2
        Assert.False(result)

    [<Fact>]
    let op_Equality_0 () =
        let v1 = Vector([| 1.; 2.; 3. |])
        let v2 = Vector([| 4.; 5.; 6. |])
        let result = v1 = v2
        Assert.False(result)

    [<Fact>]
    let op_Equality_1 () =
        let v1 = Vector([| 1.; 2.; 3. |])
        let v2 = Vector([| 1.; 2.; 3. |])
        let result = v1 = v2
        Assert.True(result)

    [<Fact>]
    let magnitude_0 () =
        let v = Vector([| 2.; 3.; 4. |])
        let result = Vector.magnitude v
        Assert.Equal(sqrt(29.), result)

    [<Fact>]
    let length_0 () =
        let v = Vector([| 2.; 3.; 4. |])
        let result = Vector.length v
        Assert.Equal(3, result)

    [<Fact>]
    let normalize_0 () =
        let v = Vector([| 2.; 3.; 4. |])
        let result = Vector.normalize v
        Assert.Equal(v / sqrt(29.), result)

    [<Fact>]
    let cross_0 () =
        let v1 = Vector([| 1.; 2.; 3. |])
        let v2 = Vector([| 2.; 3.; 4. |])
        let result = Vector.cross v1 v2
        Assert.Equal(Vector([| -1.; 2.; -1. |]), result)

    [<Fact>]
    let reflect_0 () =
        let v1 = Vector([| 1.; -1.; 0. |])
        let v2 = Vector([| 0.;  1.; 0. |])
        let result = Vector.reflect v1 v2
        Assert.Equal(Vector([| 1.; 1.; 0. |]), result)