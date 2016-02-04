module StateTraceSamples

open CommonLatex
open SlideDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
    CSharpStateTrace(TextSize.Tiny,
          (classDef "MyClass" 
              [
                typedDef "f" ["int","x"] "int" ((ret (var "x" .+ ConstInt(10)))) |> makePublic |> makeStatic
              ]) >>
           ((dots >>
             (staticMethodCall "Console" "WriteLine" [staticMethodCall "MyClass" "f" [ConstInt(10)]])) >> endProgram),
            RuntimeState<_>.Zero (constInt 1))

    CSharpStateTrace(TextSize.Tiny,
        ((classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" ("this.cnt" := constInt 0) |> makePublic
              typedDef "incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
            ]) >>
           ((dots >>
             ((typedDeclAndInit "c" "Counter" (newC "Counter" [])) >>
               (methodCall "c" "incr" [ConstInt 5])))) >> endProgram),
        RuntimeState<_>.Zero (constInt 1))

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
          Runtime.RuntimeState<_>.Zero (constInt 1))

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
          Runtime.RuntimeState<_>.Zero (constInt 1))

    PythonStateTrace(TextSize.Tiny,
          ((classDef "Counter" 
              [
                def "__init__" ["self"] ("self.cnt" := constInt 0)
                def "incr" ["self"; "diff"] ("self.cnt" := (var "self.cnt" .+ var "diff"))
              ]) >>
            ((("c" := newC "Counter" []) >>
              (methodCall "c" "incr" [ConstInt 5])) >>
              endProgram)),
          Runtime.RuntimeState<_>.Zero (constInt 1))


    PythonStateTrace(TextSize.Tiny,
      (def "f" ["x"] 
        (ifelse (var "x" .> constInt 0) 
          (ret ((call "f" [constInt -20]) .+ constInt 1))
          (ret (var "x" .* constInt 2))) >>
       call "f" [constInt 20] >>
       endProgram),
      Runtime.RuntimeState<_>.Zero (constInt 1))
  ]
