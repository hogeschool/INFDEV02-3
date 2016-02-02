module FunctionalWeek1

open CommonLatex
open SlideDefinition
open CodeDefinitionLambda
open Interpreter

let slides =
  [
    Section("Introduction")
    SubSection("Lecture topics")
    ItemsBlock
      [
        !"Course introduction"
        !"Exam and practicum"
        !"Semantics of traditional programming languages"
        !"Substitution rules and referential transparency"
        !"Basic lambda calculus"
      ]

    Section("Course introduction")
    SubSection("Course topics")
    ItemsBlock
      [
        !"We will discuss a completely new paradigm for expressing programs"
        !"This paradigm, functional programming, is based on different premises on computation"
        !"It gives guarantees of correctness in complex places, like parallelism or separation of concerns"
        !"It requires a radical conceptual shift in the way you think about programming"
      ]

    ItemsBlock
      [
        !"We will begin with a short discussion on traditional programming language \textbf{semantics}"
        !"We will then show the \textbf{lambda calculus}, which is the foundation for functional languages"
        VerticalStack
          [
          !"We will then bridge the gap between theory and practice"
          ItemsBlock
            [
              !"We will translate the lambda calculus into two mainstream functional languages: F\# and Haskell"
              !"This will cover a huge chunk of possible applications in countless other languages and libraries, from C\# LINQ to Java streams, to Scala, Scheme, Closure, etc."
            ]
          ]
      ]

    Section("Examination")
    SubSection("Exam structure")
    ItemsBlock
      [
        !"There is a theoretical exam, where you show understanding of the basic principles"
        !"There is a practical exam, where you show understanding of their concrete applications"
      ]

    SubSection("Theoretical exam")
    ItemsBlock
      [
        !"One question on a lambda calculus program execution"
        !"One question on the type system of a lambda calculus program, F\# program, or Haskell program"
        !"Both questions must be answered correctly to get a \textbf{voldoende}"
      ]

    SubSection("Practical exam")
    VerticalStack
      [
        Small
        TextBlock "Build, in groups of max four, any of the following applications in either Haskell or F\#:"

        ItemsBlock
          [
            !"A 2D simulation of a supermarket with customers, cash registers, and various aisles"
            !"A 2D simulation of a supply chain with trucks, containers, and ships"
            !"An interpreter for a Python-like language (with a parser for an extra challenge)"
            !"An interpreter for the lambda calculus  (with a parser for an extra challenge)"
          ]

        TextBlock @"We will get together at the end of the course, and the teacher(s) will remove a few lines from each of your applications. \textbf{Individually} you will have to restore them within a few hours."
      ]

    Section "Semantics of traditional programming languages"
    VerticalStack
      [
        ItemsBlock
          [
            !"Traditional, imperative programming languages are based on sharing memory through instructions"
            !"This means that subsequent instructions are not independent from each other"
            !"Any function call makes use of the available memory"
          ]
      ]

    VerticalStack
      [
        TextBlock "For example, consider the semantic rules that describe the working of ``;''"

        TypingRules
          [
            {
              Premises = [ @"\langle s_1,S,H \rangle \rightarrow \langle S_1,H_1 \rangle"; @"\langle s_2,S_1,H_1 \rangle \rightarrow \langle S_2,H_2 \rangle" ]
              Conclusion = @"\langle (s_1;s_2),S,H \rangle \rightarrow \langle S_2,H_2 \rangle"
            }
          ]
      ]

//    Section "Substitution rules and referential transparency"
//    Section "Basic lambda calculus"


//    VerticalStack
//      [
//        TextBlock "Addition:"
//        LambdaCodeBlock(TextSize.Tiny, ChurchNumerals.plus)
//      ]
//
    LambdaStateTrace(TextSize.Tiny, ((((And >>> True) >>> True) >>> !!"T") >>> !!"F"))
  ]

