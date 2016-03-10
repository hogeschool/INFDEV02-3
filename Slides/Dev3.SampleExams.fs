module Dev3.SampleExams

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime
open TypeChecker

let exam3 =
  [
    Section("Question 1")
    TextBlock @"Given the following block of code, fill in the stack, heap, and PC with all the steps taken by the program at runtime."
    ItemsBlock
      [
        ! @"Points: \textit{4 (50\% of total).}"
        ! @"Grading: one point per correctly filled-in execution step."
        ! @"Associated learning objective: \textit{abstraction}."
      ]
    CSharpStateTrace(TextSize.Small,
          ((interfaceDef "MovableObject" []) >>
             (((classDef "Car" 
                  [
                    implements "MovableObject"
                    typedDeclAndInit "direction" "float" (ConstFloat(3.14)) |> makePrivate
                    typedDef "Car" [] "" (endProgram) |> makePublic
                    typedDef "move" [("Car", "car");("float","direction")] "void" ("car.direction" := var "direction") |> makePublic |> makeStatic
                  ] >>
                (classDef "Particle" 
                  [
                    implements "MovableObject"
                    typedDeclAndInit "direction" "float" (ConstFloat(0.00)) |> makePrivate
                    typedDef "Particle" [] "" (endProgram) |> makePublic
                  ])) >>
                (((typedDeclAndInit "mo" "MovableObject" (newC "Car" [])) >>
                     (staticMethodCall "Car" "move" [ Var("mo");(ConstFloat(1.0))])) >> 
                      endProgram)))),
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
          (interfaceDef "IntList" 
                        [
                          typedSig "isEmpty" [] "bool"
                          typedSig "getValue" [] "int"
                        ]) >>
           (classDef "IntNode"
                    [
                      extends "IntList"
                      typedDecl "value" "int" |> makePrivate
                      typedDecl "tail" "IntList" |> makePrivate
                      typedDef "IntNode" [("int", "value"); ("IntList", "tail")] "" (("this.value" := var "value") >> ("this.tail" := var "tail") ) |> makePublic
                      typedDef "isEmpty" [] "bool" (ret (ConstBool(false))) |> makePublic
                      typedDef "getValue" [] "int" (ret (var ("this.value"))) |> makePublic
                    ] >>
            (classDef "IntEmpty"
                    [
                      extends "IntList"
                      typedDef "IntEmpty" [] "" (endProgram) |> makePublic
                      typedDef "isEmpty" [] "bool" (ret (ConstBool(true))) |> makePublic
                      typedDef "getValue" [] "int" (ret (ConstInt(0))) |> makePublic
                    ] >>
             (dots >> 
              (typedDeclAndInit "list" "IntList" (newC "IntNode" [(constInt(5));(newC "IntEmpty" [])]) >>
               endProgram)))),
           TypeCheckingState.Zero, false)
  ]

let exam2 =
  [
    Section("Question 1")
    TextBlock @"Given the following block of code, fill in the stack, heap, and PC with all the steps taken by the program at runtime."
    ItemsBlock
      [
        ! @"Points: \textit{4 (50\% of total).}"
        ! @"Grading: one point per correctly filled-in execution step."
        ! @"Associated learning objective: \textit{abstraction}."
      ]
    CSharpStateTrace(TextSize.Small,
          (interfaceDef "Animal" 
                        [
                          typedSig "makeSound" [] "void"
                        ] >>
           (classDef "Dog" 
                    [
                      implements "Animal"
                      typedDef "Dog" [] "" (endProgram) |> makePublic
                      typedDef "makeSound" [] "void" (staticMethodCall "Console" "WriteLine" [ConstString "Woof!"]) |> makePublic
                    ] >>
            (classDef "Cat" 
                    [
                      implements "Animal"
                      typedDef "Cat" [] "" (endProgram) |> makePublic
                      typedDef "makeSound" [] "void" (staticMethodCall "Console" "WriteLine" [ConstString "Miao!"]) |> makePublic
                    ] >>
             (typedDeclAndInit "myAnimal" "Animal" (newC "Cat" []) >>
              (methodCall "myAnimal" "makeSound" [] >> 
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
          (classDef "Employee" 
                    [
                      typedDecl "name" "string"  |> makePrivate
                      typedDef "Employee" [] "" (endProgram) |> makePublic
                      typedDef "GetName" [] "string" (ret (var"this.name") ) |> makePublic
                    ] >>
           (classDef "Manager" 
                    [
                      extends "Employee"
                      typedDef "Manager" [] "" (endProgram) |> makePublic
                      typedDef "GetFunction" [] "string" (ret (ConstString "I am the boss!") ) |> makePublic
                    ] >>
            (dots >> 
             (typedDeclAndInit "Employee" "employee" (newC "Manager" []) >>
              endProgram)))),

          TypeCheckingState.Zero, false)
  ]

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
          TypeCheckingState.Zero, false)
  ]
