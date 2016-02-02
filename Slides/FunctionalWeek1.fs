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
    SubSection "Semicolon and interference"
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

        TextBlock "First we run $s_1$ with the initial memory, then we run $s_2$ with the modified memory."

        Pause

        TypingRules
          [
            {
              Premises = [ @"\langle s_1,S,H \rangle \rightarrow \langle S_1,H_1 \rangle"; @"\langle s_2,S_1,H_1 \rangle \rightarrow \langle S_2,H_2 \rangle" ]
              Conclusion = @"\langle (s_1;s_2),S,H \rangle \rightarrow \langle S_2,H_2 \rangle"
            }
          ]
      ]

    VerticalStack
      [
        TextBlock @"What does ``\textit{first we run $s_1$ with the initial memory, then we run $s_2$ with the modified memory}'' imply?."
        Pause
        ItemsBlock 
          [
            ! @"The same instructions, executed at different moments, will produce \textbf{different results}."
            ! @"Change the order of some method calls, and some weird dependence might cause bugs or break things."
          ]
      ]

    SubSection "Goals"
    ItemsBlock 
      [
        ! @"Our goal is to ensure that behaviour of code is consistent."
        ! @"Change the order of some method calls, and the results remain the same."
        ! @"This makes it easier to test, parallelize, and in general ensure correctness."
      ]

    VerticalStack 
      [
        Question "How do we achieve this?"
        Pause
        TextBlock @"We give (shared) memory up: every piece of code is a function which output only depends on input."
        TextBlock @"This very important property is called \textbf{referential transparency}."
      ]

    Section "Basic lambda calculus"
    SubSection "Introduction"
    ItemsBlock 
      [
        ! @"The (basic) lambda calculus is an alternative mechanism to Turing Machines and the Von Neumann architecture."
        ! @"It is very different, but has equivalent expressive power."
        ! @"It is the foundation of all functional programming languages."
      ]

    SubSection "Substitution principle"
    ItemsBlock 
      [
        ! @"The (basic) lambda calculus is truly tiny when compared with its power."
        ! @"It is based on the substitution principle: calling a function with some parameters returns the function body with the variables replaced."
        ! @"There is no memory and no program counter: all we need to know is stored inside the body of the program itself."
      ]    

    SubSection "Grammar"
    VerticalStack
      [
        TextBlock @"A lambda calculus program (just \textit{program} from now on) is made up of three syntactic elements:"

        ItemsBlock 
          [
            ! @"Variables: $x$, $y$, ..."
            ! @"Abstractions (function declarations with one parameter): $\lambda x\rightarrow t$ where $x$ is a variable and $t$ is the function body (a program)."
            ! @"Applications (function calls with one argument): $t\ u$ where $t$ is the function being called (a program) and $u$ is its argument (another program)."
          ]
      ]

    VerticalStack
      [
        TextBlock "A simple example would be the identity function, which just returns whatever it gets as input"

        LambdaCodeBlock(TextSize.Tiny, "x" ==> !!"x")
      ]

    VerticalStack
      [
        TextBlock "We can call this function with a variable as argument, by writing:"

        LambdaCodeBlock(TextSize.Tiny, ("x" ==> !!"x") >>> !!"v")
      ]

    SubSection "Beta reduction"
    VerticalStack
      [
        TextBlock @"A lambda calculus program is computed by replacing lambda abstractions applied to arguments with the body of the lambda abstraction with the argument instead of the lambda parameter:"

        Pause

        TypingRules
          [
            {
              Premises = []
              Conclusion = @"(\lambda x \rightarrow t)\ u  \rightarrow_\beta t [ x \mapsto u ]"
            }
          ]

        TextBlock @"$t [ x \mapsto u ]$ means that we change variable $x$ with $u$ within $t$"
      ]

    LambdaStateTrace(TextSize.Tiny, ("x" ==> !!"x") >>> !!"v", Option.None)

    VerticalStack
      [
        TextBlock @"Multiple applications where the left-side is not a lambda abstraction are solved in a left-to-right fashion:"

        Pause

        TypingRules
          [
            {
              Premises = [ @"t \rightarrow_\beta t'"; @"u \rightarrow_\beta u'"; @"t'\ u' \rightarrow_\beta v"]
              Conclusion = @"t\ u \rightarrow_\beta v"
            }
          ]
      ]

    VerticalStack
      [
        TextBlock @"Variables cannot be further reduced, that is they stay the same:"

        Pause

        TypingRules
          [
            {
              Premises = []
              Conclusion = @"x \rightarrow_\beta x"
            }
          ]
      ]

    SubSection "Multiple parameters"
    VerticalStack
      [
        TextBlock "We can encode functions with multiple parameters by nesting lambda abstractions:"

        LambdaCodeBlock(TextSize.Tiny, "x" ==> ("y" ==> (!!"x" >>> !!"y")))
      ]

    VerticalStack
      [
        TextBlock "The parameters are then given one at a time:"

        LambdaCodeBlock(TextSize.Tiny, (("x" ==> ("y" ==> (!!"x" >>> !!"y"))) >>> !!"A") >>> !!"B")
      ]

    LambdaStateTrace(TextSize.Tiny, (("x" ==> ("y" ==> (!!"x" >>> !!"y"))) >>> !!"A") >>> !!"B", Option.None)

    Section "Closing up"
    SubSection "Example executions of (apparently) nonsensical programs"
    ItemsBlock
      [
        ! @"We will now exercise with the execution of various lambda programs."
        ! @"Try to guess what the result of these programs is, and then we shall see what would have happened."
      ]

    VerticalStack
      [
        Question "What is the result of this program execution?"

        LambdaCodeBlock(TextSize.Tiny, (("x" ==> ("y" ==> (!!"x" >>> !!"y")) >>> ("z" ==> (!!"z" >>> !!"z"))) >>> !!"A"))
      ]

    LambdaStateTrace(TextSize.Tiny, (("x" ==> ("y" ==> (!!"x" >>> !!"y")) >>> ("z" ==> (!!"z" >>> !!"z"))) >>> !!"A"), Option.None)

    VerticalStack
      [
        Question "What is the result of this program execution? Watch out for the scope of the two ``x'' variables!"

        LambdaCodeBlock(TextSize.Tiny, ("x" ==> ("x" ==> (!!"x" >>> !!"x")) >>> !!"A") >>> !!"B")
      ]

    LambdaStateTrace(TextSize.Tiny, ("x" ==> ("x" ==> (!!"x" >>> !!"x")) >>> !!"A") >>> !!"B", Option.None)

    VerticalStack
      [
        TextBlock "The first ``x'' gets replaced with ``A'', but the second ``x'' shadows it!"

        LambdaCodeBlock(TextSize.Tiny, ("x" ==> ("x" ==> (!!"x" >>> !!"x")) >>> !!"A") >>> !!"B")
      ]

    VerticalStack
      [
        TextBlock "A better formulation, less ambiguous, would turn:"

        LambdaCodeBlock(TextSize.Tiny, ("x" ==> ("x" ==> (!!"x" >>> !!"x")) >>> !!"A") >>> !!"B")

        TextBlock "...into:"

        LambdaCodeBlock(TextSize.Tiny, ("y" ==> ("x" ==> (!!"x" >>> !!"x")) >>> !!"A") >>> !!"B")
      ]

    LambdaStateTrace(TextSize.Tiny, ("y" ==> ("x" ==> (!!"x" >>> !!"x")) >>> !!"A") >>> !!"B", Option.None)


    VerticalStack
      [
        Question "What is the result of this program execution? Is there even a result?"

        LambdaCodeBlock(TextSize.Tiny, ("x" ==> (!!"x" >>> !!"x")) >>> ("x" ==> (!!"x" >>> !!"x")))
      ]

    LambdaStateTrace(TextSize.Tiny, ("x" ==> (!!"x" >>> !!"x")) >>> ("x" ==> (!!"x" >>> !!"x")), Some 2)

    VerticalStack
      [
        LambdaCodeBlock(TextSize.Tiny, ("x" ==> (!!"x" >>> !!"x")) >>> ("x" ==> (!!"x" >>> !!"x")))

        TextBlock @"It never ends! Like a \texttt{while true: ..}!"
      ]

    SubSection "Crazy teachers tormenting poor students, or ``where are my integers?''"
    VerticalStack
      [
        TextBlock "Ok, I know what you are all thinking: what is this for sick joke? This is no real programming language!"

        ItemsBlock
          [
            ! "We have some sort of functions and function calls"
            ! @"We do not have booleans and \textttt{if}'s"
            ! @"We do not have integers and arithmetic operators"
            ! @"We do not have a lot of things!"
          ]
      ]

    SubSection "Surprise!"
    TextBlock "With nothing but lambda programs we will show how to build all of these features and more."

    SubSection "Stay tuned."
    TextBlock "This will be a marvelous voyage."
  ]

