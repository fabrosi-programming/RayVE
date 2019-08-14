namespace RayVE

open System
open System.Collections
open System.Collections.Generic

type Vector([<ParamArray>] values: double[]) =
    member __.Values = values
    
    new(vector: Vector) = Vector vector.Values
    
    member __.Item
        with get(index) = values.[index]    

    member __.Magnitude
        with get() = values
            |> Array.map (fun v -> v ** 2.0)
            |> Array.sum
            |> sqrt

    member __.Normalize() = values |> Array.map (fun e -> e / __.Magnitude)

    member vector1.Cross (vector2: Vector) = [| (vector1.[1] * vector2.[2]) - (vector1.[2] * vector2.[1])
                                                (vector1.[2] * vector2.[0]) - (vector1.[0] * vector2.[2])
                                                (vector1.[0] * vector2.[1]) - (vector1.[1] * vector2.[0]) |]
                                             |> Vector
                                   
    static member (+) (left: Vector, right: Vector) = Array.map2 (+) left.Values right.Values
                                                           |> Vector

    static member (-) (left: Vector, right: Vector) = Array.map2 (-) left.Values right.Values
                                                           |> Vector
 
    static member (*) (left: Vector, right: Vector) = Array.map2 (*) left.Values right.Values
                                                           |> Array.sum

    static member (*) (scalar, vector: Vector) = vector.Values
                                                 |> Array.map (fun e -> e * scalar)
                                                 |> Vector

    static member (/) (vector: Vector, scalar) = (1.0 / scalar) * vector

    static member (~-) (vector: Vector) = -1.0 * vector

    static member Zero(size: int) = Array.replicate size 0.0
                                    |> Vector