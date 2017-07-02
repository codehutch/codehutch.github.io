function addCodeBlockCaption(ele) {

	var table = ele.parentElement.parentElement.parentElement.parentElement.parentElement;
	var caption = document.createElement("caption");
	var codeClass = ele.className.replace('hljs', '');

	caption.innerHTML = codeClass;
	table.insertBefore(caption, table.firstChild);
	table.className += ' ' + codeClass;
}

function initMultiFunctionalCodeBlocks() {

	var codeBlocks = document.getElementsByTagName('code');

	for (var i = 0; i < codeBlocks.length; i++) {
		codeBlocks[i].className = codeBlocks[i].getAttribute('lang');
		hljs.highlightBlock(codeBlocks[i]);
		addCodeBlockCaption(codeBlocks[i]);
	}

}