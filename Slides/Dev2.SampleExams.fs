module Dev2.SampleExams

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let exam1 = 
  [
    Section("Lists, functions, and iteration")
    SubSection("Question 1")
    TextBlock @"Complete the missing pieces of the \texttt{filterTooLarge} function to remove all elements greater than 100 from the input list (deﬁned as usual with \texttt{Empty} and \texttt{Node})."

    Paragraph("Guide to answering")
    ItemsBlock
      [
        ! @"Carefully read the question"
        ! @"The only input mentioned is the list, so the function cannot take any other parameters"
        ! @"To remove elements we can either return a new list, or modify the given list in place; returning a new list is usually simpler"
        ! @"The function is clearly very similar to \texttt{filter}: go, recursively, through all elements"
        ItemsBlock [
            ! @"If we reach the empty node, then we return \texttt{Empty()} as we have nothing to remove"
            ! @"Otherwise, we check the condition (in this case if the current element is greater than 100)"
            ItemsBlock [
                ! @"If it is so, then we simply return the removal of all further elements"
                ! @"Otherwise, we return a new node that includes the current value (we do not wish to discard it) and recurse on the next elements"
              ]
          ]
      ]

    PythonCodeBlock(TextSize.Small,
          (Hidden((classDef "Empty" 
                    [
                      def "__init__" ["self"] (endProgram)
                      def "IsEmpty" ["self"] (ret (constBool true))
                    ])) >>
              (Hidden((classDef "Node" 
                        [
                          def "__init__" ["self"; "x"; "xs"] (("self.Value" := var"x") >> ("self.Next" := var"xs"))
                          def "IsEmpty" ["self"] (ret (constBool false))
                        ])) >>
                    ((def "filterTooLarge" ["l"]
                        (ifelse (methodCall "l" "IsEmpty" [])
                            ((ret (newC "Empty" [])) >> endProgram)
                            (ifelse (var"l.Value" .> (constInt 100))
                               ((ret (call "filterTooLarge" [var"l.Next"])) >> endProgram)
                               ((ret (newC "Node" [var"l.Value"; call "filterTooLarge" [var"l.Next"] ]))) >> endProgram)
                        )) >>
                      endProgram))))

    SubSection("Question 2")
    TextBlock @"Complete the missing pieces of the \texttt{filterTooSmall} function to remove all elements smaller than 5 from the input list (deﬁned as usual with \texttt{Empty} and \texttt{Node})."

    Paragraph("Guide to answering")
    ItemsBlock
      [
        ! @"Carefully read the question"
        ! @"The only input mentioned is the list, so the function cannot take any other parameters"
        ! @"To remove elements we can either return a new list, or modify the given list in place; returning a new list is usually simpler"
        ! @"The function is clearly very similar to \texttt{filter}: go, recursively, through all elements"
        ItemsBlock [
            ! @"If we reach the empty node, then we return \texttt{Empty()} as we have nothing to remove"
            ! @"Otherwise, we check the condition (in this case if the current element is smaller than 5)"
            ItemsBlock [
                ! @"If it is so, then we simply return the removal of all further elements"
                ! @"Otherwise, we return a new node that includes the current value (we do not wish to discard it) and recurse on the next elements"
              ]
          ]
      ]

    PythonCodeBlock(TextSize.Small,
          (Hidden((classDef "Empty" 
                    [
                      def "__init__" ["self"] (endProgram)
                      def "IsEmpty" ["self"] (ret (constBool true))
                    ])) >>
              (Hidden((classDef "Node" 
                        [
                          def "__init__" ["self"; "x"; "xs"] (("self.Value" := var"x") >> ("self.Next" := var"xs"))
                          def "IsEmpty" ["self"] (ret (constBool false))
                        ])) >>
                    ((def "filterTooSmall" ["l"]
                        (ifelse (methodCall "l" "IsEmpty" [])
                            ((ret (newC "Empty" [])) >> endProgram)
                            (ifelse ((constInt 5) .> var"l.Value")
                               ((ret (call "filterTooSmall" [var"l.Next"])) >> endProgram)
                               ((ret (newC "Node" [var"l.Value"; call "filterTooSmall" [var"l.Next"] ]))) >> endProgram)
                        )) >>
                      endProgram))))

    SubSection("Question 3")
    TextBlock @"Complete the missing pieces of the \texttt{multiplyBy} function to multiply all elements of the input list (deﬁned as usual with \texttt{Empty} and \texttt{Node}) by the input number."

    Paragraph("Guide to answering")
    ItemsBlock
      [
        ! @"Carefully read the question"
        ! @"The two inputs mentioned are the list and a number, so the function cannot take any other parameters"
        ! @"To transform the elements we can either return a new list, or modify the given list in place; we show how to return a new list"
        ! @"The function is clearly very similar to \texttt{map}: go, recursively, through all elements"
        ItemsBlock [
            ! @"If we reach the empty node, then we return \texttt{Empty()} as we have no element to transform"
            ! @"Otherwise, we return a new node that includes the current value multiplied and recurse on the next elements"
          ]
      ]

    PythonCodeBlock(TextSize.Small,
          (Hidden((classDef "Empty" 
                    [
                      def "__init__" ["self"] (endProgram)
                      def "IsEmpty" ["self"] (ret (constBool true))
                    ])) >>
              (Hidden((classDef "Node" 
                        [
                          def "__init__" ["self"; "x"; "xs"] (("self.Value" := var"x") >> ("self.Next" := var"xs"))
                          def "IsEmpty" ["self"] (ret (constBool false))
                        ])) >>
                    ((def "multiplyBy" ["l"; "k"]
                        (ifelse (methodCall "l" "IsEmpty" [])
                            ((ret (newC "Empty" [])) >> endProgram)
                            ((ret (newC "Node" [(var"l.Value" .* var"k"); call "multiplyBy" [var"l.Next"; var"k"] ]))) >> endProgram)
                        )) >>
                      endProgram)))

    SubSection("Question 4")
    TextBlock @"Write a loop that multiplies all elements of a list \texttt{l} which are greater than zero."

    Paragraph("Guide to answering")
    ItemsBlock
      [
        ! @"Carefully read the question"
        ! @"The only variable mentioned is a list \texttt{l}, so assume it is declared and initialized"
        ! @"To multiply elements we must store the product so far; we need a variable for this, which is initialized to 1"
        ! @"We loop through all nodes of the list:"
        ItemsBlock [
            ! @"If we reach the empty node, then we are done and we stop the loop"
            ! @"Otherwise, we check the condition (in this case if the current element is greater than zero)"
            ! @"If it is so, then we simply multiply it by the \texttt{product} variable"
            ! @"After the check, we move to the next element"
          ]
      ]

    PythonCodeBlock(TextSize.Small,
          (Hidden((classDef "Empty" 
                    [
                      def "__init__" ["self"] (endProgram)
                      def "IsEmpty" ["self"] (ret (constBool true))
                    ])) >>
              (Hidden((classDef "Node" 
                        [
                          def "__init__" ["self"; "x"; "xs"] (("self.Value" := var"x") >> ("self.Next" := var"xs"))
                          def "IsEmpty" ["self"] (ret (constBool false))
                        ])) >>
                    (whiledo (methodCall "l" "IsEmpty" [] .= constBool false)
                      (
                        ((ifthen (var"l.Value" .> constInt 0)
                            ("product" := (var"product" .* var"l.Value"))
                        ) >>
                          ("l" := var"l.Next"))
                      )
                    ) >>
                      endProgram)))

    Section("Stack and heap")
    SubSection("Question 1")
    TextBlock @"Show the stack and the heap at all steps of the execution of the following function:"
    Paragraph("Guide to answering")
    ItemsBlock
      [
        ! @"Carefully read the code; take five minutes to get an idea of what the function does"
        ! @"Begin following the code, changing the variables and the PC as needed"
        ! @"Remember that whenever you encounter a function call (so also for recursion) you need to put: "
        ItemsBlock[
           ! @"another PC"
           ! "all parameters of the function"
           ! @"the place to put the return value of the fuction on the stack (in the following we call it \texttt{ret}, but use whatever name you wish: also the name of the called function might do it)"
        ]
        ! @"Only the last (rightmost) locations in the stack change, so you might choose to not rewrite those that stay the same"
        ! @"Show all PC's though, as those identify the path that code has taken"
        ! @"Do not get stuck on notation; as long as you show all the relevant values, your answer will be accepted"
      ]

    PythonStateTrace(TextSize.Small,
          ((def "f" ["n"]
              (ifelse (var"n" .> constInt 1)
                  (ret (constInt 1 .+ (call "f" [var"n" ./ constInt 2])))
                  ((ret (constInt 0)))
              )) >>
            (call "print" [call "f" [constInt 3]]) >> endProgram),
          Runtime.RuntimeState<_>.Zero (constInt 1))

    SubSection("Question 2")
    TextBlock @"Show the stack and the heap at all steps of the execution of the following function:"
    Paragraph("Guide to answering")
    TextBlock @"See above"

    PythonStateTrace(TextSize.Small,
          ((def "f" ["n"]
              (ifelse (var"n" .> constInt 1)
                  (ret (var"n" .* (call "f" [var"n" .- constInt 1])))
                  ((ret (constInt 1)))
              )) >>
            (call "print" [call "f" [constInt 3]]) >> endProgram),
          Runtime.RuntimeState<_>.Zero (constInt 1))
  ]
