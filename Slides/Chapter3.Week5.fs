module Chapter3.Week5

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
        ! @"Arrays as a simple generic data type"
        ! @"Class generators: generics"
        ! @"Interfaces and generics"
        ! @"Generic lists: a concrete example"
        ! @"Lambda"
      ]

    Section "Arrays as a simple generic data type"
    SubSection "Introduction"
    ItemsBlock
      [
        ! @"A very common necessity when programming is storing multiple values in a variable"
        ! @"There actually is a built-in datatype in most programming languages to do so"
        ! @"This datatype is called \textbf{array}"
      ]

    ItemsBlock
      [
        ! @"An array is declared with the type of the element, followed by square brackets"
        ! @"The array is then initialized by specifying the number of elements it can store"
        ! @"The elements are then written and accessed given their position in the array"
        ! @"The array cannot change size: reading or writing an elements out of bounds gives an error"
      ]

    VerticalStack
      [
        TextBlock @"An array is declared with the type of the element, followed by square brackets"

        CSharpCodeBlock(TextSize.Small,
          arrayDecl "x" "int" >> endProgram)
      ]

    VerticalStack
      [
        TextBlock @"The array is then initialized by specifying the number of elements it can store"

        CSharpCodeBlock(TextSize.Small,
          arrayDeclAndInit "x" "int" (newArray "int" 10) >> endProgram)
      ]

    VerticalStack
      [
        TextBlock @"The elements are then written and accessed given their position in the array"

        CSharpCodeBlock(TextSize.Small,
          arrayDeclAndInit "x" "int" (newArray "int" 10) >> 
          arraySet "x" 5 (constInt 100) >> 
          staticMethodCall "Console" "WriteLine" [arrayGet "x" 5] >>
          endProgram)
      ]

    CSharpStateTrace(TextSize.Small,
      arrayDeclAndInit "x" "int" (newArray "int" 10) >> 
      arraySet "x" 5 (constInt 100) >> 
      staticMethodCall "Console" "WriteLine" [arrayGet "x" 5] >>
      endProgram, RuntimeState<_>.Zero (constInt 1))

    VerticalStack
      [
        TextBlock @"The array cannot change size: reading or writing an elements out of bounds gives an error"
        TextBlock @"The program below would just crash at runtime with an \textit{array out of bounds error}"

        CSharpCodeBlock(TextSize.Small,
          arrayDeclAndInit "x" "int" (newArray "int" 10) >> 
          arraySet "x" 15 (constInt 100) >> 
          staticMethodCall "Console" "WriteLine" [arrayGet "x" 15] >>
          endProgram)
      ]

    SubSection "Arrays of various types?"
    ItemsBlock
      [
        ! @"Arrays can come in all sorts of types"
        ! @"\texttt{int[], float[], bool[], string[], Car[], ...}"
      ]
   
    VerticalStack
      [
        TextBlock @"So what is common to all arrays, independently of their specific content?"

        Pause

        ItemsBlock
          [
            ! @"An array \texttt{T[]} contains a series of elements \textbf{of any type} \texttt{T}"
            ! @"It is initialized with \texttt{new T[n]}, where \texttt{n} is the number of stored elements"
            ! @"We access the \texttt{i}-th element of array \texttt{a} of type \texttt{T[]} with \texttt{a[i]}; \texttt{a[i]} has type \texttt{T}"
            ! @"We set the \texttt{i}-th element of array \texttt{a} of type \texttt{T[]} with \texttt{a[i] = e}; \texttt{e} has type \texttt{T}"
          ]
      ]

    TextBlock @"So it makes perfect sense to speak about arrays in terms which are \textbf{generic} with respect to the type of the elements!"

    Section "Class generators: generics"
    SubSection "Introduction"
    ItemsBlock
      [
        ! @"We can define classes that follow the same philosophy just explained for arrays"
        ! @"These classes only specify a structure, but are independent of the type of their content"
        ! @"These classes are called \textbf{generic classes}"
      ]

    SubSection "An example: arbitrary pairs"
    VerticalStack
      [
        TextBlock @"Suppose we wish to define a class that stores two elements together."
        
        ItemsBlock [      
            ! @"It can be useful in many places when we want to couple two things together"
            ! @"Return two values from a function"
            ! @"Store a list of relationships"
            ! @"..."
          ]
      ]

    VerticalStack
      [
        TextBlock @"Storing two elements together should be independent of their type. We do not want to define a new version of the class for each possible combination, such as:"
        
        ItemsBlock [      
            ! @"\texttt{int} and \texttt{bool}"
            ! @"\texttt{float} and \texttt{float}"
            ! @"\texttt{bool} and \texttt{string}"
            ! @"\texttt{Car} and \texttt{int}"
            ! @"\texttt{Person} and \texttt{Dog}"
            ! @"\texttt{Man} and \texttt{Woman}"
            ! @"\texttt{Woman} and \texttt{Man}"
            ! @"\texttt{Woman} and \texttt{Woman}"
            ! @"\texttt{Man} and \texttt{Man}"
            ! @"..."
          ]
      ]

    VerticalStack
      [
        TextBlock @"We can define such a class by specifying that it depends on the types of its fields:"

        CSharpCodeBlock(TextSize.Small,
          genericClassDef ["T"; "U"] "Pair" 
            [
              typedDecl "x" "T" |> makePrivate
              typedDecl "y" "U" |> makePrivate
              typedDef "Pair" ["T","x"; "U","y"] "" (("this.x" := var"x") >> ("this.y" := var"y") >> endProgram) |> makePublic
              typedDef "First" [] "T" (ret (var "this.x")) |> makePublic
              typedDef "Second" [] "U" (ret (var "this.y")) |> makePublic
            ] >> endProgram)
      ]

    VerticalStack
      [
        TextBlock @"We can then use this class by specifying what the types of its fields are concretely:"

        CSharpCodeBlock(TextSize.Tiny,
          genericClassDef ["T"; "U"] "Pair" 
            [
              typedDecl "x" "T" |> makePrivate
              typedDecl "y" "U" |> makePrivate
              typedDef "Pair" ["T","x"; "U","y"] "" (("this.x" := var"x") >> ("this.y" := var"y") >> endProgram) |> makePublic
              typedDef "First" [] "T" (ret (var "this.x")) |> makePublic
              typedDef "Second" [] "U" (ret (var "this.y")) |> makePublic
            ] >> 
          genericTypedDeclAndInit ["int"; "bool"] "p" "Pair" (genericNewC "Pair" ["int"; "bool"] [constInt 10; constBool true]) >>
          staticMethodCall "Console" "WriteLine" [methodCall "p" "First" []] >>
          staticMethodCall "Console" "WriteLine" [methodCall "p" "Second" []] >>
          endProgram)
      ]

    CSharpStateTrace(TextSize.Tiny,
      genericClassDef ["T"; "U"] "Pair" 
        [
          typedDecl "x" "T" |> makePrivate
          typedDecl "y" "U" |> makePrivate
          typedDef "Pair" ["T","x"; "U","y"] "" (("this.x" := var"x") >> ("this.y" := var"y") >> endProgram) |> makePublic
          typedDef "First" [] "T" (ret (var "this.x")) |> makePublic
          typedDef "Second" [] "U" (ret (var "this.y")) |> makePublic
        ] >> 
      genericTypedDeclAndInit ["int"; "bool"] "p" "Pair" (genericNewC "Pair" ["int"; "bool"] [constInt 10; constBool true]) >>
      staticMethodCall "Console" "WriteLine" [methodCall "p" "First" []] >>
      staticMethodCall "Console" "WriteLine" [methodCall "p" "Second" []] >>
      endProgram, RuntimeState<_>.Zero (constInt 1))

    ItemsBlock
      [
        ! @"In Java, generic arguments cannot be all those primitive types with non-reference values that sit directly on the stack"
        ! @"This means that we cannot write \texttt{Pair<int,int>} in Java"
        ! @"The standard library contains \textbf{reference versions} of those types, starting with a capital letter, such as \texttt{Integer}, etc."
        ! @"Those types are like the primitive types, but their values are references that point to the actual value on the heap"
        ! @"We can then write \texttt{Pair<Integer,Integer>}"
      ]

    VerticalStack
      [
        TextBlock @"The types of the fields can change, but the class implementation always remains the same:"

        CSharpCodeBlock(TextSize.Tiny,
          genericTypedDeclAndInit ["int"; "bool"] "p" "Pair" (genericNewC "Pair" ["int"; "bool"] [constInt 10; constBool true]) >>
          genericTypedDeclAndInit ["float"; "bool"] "p" "Pair" (genericNewC "Pair" ["float"; "bool"] [constFloat 10.0; constBool false]) >>
          genericTypedDeclAndInit ["bool"; "bool"] "p" "Pair" (genericNewC "Pair" ["bool"; "bool"] [constBool false; constBool true]) >>
          genericTypedDeclAndInit ["string"; "int"] "p" "Pair" (genericNewC "Pair" ["string"; "int"] [constString "First item"; constInt 5]) >>
          dots >>
          endProgram)
      ]

    SubSection "Typechecking of generic classes"
    ItemsBlock
      [
        ! @"Typecheking is simply a form of \textbf{substitution}"
        ! @"When the class is instantiated with types as arguments, a new version of the class is created"
        ! @"The created class has the concrete versions of these parameters in it"
      ]

    CSharpTypeTrace(TextSize.Tiny,
      genericClassDef ["T"; "U"] "Pair" 
        [
          typedDecl "x" "T" |> makePrivate
          typedDecl "y" "U" |> makePrivate
          typedDef "Pair" ["T","x"; "U","y"] "" (("this.x" := var"x") >> ("this.y" := var"y") >> endProgram) |> makePublic
          typedDef "First" [] "T" (ret (var "this.x")) |> makePublic
          typedDef "Second" [] "U" (ret (var "this.y")) |> makePublic
        ] >> 
      genericTypedDeclAndInit ["int"; "bool"] "p" "Pair" (genericNewC "Pair" ["int"; "bool"] [constInt 10; constBool true]) >>
      endProgram, TypeCheckingState.Zero, true)

    VerticalStack
      [
        TextBlock "This means that the compiler would actually generate code that behaves like the following:"

        CSharpCodeBlock(TextSize.Tiny,
          classDef "PairIntBool" 
            [
              typedDecl "int" "x" |> makePrivate
              typedDecl "bool" "y" |> makePrivate
              typedDef "PairIntBool" ["int","x"; "bool","y"] "" (("this.x" := var"x") >> ("this.y" := var"y") >> endProgram) |> makePublic
              typedDef "First" [] "int" (ret (var "this.x")) |> makePublic
              typedDef "Second" [] "bool" (ret (var "this.y")) |> makePublic
            ] >> 
          typedDeclAndInit "p" "PairIntBool" (newC "PairIntBool" [constInt 10; constBool true]) >>
          endProgram)
      ]

    Section @"Interfaces and generics"
    SubSection "Introduction"
    ItemsBlock [
        ! @"Interfaces can also be defined generically."
        ! @"When implementing a generic interface, we need to provide the types of its generic arguments."
      ]

    VerticalStack
      [
        TextBlock "We can define a generic interface as follows:"

        CSharpCodeBlock(TextSize.Tiny,
          genericInterfaceDef ["T"; "U"] "IPair" 
            [
              typedSig "First" [] "T"
              typedSig "Second" [] "U"
            ] >> 
          endProgram)
      ]

    VerticalStack
      [
        TextBlock "The generic interface can then be implemented by a class (generic or not) as follows:"

        CSharpCodeBlock(TextSize.Tiny,
          dots >>
          genericClassDef ["T"; "U"] "Pair" 
            [
              implements "IPair<T,U>"
              typedDecl "T" "x" |> makePrivate
              typedDecl "U" "y" |> makePrivate
              typedDef "Pair" ["T","x"; "U","y"] "" (("this.x" := var"x") >> ("this.y" := var"y") >> endProgram) |> makePublic
              typedDef "First" [] "T" (ret (var "this.x")) |> makePublic
              typedDef "Second" [] "U" (ret (var "this.y")) |> makePublic
            ] >> 
          genericTypedDeclAndInit ["int"; "bool"] "p" "IPair" (genericNewC "Pair" ["int"; "bool"] [constInt 10; constBool true]) >>
          endProgram)
      ]


    Section @"Generic lists: a concrete example"
    SubSection "Ingredients"
    ItemsBlock [
        ! @"We need an \texttt{List<T>} generic interface"
        ! @"We then need two generic classes that implement the interface: \texttt{Empty<T>} and \texttt{Node<T>}"
        ! @"In \texttt{Empty} the methods fail with a (descriptive) error."
      ]

    TextBlock @"Live coding demo: generic lists."

    Section @"Lambda functions"
    SubSection "Introduction"
    ItemsBlock [
        ! @"Both C\# and (since very recently) Java feature anonymous functions"
        ! @"They are very handy whenever we need to implement an interface with a single method, such as \texttt{PerformAction} or similar."
      ]

    VerticalStack
      [
        TextBlock @"In C\# lambda functions have types:"

        ItemsBlock
          [
            ! @"\texttt{Func<T,U>} for a function that takes as input a parameter of type \texttt{T}, and which returns a value of type \texttt{U}"
            ! @"\texttt{Func<T1,T2,U>} for a function that takes as input parameters of type \texttt{T1} and \texttt{T2}, and which returns a value of type \texttt{U}"
            ! @"..."
          ]
        
        ItemsBlock
          [
            ! @"\texttt{Action<T>} for a function that takes as input a parameter of type \texttt{T}, and which returns nothing (\texttt{void})"
            ! @"\texttt{Action<T1,T2>} for a function that takes as input parameters of type \texttt{T1} and \texttt{T2}, and which returns nothing (\texttt{void})"
            ! @"..."
          ]
      ]

    VerticalStack
      [
        TextBlock @"In Java lambda functions have many more types (see the documentation for the full list):"

        ItemsBlock
          [
            ! @"\texttt{Function<T,R>} for a function that takes as input a parameter of type \texttt{T}, and which returns a value of type \texttt{R}"
            ! @"\texttt{BiFunction<T1,T2,R>} for a function that takes as input parameters of type \texttt{T1} and \texttt{T2}, and which returns a value of type \texttt{R}"
            ! @"\texttt{Predicate<T>} for a function that takes as input a parameter of type \texttt{T}, and which returns a boolean value"
            ! @"..."
          ]
      ]

    VerticalStack
      [
        TextBlock @"Declaration of lambda functions is quite simple: the parameters are separated from the body by an ASCII arrow."
        TextBlock @"Calling lambda functions is also simple: just brackets with the argument in C\#, and an appropriate method in Java (see documentation)."

        CSharpCodeBlock(TextSize.Small,
          genericLambdaFuncDecl "int" "int" "f" "x" (ret (var"x" .+ constInt 2)) >>
          staticMethodCall "Console" "WriteLine" [genericLambdaFuncCall "f" [constInt 10]] >>
          endProgram) |> Unrepeated
      ]

    CSharpStateTrace(TextSize.Small,
      genericLambdaFuncDecl "int" "int" "f" "x" (ret (var"x" .+ constInt 2)) >>
      staticMethodCall "Console" "WriteLine" [genericLambdaFuncCall "f" [constInt 10]] >>
      endProgram, RuntimeState<_>.Zero (constInt 1))

    TextBlock @"Live coding demo: generic lists with map, filter, and fold."

    Section("Conclusion")
    SubSection("Looking back")
    ItemsBlock
      [
        !"Generics make it possible to define a class once, but use it with multiple types as arguments"
        !"It is particularly useful for containers such as arrays, tuples, lists, etc."
        !"It is particularly useful for relationships such as functions"
      ]
  ]
