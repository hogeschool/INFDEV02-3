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
    do System.Console.ReadLine() |> ignore
    if openPDF then
      try
        do (System.Diagnostics.Process.Start(pdf)).WaitForExit()
      with
      | e -> printfn "Complaint: %A" e
    do System.IO.File.Delete tex

[<EntryPoint>]
let main argv = 
  do batchProcess StateTraceSamples.slides "week1" "State traces test" true false
//  do batchProcess Week1.slides "week1" "Introduction" true false
  0
