document.getElementById('t-shirts_button').onclick = function(){
	var hangs = document.getElementsByClassName('hang');
	for (var i = 0; i < hangs.length; i++) {
		hangs[i].parentElement.style.display = 'none';
	}
    var hangs = document.getElementsByClassName('scarves');
	for (var i = 0; i < hangs.length; i++) {
		hangs[i].parentElement.style.display = 'none';
	}
	var hangs = document.getElementsByClassName('sweaters');
	for (var i = 0; i < hangs.length; i++) {
		hangs[i].parentElement.style.display = 'none';
	}
    var hangs = document.getElementsByClassName('t-shirts');
	for (var i = 0; i < hangs.length; i++) {
		hangs[i].parentElement.style.display = 'inline-table';
	}
}