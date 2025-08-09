namespace RayVE.FSharp.Tests

open Xunit
open RayVE

module SurfaceTests =
    module Pattern =

        [<Fact>]
        let colorAt_Solid () =
            let pattern = Solid(Color(0.1, 0.2, 0.3), Matrix.Create.identity 3)
            let point = Point3D(0.0, 0.0, 0.0)
            let color = Pattern.colorAt pattern point
            Assert.Equal(Color(0.1, 0.2, 0.3), color)