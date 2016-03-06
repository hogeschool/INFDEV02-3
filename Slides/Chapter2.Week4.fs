module Chapter2.Week4

open CommonLatex
open LatexDefinition
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
        !"Beyond type equality"
        !"Defining our own subtypes"
        !"Lists and state machines"
      ]

    Section "Beyond type equality"
    SubSection "Introduction"
    ItemsBlock
      [
        ! @"In the previous lecture we have seen that Java/C\# have a static type system"
        ! @"Programs that make no sense are outright refused by the compiler"
        ! @"``Make no sense'' means calling methods or making assignments with the wrong types"
      ]

    SubSection "Wrong types"
    VerticalStack
      [
        TextBlock @"So far, we defined a type to be wrong if it is not exactly the same as what we expected."
        TextBlock @"If we expected an \texttt{int}, but got a \texttt{Person}, then clearly something was off and we expect\footnote{welcome, actually} a compiler error."
      ]

    TextBlock @"The two most important typing rules for these violations are those of variable assignment and method call."

    Advanced(
      VerticalStack[
        TextBlock @"When assigning a variable, we expect the type of the expression and that of the variable to be exactly the same: $T=D[x]$"

        TypingRules[
          {
            Premises = [ @"\langle \mathtt{e}, D \rangle \rightarrow \langle T, D \rangle"; @"T = D[x]" ]
            Conclusion = @"\langle \mathtt{x = e}, D \rangle \rightarrow \langle D[\mathtt{void}], D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        TextBlock @"When calling a method, we expect the type of the parameters and those of the arguments to be exactly the same: $\mathtt{P}_i = \mathtt{P}'_i$"

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{c}, D \rangle \rightarrow \langle \mathtt{C}, D \rangle"
                        @"\langle \mathtt{m}, C \rangle \rightarrow \langle (\mathtt{P}_1 \times \mathtt{P}_2 \times \dots \times \mathtt{P}_n \rightarrow \mathtt{R}), C \rangle"
                        @"\langle \mathtt{p_i}, D \rangle \rightarrow \langle \mathtt{P}'_i, D \rangle"
                        @"\mathtt{P}_i = \mathtt{P}'_i"]
            Conclusion = @"\langle (\mathtt{c.m(..p_i..)}), D \rangle \rightarrow \langle \mathtt{R}, D \rangle"
          }
        ]])


    VerticalStack
      [
        TextBlock @"For example, the program below violates typing rules:"

        CSharpCodeBlock(TextSize.Tiny,
              (typedDeclAndInit "x" "int" (constInt 5) >>
               ("x" := constString "uh!?" >> 
                endProgram))) |> Unrepeated
      ]

    VerticalStack
      [
        TextBlock @"For example, the program below violates typing rules:"

        CSharpCodeBlock(TextSize.Tiny,
              (((classDef "Counter" 
                  [
                    typedDecl "cnt" "int" |> makePrivate
                    typedDef "Counter" [] "" ("this.cnt" := constInt 0) |> makePublic
                    typedDef "Incr" ["int","diff"] "void" ("this.cnt" := (var "this.cnt" .+ var "diff")) |> makePublic
                  ]) >>
                (((typedDeclAndInit "c" "Counter" (newC "Counter" [])) >>
                   (methodCall "c" "Incr" [var"c"])) >> 
                    endProgram)))) |> Unrepeated
      ]

    SubSection "Always wrong?"
    VerticalStack
      [
        TextBlock @"\texttt{int x = 5.5;}   makes no sense"
        ! @"This will also give a compiler error."
      ]

    ItemsBlock 
      [
        ! @"What about a similar expression, such as \texttt{float x = 10;}?"
        ! @"\texttt{x} is a \texttt{float}"
        ! @"\texttt{10} is an \texttt{int}"
        ! @"\texttt{float} $\neq$ \texttt{int}, so we should get a type error."
      ]

    VerticalStack
      [
        TextBlock @"\texttt{float x = 10;}   makes sense, even though it violates the strictest typing rules described so far."
        TextBlock @"Floating point numbers ``contain'' integers, so converting the integer 10 to 10.0 loses no information"
      ]

    SubSection "Subtyping"
    TextBlock @"This means that the typing rules as seen so far are \textbf{too restrictive}: we should be able to accept an assignment from a more specific data type (such as \textttt{int}) to a less specific data type (such as \texttt{float})."

    SubSection "More and less specific data types"
    VerticalStack
      [
        TextBlock @"Consider any two data types, \texttt{T} and \texttt{S}"
        TextBlock @"We say that \texttt{S <: T}, read ``S is a subtype of T'', to mean that any value of type \texttt{S} can be safely used where a value of type \texttt{T} is expected."
        TextBlock @"We also say that, when \texttt{S <: T}, \texttt{T} \textbf{generalizes} \texttt{S}."
      ]

    TextBlock @"For example, \texttt{int <: float}, because any value of type \texttt{int} can be safely used where a value of type \texttt{float} is expected (as the conversion loses no data, and thus can be inserted by the compiler itself)."

    TextBlock @"We can now amend many of our typing rules: instead of type equality, we can use subtyping to preserve safety, but achieve more flexibility."

    Advanced(
      VerticalStack[
        TextBlock @"When assigning a variable, we expect the type of the expression \textbf{to be a subtype} of that of the variable."

        TypingRules[
          {
            Premises = [ @"\langle \mathtt{e}, D \rangle \rightarrow \langle T, D \rangle"; @"T <: D[x]" ]
            Conclusion = @"\langle \mathtt{x = e}, D \rangle \rightarrow \langle D[\mathtt{void}], D \rangle"
          }
        ]])

    Advanced(
      VerticalStack[
        TextBlock @"When calling a method, we expect the type of the parameters \textbf{to be subtypes} of the types of the arguments."

        Tiny

        TypingRules[
          {
            Premises = [@"\langle \mathtt{c}, D \rangle \rightarrow \langle \mathtt{C}, D \rangle"
                        @"\langle \mathtt{m}, C \rangle \rightarrow \langle (\mathtt{P}_1 \times \mathtt{P}_2 \times \dots \times \mathtt{P}_n \rightarrow \mathtt{R}), C \rangle"
                        @"\langle \mathtt{p_i}, D \rangle \rightarrow \langle \mathtt{P}'_i, D \rangle"
                        @"\mathtt{P}_i :> \mathtt{P}'_i"]
            Conclusion = @"\langle (\mathtt{c.m(..p_i..)}), D \rangle \rightarrow \langle \mathtt{R}, D \rangle"
          }
        ]])
    
    Section("Defining our own subtypes")
    SubSection("Customizable abstractions, remember?")
    VerticalStack[
      TextBlock @"Remember that we can define our own classes to extend the data types of the language when they are insufficient for our domain."
      TextBlock @"We can make use of subtyping for those custom classes."
      TextBlock @"This makes it possible to capture \textbf{generalization relationships} between our own data types."
    ]

    SubSection("Generalization relationships?")
    VerticalStack[
      TextBlock @"Can you think of a few examples of classes that generalize each other?"
      Pause
      TextBlock @"\texttt{Person} generalizes \texttt{Student}."
      TextBlock @"\texttt{LightEmitter} generalizes \texttt{Lamp}."
      TextBlock @"\texttt{Animal} generalizes \texttt{Dog}."
    ]

    VerticalStack[
      TextBlock @"Which generalizes which?"
      TextBlock @"\texttt{Mercedes} vs \texttt{CarBrand}? \pause \texttt{CarBrand :> Mercedes} \pause"
      TextBlock @"\texttt{LivingSpace} vs \texttt{Apartment}? \pause \texttt{LivingSpace :> Apartment} \pause"
      TextBlock @"\texttt{Cat} vs \texttt{Bird}? \pause Neither. \pause"
      TextBlock @"\texttt{Person} vs \texttt{Employee}? \pause \texttt{Person :> Employee} \pause"
      TextBlock @"\texttt{Student} vs \texttt{Person}? \pause \texttt{Person :> Student}. \pause"
      TextBlock @"\texttt{Student} vs \texttt{Employee}? \pause Neither. \pause"
    ]

    SubSection("Class inheritance")
    VerticalStack[
      TextBlock @"Consider, in particular, the following relationships:"
      TextBlock @"\texttt{Person :> Employee}"
      TextBlock @"\texttt{Person :> Student}"
      Pause
      TextBlock @"We could imagine that \texttt{Person}, \texttt{Employee}, and \texttt{Student} are all classes"
      TextBlock @"Moreover, \texttt{Person} and \texttt{Employee} are somehow related"
      TextBlock @"Similarly, \texttt{Person} and \texttt{Student} are related in the same way"
    ]

    ItemsBlock[
      ! @"This relationships is modeled, in Java/C\#, with class inheritance."
      ! @"A class such as \texttt{Employee} will therefore inherit from class \texttt{Person}"
      ! @"This  means that \texttt{Employee} will automatically have all fields and methods of \texttt{Person}"
    ]

    VerticalStack[
      TextBlock @"When we use inheritance, for example of \texttt{Person} from \texttt{Employee}, then the language automatically infers that \texttt{Person :> Employee}"
      TextBlock @"This means therefore that anywhere in the language (a variable, a parameter, etc.) where we expected a \texttt{Person}, we can give an \texttt{Employee}."
      TextBlock @"This provides polymorphism, as the same data type (in this case \texttt{Person}) can have multiple shapes: a \texttt{Person}, an \texttt{Employee}, a \texttt{Student}, etc."
    ]

    VerticalStack[
      TextBlock @"Inheritance requires very little code: in C\#, a colon (:) suffices with the name of the inherited class next to that of the defined class."
      TextBlock @"In Java, we use the keyword \texttt{extends} instead of the colon."
      TextBlock @"It is possible to inherit at most one class."
    ]

    VerticalStack[
      TextBlock @"The program below works: why?"

      CSharpCodeBlock(TextSize.Tiny,
            ((classDef "A" 
                [
                  typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
                ]) >>
             ((classDef "B" 
                [
                  extends "A"
                  typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                ]) >>
              (((typedDeclAndInit "b" "A" (newC "B" [])) >>
                 ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [ConstInt 5]]) >> 
                    endProgram))))))

      Pause
      TextBlock @"Because the declaration of \texttt{b} specifies \texttt{A} as the type, but whenever we expect an \texttt{A} we can use a \texttt{B} thanks to inheritance."
    ]

    CSharpTypeTrace(TextSize.Tiny,
          ((classDef "A" 
              [
                typedDef "A" [] "" endProgram |> makePublic
                typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
              ]) >>
              ((classDef "B" 
                  [
                    extends "A"
                    typedDef "B" [] "" endProgram |> makePublic
                    typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                  ]) >>
                  (((typedDeclAndInit "b" "A" (newC "B" [])) >>
                      ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [ConstInt 5]]) >> 
                        endProgram))))), TypeCheckingState.Zero, false)

    CSharpStateTrace(TextSize.Tiny,
          ((classDef "A" 
              [
                typedDef "A" [] "" endProgram |> makePublic
                typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
              ]) >>
              ((classDef "B" 
                  [
                    extends "A"
                    typedDef "B" [] "" endProgram |> makePublic
                    typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                  ]) >>
                  (((typedDeclAndInit "b" "A" (newC "B" [])) >>
                      ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [ConstInt 5]]) >> 
                        endProgram))))), 
            Runtime.RuntimeState<_>.Zero (constInt 1))

    TextBlock @"Similarly, given a method that expects a parameter of type \texttt{A} could accept a parameter of type \texttt{C}."

    VerticalStack[
      TextBlock @"Inheritance does not make all combinations possible. For example, the program below does not work: why?"

      CSharpCodeBlock(TextSize.Tiny,
            ((classDef "A" 
                [
                  typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
                ]) >>
              ((classDef "B" 
                  [
                    extends "A"
                    typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                  ]) >>
                 ((classDef "C" 
                    [
                      extends "A"
                      typedDef "O" ["int","z"] "int" (ret (var "z" .- constInt 1)) |> makePublic
                    ]) >>
                    (((typedDeclAndInit "b" "B" (newC "C" [])) >>
                       ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [ConstInt 5]]) >> 
                          endProgram)))))))
      
      Pause
      TextBlock @"There is no inheritance relationship between \texttt{B} and \texttt{C}!"
    ]

    TextBlock @"The subtyping relationship is transitive. This means that given X <: Y and Y <: Z, implies X <: Z."

    VerticalStack[
      TextBlock @"Inheritance can also perform multiple conversion steps:"

      CSharpCodeBlock(TextSize.Tiny,
            ((classDef "A" 
                [
                  typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
                ]) >>
              ((classDef "B" 
                  [
                    extends "A"
                    typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                  ]) >>
                 ((classDef "C" 
                    [
                      extends "B"
                      typedDef "O" ["int","z"] "int" (ret (var "z" .- constInt 1)) |> makePublic
                    ]) >>
                    (((typedDeclAndInit "a" "A" (newC "C" [])) >>
                       ((staticMethodCall "Console" "WriteLine" [methodCall "a" "M" [ConstInt 5]]) >> 
                          endProgram)))))))
    ]

    CSharpTypeTrace(TextSize.Tiny,
          ((classDef "A" 
              [
                typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
              ]) >>
            ((classDef "B" 
                [
                  extends "A"
                  typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                ]) >>
                ((classDef "C" 
                    [
                      extends "B"
                      typedDef "O" ["int","z"] "int" (ret (var "z" .- constInt 1)) |> makePublic
                    ]) >>
                    (((typedDeclAndInit "a" "A" (newC "C" [])) >>
                        ((staticMethodCall "Console" "WriteLine" [methodCall "a" "M" [ConstInt 5]]) >> 
                          endProgram)))))), TypeCheckingState.Zero, false)

    CSharpStateTrace(TextSize.Tiny,
          ((classDef "A" 
              [
                typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
              ]) >>
            ((classDef "B" 
                [
                  extends "A"
                  typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                ]) >>
                ((classDef "C" 
                    [
                      extends "B"
                      typedDef "C" [] "" endProgram |> makePublic
                      typedDef "O" ["int","z"] "int" (ret (var "z" .- constInt 1)) |> makePublic
                    ]) >>
                    (((typedDeclAndInit "a" "A" (newC "C" [])) >>
                        ((staticMethodCall "Console" "WriteLine" [methodCall "a" "M" [ConstInt 5]]) >> 
                          endProgram)))))), 
            Runtime.RuntimeState<_>.Zero (constInt 1))

    VerticalStack[
      Tiny
      TextBlock @"Of course, when inheriting, we can still use all methods available given a variable type."
      TextBlock @"The code below does indeed work. Why?"

      CSharpCodeBlock(TextSize.Tiny,
            ((classDef "A" 
                [
                  typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
                ]) >>
              ((classDef "B" 
                  [
                    extends "A"
                    typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                  ]) >>
                 ((classDef "C" 
                    [
                      extends "B"
                      typedDef "O" ["int","z"] "int" (ret (var "z" .- constInt 1)) |> makePublic
                    ]) >>
                    (((typedDeclAndInit "b" "B" (newC "C" [])) >>
                       ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [ConstInt 5]]) >> 
                          ((staticMethodCall "Console" "WriteLine" [methodCall "b" "N" [ConstInt 5]]) >>
                              endProgram))))))))
      
      Pause
      TextBlock @"It is possible to call both methods \texttt{M} and \texttt{N} on an instance of \texttt{B}."
    ]
    
    CSharpTypeTrace(TextSize.Tiny,
          ((classDef "A" 
              [
                typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
              ]) >>
            ((classDef "B" 
                [
                  extends "A"
                  typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                ]) >>
                ((classDef "C" 
                    [
                      extends "B"
                      typedDef "O" ["int","z"] "int" (ret (var "z" .- constInt 1)) |> makePublic
                    ]) >>
                    (((typedDeclAndInit "b" "B" (newC "C" [])) >>
                        ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [ConstInt 5]]) >> 
                          ((staticMethodCall "Console" "WriteLine" [methodCall "b" "N" [ConstInt 5]]) >>
                              endProgram))))))), TypeCheckingState.Zero, false)

    CSharpStateTrace(TextSize.Tiny,
          ((classDef "A" 
              [
                typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
              ]) >>
            ((classDef "B" 
                [
                  extends "A"
                  typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                ]) >>
                ((classDef "C" 
                    [
                      extends "B"
                      typedDef "C" [] "" endProgram |> makePublic
                      typedDef "O" ["int","z"] "int" (ret (var "z" .- constInt 1)) |> makePublic
                    ]) >>
                    (((typedDeclAndInit "b" "B" (newC "C" [])) >>
                        ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [ConstInt 5]]) >> 
                          ((staticMethodCall "Console" "WriteLine" [methodCall "b" "N" [ConstInt 5]]) >>
                              endProgram))))))), Runtime.RuntimeState<_>.Zero (constInt 1))

    VerticalStack[
      TextBlock @"A major difference with Python is that, even if the instance may allow calling some methods, subtyping might disallow it."
      TextBlock @"The code below does not work. Why?"

      CSharpCodeBlock(TextSize.Tiny,
            ((classDef "A" 
                [
                  typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
                ]) >>
              ((classDef "B" 
                  [
                    extends "A"
                    typedDef "N" ["int","y"] "int" (ret (var "y" .* constInt 10)) |> makePublic
                  ]) >>
                 ((classDef "C" 
                    [
                      extends "B"
                      typedDef "O" ["int","z"] "int" (ret (var "z" .- constInt 1)) |> makePublic
                    ]) >>
                    (((typedDeclAndInit "b" "B" (newC "C" [])) >>
                       ((staticMethodCall "Console" "WriteLine" [methodCall "b" "O" [ConstInt 5]]) >> 
                          endProgram)))))))
      
      Pause
      TextBlock @"\texttt{b} is declared with type \texttt{B}, which has no method \texttt{O}."
    ]

    Section "Live code demo: person, employee, and student/animal, dog, cat/..."

    Section "Interfaces and polymorphism"
    SubSection "Zero-level data types"
    ItemsBlock [
        ! @"The subtyping relationship can start from a data type so general that it has no concrete implementation."
        ! @"This data type is called an \textbf{interface}."
        ! @"Interfaces are classes defined with the keyword \texttt{interface}. They have no fields, and no implementation of their methods."
        ! @"We say that a class implements, not inherits from, one or more interfaces."
      ]

    ItemsBlock [
        ! @"Interfaces are especially useful to specify what requirements a data type must satisfy to be used in a context."
        ! @"With interfaces we are not bound to also giving a ``default'' implementation."
      ]

    VerticalStack[
      TextBlock @"Implementing interfaces requires very little code: in C\#, a colon (:) suffices with the name of the implemented interface next to that of the defined class."
      TextBlock @"In Java, we use the keyword \texttt{implements} instead of the colon."
      TextBlock @"It is possible to implement multiple interfaces from the same class."
    ]

    VerticalStack[
      TextBlock @"The program below works: why?"

      CSharpCodeBlock(TextSize.Tiny,
            ((interfaceDef "A" 
                [
                  typedSig "M" ["int","x"] "int"
                ]) >>
             ((classDef "B" 
                [
                  implements "A"
                  typedDef "M" ["int","x"] "int" (ret (var "x" .+ var "x")) |> makePublic
                ]) >>
              (((typedDeclAndInit "b" "A" (newC "B" [])) >>
                 ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [ConstInt 5]]) >> 
                    endProgram))))))

      Pause
      TextBlock @"Because the declaration of \texttt{b} specifies \texttt{A} as the type, but whenever we expect an \texttt{A} we can use a \texttt{B} thanks to the subtyping of implemented interfaces."
    ]

    VerticalStack[
      TextBlock @"The program below does not work: why?"

      CSharpCodeBlock(TextSize.Tiny,
            ((interfaceDef "A" 
                [
                  typedSig "M" ["int","x"] "int"
                ]) >>
              (((typedDeclAndInit "a" "A" (newC "A" [])) >>
                 ((staticMethodCall "Console" "WriteLine" [methodCall "a" "M" [ConstInt 5]]) >> 
                    endProgram)))))

      Pause
      TextBlock @"Because \texttt{A} has no implementation and so cannot be instantiated: what code could we possibly execute for method \texttt{M}?"
    ]

    TextBlock @"Polymorphism can be used in a lot of contexts, as long as the conversion we expect of the language is provably safe."

    VerticalStack[
      TextBlock @"The program below works: why?"

      CSharpCodeBlock(TextSize.Tiny,
            ((interfaceDef "A" 
                [
                  typedSig "M" ["A","x"] "A"
                ]) >>
             ((classDef "B" 
                [
                  implements "A"
                  typedDef "M" ["A","x"] "A" (ret (var "this")) |> makePublic
                ]) >>
              (((typedDeclAndInit "b" "A" (newC "B" [])) >>
                 ((staticMethodCall "Console" "WriteLine" [methodCall "b" "M" [newC "B" []]]) >> 
                    endProgram))))))

      Pause
      TextBlock @"Because the argument to \texttt{M}, which should be an \texttt{A}, can safely accept a \texttt{B} as well."
    ]

    Section "Live code demo: list, empty, and node."

    Section("Conclusion")
    SubSection("Looking back")
    ItemsBlock
      [
        !"Polymorphism makes it possible to pass different data types to other contexts, as long as the conversion is safe"
        !"Inheritance is the basic mechanism of polymorphism"
        !"Interfaces make this even more powerful by allowing the use of polymorphism without a concrete data type"
      ]
  ]

//TODO: arrays as primitive data types
//TODO: lambda's
