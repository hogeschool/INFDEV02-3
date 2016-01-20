module Week1

open CommonLatex
open SlideDefinition
open CodeDefinition

let slides = 
  [
//    PythonStateTrace(TextSize.Small,
//      (def "f" ["x"] 
//        (ifelse (var "x" .> constInt 0) 
//          (ret ((call "f" [constInt -20]) .+ constInt 1))
//          (ret (var "x" .* constInt 2))) >>
//       call "f" [constInt 20] >>
//       endProgram),
//      { Stack = [["PC", constInt 1] |> Map.ofList]; Heap = Map.empty; InputStream = [] }
//    )

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
          (typedDef "f" ["int", "x"] "int" (ret (var "x" .* constInt 2))))
      Question @"What has improved and why?"
      Pause
      TextBlock "The second definition encodes information about what goes in and what comes out of the function"
      ]

    VerticalStack[
      Question @"Is this possible now?"
      CSharpCodeBlock(TextSize.Normal,
         (typedDef "f" ["int", "x"] "int" (ret (var "x" .* constInt 2)) >>
          call "f" [constString "nonsense"]))
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
                  (call "Console.WriteLine" [constString "safer"]))))
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
          call "g" [constInt -1]))
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

    Section "Statically typed, object-oriented, modern programming language"
    SubSection "Introduction and motivation"
    ItemsBlock
      [
        ! @"We will use Java and C\#"
        ! @"They are extremely similar in philosophy, syntax, type system, and semantics"
        ! @"Each one apart is somewhat limited"
        ! @"Together they cover a huge chunk of theory and practical applications"
      ]

    SubSection "Pros"
    VerticalStack[
      TextBlock "Java"
      Items
        [
          ! @"Hugely used in businesses"
          ! @"Immense ecosystem of tools and libraries"
          ! @"Great support on most platforms"
        ]
      TextBlock @"C\#"
      Items
        [
          ! @"Dominant in semi-high performance applications (games, simulations)"
          ! @"Extremely clean and careful design of libraries and advanced language constructs"
          ! @"Good support on most platform"
        ]
      ]

    SubSection "Cons"
    VerticalStack[
      TextBlock "Java"
      Items
        [
          ! @"Very slow to evolve"
          ! @"Less clean design with lots of historical corner cases"
          ! @"Too large a community means dozens of competing libraries for most common tasks"
        ]
      TextBlock @"C\#"
      Items
        [
          ! @"Less adopted outside the Microsoft world, though Mono and .Net Core are helping"
          ! @"Historical bad perception of the whole company polluted language reputation"
          ! @"No immense collection of competing libraries and build systems"
        ]
      ]

    SubSection "Practicum and assignments"
    Items
      [
        ! @"Just choose whatever you like the most"
        ! @"Both languages and all supported libraries are accepted"
        ! @"Moreover, the differences between the two are minimal: learn one, but be aware that you are also learning the other"
        ! @"We will point the differences out whenever needed"
      ]

    Section @"From Python to Java/C\#"
    SubSection "Where does the program go?"
    ItemsBlock
      [
        ! @"In Python you can just begin writing code anywhere in a file"
        ! @"This will not be true anymore in Java/C\#"
        ! @"Separate snippets of code cannot be just pasted in an empty file and tried out"
      ]

    VerticalStack[
      Small

      ItemsBlock[ 
        ! @"Most basic Python constructs translate almost directly to JC\#"
        ! @"Lines and instructions always end with a semicolon (;)"
        ! @"Variables are always declared before use, specifying their type."
      ]

      PythonCodeBlock(TextSize.Small,
          ("x" := (constInt 10 .+ constInt 20)))

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Small,
          (typedDecl "x" "int" >>
           ("x" := (constInt 10 .+ constInt 20))))

      TextBlock @"or, alternatively:"

      CSharpCodeBlock(TextSize.Small,
          (typedDeclAndInit "x" "int" (constInt 10 .+ constInt 20)))
    ]

    VerticalStack[
      TextBlock @"JC\# support similar sets of primitive data types"

      ItemsBlock[ 
        ! @"integers in various sizes: \texttt{byte}, \texttt{short}, \texttt{int}, \texttt{long}, and many others"
        ! @"floats in various sizes: \texttt{float} and \texttt{double}"
        ! @"strings: \texttt{string}"
      ]

      TextBlock @"These types are richer than Python, because we can specify their size, and thus precision, instead of the one-size-fits-all solution of Python"
    ]

    VerticalStack[
      TextBlock @"Each primitive data type has a different range and uses more or less memory"

      ItemsBlock[ 
        ! @"\texttt{byte} is 1 byte, and it goes from -128 to 127"
        ! @"\texttt{short} is 2 bytes, and it goes from -32,768 to 32,767"
        ! @"\texttt{int} is 4 bytes, and it goes from $-2^{31}$ to $2^{31}-1$"
        ! @"\texttt{float} is 4 bytes, and it has a very wide range \textbf{with non-uniform steps between adjacent values!}$"
        ! "..."
      ]

      TextBlock @"Some bugs may depend on attempts to write beyond the range or at a higher precision than supported by the type."
    ]

    VerticalStack[
      Small

      ItemsBlock[ 
        ! @"Python operators translate almost directly to JC\#"
        ! @"Only exception are the logical operators"
        ! @"\texttt{not} becomes (!), \texttt{or} becomes ($\|$), and \texttt{and} becomes (\&\&)"
      ]

      PythonCodeBlock(TextSize.Small,
          ("b" := ((constInt 10 .+ constInt 20) ./ constInt 2) .> constInt 5))

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Small,
          (typedDeclAndInit "b" "bool" ((constInt 10 .+ constInt 20) ./ constInt 2) .> constInt 5))
    ]

    VerticalStack[
      Small

      ItemsBlock[ 
        ! @"Python function calls translate directly to JC\#"
        ! @"Only difference is, again, the semicolon"
        ! @"Behaviour remains precisely the same"
      ]

      PythonCodeBlock(TextSize.Tiny,
        (call "print" [call "int" [(call "input" [])]]))

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Tiny,
        (call "print" [call "Int32.Parse" [(call "Console.ReadLine" [])]]))
    ]

    ItemsBlock
      [
        ! @"Java and C\#\footnote{From now on JC\#} are curly-bracket languages"
        ! @"This means that any block of code must now appear between curly brackets \{ and \}"
        ! @"There are no more colons (:) to delimit declarations"
        ! @"Indentation remains important for the reader\footnote{And the student aiming for a passing grade!}, but the languages do not care"
        ! @"Programs in JC\# tend to be longer in part because of this"
      ]

    VerticalStack[
      Small

      ItemsBlock[ 
        ! @"Python statements translate almost directly to JC\#"
        ! @"Only difference are the brackets and the lack of semicolon"
        ! @"Behaviour remains precisely the same"
      ]

      PythonCodeBlock(TextSize.Tiny,
        ("x" := (call "int" [(call "input" [])])) >>
        (ifelse (var "x" .> constInt 0) 
          ((call "print" [constString "greater"]))
          ((call "print" [constString "smaller"]))))

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Tiny,
        (typedDeclAndInit "x" "int" (call "Int32.Parse" [(call "Console.ReadLine" [])])) >>
        (ifelse (var "x" .> constInt 0) 
          (call "Console.WriteLine" [constString "greater"])
          (call "Console.WriteLine" [constString "smaller"])))
    ]

    VerticalStack[
      Small

      PythonCodeBlock(TextSize.Tiny,
        ("x" := (call "int" [(call "input" [])])) >>
         (("cnt" := constInt 0) >>
          (whiledo (var "x" .> constInt 0) 
            (("cnt" := (var "cnt" .+ constInt 1)) >>
             ("x" := (var "x" ./ constInt 2))))))

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Tiny,
        (typedDeclAndInit "x" "int" (call "Int32.Parse" [(call "Console.ReadLine" [])])) >>
         ((typedDeclAndInit "cnt" "int" (constInt 0)) >>
          (whiledo (var "x" .> constInt 0) 
            (("cnt" := (var "cnt" .+ constInt 1)) >>
             ("x" := (var "x" ./ constInt 2))))))
    ]

    ItemsBlock
      [
        ! @"JC\# are object-oriented languages"
        ! @"This means that (almost) everything is an \textbf{object}, that is an instance of a \textbf{class}"
        ! @"All JC\# programs will therefore begin with a class definition"
      ]

    VerticalStack[
      TextBlock @"A class in JC\# looks very much like a Python class, where init is a method with the name of the class itself and fields must be declared, like variables, within the body of the class."

      PythonCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              def "__init__" ["self"] ("self.cnt" := constInt 0)
            ])

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              typedDecl "cnt" "int"
              typedDef "Counter" [] "" ("cnt" := constInt 0)
            ])
    ]

    VerticalStack[
      TextBlock @"If we want to add methods, we also need to be aware of the type of each of their parameter and of the type they return."

      PythonCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              def "__init__" ["self"] ("self.cnt" := constInt 0)
              def "incr" ["self"; "diff"] ("self.cnt" := (var "self.cnt" .+ var "diff"))
            ])

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              typedDecl "cnt" "int"
              typedDef "Counter" [] "" ("cnt" := constInt 0)
              typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff"))
            ])
    ]

//Statically typed, object-oriented programming languages
//Arrays as primitive data types
//Instancing classes, calling methods, and calling static methods
//Static methods (main)
//\textbf{Advanced} lambda's
//Add Java examples as well, with keywords for specific translation later instead of strings for the methods
//The Java examples appear after C# in a new slide, right beneath the C# example
//
//\SlideSection{Conclusion}
//\SlideSubSection{Lecture topics}
//\begin{slide}{
//\item What problem did we solve today, and how?
//}\end{slide}
  ]
