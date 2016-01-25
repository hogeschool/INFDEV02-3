module StateTraceSamples

open CommonLatex
open SlideDefinition
open CodeDefinition
open Interpreter

let slides = 
  [
    CSharpStateTrace(TextSize.Tiny,
          ((interfaceDef "ICounter" 
              [
                typedSig "Incr" ["int","diff"] "void"
              ]) >>
           ((classDef "Counter" 
              [
                implements "ICounter"
                typedDecl "cnt" "int" |> makePrivate
                typedDef "Counter" [] "" ("this.cnt" := constInt 0) |> makePublic
                typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
              ]) >>
            (((typedDeclAndInit "c" "ICounter" (newC "Counter" [])) >>
               (methodCall "c" "Incr" [ConstInt 5])) >> 
                endProgram))),
          { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1 })

    CSharpStateTrace(TextSize.Tiny,
          ((classDef "Counter" 
              [
                typedDecl "cnt" "int" |> makePrivate
                typedDef "Counter" [] "" ("this.cnt" := constInt 0) |> makePublic
                typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
              ]) >>
           (((typedDeclAndInit "c" "Counter" (newC "Counter" [])) >>
              (methodCall "c" "Incr" [ConstInt 5])) >> 
               endProgram)),
          { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1 })

    PythonStateTrace(TextSize.Tiny,
          ((classDef "Counter" 
              [
                def "__init__" ["self"] ("self.cnt" := constInt 0)
                def "incr" ["self"; "diff"] ("self.cnt" := (var "self.cnt" .+ var "diff"))
              ]) >>
            ((("c" := newC "Counter" []) >>
              (methodCall "c" "incr" [ConstInt 5])) >>
              endProgram)),
          { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1 })


    PythonStateTrace(TextSize.Tiny,
      (def "f" ["x"] 
        (ifelse (var "x" .> constInt 0) 
          (ret ((call "f" [constInt -20]) .+ constInt 1))
          (ret (var "x" .* constInt 2))) >>
       call "f" [constInt 20] >>
       endProgram),
      { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = []; HeapSize = 1 }
    )
  ]
