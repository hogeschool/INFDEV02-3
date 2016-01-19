module Week1

open SlideDefinition
open CodeDefinition

let slides = 
  [
    Section("Introduction")
    SubSection("Lecture topics")
    ItemsBlock
      [
        !"Intro to DEV3"
        !"What have we learned so far?"
        !"What are types?"
        !(@"(\textbf{Advanced}) Typing and semantic rules: how do we read them?")
        !(@"Introduction to Java and C\# (\textbf{advanced}) with type rules and semantics")
      ]
    
    Section("Introduction to DEV3")
    SubSection("Exam")
    ItemsBlock
      [
        !"written exam"
        !"4 open questions"
        !"code, type system, and semantics"
        !(@"no grade: go (score$\ge$75) or no go (otherwise)")
      ]

    SubSection("Homework")
    ItemsBlock
      [
        !"homework to prepare step-by-step"
        !"builds up to actual practicum"
        !"there is no grade for this"
      ]

    SubSection("Practicum assignments")
    ItemsBlock
      [
        !"a connected series of programming tasks"
        !"build a simulation similar to that of DEV2"
        !"use the additional structure and help offered by static typing and object orientation"
        !(@"\textbf{mandatory}, but with no direct grade")
      ]

    SubSection("Oral")
    ItemsBlock
      [
        !"the oral is entirely based on the practicum assignments"
        !"we remove some pieces of code from the working solutions and you fill them back in"
        !"the oral gives you the final grade for the course"
      ]

    SubSection("Expected study effort")
    ItemsBlock
      [
        !(@"between 10 and 20 \textbf{net}\footnote{No, 9gag does not count even if the slides are open on another monitor} hours a week")
        !"read every term on the slides and every sample"
        !"if you do not understand it perfectly, either ask a teacher, google, or brainstorm with other students"
        !(@"every sample of code on the slides you should both \textbf{understand} and \textbf{try out} on your machine")
      ]

    SubSection("Cooperation between SLC's and DEV")
    ItemsBlock
      [
        !"we will aim towards a better comunication structure with SLC's"
        !"we will use the homework as a measure of your study and effort"
        !"we are more than willing to help you in any way we can, but without your work we cannot do anything\pause"
        !"please learn to program, it is seriously awesome :)"
      ]

    Section @"What have we learnt so far?"
    SubSection "Python in a nutshell"
    ItemsBlock
      [
        ! @"How do \textbf{all} programming languages work underneath: PC, stack, and heap"
        ! "Basic code constructs: variables, conditionals, loops, primitive data types"
        ! "Customizable abstractions: functions, recursive functions, classes, methods"
      ]

    SubSection "Issues with Python"
    ItemsBlock
      [
        ! "Lack of constraints: how can we specify that a function only takes integers as input"
        ! "Lack of structure: how can we specify that a variable will certainly support some methods"
        ! "Lack of assurances: how can we guarantee that programs with evident errors are not run"
      ]

    SubSection "Broken code examples"
    VerticalStack[
      Question "What is wrong with this?"
      PythonCodeBlock(
          def "f" ["x"] (ret (var "x" .* constInt 2)) >>
          call "f" [constString "nonsense"])
      Pause
      TextBlock "The function clearly works with integers, but is given a string"
      ]

    VerticalStack[
      Question "What is wrong with this?"
      PythonCodeBlock(
          "x" := (call "input" []) >>
          ifelse (var "x" .> constInt 100) 
                 (call "print" [constString "dumb"])
                 (call "print" [constString "dumber"]))
      Pause
      TextBlock @"The comparison is nonsensical if \texttt{x} is not a number"
      ]

    VerticalStack[
      Question "What is wrong with this?"
      PythonCodeBlock(
          def "g" ["car"] (ret (methodCall "car" "drive" [constInt 2])) >>
          call "g" [constInt -1])
      Pause
      TextBlock @"We expect something with a \texttt{drive} method, but get an integer instead"
      ]

    SubSection "Possible solutions"
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
      PythonCodeBlock(
          ifelse (call "randint" [constInt 0; constInt 100000] .> constInt 99999)
                 (call "g" [constInt -1])
                 (call "g" [var "mercedesSL500"]))
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
      PythonCodeBlock(
          def "f" ["x"] (ret (var "x" .* constInt 2)))
      TextBlock "Becomes, typed:"
      CSharpCodeBlock(
          typedDef "f" ["int", "x"] "int" (ret (var "x" .* constInt 2)))
      Question @"What has improved and why?"
      Pause
      TextBlock "The second definition encodes information about what goes in and what comes out of the function"
      ]

    VerticalStack[
      Question @"Is this possible now?"
      CSharpCodeBlock(
          typedDef "f" ["int", "x"] "int" (ret (var "x" .* constInt 2)) >>
          call "f" [constString "nonsense"])
      Pause
      TextBlock "No: we get a compiler error because a string cannot be used where a number is expected"
      ]

    VerticalStack[
      PythonCodeBlock(
          "x" := (call "input" []) >>
          ifelse (var "x" .> constInt 100) 
                 (call "print" [constString "dumb"])
                 (call "print" [constString "dumber"]))
      TextBlock "Becomes, typed:"
      CSharpCodeBlock(
          typedDecl "x" "int" >>
          (("x" := (call "Int32.Parse" [(call "Console.ReadLine" [])])) >>
           ifelse (var "x" .> constInt 100) 
                  (call "Console.WriteLine" [constString "safe"])
                  (call "Console.WriteLine" [constString "safer"])))
      Question @"What has improved and why?"
      Pause
      TextBlock "The variable declaration specifies what is allowed (and what is not) inside the variable."
      ]

    VerticalStack[
      PythonCodeBlock(
          def "g" ["car"] (ret (methodCall "car" "drive" [constInt 2])) >>
          call "g" [constInt -1])
      TextBlock "Becomes, typed:"
      CSharpCodeBlock(
          typedDef "g" ["Car","car"] "void" (ret (methodCall "car" "drive" [constInt 2])) >>
          call "g" [constInt -1])
      Question @"What has improved and why?"
      Pause
      TextBlock "The function declaration specifies available methods of \texttt{car}. We will thus get a compiler error."
      ]

    SubSection "Typing rules and semantic rules"
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

    ItemsBlock[
      ! @"Let us apply this concept to programming languages"
      ! @"We do this independently from the specific programming language"
      ! "We focus on conditionals"
    ]

    ItemsBlock[
      !! "if c then a else b"
      ! "When does this make sense?"
      Pause
      ! "c needs to be a boolean expression"
      ! "a is a code block (evaluates to None)"
      ! "b is a code block (evaluates to None)"
    ]

    Advanced(
      VerticalStack[
        ItemsBlock[
          ! "We want to specify this in the typing rule notation"
          ! @"Assume that "":"" means ""has type"", as in:"
          !! "x : int"
          ! @"Assume that \texttt{void} is the type without values"
        ]
        TypingRules[
          {
            Premises = [@"\mathtt{c : Boolean}"; @"\mathtt{a : void}"; @"\mathtt{b : void}"]
            Conclusion = @"\mathtt{if\ c\ then\ a\ else\ b\ :\ void}"
          }
        ]
        TextBlock "If a part of a program does not have a type derived through the typing rules (also void is fine), then the whole program cannot be run and we get a compiler error"
        ])

    TextBlock @"We can use typing rules\footnote{In this case we simply call them \textbf{inference rules}} in a broader scope: also for specifying the semantics of constructs"

    ItemsBlock[
      !! "if c then a else b"
      ! "What does this do?"
      Pause
      ! "if c evaluates to True, then we evaluate a"
      ! "if c evaluates to False, then we evaluate b"
    ]

    Advanced(
      VerticalStack[
        ItemsBlock[
          ! "We want to specify this in the inference rule notation"
          ! @"Assume that $\rightarrow$ means ""evaluates to"", as in:"
          ! @"$3+1 \rightarrow 4$"
        ]
        Tiny
        TypingRules[
          {
            Premises = [@"\langle c,S,H \rangle \rightarrow \langle True,S',H' \rangle"; @"\langle a,S',H' \rangle \rightarrow \langle res,S'',H'' \rangle"]
            Conclusion = @"\langle \mathtt{if\ c\ then\ a\ else\ b},S,H \rangle \rightarrow \langle res,S'',H'' \rangle"
          }
          {
            Premises = [@"\langle c,S,H \rangle \rightarrow \langle False,S',H' \rangle"; @"\langle b,S',H' \rangle \rightarrow \langle res,S'',H'' \rangle"]
            Conclusion = @"\langle \mathtt{if\ c\ then\ a\ else\ b},S,H \rangle \rightarrow \langle res,S'',H'' \rangle"
          }
        ]])

    PythonStateTrace(
      (def "f" ["x"] (ret (var "x" .* constInt 2)) >>
       call "f" [constInt 20] >>
       endProgram),
      { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = [] }
    )

//Statically typed, object-oriented programming languages
//We will use Java and C\#
//Java: some pros and cons
//C\#: some pros and cons
//Assignment can be done in either of the languages
//Why both? Extremely used, together they cover a lot of possible very different applications on the most popular platforms.
//
//
//From Python to Java/C\#
//Always put semantics and typing information when needed
//Classes
//Static methods (main)
//Variables, statements, function calls, primitive data types (also arrays)
//Fields
//Methods
//Constructors
//\textbf{Advanced} lambda's
//
//
//\SlideSection{Conclusion}
//\SlideSubSection{Lecture topics}
//\begin{slide}{
//\item What problem did we solve today, and how?
//}\end{slide}
  ]
