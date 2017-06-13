@{
    Layout = "post";
    Title = "FParsec Palette";
    Date = "2017-05-20T08:20:41";
    Tags = "F# FParsec Parser Combinator Functional Programming";
    Description = "The single page reference to both FParsec and FParsec-Pipes that you've been waiting your whole life for";
}

** FParsec: _Palette_ **
-------------------------------------------------

**At last, the single page reference to both FParsec and FParsec-Pipes that you've been waiting your whole life for**.
[FParsec](http://www.quanttec.com/fparsec/) is an amazing [parser combinator](https://en.wikipedia.org/wiki/Parser_combinator)
library for F# written by Stephan Tolksdorf (and based on Haskell's [Parsec](https://wiki.haskell.org/Parsec)). Learning it means
holding quite a lot of different function names in your head, so here's an attempt to provide a single page cheat-sheet.

<div class="palette fewerColumnsPalette">

> ## Single Char Parsers ##
> * pchar c
> * skipChar c
> * charReturn c v
> * anyChar {+skip}
> * satisfy fcb {+skip}{+label}
> * anyOf s {+skip}
> * noneOf s {+skip}
> * letter {+ascii}
> * lower {+ascii}
> * upper {+ascii}
> * digit
> * hex
> * octal

---

> ## Numeric Parsers ##
> * pfloat
> * p[u]int8/16/32/64
> * numberLiteral options label

---

> ## Direct String Parsers ##
> * pstring s {+CI}
> * skipString s {+CI}
> * stringReturn s {+CI}
> * anyString n {+skip}
> * restOfLine b {+skip}
> * regex r
> * charsTillString s b n {+skip}{+CI}
> * identifier o
> * many[1]Satisfy fcb {+skip}
> * many[1]Satisfy2 fhcb ftcb {+skip}{+label}
> * manyMinMaxSatisfy n m fcb {+skip}{+label}
> * manyMinMaxSatisfy2 n m fhcb ftcp {+skip}{+label}

---

> ## Composite String Parsers ##
> * many[1]Chars cp
> * many[1]Chars2 cph cpt
> * manyCharsTill cp endp {+apply}
> * manyCharsTill2 cph cpt endp {+apply}
> * many[1]Strings sp
> * many[1]Strings2 sph spt
> * stringsSepBy sp sep
> * skipped p
> * withSkippedString f p

---

> ## Whitespace Parsers ##
> * newline {+skip}
> * unicodeNewLine {+skip}
> * newlineReturn v
> * unicodeNewlineReturn v
> * spaces[1] (includes newlines)
> * unicodeSpaces[1] (includes newlines)
> * eof

---

> ## Chaining Parsers ##
> * preturn x
> * p >>% x
> * p |>> f
> * p >>= fp
> * pl >>. pr (take right)
> * pl .>> pr (take left)
> * pl .>>. pr (take both)
> * between pLeft pRight p
> * tuple2/3/4/5 p1 p2 ...
> * pipe2/3/4/5 p1 p2 ... f

---

> ## Sequence Parsers ##
> * parray n p
> * skipArray n p
> * many[1] p {+skip}
> * sepBy[1] p pSep {+skip}
> * sepEndBy[1] p pSep {+skip}
> * many[1]Till p pEnd {+skip}
> * chainl[1] p pSepOp
> * chainr[1] p pSepOp

---

> ## Optional Parsers ##
> * p1 <|> pOnlyTryIfP1FailedOnFirstToken
> * choice seqOfParsers
> * p <|>% x
> * opt p
> * optional p

---

> ## Backtracking Parsers ##
> * attempt pAttemptButBacktrackIfFails
> * pBacktrackIfFails >>? pBacktrackIfFailsFirstToken
> * pBacktrackIfFails .>>? pBacktrackIfFailsFirstToken
> * pBacktrackIfFails .>>.? pBacktrackIfFailsFirstToken
> * pBacktrackIfFails >>=? fpBacktrackIfFailsFirstToken

---

> ## Conditional Look Ahead Parsers ##
> * notEmpty p
> * [not]followedBy p {+label}
> * notFollowedByEof
> * [not]followedByString[CI] s
> * next[2]CharSatisfies[Not] fcb
> * previousCharSatisfies[Not] fcb
> * lookAhead p

---

> ## Error Messaging Parsers ##
> * p < ? > label
> * p < ?? > label
> * fail msg
> * failFatally msg

---

> ## State Handling Parsers ##
> * getUserState
> * setUserState u
> * updateUserState f
> * userStateSatisfies f
> * getPosition

---

</div>

### _FParsec **Pipes**_ ###

[FParsec Pipes](https://github.com/rspeele/FParsec-Pipes) is an independant extension to FParsec, written by Robert Peele.
It adds some new operators and combinators that make it (arguably) more intuitive to translate a grammar into a parser.
The [documentation](http://rspeele.github.io/FParsec-Pipes/Intro.html) for Pipes is actually pretty concise, but I wanted
a single page reference to both libraries so I've documented some of it below.

<div class="palette fewerColumnsPalette">

> ## Default Parsers ##
> * %"a specific string" // pstring ...
> * %ci "case insensitive" // pstringCI ...
> * %'c' // pchar ...
> * %ci 'c' // pcharCI ...
> * %['a'; 'b'] // choice [...]
> * %["hello"; "there"] // choice [...]
> * %[pint32; pMyParser] // choice [...]

---

> ## Parsers For Types ##
> * %p< int > // pint32
> * %p< uint16 > // puint16
> * %p< float > // pfloat
> * %p< Position > // getPosition

---

> ## Pipes ##
> * %% // start a pipe
> * -- // build pipe
> * +. // capture element
> * -|> // empty captured pipe elements to func
> * -%> auto // empty captured pipe elements to tuple
> * ?- // wrap left side in an attempt
> * -? // attempts left side and then .>>.? right

---

> ## Repeats and Separations ##
> * pA * qty.[3..] // 3+ pAs
> * qty.[2..4] / ',' * pA // 2 to 4 pAs - sepBy comma
> * qty.[..5] / ',' * pA // up to 5 pAs - sepEndBy comma

---

</div>