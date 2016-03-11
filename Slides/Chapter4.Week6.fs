module Chapter4.Week6

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
        !"State machines"
        !"Entity/component"
        !"Option with higher order function accessors"
      ]

    Section("Introduction")
    SubSection("Agenda")
    ItemsBlock
      [
        !"In this lecture we perform a code-centered review of the topics seen so far"
        !"We will see (by coding them) a series of examples of polymorphism, generics, and higher order functions"
      ]


    Section("State machines")
    SubSection("Moving parts")
    ItemsBlock
      [
        ! @"State machines are all based on the implementation of the \texttt{StateMachine} interface, with methods:"
        ItemsBlock
          [
            ! @"\texttt{Step}, which returns \texttt{true} when it is done and \texttt{false} when it is still running"
            ! @"\texttt{Reset}, which resets the state machine to its initial state"
          ]
        ! @"Concrete implementations of \texttt{StateMachine} are (just for this example):"
        ItemsBlock
          [
            ! @"\texttt{Wait}, which waits for a given amount of time"
            ! @"\texttt{Repeat}, which repeats forever the state machine it gets as argument"
            ! @"\texttt{SayHello}, which prints hello on the screen just once"
          ]
      ]

    SubSection "Live code demo: state machines."

    Section("Entity/components")
    SubSection("Moving parts")
    ItemsBlock
      [
        ! @"An entity/component system is based on the composition of an entity by means of multiple generic components:"
        ! @"The entity in our example is a \texttt{Car}, which features a series of components:"
        ItemsBlock
          [
            ! @"\texttt{FuelTank}, which is an interface"
            ! @"\texttt{Engine}, which is an interface"
            ! @"\texttt{Wheels}, which is an interface"
          ]
        ! @"When creating a concrete \texttt{Car}, we must pass an instance of a concrete implementor of the above interfaces"
        ! @"The \texttt{Car} simply connects the components, but has no idea what they do precisely"
      ]

    SubSection "Live code demo: entity/components."

    Section("Option with higher order function accessors")
    SubSection("Moving parts")
    ItemsBlock
      [
        ! @"An option data type is a wrapper around a value of a generic type \texttt{T}, which might (or might not) be absent"
        ! @"The \texttt{Option<T>} interface has only one method:"
        ItemsBlock
          [
            ! @"\texttt{Visit}, which takes as input two functions: one to process the value, one to provide a fallback otherwise"
          ]
        ! @"There are only two concrete implementations of \texttt{Option<T>}:"
        ItemsBlock
          [
            ! @"\texttt{Some<T>}, which contains a value of type \texttt{T}"
            ! @"\texttt{None<T>}, which contains no value of type \texttt{T} and acts as a sort of strongly typed \texttt{null} value"
          ]
      ]

    SubSection "Option with higher order function accessors."

    Section("Conclusion")
    SubSection("Looking back")
    ItemsBlock
      [
        ! @"Polymorphism makes it possible to pass different data types to other contexts, as long as the conversion is safe"
        ! @"Generics make it possible to define a class once, but use it with multiple types as arguments"
        ! @"Their combination makes it possible to reach amazing levels of abstraction, but require careful thought to be used correctly"
        ! @"Use design (and UML-style reasoning) like violence: if it does not solve the problem, just use more"
      ]
  ]
