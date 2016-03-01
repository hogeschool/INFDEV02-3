module Dev3.SampleExams

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime
open TypeChecker

let exam1 = 
  [
    Section("Question 1")
    TextBlock @"Given the following block of code, fill in the stack, heap, and PC with all steps taken by the program at runtime."
    ItemsBlock
      [
        ! @"Points: \textit{4 (50\% of total).}"
        ! @"Grading: one point per correctly filled-in execution step."
        ! @"Associated learning objective: \textit{abstraction}."
      ]

    CSharpStateTrace(TextSize.Small,
          ((interfaceDef "A" 
              [
                typedSig "M" ["int","x"] "int"
              ]) >>
             ((((classDef "C" 
                  [
                    implements "A"
                    typedDef "C" [] "" (endProgram) |> makePublic
                    typedDef "M" ["int","x"] "int" (ret (var"x" .+ constInt 2) ) |> makePublic
                  ]) >>
                (classDef "D" 
                  [
                    implements "A"
                    typedDef "D" [] "" (endProgram) |> makePublic
                    typedDef "M" ["int","x"] "int" (ret (var"x" .+ constInt 2) ) |> makePublic
                  ])) >>
                (dots >> 
                  (((typedDeclAndInit "myA" "A" (newC "C" [])) >>
                     (staticMethodCall "Console" "WriteLine" [methodCall "myA" "M" [ConstInt 5]])) >> 
                      endProgram))))),
          Runtime.RuntimeState<_>.Zero (constInt 1))
              

    Section("Question 2")
    TextBlock @"Given the following block of code, fill in the declarations, class definitions, and PC with all steps taken by the compiler while type checking."

    ItemsBlock
      [
        ! @"Points: \textit{4 (50\% of total).}"
        ! @"Grading: one point per correctly filled-in type checking step."
        ! @"Associated learning objective: \textit{type checking}."
      ]

    CSharpTypeTrace(TextSize.Small,
          ((interfaceDef "A" 
              [
                typedSig "M" ["int","x"] "int"
              ]) >>
             ((((classDef "C" 
                  [
                    implements "A"
                    typedDef "C" [] "" (endProgram) |> makePublic
                    typedDef "M" ["int","x"] "int" (ret (var"x" .+ constInt 2) ) |> makePublic
                  ]) >>
                (classDef "D" 
                  [
                    implements "A"
                    typedDef "D" [] "" (endProgram) |> makePublic
                    typedDef "M" ["int","x"] "int" (ret (var"x" .+ constInt 2) ) |> makePublic
                  ])) >>
                (dots >> 
                  (((typedDeclAndInit "myA" "A" (newC "C" [])) >>
                     (staticMethodCall "Console" "WriteLine" [methodCall "myA" "M" [ConstInt 5]])) >> 
                      endProgram))))),
          TypeCheckingState.Zero)
  ]
