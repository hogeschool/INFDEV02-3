let batchProcess source name title openPDF =
  let pdf = name + ".pdf"
  let tex = name + ".tex"
  do System.IO.File.Delete pdf
  do System.IO.File.WriteAllText(tex, source |> SlideDefinition.generateLatexFile title)
  let p = System.Diagnostics.ProcessStartInfo("pdflatex.exe", "-synctex=1 -interaction=nonstopmode " + tex)
  do p.UseShellExecute <- false
//  do p.RedirectStandardError <- true
//  do p.RedirectStandardInput <- true
//  do p.RedirectStandardOutput <- true
  do System.Diagnostics.Process.Start p |> ignore
  do System.Console.ReadLine() |> ignore
  if openPDF then
    do System.Diagnostics.Process.Start(pdf) |> ignore
  do System.IO.File.Delete tex

[<EntryPoint>]
let main argv = 
  do batchProcess Week1.slides "week1" "Introduction" true
  0
