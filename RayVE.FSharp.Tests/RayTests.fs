namespace RayVE.FSharp.Tests

open Xunit
open RayVE

module RayTests =

    [<Fact>]
    let op_multiply_0 () =
        let origin = Point3D(1.0, 2.0, 3.0)
        let direction = Vector3D(4.0, 5.0, 6.0)
        let ray = Ray(origin, direction)
        let matrix = Matrix([| [| 1.; 2.; 3; 0. |];
                               [| 5.; 6.; 3; 0. |];
                               [| 1.; 3.; 4; 0. |];
                               [| 0.; 0.; 0; 1. |]; |])
        let result = ray * matrix
        let expected_origin = Point3D(14., 23., 21.)
        let expected_direction = Vector3D(0.41947012443269766, 0.6711521990923163, 0.6112278956019308)
        Assert.Equal(Ray(expected_origin, expected_direction), result)

    [<Fact>]
    let op_multiply_1 () =
        let origin = Point3D(1.0, 2.0, 3.0)
        let direction = Vector3D(4.0, 5.0, 6.0)
        let ray = Ray(origin, direction)
        let matrix = Matrix([| [| 1.; 2.; 3; 0. |];
                               [| 5.; 6.; 3; 0. |];
                               [| 1.; 3.; 4; 0. |];
                               [| 0.; 0.; 0; 1. |]; |])
        let result = matrix * ray
        let expected_origin = Point3D(14., 26., 19.)
        let expected_direction = Vector3D(0.3695780952934598, 0.785353452498602, 0.4966205655505866)
        Assert.Equal(Ray(expected_origin, expected_direction), result)