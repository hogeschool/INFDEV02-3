open Compile

[<EntryPoint>]
let main argv = 
  //do batchProcess Chapter1.Week1_2.slides "dev3_week1_2" "The INFDEV team" "Introduction" true false
  //do batchProcess Chapter1.Week3.slides "dev3_week3" "The INFDEV team" "Type systems" true false
  do batchProcess StateTraceSamples.slides "test" "The INFDEV team" "test" true false
  0
