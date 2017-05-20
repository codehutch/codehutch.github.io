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
function names in your head, so Here's my attempt to provide a one screen cheat-sheet.

<div class="palette">

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

> ## Direct String Parsers ##
> * pstring s {+CI}
> * skipString s {+CI}
> * stringReturn s {+CI}
> * anyString n {+skip}
> * restOfLine b {+skip}
> * charsTillString s b n {+skip}{+CI}
> * manySatisfy fcb {+skip}
> * many1Satisfy fcb {+skip}
> * manySatisfy2 fhcb ftcb {+skip}{+label}
> * many1Satisfy2 fhcb ftcb {+skip}{+label}
> * manyMinMaxSatisfy n m fcb {+skip}{+label}
> * manyMinMaxSatisfy2 n m fhcb ftcp {+skip}{+label}
> * regex r
> * identifier o

---

</div>