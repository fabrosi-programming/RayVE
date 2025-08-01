namespace RayVE

open System

type Vector([<ParamArray>] values: double[]) =
    member __.Values =
        values
    
    new(vector: Vector) =
        Vector vector.Values
    
    member __.Item
        with get(index) = values.[index]
                                   
    override this.Equals other =
        match other with
        | :? Vector as v when this.Values.Length = v.Values.Length ->
            Array.map2 (-) this.Values v.Values
            |> Array.forall (fun diff -> Math.Abs(diff) <= Constants.EPSILON)
        | _ -> false

    override __.GetHashCode() =
        __.Values.GetHashCode()

    override __.ToString() =
        values
        |> Array.map string
        |> String.concat ", "
        |> sprintf "(%s)"

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

module Vector =
    let magnitude (vector: Vector) =
        vector.Values
        |> Array.map (fun v -> v ** 2.0)
        |> Array.sum
        |> sqrt

    let length (vector: Vector) =
        vector.Values.Length

    let normalize (vector: Vector) =
        let m = magnitude vector
        vector.Values
        |> Array.map (fun e -> e / m)
        |> Vector

    let cross (v1: Vector) (v2: Vector) =
        if length v1 <> 3 then
            invalidArg "v1" "Cross product requires 3D vectors"
        if length v2 <> 3 then
            invalidArg "v2" "Cross product requires 3D vectors"
        [| (v1.[1] * v2.[2]) - (v1.[2] * v2.[1])
           (v1.[2] * v2.[0]) - (v1.[0] * v2.[2])
           (v1.[0] * v2.[1]) - (v1.[1] * v2.[0]) |]
        |> Vector

    module Create =
        let zero(size: int) =
            Array.replicate size 0.0
            |> Vector