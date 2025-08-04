namespace RayVE.FSharp.Tests

open Xunit
open RayVE

module MathTests =

    [<Fact>]
    let roundUp_0 () =
        let result = Math.roundUp 2 1.2345
        Assert.Equal(1.24, result)

    [<Fact>]
    let roundUp_1 () =
        let result = Math.roundUp 3 1.2345
        Assert.Equal(1.235, result)

    [<Fact>]
    let clamp_0 () =
        let result = Math.clamp 1 3 4
        Assert.Equal(3, result)
    
    [<Fact>]
    let clamp_1 () =
        let result = Math.clamp 1 3 -3
        Assert.Equal(1, result)

    [<Fact>]
    let discriminant_0 () =
        let result = Math.discriminant 2. 3. 4.
        Assert.Equal(-23., result)