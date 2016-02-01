module FunctionalWeek1

open CommonLatex
open SlideDefinition
open CodeDefinitionLambda
open Interpreter

module ChurchNumerals =
  let zero = ("s" ==> ("z" ==> !!"z"))
  let one = ("s" ==> ("z" ==> (!!"s" >>> !!"z")))
  let two = ("s" ==> ("z" ==> (!!"s" >>> (!!"s" >>> !!"z"))))
  let succ = ("n" ==> ("s" ==> ("z" ==> (!!"s" >>> ((!!"n" >>> !!"s") >>> !!"z")))))
  let mult = ("m" ==> ("n" ==> ("s" ==> (!!"m" >>> (!!"n" >>> !!"s")))))
  let plus = ("m" ==> 
              ("n" ==> 
               ("s" ==> 
                ("z" ==> 
                  ((!!"m" >>> !!"s") >>> 
                    ((!!"n" >>> !!"s") >>> !!"z"))))))

let slides =
  [
//    Section("Introduction")
//    SubSection("Lecture topics")
//    VerticalStack
//      [
//        TextBlock "Numbers:"
//        LambdaCodeBlock(TextSize.Tiny, ChurchNumerals.zero)
//        LambdaCodeBlock(TextSize.Tiny, ChurchNumerals.one)
//        LambdaCodeBlock(TextSize.Tiny, ChurchNumerals.two)
//      ]
//
//    VerticalStack
//      [
//        TextBlock "Addition:"
//        LambdaCodeBlock(TextSize.Tiny, ChurchNumerals.plus)
//      ]

//    LambdaStateTrace(TextSize.Tiny, (("x" ==> ("y" ==> ("z" ==> ("w" ==> (!!"x" >>> !!"y" >>> !!"z" >>> !!"w" ))))) >>> !!"8" >>> !!"=")>>> !!"=" >>> !!"D")
//    LambdaStateTrace(TextSize.Tiny, (((ChurchNumerals.plus >>> !!"1") >>> !!"1") >>> !!"S") >>> !!"Z")
//    LambdaStateTrace(TextSize.Tiny, (((ChurchNumerals.plus >>> ChurchNumerals.one) >>> ChurchNumerals.one) >>> !!"S") >>> !!"Z")
    LambdaStateTrace(TextSize.Tiny, (((defaultTerms.[!!"AND"] >>> defaultTerms.[!!"TRUE"] >>> defaultTerms.[!!"TRUE"]) >>> !!"T") >>> !!"F"))

    VerticalStack
      [
        TextBlock "Multiplication:"
        LambdaCodeBlock(TextSize.Tiny, ChurchNumerals.mult)
      ]
  ]

