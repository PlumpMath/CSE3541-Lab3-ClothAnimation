module Util

let rec slicesBySize size list =
    match list with
    | [] -> [] // case needed for type inference
    | list when list.Length < size -> [list]
    | _ ->
        let first = list |> Seq.take size |> List.ofSeq
        let rest = list |> Seq.skip size |> List.ofSeq

        [first] @ slicesBySize size rest