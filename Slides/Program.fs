[<EntryPoint>]
let main argv = 
  System.IO.File.WriteAllText("week1.tex", Week1.slides |> SlideDefinition.generateLatexFile "Introduction")
  0
