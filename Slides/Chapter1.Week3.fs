module Chapter1.Week3

open CommonLatex
open SlideDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
    Section("Introduction")
    SubSection("Lecture topics")
    ItemsBlock
      [
        !"Issues with Python"
        !"Issues with Python and possible solutions"
        !"Static typing"
      ]

    Section "Issues with Python"
    SubSection "Lack of..."
    ItemsBlock
      [
        ! "Lack of constraints: how can we specify that a function only takes integers as input"
        ! "Lack of structure: how can we specify that a variable will certainly support some methods"
        ! "Lack of assurances: how can we guarantee that programs with evident errors are not run"
      ]

    SubSection "Broken code examples"
    VerticalStack[
      Question "What is wrong with this?"
      PythonCodeBlock(TextSize.Normal,
          def "f" ["x"] (ret (var "x" .* constInt 2)) >>
          call "f" [constString "nonsense"])
      Pause
      TextBlock "The function clearly works with integers, but is given a string"
      ]

    VerticalStack[
      Question "What is wrong with this?"
      PythonCodeBlock(TextSize.Normal,
         ("x" := (call "input" []) >>
          ifelse (var "x" .> constInt 100) 
                 (call "print" [constString "dumb"])
                 (call "print" [constString "dumber"])))
      Pause
      TextBlock @"The comparison is nonsensical if \texttt{x} is not a number"
      ]

    VerticalStack[
      Question "What is wrong with this?"
      PythonCodeBlock(TextSize.Normal,
         (def "g" ["car"] (ret (methodCall "car" "drive" [constInt 2])) >>
          call "g" [constInt -1]))
      Pause
      TextBlock @"We expect something with a \texttt{drive} method, but get an integer instead"
      ]

    Section "Possible solutions"
    SubSection "Testing?"
    ItemsBlock
      [
        ! "Testing the program should be enough"
        Pause
        ! "Right?"
        Pause
        ! "No. The number of possible execution paths is immense (order of billions), and each test only takes one."
        ! "Testing can only guarantee presence of bugs, but not their absence!"
      ]

    VerticalStack[
      Question @"How many times would we need to test to be sure there is no error?"
      PythonCodeBlock(TextSize.Normal,
         (ifelse (call "randint" [constInt 0; constInt 100000] .> constInt 99999)
                 (call "g" [constInt -1])
                 (call "g" [var "mercedesSL500"])))
      Pause
      TextBlock ">100000"
      ]

    ItemsBlock
      [
        ! "We want our programming languages to perform checks for us"
        ! "Clearly nonsensical programs should be rejected before we can even run them"
        ! @"It is safer and easier to spend more time ""talking"" with the IDE than hoping to find all errors at runtime"
      ]

    Section "Static typing"
    SubSection "Introduction"
    ItemsBlock
      [
        ! @"The language verifies\footnote{By means of the \textbf{compiler}.}, before running code, that all variables are correctly used"
        ! @"""Correctly used"" means that they are guaranteed to support all operations used on them"
        ! "This is by far and large the most typical solution to increase safety and productivity"
      ]

    SubSection "What is static typing?"
    ItemsBlock
      [
        ! "When declaring a variable, we also specify what sort of data it will contain"
        ! @"The \textbf{sort} of data contained is called \textbf{TYPE} of the variable"
        ! "Types can be either primitives (int, string, etc.), custom (classes), or compositions (functions, list of elements of a given type, etc.)"
      ]

    ItemsBlock
      [
        ! "Especially in mainstream languages, the specification of the type of a variable is done by hand by the programmer"
        ! "In other languages (mostly functional languages like F\#, Haskell, etc.) the type of variables is automatically guessed by the compiler"
        ! "In our case our programs will become a bit more verbose but better specified"
        ! "Still, static typing is not necessarily connected with verbosity"
      ]

    VerticalStack[
      PythonCodeBlock(TextSize.Normal,
          (def "f" ["x"] (ret (var "x" .* constInt 2))))
      TextBlock "Becomes, typed:"
      CSharpCodeBlock(TextSize.Normal,
          (typedDef "f" ["int", "x"] "int" (ret (var "x" .* constInt 2)))) |> Unrepeated
      Question @"What has improved and why?"
      Pause
      TextBlock "The second definition encodes information about what goes in and what comes out of the function"
      ]

    VerticalStack[
      Question @"Is this still possible to write (as it was in Python)?"
      CSharpCodeBlock(TextSize.Normal,
         (typedDef "f" ["int", "x"] "int" (ret (var "x" .* constInt 2)) >>
          call "f" [constString "nonsense"])) |> Unrepeated
      Pause
      TextBlock "No: we get a compiler error because a string cannot be used where a number is expected"
      ]

    VerticalStack[
      PythonCodeBlock(TextSize.Normal,
         ("x" := (call "input" []) >>
          ifelse (var "x" .> constInt 100) 
                 (call "print" [constString "dumb"])
                 (call "print" [constString "dumber"])))
      TextBlock "Becomes, typed:"
      CSharpCodeBlock(TextSize.Normal,
         (typedDecl "x" "int" >>
          (("x" := (call "Int32.Parse" [(call "Console.ReadLine" [])])) >>
           ifelse (var "x" .> constInt 100) 
                  (call "Console.WriteLine" [constString "safe"])
                  (call "Console.WriteLine" [constString "safer"])))) |> Unrepeated
      Question @"What has improved and why?"
      Pause
      TextBlock "The variable declaration specifies what is allowed (and what is not) inside the variable."
      ]

    VerticalStack[
      PythonCodeBlock(TextSize.Normal,
         (def "g" ["car"] (ret (methodCall "car" "drive" [constInt 2])) >>
          call "g" [constInt -1]))
      TextBlock "Becomes, typed:"
      CSharpCodeBlock(TextSize.Normal,
         (typedDef "g" ["Car","car"] "void" (ret (methodCall "car" "drive" [constInt 2])) >>
          call "g" [constInt -1])) |> Unrepeated
      Question @"What has improved and why?"
      Pause
      TextBlock "The function declaration specifies available methods of \texttt{car}. We will thus get a compiler error."
      ]

    Section "Typing rules and semantic rules"
    SubSection "How do we describe them?"
    ItemsBlock[
      !"How do we describe such relations clearly?"
      ! @"We use the so-called \textbf{typing rules}, which specify what may be done and what not"
      ! "Typing rules are quite intuitive: they state that if one or more premises are true, then the conclusion is true as well"
    ]

    SubSection "Reading typing rules"
    Advanced(
      VerticalStack[
        TypingRules[
          {
            Premises = ["A"; "B"]
            Conclusion = "C"
          }
        ]
        TextBlock @"If A and B are true, then we can conclude C" 
        ])

    Advanced(
      VerticalStack[
        TypingRules[
          {
            Premises = [@"\text{I wish to buy a pretty car}"; @"\text{I have 120000 euros}"]
            Conclusion = @"\text{I buy a Mercedes SL500}"
          }
        ]
        Question "How do we read this rule?"
        Pause
        TextBlock @"If I have 120000 euros and I wish to buy a pretty car, then I buy a Mercedes SL500" 
        ])

    Advanced(
      VerticalStack[
        TypingRules[
          {
            Premises = [@"\text{I have my umbrella with me}"; @"\text{It is raining}"]
            Conclusion = @"\text{I open my umbrella}"
          }
        ]
        Question "How do we read this rule?"
        Pause
        TextBlock @"If I have my umbrella with me, and it is raining, then I open my umbrella" 
        ])

    TextBlock "Let us apply this machinery to programming languages"
    ItemsBlock 
      [      
        !"Let us apply this machinery to programming languages"
        !"We will effectively give the specification of a modern compiler"
        !"This looks like a ``broadly scoped'' execution of the program, and it is indeed such"
        !"Instead of having a coupling of the variables with values in the stack and the heap, we maintain a coupling of the variables with their declared type"
      ]

    Advanced(
      VerticalStack[
        ItemsBlock[
          ! "We want to specify this in the typing rule notation"
          ! "The typing rules manipulate a stack of declarations"
          ! "x : int"
        ]
        TypingRules[
          {
            Premises = [@"\mathtt{c : Boolean}"; @"\mathtt{a : void}"; @"\mathtt{b : void}"]
            Conclusion = @"\mathtt{if\ c\ then\ a\ else\ b\ :\ void}"
          }
        ]
        TextBlock "If a part of a program does not have a type derived through the typing rules (also void is fine), then the whole program cannot be run and we get a compiler error"
        ])

    // primitive types, and basic declaration


//    TextBlock @"We can use typing rules\footnote{In this case we simply call them \textbf{inference rules}} in a broader scope: also for specifying the semantics of constructs"
//
//    ItemsBlock[
//      !! "if c then a else b"
//      ! "What does this do?"
//      Pause
//      ! "if c evaluates to True, then we evaluate a"
//      ! "if c evaluates to False, then we evaluate b"
//    ]
//
//    Advanced(
//      VerticalStack[
//        ItemsBlock[
//          ! "We want to specify this in the inference rule notation"
//          ! @"Assume that $\rightarrow$ means ""evaluates to"", as in:"
//          ! @"$3+1 \rightarrow 4$"
//        ]
//        Tiny
//        TypingRules[
//          {
//            Premises = [@"\langle c,S,H \rangle \rightarrow \langle True,S',H' \rangle"; @"\langle a,S',H' \rangle \rightarrow \langle res,S'',H'' \rangle"]
//            Conclusion = @"\langle \mathtt{if\ c\ then\ a\ else\ b},S,H \rangle \rightarrow \langle res,S'',H'' \rangle"
//          }
//          {
//            Premises = [@"\langle c,S,H \rangle \rightarrow \langle False,S',H' \rangle"; @"\langle b,S',H' \rangle \rightarrow \langle res,S'',H'' \rangle"]
//            Conclusion = @"\langle \mathtt{if\ c\ then\ a\ else\ b},S,H \rangle \rightarrow \langle res,S'',H'' \rangle"
//          }
//        ]])
  ]

//remove the : notation, and use T v instead

//TODO: Arrays as primitive data types
//TODO: \textbf{Advanced} lambda's
//TODO: methodCall should take arbitrary expression, not variable names
