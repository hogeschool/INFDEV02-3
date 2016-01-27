let batchProcess source name title openPDF compileTwice =
    let pdf = name + ".pdf"
    let tex = name + ".tex"
    do System.IO.File.Delete pdf
    do System.IO.File.WriteAllText(tex, source |> SlideDefinition.generateLatexFile title)
    for i = 0 to (if compileTwice then 1 else 0) do
//      let p = System.Diagnostics.ProcessStartInfo("pdflatex.exe", "-synctex=1 -interaction=nonstopmode " + tex)
      let p = System.Diagnostics.ProcessStartInfo("pdflatex.exe", "-synctex=1 -interaction=batchmode " + tex)
      do p.UseShellExecute <- false
      do (System.Diagnostics.Process.Start p).WaitForExit()
    let final_pdf = System.IO.Path.Combine([|@"..\..\"; pdf|])
    try
      System.IO.File.Delete(final_pdf)
    with
    | e -> printfn "File delete complaint: %A" e
    try
      System.IO.File.Move(pdf, final_pdf)
    with
    | e -> printfn "File move complaint: %A" e
    do System.Console.ReadLine() |> ignore
    if openPDF then
      try
        do (System.Diagnostics.Process.Start(final_pdf)).WaitForExit()
      with
      | e -> printfn "Open PDF complaint: %A" e
    do System.IO.File.Delete tex

[<EntryPoint>]
let main argv = 
//  do batchProcess StateTraceSamples.slides "stateTraces" "State traces test" true false
  //do batchProcess FunctionalWeek1.slides "test" "Test" true false
  do batchProcess Week1.slides "week1" "Introduction" true false
  0
