@{
    Layout = "post";
    Title = "FParsec Palette";
    Date = "2017-05-20T08:20:41";
    Tags = "F# FParsec Parser Combinator Functional Programming";
    Description = "A palette / cheat-sheet for FParsec";
}

** FParsec: _Palette_ **
-------------------------------------------------

[FParsec](http://www.quanttec.com/fparsec/) is a beautiful [parser combinator](https://en.wikipedia.org/wiki/Parser_combinator)
library for F# (based on Haskell's [Parsec](https://wiki.haskell.org/Parsec)). Learning it means holding quite a lot of different
function names in your head, so here's an attempt to provide a single page cheat-sheet.

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
> * p >>= f
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
> * p1 <|> p2
> * choice seqOfParsers
> * p <|>% x
> * opt p
> * optional p

---

> ## Backtracking Parsers ##
> * attempt p
> * p1 >>? p2
> * p1 .>>? p2
> * p1 .>>.? p2
> * p1 >>=? p2

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
> * p <?> label
> * p <??> label
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