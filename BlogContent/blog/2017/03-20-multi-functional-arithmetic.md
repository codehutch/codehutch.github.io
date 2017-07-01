@{
    Layout = "post";
    Title = "multi-functional-arithmetic";
    Date = "2917-03-20T08:25:11";
    Tags = "";
    Description = "Arithmetic operations compared in various functional languages";
}

<script>hljs.initHighlightingOnLoad();</script>

** _multifunctional:_ ARITHMETIC **
---------------------------------------------------------------------

##### Types #####

<div class="flex">

~~~~haskell



~~~~

~~~~fsharp

let x:int = 32   // System.Int32
let x:int32 = 32 // System.Int32
let x:int64 = 64 // System.Int64


~~~~

~~~~scala


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

<script>

	function addCaption(ele) {

		var table = ele.parentElement.parentElement.parentElement.parentElement.parentElement;
		var caption = document.createElement("caption");
		var codeClass = ele.className.replace('hljs', '');

		caption.innerHTML = codeClass;
		table.insertBefore(caption, table.firstChild);
		table.className += ' ' + codeClass;
	}

	var codeBlocks = document.getElementsByTagName('code');

	for (var i = 0; i < codeBlocks.length; i++) {
		codeBlocks[i].className = codeBlocks[i].getAttribute('lang');
		hljs.highlightBlock(codeBlocks[i]);
		addCaption(codeBlocks[i]);
	}

</script>
