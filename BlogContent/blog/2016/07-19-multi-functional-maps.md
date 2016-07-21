@{
    Layout = "post";
    Title = "multi-functional-maps";
    Date = "2016-07-16T08:38:02";
    Tags = "";
    Description = "Maps in Haskell / F# / Scala / Clojure";
}

** _multifunctional:_ MAPS **
---------------------------------------------------------------------

##### Creating #####

<div class="flex">

~~~~haskell

import qualified Data.Map as Map

let bovineness = Map.insert "Cow" 9 . Map.insert "Car" 0 $ Map.empty

let fruitAcceptability = Map.fromList [("Apple", 4),("Orange",7)]

~~~~

~~~~fsharp

let annoyingness = Map.empty
                      .Add("Fish", 0)
                      .Add("Bee", 9)

let niceness = [("Cream Egg", 7); ("Mini Egg", 8)] |> Map.ofList

~~~~

~~~~scala

var annoyingness=Map("Cat" -> 3, "Dog" -> 7)

~~~~

~~~~clojure

~~~~

</div>

### _Some aspect **of** Maps_ ###

##### Stuff #####

<div class="flex">

~~~~haskell

~~~~

~~~~fsharp

~~~~

~~~~scala

~~~~

~~~~clojure

~~~~

</div>

#### _Some other aspect **of** Maps_ ####

##### More Stuff #####

<div class="flex">

~~~~haskell

~~~~

~~~~fsharp

~~~~

~~~~scala

~~~~

~~~~clojure

~~~~

</div>
