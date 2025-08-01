namespace RayVE

module String =
    let chunkSplit chunkSize joinCharacter (values: seq<string>) =
        // contained mutation is probably more performant on large sequences
        // of values than using recursion or folding would be, gives true
        //streaming behavior
        let mutable acc = ""
        seq { for elem in values do
                  if acc.Length + elem.Length + 1 > chunkSize then
                      yield acc
                      acc <- elem
                  elif acc.Length = 0 then acc <- elem
                  else acc <- acc + joinCharacter + elem
              yield acc }