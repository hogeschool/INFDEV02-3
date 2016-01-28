module Week1

open CommonLatex
open SlideDefinition
open CodeDefinitionImperative
open Interpreter

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

    Section "Modern, object-oriented programming languages"
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
      ItemsBlock
        [
          ! @"Hugely used in businesses"
          ! @"Immense ecosystem of tools and libraries"
          ! @"Great support on most platforms"
        ]
      TextBlock @"C\#"
      ItemsBlock
        [
          ! @"Dominant in semi-high performance applications (games, simulations)"
          ! @"Extremely clean and careful design of libraries and advanced language constructs"
          ! @"Good support on most platform"
        ]
      ]

    SubSection "Cons"
    VerticalStack[
      TextBlock "Java"
      ItemsBlock
        [
          ! @"Very slow to evolve"
          ! @"Less clean design with lots of historical corner cases"
          ! @"Too large a community means dozens of competing libraries for most common tasks"
        ]
      TextBlock @"C\#"
      ItemsBlock
        [
          ! @"Less adopted outside the Microsoft world, though Mono and .Net Core are helping"
          ! @"Historical bad perception of the whole company polluted language reputation"
          ! @"No immense collection of competing libraries and build systems"
        ]
      ]

    SubSection "Practicum and assignments"
    ItemsBlock
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

    TextBlock @"All snippets of Java and C\# that we will see now cannot (until we see the \texttt{Main}) just be pasted in an empty file and run like we did for Python!!!"

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
        (call "Console.WriteLine" [call "Int32.Parse" [(call "Console.ReadLine" [])]]))
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
      TextBlock @"A class in JC\# looks very much like a Python class, with some minor differences:"
      ItemsBlock
        [
          ! @"\texttt{\_\_init\_\_} is a method with the name of the class itself"
          ! @"all fields must be declared, like variables, within the body of the class"
          ! @"\texttt{self} is now called \texttt{this}"
        ]
      ]

    VerticalStack[
      PythonCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              def "__init__" ["self"] ("self.cnt" := constInt 0)
            ])

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" ("this.cnt" := constInt 0) |> makePublic
            ])
    ]

    ItemsBlock 
      [ 
        ! @"We can limit visibility of attributes (and methods) in a class in JC\#;" 
        ! @"This means we can prevent a user of a class from accidentally using something in the wrong way" 
        ! @"Most important attributes are" 
        Items
          [ 
            ! @"\texttt{public}, means every part of the program can access it" 
            ! @"\texttt{private}, means it can only be accessed from inside the class" 
          ]
        ! @"We assume for the moment that the constructor will always be \texttt{public}" 
      ]

    VerticalStack[
      TextBlock @"Assuming \texttt{x} being an instance of \texttt{C}, this would be an invalid program:"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "C" 
              [
                typedDecl "a" "int" |> makePrivate
                typedDecl "b" "int" |> makePublic
                typedDef "C" [] "" (("a" := constInt 0) >> ("b" := constInt 0)) |> makePublic
              ])) >>
            (dots >>
             call "Console.WriteLine" [var "x.a"]))

      Question @"In what sense \textit{invalid}?"

      Pause

      TextBlock @"The \textbf{compiler} will literally refuse to run the program by saying that \texttt{a} is private, and thus may not be accessed."
    ]

    VerticalStack[
      TextBlock @"Assuming \texttt{x} being an instance of \texttt{C}, this would be a valid program, just like in Python:"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "C" 
              [
                typedDecl "a" "int" |> makePrivate
                typedDecl "b" "int" |> makePublic
                typedDef "C" [] "" (("a" := constInt 0) >> ("b" := constInt 0)) |> makePublic
              ])) >>
            (dots >>
             call "Console.WriteLine" [var "x.b"]))

      TextBlock @"This suggests that Python is like Java/C\# where all class attributes are automatically declared as \texttt{public}."
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
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" ("cnt" := constInt 0) |> makePublic
              typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
            ])
    ]

    ItemsBlock
      [
        ! @"Methods can, just like attributes, either \texttt{private} or \texttt{public}"
        ! @"\texttt{public} methods can be called from anywhere"
        ! @"\texttt{private} methods may only be called from inside the class itself"      
      ]

    VerticalStack[
      TextBlock @"Now that we have a class, we can instantiate it and call its methods."

      PythonCodeBlock(TextSize.Tiny,
          (classDef "Counter" 
            [
              def "__init__" ["self"] ("self.cnt" := constInt 0)
              def "incr" ["self"; "diff"] ("self.cnt" := (var "self.cnt" .+ var "diff"))
            ]) >>
          ((("c" := newC "Counter" []) >>
            (methodCall "c" "incr" [ConstInt 5]))))

      TextBlock @"The above Python becomes, in both JC\#:"

      CSharpCodeBlock(TextSize.Tiny,
          (classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" ("cnt" := constInt 0) |> makePublic
              typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
            ]) >>
          (((typedDeclAndInit "c" "Counter" (newC "Counter" [])) >>
            (methodCall "c" "Incr" [ConstInt 5]))))
    ]

    VerticalStack[
      TextBlock @"Method access determines where they can be called. Suppose \texttt{x} is of type \texttt{C}:"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "C" 
              [
                typedDecl "a" "int" |> makePrivate
                typedDecl "b" "int" |> makePublic
                typedDef "C" [] "" (("this.a" := constInt 0) >> ("this.b" := constInt 0)) |> makePublic
                typedDef "IncrA" ["int","diff"] "void" ("this.a" := (var "this.a" .+ var "diff")) |> makePublic
                typedDef "IncrB" ["int","diff"] "void" ("this.b" := (var "this.b" .+ var "diff")) |> makePrivate
              ])) >>
            (dots >>
             methodCall "x" "IncrA" [ConstInt 10]))

      Question @"Will this program be allowed to run?"
      Pause
      TextBlock @"Yes, because \texttt{IncrA} is a public method."
    ]

    VerticalStack[
      TextBlock @"Method access determines where they can be called. Suppose \texttt{x} is of type \texttt{C}:"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "C" 
              [
                typedDecl "a" "int" |> makePrivate
                typedDecl "b" "int" |> makePublic
                typedDef "C" [] "" (("this.a" := constInt 0) >> ("this.b" := constInt 0)) |> makePublic
                typedDef "IncrA" ["int","diff"] "void" ("this.a" := (var "this.a" .+ var "diff")) |> makePublic
                typedDef "IncrB" ["int","diff"] "void" ("this.b" := (var "this.b" .+ var "diff")) |> makePrivate
              ])) >>
            (dots >>
             methodCall "x" "IncrB" [ConstInt 10]))

      Question @"Will this program be allowed to run?"
      Pause
      TextBlock @"No, because \texttt{IncrB} is a private method."
    ]

    TextBlock @"Surprisingly, both Java and C\# miss simple functions like those of Python: this means that they need to be emulated as methods."

    VerticalStack[
      TextBlock @"Simple Python functions become \textit{static methods} in both Java and C\#."

      PythonCodeBlock(TextSize.Tiny,
          def "f" ["x"]
            (ret (var "x" .+ ConstInt(10))))

      TextBlock @"The above Python needs to be put inside a class and be marked as \texttt{static}, in both Java and C\#:"

      CSharpCodeBlock(TextSize.Tiny,
          classDef "MyClass" 
            [
              typedDef "f" ["int","x"] "int" ((ret (var "x" .+ ConstInt(10)))) |> makePublic |> makeStatic
            ])
    ]

    VerticalStack[
      TextBlock @"\textit{static methods} are called without an instance of the class left of the dot, but rather with the name of the class they are declared in"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "MyClass" 
              [
                typedDef "f" ["int","x"] "int" ((ret (var "x" .+ ConstInt(10)))) |> makePublic |> makeStatic
              ])) >>
           (dots >>
            staticMethodCall "MyClass" "f" [ConstInt(10)]))
    ]

    ItemsBlock 
      [
        ! @"Java and C\# programs do not just begin at the top of a file."
        ! @"The program is a class with a special static method, called \texttt{main}."
        ! @"The arguments to this method are an array of strings, the command line parameters\footnote{Just ignore, it is mostly not used.}."
      ]

    VerticalStack[
      TextBlock @"Here is our first actual Java/C\# program of the day!"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "Program" 
              [
                typedDef "Main" ["String[]","args"] "void" (staticMethodCall "Console" "WriteLine" [ConstString "Hello world!"]) |> makePublic |> makeStatic
              ])))
    ]


//Arrays as primitive data types
//\textbf{Advanced} lambda's
//Multiple classes and files in Java
//Add Java examples as well, with keywords for specific translation later instead of strings for the methods (such as read, parse, and print)
//The Java examples appear after C# in a new slide, right beneath the C# example
//Each C# example (but not the duplicated Java version) has its own stack and heap
//
//\SlideSection{Conclusion}
//\SlideSubSection{Lecture topics}
//\begin{slide}{
//\item What problem did we solve today, and how?
//}\end{slide}
  ]
