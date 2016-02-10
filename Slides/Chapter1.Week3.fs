module Chapter1.Week3

open CommonLatex
open SlideDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime
open TypeChecker

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
      TextBlock @"$\ge 100000$"
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
      TextBlock @"A variable declaration in C\# or Java is prefixed by the type of the variable"

      ItemsBlock [
          ! @"\texttt{int x;} declares an integer variable"
          ! @"\texttt{string s;} declares a string variable"
          ! @"\texttt{float f;} declares a floating point variable"
          ! @"..."
        ]
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
         (typedDef "g" ["Car","car"] "int" (ret (methodCall "car" "drive" [constInt 2])) >>
          call "g" [constInt -1])) |> Unrepeated
      Question @"What has improved and why?"
      Pause
      TextBlock @"The function declaration specifies that \texttt{car} is an instance of the \texttt{Car} class. We will thus get a compiler error."
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
            Premises = [@"\text{It is raining}"; @"\text{I have my umbrella with me}"]
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
        !"This process is called type checking"
      ]

    Advanced(
      ItemsBlock[
        ! @"We want to specify this in the typing rule notation"
        ! @"The typing rules manipulate a stack of declarations which we will call $D$"
        ! @"Each typing rule will add or remove variable declarations and return the type of the current expression"
        ! @"Instead of coupling each variable with its value, we couple it with its type"
      ])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"The simplest typing rule is the one that finds a variable declaration"
            ! @"A declaration adds to the declarations $D$ the variable, connected with its type"
          ]

        TypingRules[
          {
            Premises = []
            Conclusion = @"\langle (\mathtt{T\ v;}), D \rangle \rightarrow \langle \mathtt{void}, D[v \mapsto T] \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
        ((typedDeclAndInit "x" "int" (ConstInt 10)) >> endProgram),
        TypeCheckingState<Code>.Zero)

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"When we look the variable up, its type is whatever type was found connected to it in the declarations"
            ! @"This does further nothing to the declarations"
            ! @"Let's assume that \texttt{x} is a variable name"
          ]

        TypingRules[
          {
            Premises = []
            Conclusion = @"\langle \mathtt{x}, D \rangle \rightarrow \langle D[\mathtt{x}], D \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
        (((typedDeclAndInit "x" "int" (ConstInt 10))) >>
           ("x" := (var"x" .+ (ConstInt 5))) >> endProgram),
        TypeCheckingState<Code>.Zero)

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Another simple typing rule is the one that types a constant value"
            ! @"It does nothing to the declarations"
            ! @"Let's assume that \texttt{i} is an integer constant"
          ]

        TypingRules[
          {
            Premises = []
            Conclusion = @"\langle \mathtt{i}, D \rangle \rightarrow \langle \mathtt{int}, D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Let's assume that \texttt{f} is a floating point constant"
          ]

        TypingRules[
          {
            Premises = []
            Conclusion = @"\langle \mathtt{f}, D \rangle \rightarrow \langle \mathtt{float}, D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Let's assume that \texttt{s} is a string constant"
          ]

        TypingRules[
          {
            Premises = []
            Conclusion = @"\langle \mathtt{s}, D \rangle \rightarrow \langle \mathtt{string}, D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Let's assume that \texttt{b} is a boolean constant"
          ]

        TypingRules[
          {
            Premises = []
            Conclusion = @"\langle \mathtt{b}, D \rangle \rightarrow \langle \mathtt{bool}, D \rangle"
          }
        ]])

    TextBlock "More complex typing rules compose together the types of different statements"

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"The typing rule for operators such as \texttt{+} requires the operands to be compatible"
            ! @"The type of both operands is often the same, for example \texttt{int} or \texttt{float}"
            ! @"The resulting type is then the type of both operands"
            ! @"Operands do not modify the current declarations"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{int}, D \rangle"
                        @"\langle \mathtt{b}, D \rangle \rightarrow \langle \mathtt{int}, D \rangle"]
            Conclusion = @"\langle (\mathtt{a+b}), D \rangle \rightarrow \langle \mathtt{int}, D \rangle"
          }
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{float}, D \rangle"
                        @"\langle \mathtt{b}, D \rangle \rightarrow \langle \mathtt{float}, D \rangle"]
            Conclusion = @"\langle (\mathtt{a+b}), D \rangle \rightarrow \langle \mathtt{float}, D \rangle"
          }
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{string}, D \rangle"
                        @"\langle \mathtt{b}, D \rangle \rightarrow \langle \mathtt{string}, D \rangle"]
            Conclusion = @"\langle (\mathtt{a+b}), D \rangle \rightarrow \langle \mathtt{string}, D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"The type of both operands could differ, but still be compatible (for example adding an \texttt{int} and a \texttt{float})"
            ! @"The resulting type is then the most generic type of the operands"
            ! @"Operands do not modify the current declarations"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{int}, D \rangle"
                        @"\langle \mathtt{b}, D \rangle \rightarrow \langle \mathtt{float}, D \rangle"]
            Conclusion = @"\langle (\mathtt{a+b}), D \rangle \rightarrow \langle \mathtt{float}, D \rangle"
          }
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{float}, D \rangle"
                        @"\langle \mathtt{b}, D \rangle \rightarrow \langle \mathtt{int}, D \rangle"]
            Conclusion = @"\langle (\mathtt{a+b}), D \rangle \rightarrow \langle \mathtt{float}, D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Statements in a sequence both modify, top-to-bottom, the declarations"
            ! @"Usually we expect the statements to simply return nothing, that is \texttt{void}"
            ! @"Further we cannot say anything about what they each do"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{void}, D_1 \rangle"
                        @"\langle \mathtt{b}, D_1 \rangle \rightarrow \langle \mathtt{void}, D_2 \rangle"]
            Conclusion = @"\langle (\mathtt{a;b}), D \rangle \rightarrow \langle \mathtt{void}, D_2 \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
        ((typedDeclAndInit "x" "int" (ConstInt 10)) >>
         ((typedDeclAndInit "y" "int" (ConstInt 20)) >>
           ("x" := (var"x" .+ var"y")) >> endProgram)),
        TypeCheckingState<Code>.Zero)

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"The typing rule for an \texttt{if-then-else} requires the condition to be a boolean expression, and assumes the type of both the then and the else bodies"
            ! @"The type of both the then and the else bodies must be the same (usually \texttt{void}, something else in case of function returns)"
            ! @"It does not add anything to the declarations, even though the bodies of the then and the else might declare local variables"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{c}, D \rangle \rightarrow \langle \mathtt{bool}, D \rangle"
                        @"\langle \mathtt{A}, D \rangle \rightarrow \langle \mathtt{T}, D' \rangle"
                        @"\langle \mathtt{B}, D \rangle \rightarrow \langle \mathtt{U}, D' \rangle"
                        @"T = U"]
            Conclusion = @"\langle (\mathtt{if\ c\ \{\ A\ \} else \{\ B\ \}}), D \rangle \rightarrow \langle \mathtt{T}, D \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
        ((typedDeclAndInit "x" "int" (ConstInt 10)) >>
         ((typedDeclAndInit "y" "int" (ConstInt 20)) >>
           (ifelse (var"x" .> var"y") 
                   ((typedDeclAndInit "z" "string" (ConstString "x")) >> staticMethodCall "Console" "WriteLine" [var "z"]) 
                   ((typedDeclAndInit "z" "string" (ConstString "y")) >> staticMethodCall "Console" "WriteLine" [var "z"])) >> endProgram)),
        TypeCheckingState<Code>.Zero)

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"The typing rule for a \texttt{while} loop requires the condition to be a boolean expression, and assumes the type of the body"
            ! @"The type of the body can be anything (usually \texttt{void}, something else in case of function returns)"
            ! @"It does not add anything to the declarations, even though the body might declare local variables"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{c}, D \rangle \rightarrow \langle \mathtt{bool}, D \rangle"
                        @"\langle \mathtt{B}, D \rangle \rightarrow \langle \mathtt{T}, D_1 \rangle"]
            Conclusion = @"\langle (\mathtt{while\ c\ \{\ A\ \}}), D \rangle \rightarrow \langle \mathtt{T}, D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"The typing rule for a \texttt{class} declaration adds the class declaration to the declarations with all its fields and methods"
            ! @"When adding the declaration of the class, we have to check that the types of the method bodies match their declarations"
            ! @"Assume that \texttt{C} is the class name, $f_i$ is the i-th field in the class (of type $F_i$), and \texttt{m}$_j$ is the j-th method in the class (with type $M_j$)"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"D_1 := D[C \mapsto [..\ f_i \mapsto F_i\ ..\ m_j \mapsto M_j\ ..]] "
                        @"\langle \mathtt{M_j\ m_j}, D_1[\mathtt{this} \mapsto C] \rangle \rightarrow \langle M^1_j, D_2 \rangle"
                        @"M_j = M^1_j"]
            Conclusion = @"\langle (\mathtt{class\ C\ \{\ ..F_i\ f_i..\ \ ..M_j\ m_j..\ \}}), D \rangle \rightarrow \langle \mathtt{T}, D_1 \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"When type checking a \texttt{method} declaration (within a class declaration} we type check its body and compare the result with the type of the declaration"
            ! @"Assume that \texttt{C} is the class name, $p_i$ is the i-th parameter of the method (of type $P_i$), and \texttt{b} is the method body"
            ! @"The type of a method is of the form $P_1 \times P_2 \times \dots \times P_n \rightarrow R$, where $P_l$ is the type of the l-th parameter and $R$ is the return type"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle (\mathtt{b}), D[..p_l \mapsto P_l..] \rangle \rightarrow \langle \mathtt{R}, D_1 \rangle"]
            Conclusion = @"\langle (\mathtt{R\ m(..P_l\ p_l..)b}), D \rangle \rightarrow \langle (P_1 \times P_2 \times \dots \times P_n \rightarrow R), D \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
        ((classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" (("this.cnt" := constInt 0) >> endProgram) |> makePublic
              typedDef "incr" ["int","diff"] "void" (("this.cnt" := (var "this.cnt" .+ var "diff")) >> endProgram) |> makePublic
            ]) >>
          endProgram),
        TypeCheckingState<Code>.Zero)

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"When type checking a \texttt{return} statement, we typecheck its argument"
            ! @"The type of the argument is also the type of the \texttt{return} statement"
            ! @"There is no change to the declarations"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{x}, D \rangle \rightarrow \langle \mathtt{T}, D \rangle"]
            Conclusion = @"\langle (\mathtt{return\ x}), D \rangle \rightarrow \langle T, D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Statements in a sequence might contain \texttt{return} statements"
            ! @"In this case one of them might not return \texttt{void}"
            ! @"Their sequence will assume the non-\texttt{void} type"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{T}, D_1 \rangle"
                        @"\langle \mathtt{b}, D_1 \rangle \rightarrow \langle \mathtt{void}, D_2 \rangle"]
            Conclusion = @"\langle (\mathtt{a;b}), D \rangle \rightarrow \langle \mathtt{T}, D_2 \rangle"
          }
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{void}, D_1 \rangle"
                        @"\langle \mathtt{b}, D_1 \rangle \rightarrow \langle \mathtt{T}, D_2 \rangle"]
            Conclusion = @"\langle (\mathtt{a;b}), D \rangle \rightarrow \langle \mathtt{T}, D_2 \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Statements in a sequence might contain \texttt{return} statements"
            ! @"They might both return a non-\texttt{void} type"
            ! @"Their sequence will assume the non-\texttt{void} type of both, which must be the same"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{a}, D \rangle \rightarrow \langle \mathtt{T}, D_1 \rangle"
                        @"\langle \mathtt{b}, D_1 \rangle \rightarrow \langle \mathtt{U}, D_2 \rangle"
                        @"T = U"]
            Conclusion = @"\langle (\mathtt{a;b}), D \rangle \rightarrow \langle \mathtt{T}, D_2 \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
        ((classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePrivate
              typedDef "Counter" [] "" (("this.cnt" := constInt 0) >> endProgram) |> makePublic
              typedDef "incr" ["int","diff"] "int" ((("this.cnt" := (var "this.cnt" .+ var "diff")) >> (ret (var "this.cnt"))) >> endProgram) |> makePublic
            ]) >>
          endProgram),
        TypeCheckingState<Code>.Zero)

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Sometimes we may look a field $f$ up from an instance $x$ of a class"
            ! @"This assumes the type of the field, which needs to be looked up in the class descriptor found in the declarations"
            ! @"No declaration is further modified"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{x}, D \rangle \rightarrow \langle \mathtt{C}, D \rangle"
                        @"\langle \mathtt{f}, C \rangle \rightarrow \langle \mathtt{F}, C \rangle"
                        @"T = U"]
            Conclusion = @"\langle (\mathtt{x.f}), D \rangle \rightarrow \langle \mathtt{F}, D \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
        ((classDef "Counter" 
            [
              typedDecl "cnt" "int" |> makePublic
              typedDef "Counter" [] "" (("this.cnt" := constInt 0) >> endProgram) |> makePublic
            ]) >>
              (((typedDeclAndInit "c" "ICounter" (newC "Counter" [])) >>
                 (typedDeclAndInit "x" "int" (var"c.cnt"))) >> 
                  endProgram)),
        TypeCheckingState<Code>.Zero)

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"Sometimes we may call a method $m$ up from an instance $x$ of a class and with parameters $p_i$"
            ! @"This assumes the return type of the method, provided that all parameter types match the types expected by the method"
            ! @"No declaration is further modified"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{x}, D \rangle \rightarrow \langle \mathtt{C}, D \rangle"
                        @"\langle \mathtt{m}, C \rangle \rightarrow \langle (\mathtt{P}_1 \times \mathtt{P}_2 \times \dots \times \mathtt{P}_n \rightarrow \mathtt{R}), C \rangle"
                        @"\langle \mathtt{p_i}, D \rangle \rightarrow \langle \mathtt{P}'_i, D \rangle"
                        @"\mathtt{P}_i = \mathtt{P}'_i"]
            Conclusion = @"\langle (\mathtt{x.f(..p_i..)}), D \rangle \rightarrow \langle \mathtt{R}, D \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
          (((classDef "Counter" 
              [
                typedDecl "cnt" "int" |> makePrivate
                typedDef "Counter" [] "" ("this.cnt" := constInt 0) |> makePublic
                typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
              ]) >>
            (((typedDeclAndInit "c" "Counter" (newC "Counter" [])) >>
               (methodCall "c" "Incr" [ConstInt 5])) >> 
                endProgram))),
          TypeCheckingState.Zero)

    Advanced(
      VerticalStack[
        ItemsBlock[
            ! @"We may call a static method $m$ from class $C$ and with parameters $p_i$"
            ! @"This assumes the return type of the method, provided that all parameter types match the types expected by the method"
            ! @"We do not need to look up the class because it is already specified in the call"
            ! @"No declaration is further modified"
          ]

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{m}, C \rangle \rightarrow \langle (\mathtt{P}_1 \times \mathtt{P}_2 \times \dots \times \mathtt{P}_n \rightarrow \mathtt{R}), C \rangle"
                        @"\langle \mathtt{p_i}, D \rangle \rightarrow \langle \mathtt{P}'_i, D \rangle"
                        @"\mathtt{P}_i = \mathtt{P}'_i"]
            Conclusion = @"\langle (\mathtt{C.f(..p_i..)}), D \rangle \rightarrow \langle \mathtt{R}, D \rangle"
          }
        ]])

    CSharpTypeTrace(TextSize.Tiny,
        ((classDef "Utils" 
            [
              typedDef "AddThree" ["int","a";"int","b";"int","c"] "int" (ret(var"a" .+ var"b" .+ var"c")) |> makePublic |> makeStatic
            ]) >>
              (((typedDeclAndInit "x" "int" (staticMethodCall "Utils" "AddThree" [constInt 10; constInt 20; constInt 30]))) >> 
                  endProgram)),
        TypeCheckingState<Code>.Zero)

    ItemsBlock[
        ! @"The constructor of a class is simply a specially named static method"
        ! @"It has no further typing rules"
      ]

    CSharpTypeTrace(TextSize.Tiny,
        ((classDef "CounterFrom" 
            [
              typedDecl "cnt" "int" |> makePublic
              typedDef "CounterFrom" ["int","cnt0"] "" (("this.cnt" := var"cnt0") >> endProgram) |> makePublic
            ]) >>
              ((typedDeclAndInit "c" "CounterFrom" (newC "CounterFrom" [ConstInt 100])) >>
                  endProgram)),
        TypeCheckingState<Code>.Zero)

    Section("Conclusion")
    SubSection("Looking back")
    ItemsBlock
      [
        !"Python is brittle and breaks easily"
        !"Static typing is a way to run a coarse simulation of the program"
        !"If type checking fails, then the program cannot be guaranteed to run correctly, and we get a compiler error"
        !"Safer programming, but at the cost of being able to run less programs that might still be valid"
      ]
  ]
