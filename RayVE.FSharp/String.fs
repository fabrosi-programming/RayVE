namespace RayVE

module String =
    let chunkSplit chunkSize joinCharacter (values: seq<string>) =
        //TODO: figure out how to do this without a mutable accumulator
        let mutable acc = ""
        seq { for elem in values do
                  if acc.Length + elem.Length + 1 > chunkSize then
                      yield acc
                      acc <- elem
                  elif acc.Length = 0 then acc <- elem
                  else acc <- acc + joinCharacter + elem
              yield acc }