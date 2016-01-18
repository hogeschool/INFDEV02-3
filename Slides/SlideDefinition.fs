module SlideDefinition
open CodeDefinition

let beginFrame = @"\begin{frame}[fragile]{\CurrentSection}" + "\n"
let endFrame = "\n" + @"\end{frame}" + "\n\n"
let beginBlock = @"\begin{block}{\CurrentSubSection}" + "\n"
let endBlock = "\n" + @"\end{block}" + "\n\n"
let beginExampleBlock = @"\begin{exampleblock}{}" + "\n"
let endExampleBlock = "\n" + @"\end{exampleblock}" + "\n\n"
let beginItemize = @"\begin{itemize}" + "\n"
let endItemize = "\n" + @"\end{itemize}" + "\n"
let beginCode = @"\begin{lstlisting}" + "\n"
let endCode = "\n" + @"\end{lstlisting}" + "\n"
let beginMath = @"$$"
let endMath = @"$$" + "\n"

type SlideElement = 
  | Section of string 
  | Advanced of SlideElement
  | SubSection of string 
  | Pause
  | Question of string
  | InlineCode of string
  | Text of string
  | Block of SlideElement
  | Items of List<SlideElement>
  | PythonCodeBlock of Code
  | TypingRules of List<TypingRule>
  | CSharpCodeBlock of Code
  | VerticalStack of List<SlideElement>
  | PythonStateTrace of Code * RuntimeState
  with
    member this.ToStringAsElement() = 
      match this with
      | Pause -> @"\pause"
      | Question q -> 
          sprintf @"%s%s%s" beginExampleBlock q endExampleBlock
      | InlineCode c -> sprintf @"\texttt{%s}" c
      | Text t -> t
      | Block t ->
          sprintf @"%s%s%s" beginExampleBlock (t.ToStringAsElement()) endExampleBlock
      | Items items ->
          sprintf @"%s%s%s" beginItemize (items |> Seq.map(fun item -> @"\item " + item.ToStringAsElement() + "\n") |> Seq.fold (+) "" ) endItemize
      | PythonCodeBlock c ->
          sprintf @"%s%s%s" beginCode (c.AsPython "") endCode
      | TypingRules tr ->
          let trs = tr |> List.map (fun t -> t.ToString())
          (List.fold (+) "" trs)
      | VerticalStack ses ->
          let sess = ses |> List.map (fun se -> se.ToStringAsElement() + " \n")
          (List.fold (+) "" sess)
      | _ -> ""
    override this.ToString() =
      match this with
      | Section t ->
        sprintf @"\SlideSection{%s}%s" t "\n"
      | SubSection t ->
        sprintf @"\SlideSubSection{%s}%s" t "\n"
      | Advanced se ->
        //\footnote{Warning: this material is to be considered advanced!}
        sprintf @"%s 
                  %s%s" beginFrame (se.ToStringAsElement()) endFrame
      | Block t ->
          sprintf @"%s%s%s%s%s" beginFrame beginBlock (t.ToStringAsElement()) endBlock endFrame
      | Pause -> @"\pause"
      | Question q ->
          sprintf @"%s%s%s" beginFrame q endFrame
      | InlineCode c ->
          sprintf @"%s\texttt{%s}%s" beginCode c endCode
      | Text t -> t
      | Items items ->
          sprintf @"%s%s%s%s%s" beginFrame beginItemize (items |> Seq.map(fun item -> @"\item " + item.ToStringAsElement() + "\n") |> Seq.fold (+) "" ) endItemize endFrame
      | PythonCodeBlock c ->
          sprintf @"%s%s%s%s%s" beginFrame beginCode (c.AsPython "") endCode endFrame
      | TypingRules tr ->
          let trs = tr |> List.map (fun t -> t.ToString())
          sprintf @"%s%s%s" beginFrame (List.fold (+) "" trs) endFrame
      | VerticalStack ses ->
          let sess = ses |> List.map (fun se -> se.ToStringAsElement() + " \n")
          sprintf @"%s%s%s" beginFrame (List.fold (+) "" sess) endFrame
      | PythonStateTrace(c,st) ->
        ""
      | _ -> ""

and TypingRule =
  {
    Premises : List<string>
    Conclusion : string
  }
  with 
    override this.ToString() =
      let ps = 
        match this.Premises |> List.map ((+) "\ ") with
        | [] -> ""
        | ps -> ps |> List.reduce (fun a b -> a + "\wedge" + b)
      sprintf @"%s\frac{%s}{%s}%s" beginMath ps this.Conclusion endMath

let (!) = Text
let (!!) = InlineCode
let ItemsBlock l = l |> Items |> Block 
let TextBlock l = l |> Text |> Block 

let rec generateLatexFile title (slides:List<SlideElement>) =
  @"\documentclass{beamer}
\usetheme[hideothersubsections]{HRTheme}
\usepackage{beamerthemeHRTheme}
\usepackage[utf8]{inputenc}
\usepackage{graphicx}
\usepackage[space]{grffile}
\usepackage{listings}
\lstset{language=C,
basicstyle=\ttfamily\footnotesize,
mathescape=true,
breaklines=true}
\lstset{
  literate={ï}{{\""i}}1
           {ì}{{\`i}}1
}

\title{" + title + @"}

\author{TEAM INFDEV}

\institute{Hogeschool Rotterdam \\ 
Rotterdam, Netherlands}

\date{}

\begin{document}
\maketitle
" + (List.map (fun x -> x.ToString()) slides |> List.fold (+) "") + @"
\begin{frame}{This is it!}
\center
\fontsize{18pt}{7.2}\selectfont
The best of luck, and thanks for the attention!
\end{frame}

\end{document}"
  