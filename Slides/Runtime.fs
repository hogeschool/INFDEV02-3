module Runtime

open CommonLatex


let printBindings toString isHidden (b:Map<string,'code>) =
  let pc = b |> Map.tryFind "PC"
  let ret = b |> Map.tryFind "ret"
  let b' = b |> Map.remove "PC" |> Map.remove "ret"
  let entries = [ for x in b' do if isHidden x.Value then () else yield x]
  let innerNames = [ for x in entries do yield x.Key ]
  let innerValues = [ for x in entries do yield x.Value ]
  let names = (match ret with | Option.None -> innerNames | _ -> "ret" :: innerNames)
  let values = (match ret with | Option.None -> innerValues | Some x -> x :: innerValues)
  let names = (match pc with | Option.None -> names | _ -> "PC" :: names)
  let values = (match pc  with | Option.None -> values | Some x -> x :: values)
  let allNames = if names |> List.isEmpty then "" else names |> List.reduce (fun a b -> a + " & " + b)
  let allValues = if values |> List.isEmpty then "" else values |> List.map (fun v -> (toString v) "") |> List.reduce (fun a b -> a + " & " + b)
  allNames,allValues

type RuntimeState<'code> = { Stack : List<Map<string, 'code>>; HeapSize : int; Heap : Map<string, 'code>; InputStream : List<string>; OutputStream : List<string> }
  with 
    static member Zero intOne =
      { Stack = [["PC", intOne] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1; OutputStream = [] }
    static member WithInput intOne input =
      { Stack = [["PC", intOne] |> Map.ofList]; Heap = Map.empty; InputStream = input; HeapSize = 1; OutputStream = [] }
    member this.AsSlideContent dots isHidden toString =
      let stack = 
        match this.Stack with
        | [] -> []
        | c::k ->
          c :: [ for x in k do 
                 yield Map.empty |> Map.add "..." dots |> Map.add "PC" (x.["PC"])]
      let stackFrames = 
        [
          for sf in stack do
          yield printBindings toString isHidden sf 
        ] |> List.rev
      let stackNamesByFrame = stackFrames |> List.map fst
      let stackValuesByFrame = stackFrames |> List.map snd
      let stackNames = stackNamesByFrame |> List.reduce (fun a b -> a + " & & " + b)
      let stackValues = stackValuesByFrame |> List.reduce (fun a b -> a + " & & " + b)

      let hd = 
        [ 
          for sf in stack do
            yield [for v in sf do yield "c"]
        ] |> List.rev |> List.reduce (fun a b -> a @ (@">{\columncolor[gray]{0.8}}c" :: b))
      let stackTableContent = sprintf "%s \\\\\n\\hline\n%s \\\\\n\\hline\n" stackNames stackValues
      let stackTable = sprintf "%s\n%s\n%s\n" (beginTabular hd) stackTableContent endTabular

      let heap = 
        if this.Heap |> Seq.filter (fun x -> isHidden x.Value |> not) |> Seq.isEmpty then ""
        else 
          let heapNames,heapValues = printBindings toString isHidden this.Heap
          let hd = heapNames |> Seq.map (fun _ -> "c") |> Seq.toList
          let heapTableContent = sprintf "%s \\\\\n\\hline\n%s \\\\\n\\hline\n" heapNames heapValues
          sprintf "%s\n%s\n%s" (beginTabular hd) heapTableContent endTabular

      let consoleInput = 
        if this.InputStream.IsEmpty then ""
        else 
          let hd = this.InputStream |> Seq.map (fun _ -> ">{\columncolor{black}}c") |> Seq.toList
          let inputs = this.InputStream  |> List.map (fun a -> @"\white{\texttt{" + a + "}}") 
                                          |> List.reduce (fun a b -> a + " & " + b)
          let inputsTableContent = sprintf "%s \\\\ \hline\n" inputs
          sprintf "%s\n%s\n%s" (beginTabular hd) inputsTableContent endTabular

      let consoleOutput = 
        if this.OutputStream.IsEmpty then ""
        else 
          let hd = this.OutputStream |> Seq.map (fun _ -> ">{\columncolor{black}}c") |> Seq.toList
          let outputs = this.OutputStream |> List.rev
                                          |> List.map (fun a -> @"\white{\texttt{" + a + "}}") 
                                          |> List.reduce (fun a b -> a + " & " + b)
          let outputsTableContent = sprintf "%s \\\\ \hline\n" outputs
          sprintf "%s\n%s\n%s" (beginTabular hd) outputsTableContent endTabular

      stackTable, heap, consoleInput, consoleOutput

