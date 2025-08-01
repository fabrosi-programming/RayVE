namespace RayVE

module Intersection =
    let position intersection =
        Ray.position intersection.Ray intersection.Distance

    let normalVector intersection =
        let candidate = Surface.normal intersection.Surface (position intersection)
        let eyeVector = -intersection.Ray.Direction
        let dot = eyeVector * candidate
        if dot < 0.0 then -candidate else candidate

    let overPosition intersection =
        position intersection + normalVector intersection * Constants.EPSILON