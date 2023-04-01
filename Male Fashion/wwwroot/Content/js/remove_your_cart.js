document.getElementById('button_remove').onclick = function(){
	var hangs = document.getElementsByClassName('san_pham');
	// for (var i = 0; i < hangs.length; i++) {
	// 	hangs[i].parentElement.style.display = 'none';
	// }
    hangs.parentElement.style.display = 'none';
}