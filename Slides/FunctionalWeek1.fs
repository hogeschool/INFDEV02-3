module FunctionalWeek1

open CommonLatex
open SlideDefinition
open CodeDefinitionLambda
open Interpreter

let slides =
  [
    Section("Introduction")
    SubSection("Lecture topics")
    InferenceCodeBlock(TextSize.Small, Application(Lambda("x",Application(Var"x", Var"y")), Var"+"))
  ]

