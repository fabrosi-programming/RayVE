namespace RayVE

open System

type Vector([<ParamArray>] values: double[]) =
    member __.Values =
        values
    
    new(vector: Vector) =
        Vector vector.Values
    
    member __.Item
        with get(index) = values.[index]    

    member __.Magnitude
        with get() = values
            |> Array.map (fun v -> v ** 2.0)
            |> Array.sum
            |> sqrt

    member __.Length
        with get() = values.Length

    member __.Normalize() =
        values |> Array.map (fun e -> e / __.Magnitude)
               |> Vector

    member v1.Cross (v2: Vector) =
        [| (v1.[1] * v2.[2]) - (v1.[2] * v2.[1])
           (v1.[2] * v2.[0]) - (v1.[0] * v2.[2])
           (v1.[0] * v2.[1]) - (v1.[1] * v2.[0]) |]
        |> Vector
                                   
    override this.Equals other =
        match other with
        | :? Vector as v -> Array.map2 (-) this.Values v.Values
                            |> Array.exists (fun e -> e > Constants.EPSILON)
                            |> not
        | _ -> false

    override __.GetHashCode() =
        __.Values.GetHashCode()

    override __.ToString() =
        "(" + (seq { for i in 0 .. values.Length - 1 -> values.[i].ToString() }
              |> String.concat ", " ) + ")"

    static member (+) (left: Vector, right: Vector) =
        Array.map2 (+) left.Values right.Values
        |> Vector

    static member (-) (left: Vector, right: Vector) =
        Array.map2 (-) left.Values right.Values
        |> Vector
 
    static member (*) (left: Vector, right: Vector) =
        Array.map2 (*) left.Values right.Values
        |> Array.sum

    static member (*) (scalar, vector: Vector) =
        vector.Values
        |> Array.map (fun e -> e * scalar)
        |> Vector

    static member (*) (vector: Vector, scalar: float) =
        scalar * vector

    static member (/) (vector: Vector, scalar) =
        (1.0 / scalar) * vector

    static member (~-) (vector: Vector) =
        -1.0 * vector

    static member op_Inequality (left: Vector, right: Vector) =
        not (left.Equals right)

    static member op_Equality (left: Vector, right: Vector) =
        left.Equals right

    static member Zero(size: int) =
        Array.replicate size 0.0
        |> Vector