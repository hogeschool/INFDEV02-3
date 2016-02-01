module Working_together_as_teachers

open CommonLatex
open SlideDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
    Section("Introduction")
    SubSection("Agenda")
    ItemsBlock
      [
        !"What is central? Teacher, student, or subject?"
        !"Inspiring the student relationship with the subject"
        !"Accepting self, others, and a common body of knowledge"
        !"Reflection in groups"
      ]

    Section("What is central?")
    SubSection("The teacher?")
    ItemsBlock
      [
        !"Traditional idea of a single, expert teacher"
        !"Knowledge is a rain of ambrosia from the lips of the expert"
        !"The expert has devoted a life of study to the subject, and really is an expert"
        !"Students must adapt and accept it, without question"
      ]

    TextBlock "In the long run, the teacher might lose intellectual humility and contact with reality as he is not challenged anymore."

    SubSection("The student?")
    ItemsBlock
      [
        !"Progressive idea of everything centered around the student"
        !"Student determines pace, depth, and projects"
        !"Students success rate is much higher, and also feedback"
        !"Teachers must adapt and accept it, without question"
      ]

    TextBlock "(Resistance to) growth is painful, and students will actively avoid subjects that are too hard, as the system incentivises choosing something simpler."

    SubSection("The subject?")
    ItemsBlock
      [
        !"Positivistic idea: there is an objective reality that must be learnt"
        !"The subject determines the study, neither the teacher nor the student"
        !"Qualitative results can be very high, as students are required to satisfy general standards"
        !"Teachers and students must adapt and accept it, without question"
      ]

    TextBlock "Teachers do not get to teach their pet topic, and students are still forced: nobody is really happy."

    Section("Inspiring the student relationship with the subject")
    SubSection("A humble proposal")
    ItemsBlock
      [
        !"The centrality lies in the inspiration of a relationship between student and subject, objectively meant"
        !"This relationship is inspired and informed by the relationship between teacher and subject, explained and paraded"
      ]

    TextBlock "Teachers focus on their passion, but stay as close as possible to the ``reasonable ground''. Students are encouraged to find their own way through the subject, but within boundaries and playfully."

    Section("Accepting self, others, and a body of knowledge")
    SubSection("What about more teachers, everyone with...")
    ItemsBlock
      [
        !"...an own relationship with the subject"
        !"...an own interest"
        !"...own insecurities (``what if I cannot do this?'')"
        !"?"
      ]

    SubSection("Start by accepting important things")
    ItemsBlock
      [
        ! @"You have to relinquish a lot of control, but...\pause"
        !"...you are not alone in this new journey as a teacher"
      ]

    ItemsBlock
      [
        !"Accept that you cannot do everything equally well: let others do what they do better than you"
        !"Accept that you do some things well: require to do them"
        !"Accept that you are not infallible: change will bring you to the verge of mistakes, show students how to gracefully admit them"
      ]

    SubSection("Then move on to the subject(s)")
    ItemsBlock
      [
        !"There is a common, basic body of knowledge (it is usually written on the wall, somewhere)"
        !"It might not be ideal for you as a teacher, but it is what needs to be done"
        !"Deep in our hearts we all tend to know what it is"
      ]

    ItemsBlock
      [
        !"Disagreement means that something is not common: remove it from the courses"
        !"Use logic as the fundamental tool, not intuition or metaphors"
        ! @"Cute and intuitive are highly subjective: make it possible for everyone (other teachers \textbf{and} students) to follow"
      ]

    SubSection("And do not forget...")
    TextBlock "...we are here to inspire students, not entertain them or ourselves!"

    Section("Conclusion")
    SubSection("Recap")
    ItemsBlock
      [
        !"Centrality lies not by any of teacher, student, or subject"
        !"We are here to inspire a (personal) relationship between the student and the subject subject"
        !"We cannot do this if we do not accepting ourselves, others, and the reality of what we teach"
      ]

    SubSection("But if we do this right...")
    TextBlock "...we can change lives for ever!"

    Question "Questions?"

    Section("Let's get active now!")
    SubSection("In groups of three")
    TextBlock "Take some minutes for all the questions, and then let's discuss and present."

    VerticalStack[
        Question "Where do we stand on the triangle teacher-student-subject?"
        Pause
        Question "Are we succeeding in teamwork as teachers? Why/how so (or not)?"
        Pause
        Question "What are your strengths and weaknesses as a teacher? And according to others in your group?"
        Pause
        Question @"What can you, and only you, \textbf{offer} to students that they really need?"
        Pause
        Question @"What can you \textbf{not offer} to students that they really need?"
      ]
  ]
