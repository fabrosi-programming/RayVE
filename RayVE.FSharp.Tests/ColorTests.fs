namespace RayVE.FSharp.Tests

open Xunit
open RayVE

module ColorTests =
    
    [<Fact>]
    let op_add_0 () =
        let c1 = Color(0.9, 0.6, 0.75)
        let c2 = Color(0.7, 0.1, 0.25)
        let expected = Color(1.6, 0.7, 1.0)
        Assert.Equal(expected, c1 + c2)
    
    [<Fact>]
    let op_subtract_0 () =
        let c1 = Color(0.9, 0.6, 0.75)
        let c2 = Color(0.7, 0.1, 0.25)
        let expected = Color(0.2, 0.5, 0.5)
        Assert.Equal(expected, c1 - c2)
    
    [<Fact>]
    let op_multiply_0 () =
        let c1 = Color(1.0, 0.2, 0.4)
        let c2 = Color(0.9, 1.0, 0.1)
        let expected = Color(0.9, 0.2, 0.04)
        Assert.Equal(expected, c1 * c2)
    
    [<Fact>]
    let op_multiply_1 () =
        let c = Color(0.2, 0.3, 0.4)
        let scalar = 2.0
        let expected = Color(0.4, 0.6, 0.8)
        Assert.Equal(expected, scalar * c)
        Assert.Equal(expected, c * scalar)

    [<Fact>]
    let op_divide_0 () =
        let c = Color(0.4, 0.8, 1.6)
        let scalar = 2.0
        let expected = Color(0.2, 0.4, 0.8)
        Assert.Equal(expected, c / scalar)
    
    [<Fact>]
    let PPMValue_0 () =
        let maxValue = 255
        let rawValue = 1.5
        let expected = 191
        let result = Color.PPMValue maxValue rawValue
        Assert.Equal(expected, result)
    
    [<Fact>]
    let toPPM_0 () =
        let color = Color(0.5, 0.75, 1.0)
        let maxValue = 255
        let expected = [| "127"; "191"; "255" |]
        let result = Color.toPPM color maxValue
        Assert.Equal<string[]>(expected, result)