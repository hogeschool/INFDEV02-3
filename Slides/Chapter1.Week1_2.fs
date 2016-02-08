module Chapter1.Week1_2

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
        !"Intro to DEV3"
        !"What have we learned so far?"
        !"Basic notions of types and declarations"
        !(@"Introduction to Java and C\# with execution examples")
      ]
    
    Section("Introduction to DEV3")
    SubSection("Take pride in what you do")
    ItemsBlock
      [
        !"The hardest part is over"
        !"You have now really begun with learning to program"
        !"We are proud of you and your results so far"
        !"Remember to enjoy how much you are learning"
      ]

    SubSection("Exam")
    ItemsBlock
      [
        !"written exam"
        !"4 open questions"
        !"code, type system, and semantics"
        !(@"no grade: go (score$\ge$75) or no go (otherwise)")
      ]

    SubSection("Exercises")
    ItemsBlock
      [
        !"exercises to prepare step-by-step"
        !"builds up to actual practicum"
        !"there is no grade for this"
      ]

    SubSection("Assignments")
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
        !"the oral is entirely based on the assignments"
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
          ! @"Dominantly used in businesses"
          ! @"Extremely Immense ecosystem of tools and libraries"
          ! @"Great support on most platforms"
          ! @"A large community means dozens of libraries for most common tasks"
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
          ! @"Slow to evolve, because of input from developers"
          ! @"Less clean design with lots of historical corner cases"
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

    SubSection "Basic differences"
    VerticalStack[
      Small

      ItemsBlock[ 
        ! @"Most basic Python constructs translate almost directly to Java/C\#"
        ! @"Lines and instructions always end with a semicolon (;)"
        ! @"Variables are always declared before use, specifying their type."
      ]

      PythonCodeBlock(TextSize.Small,
          ("x" := (constInt 10 .+ constInt 20)))

      TextBlock @"The above Python becomes, in C\#:"

      CSharpCodeBlock(TextSize.Small,
          (typedDecl "x" "int" >>
           ("x" := (constInt 10 .+ constInt 20))))

      TextBlock @"or, alternatively:"

      CSharpCodeBlock(TextSize.Small,
          (typedDeclAndInit "x" "int" (constInt 10 .+ constInt 20)))
    ]

    TextBlock @"This snippet (remember: we cannot just copy and paste it) produces the same execution in both Python and Java/C\#!"

    CSharpStateTrace(TextSize.Tiny,
        (typedDeclAndInit "x" "int" (constInt 10 .+ constInt 20) >> endProgram),
        RuntimeState<_>.Zero (constInt 1))


    SubSection "Primitive data types"
    VerticalStack[
      TextBlock @"Java/C\# support similar sets of primitive data types"

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

    SubSection "Operators and expressions"
    VerticalStack[
      Small

      ItemsBlock[ 
        ! @"Python operators translate almost directly to Java/C\#"
        ! @"Only exception are the logical operators"
        ! @"\texttt{not} becomes (!), \texttt{or} becomes ($\|\|$), and \texttt{and} becomes (\&\&)"
      ]

      PythonCodeBlock(TextSize.Small,
          ("b" := ((constInt 10 .+ constInt 20) ./ constInt 2) .> constInt 5))

      TextBlock @"The above Python becomes, in C\#:"

      CSharpCodeBlock(TextSize.Small,
          ((typedDeclAndInit "b" "bool" (((constInt 10 .+ constInt 20) ./ constInt 2) .> constInt 5)) >> endProgram))
    ]

    TextBlock @"This snippet (remember: we cannot just copy and paste it) produces the same execution in both Python and Java/C\#!"

    CSharpStateTrace(TextSize.Tiny,
        ((typedDeclAndInit "b" "bool" (((constInt 10 .+ constInt 20) ./ constInt 2) .> constInt 5)) >> endProgram),
        RuntimeState<_>.Zero (constInt 1))

    SubSection "Function calls"
    VerticalStack[
      Small

      ItemsBlock[ 
        ! @"Python function calls translate directly to Java/C\#"
        ! @"Only difference is, again, the semicolon"
        ! @"Behaviour remains precisely the same"
      ]

      PythonCodeBlock(TextSize.Tiny,
        (call "print" [call "int" [(call "input" [])]]))

      TextBlock @"The above Python becomes, in C\#:"

      CSharpCodeBlock(TextSize.Tiny,
        (staticMethodCall "Console" "WriteLine" [staticMethodCall "Int32" "Parse" [(staticMethodCall "Console" "ReadLine" [])]]))
    ]

    TextBlock @"This snippet (remember: we cannot just copy and paste it) produces the same execution in both Python and Java/C\#!"

    CSharpStateTrace(TextSize.Tiny,
        ((staticMethodCall "Console" "WriteLine" [staticMethodCall "Int32" "Parse" [(staticMethodCall "Console" "ReadLine" [])]]) >> endProgram),
        RuntimeState<_>.WithInput (constInt 1) ["100"])

    SubSection "Control flow statements"
    ItemsBlock
      [
        ! @"Java and C\# are curly-bracket languages"
        ! @"This means that any block of code must now appear between curly brackets \{ and \}"
        ! @"There are no more colons (:) to delimit declarations"
        ! @"Indentation remains important for the reader\footnote{And the student aiming for a passing grade!}, but the languages do not care"
        ! @"Programs in Java/C\# tend to be longer in part because of this"
      ]

    VerticalStack[
      Small

      ItemsBlock[ 
        ! @"Python statements translate almost directly to Java/C\#"
        ! @"Only difference are the brackets and the lack of semicolon"
        ! @"Behaviour remains precisely the same"
      ]

      PythonCodeBlock(TextSize.Tiny,
        ("x" := (call "int" [(call "input" [])])) >>
        (ifelse (var "x" .> constInt 0) 
          ((call "print" [constString "greater"]))
          ((call "print" [constString "smaller or equal"]))))

      TextBlock @"The above Python becomes, in C\#:"

      CSharpCodeBlock(TextSize.Tiny,
        (typedDeclAndInit "x" "int" (staticMethodCall "Int32" "Parse" [(staticMethodCall "Console" "ReadLine" [])])) >>
        (ifelse (var "x" .> constInt 0) 
          (staticMethodCall "Console" "WriteLine" [constString "greater"])
          (staticMethodCall "Console" "WriteLine" [constString "smaller or equal"])))
    ]

    TextBlock @"This snippet (remember: we cannot just copy and paste it) produces the same execution in both Python and Java/C\#!"

    CSharpStateTrace(TextSize.Tiny,
        ((typedDeclAndInit "x" "int" (staticMethodCall "Int32" "Parse" [(staticMethodCall "Console" "ReadLine" [])])) >>
         ((ifelse (var "x" .> constInt 0) 
             (staticMethodCall "Console" "WriteLine" [constString "greater"])
             (staticMethodCall "Console" "WriteLine" [constString "smaller or equal"])))),
        RuntimeState<_>.WithInput (constInt 1) ["100"])

    VerticalStack[
      Small

      PythonCodeBlock(TextSize.Tiny,
        ("x" := (call "int" [(call "input" [])])) >>
         (("cnt" := constInt 0) >>
          (whiledo (var "x" .> constInt 0) 
            (("cnt" := (var "cnt" .+ constInt 1)) >>
             ("x" := (var "x" ./ constInt 2))))))

      TextBlock @"The above Python becomes, in C\#:"

      CSharpCodeBlock(TextSize.Tiny,
        (typedDeclAndInit "x" "int" (staticMethodCall "Int32" "Parse" [(staticMethodCall "Console" "ReadLine" [])])) >>
         ((typedDeclAndInit "cnt" "int" (constInt 0)) >>
          (whiledo (var "x" .> constInt 0) 
            (("cnt" := (var "cnt" .+ constInt 1)) >>
             ("x" := (var "x" ./ constInt 2))))))
    ]

    TextBlock @"This snippet (remember: we cannot just copy and paste it) produces the same execution in both Python and Java/C\#!"

    CSharpStateTrace(TextSize.Tiny,
        ((((typedDeclAndInit "x" "int" (staticMethodCall "Int32" "Parse" [(staticMethodCall "Console" "ReadLine" [])])) >>
           ((typedDeclAndInit "cnt" "int" (constInt 0)) >>
            (whiledo (var "x" .> constInt 1) 
              (("cnt" := (var "cnt" .+ constInt 1)) >>
               ("x" := (var "x" ./ constInt 2)))))) >> 
            staticMethodCall "Console" "WriteLine" [(constString "Result is ") .+ (toString (var "cnt"))]) >> endProgram),
        RuntimeState<_>.WithInput (constInt 1) ["32"])

    SubSection "Part I"
    TextBlock @"End of part I"

    SubSection "Part II"
    TextBlock @"Beginning of part II"

    SubSection "Classes"
    ItemsBlock
      [
        ! @"Java/C\# are object-oriented languages"
        ! @"This means that (almost) everything is an \textbf{object}, that is an instance of a \textbf{class}"
        ! @"All Java/C\# programs will therefore begin with a class definition"
      ]

    VerticalStack[
      TextBlock @"A class in Java/C\# looks very much like a Python class, with some minor differences:"
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

      TextBlock @"The above Python becomes, in C\#:"

      CSharpCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" ("this.cnt" := constInt 0) |> makePublic
            ])
    ]

    SubSection "Visibility"
    ItemsBlock 
      [ 
        ! @"We can limit visibility of attributes (and methods) in a class in Java/C\#;" 
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
             staticMethodCall "Console" "WriteLine" [var "x.a"]))

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
             staticMethodCall "Console" "WriteLine" [var "x.b"]))

      TextBlock @"This suggests that Python is like Java/C\# where all class attributes are automatically declared as \texttt{public}."
    ]

    SubSection "Methods"
    VerticalStack[
      TextBlock @"If we want to add methods, we also need to be aware of the type of each of their parameter and of the type they return."

      PythonCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              def "__init__" ["self"] ("self.cnt" := constInt 0)
              def "incr" ["self"; "diff"] ("self.cnt" := (var "self.cnt" .+ var "diff"))
            ])

      TextBlock @"The above Python becomes, in C\#:"

      CSharpCodeBlock(TextSize.Tiny,
          classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" ("cnt" := constInt 0) |> makePublic
              typedDef "incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
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

      TextBlock @"The above Python becomes, in C\#:"

      CSharpCodeBlock(TextSize.Tiny,
          (classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" ("cnt" := constInt 0) |> makePublic
              typedDef "incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
            ]) >>
          ((dots >>
            ((typedDeclAndInit "c" "Counter" (newC "Counter" []))) >>
             (methodCall "c" "incr" [ConstInt 5]))))
    ]

    TextBlock @"This snippet (remember: we cannot just copy and paste it) produces the same execution in both Python and Java/C\#!"

    CSharpStateTrace(TextSize.Tiny,
        ((classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" ("this.cnt" := constInt 0) |> makePublic
              typedDef "incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
            ]) >>
           ((dots >>
             (typedDeclAndInit "c" "Counter" (newC "Counter" [])) >>
              (methodCall "c" "incr" [ConstInt 5])))) >> endProgram,
        RuntimeState<_>.Zero (constInt 1))

    VerticalStack[
      TextBlock @"Method access determines where they can be called. Suppose \texttt{x} is of type \texttt{C}:"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "C" 
              [
                typedDecl "a" "int" |> makePrivate
                typedDecl "b" "int" |> makePublic
                typedDef "C" [] "" (("this.a" := constInt 0) >> ("this.b" := constInt 0)) |> makePublic
                typedDef "incrA" ["int","diff"] "void" ("this.a" := (var "this.a" .+ var "diff")) |> makePublic
                typedDef "incrB" ["int","diff"] "void" ("this.b" := (var "this.b" .+ var "diff")) |> makePrivate
              ])) >>
            (dots >>
             methodCall "x" "incrA" [ConstInt 10]))

      Question @"Will this program be allowed to run?"
      Pause
      TextBlock @"Yes, because \texttt{incrA} is a public method."
    ]

    VerticalStack[
      TextBlock @"Method access determines where they can be called. Suppose \texttt{x} is of type \texttt{C}:"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "C" 
              [
                typedDecl "a" "int" |> makePrivate
                typedDecl "b" "int" |> makePublic
                typedDef "C" [] "" (("this.a" := constInt 0) >> ("this.b" := constInt 0)) |> makePublic
                typedDef "incrA" ["int","diff"] "void" ("this.a" := (var "this.a" .+ var "diff")) |> makePublic
                typedDef "incrB" ["int","diff"] "void" ("this.b" := (var "this.b" .+ var "diff")) |> makePrivate
              ])) >>
            (dots >>
             methodCall "x" "incrB" [ConstInt 10]))

      Question @"Will this program be allowed to run?"
      Pause
      TextBlock @"No, because \texttt{incrB} is a private method."
    ]

    SubSection "Static methods"
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
              typedDef "f" ["int","x"] "int" ((ret (var "x" .+ ConstInt(10)))) |> makeStatic |> makePublic 
            ])
    ]

    VerticalStack[
      TextBlock @"\textit{static methods} are called without an instance of the class left of the dot, but rather with the name of the class they are declared in"

      CSharpCodeBlock(TextSize.Tiny,
          ((classDef "MyClass" 
              [
                typedDef "f" ["int","x"] "int" ((ret (var "x" .+ ConstInt(10)))) |> makeStatic |> makePublic 
              ])) >>
           ((dots >>
             (staticMethodCall "Console" "WriteLine" [staticMethodCall "MyClass" "f" [ConstInt(10)]])) >> endProgram))
    ]

    TextBlock @"This snippet (remember: we cannot just copy and paste it) produces the same execution in both Python and Java/C\#!"

    CSharpStateTrace(TextSize.Tiny,
          (classDef "MyClass" 
              [
                typedDef "f" ["int","x"] "int" ((ret (var "x" .+ ConstInt(10)))) |> makePublic |> makeStatic
              ]) >>
           ((dots >>
             (staticMethodCall "Console" "WriteLine" [staticMethodCall "MyClass" "f" [ConstInt(10)]])) >> endProgram),
            RuntimeState<_>.Zero (constInt 1))

    SubSection "The \texttt{Main} method"
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
                typedDef "main" ["String[]","args"] "void" (staticMethodCall "Console" "WriteLine" [ConstString "Hello world!"]) |> makeStatic |> makePublic 
              ])))

      Pause
      TextBlock @"We will now run it: this is the first program we could copy in a file and just compile and run!"
    ]

    CSharpStateTrace(TextSize.Tiny,
        ((classDef "Program" 
            [
              typedDef "Main" ["String[]","args"] "void" (staticMethodCall "Console" "WriteLine" [ConstString "Hello world!"]) |> makeStatic |> makePublic
            ] >> mainCall)),
        RuntimeState<_>.Zero (constInt 1))

    Section("Conclusion")
    SubSection("What have we seen so far?")
    ItemsBlock
      [
        !"Intro to DEV3"
        !"What we have learned so far: Python, from variables to basic classes"
        !"Primitive types and declarations: an intuition about the type system"
        !(@"Introduction to Java and C\#: from variables to basic classes, with execution examples")
      ]
  ]

// arrays
