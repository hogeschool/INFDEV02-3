module FunctionalWeek2

open CommonLatex
open SlideDefinition
open CodeDefinitionLambda
open Interpreter

let slides =
  [
    Section("Introduction")
    SubSection("Lecture topics")
    VerticalStack
      [
        TextBlock "Simply typed lambda calculus"
        TextBlock "Make it pretty: delta rules"
        TextBlock "Booleans, boolean logic, if-then-else"
        TextBlock "Naturals"
        TextBlock "Let and let-rec"
        TextBlock "Making it real: F\# and Haskell"
      ]

//    VerticalStack
//      [
//        TextBlock "Addition:"
//        LambdaCodeBlock(TextSize.Tiny, ChurchNumerals.plus)
//      ]
//
//    LambdaStateTrace(TextSize.Tiny, ((((And >>> True) >>> True) >>> !!"T") >>> !!"F"))
//
//    VerticalStack
//      [
//        TextBlock "Multiplication:"
//        LambdaCodeBlock(TextSize.Tiny, (deltaRules Mult).Value)
//      ]
  ]

