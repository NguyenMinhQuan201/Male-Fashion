var inputs = document.forms['login'].getElementsByTagName('input');
  		var run_onchange = false;
  		function valid(){
   			var errors = false;
   			var reg_mail = /^[A-Za-z0-9]+([_\.\-]?[A-Za-z0-9])*@[A-Za-z0-9]+([\.\-]?[A-Za-z0-9]+)*(\.[A-Za-z]+)+$/;
   			for(var i=0; i<inputs.length; i++){
	    		var value = inputs[i].value;
	    		var id = inputs[i].getAttribute('id');   
	    		// Initialize span
	    		var span = document.createElement('span');
	    		// If span was exist then remove
	    		var p = inputs[i].parentNode;
	    		if(p.lastChild.nodeName == 'SPAN') {p.removeChild(p.lastChild);}
	   			// Check empty issue
	    		if(value == ''){
	     			span.innerHTML ='Must be completed';
	    		}else{
	    			//Check other issue
	     			if(id == 'password'){
	      				if(value.length <6){span.innerHTML ='Password must have at least 6 letters';}
	      				var pass =value;
	     			}
	    		}
	    		//If have errors then run onchange, submit return false, highlight border
	    		if(span.innerHTML != ''){
	     			inputs[i].parentNode.appendChild(span);
	     			errors = true;
	     			run_onchange = true;
	     			inputs[i].style.border = '1px solid #c6807b';
	     			inputs[i].style.background = '#fffcf9';
	    		}
   			}// end for
  
   			if(errors == false) {
   				alert('Welcome back!!');
   				window.location.href = "./loginedhome.html";
   			}
   			return !errors;
  		}// end valid()

  		// Run valid()
  		var register = document.getElementById('submit');
  		register.onclick = function(){
   			return valid();
  		}
 
  		// Onchange event
   		for(var i=0; i<inputs.length; i++){
    		var id = inputs[i].getAttribute('id');
    		inputs[i].onchange = function(){
     			if(run_onchange == true){
      				this.style.border = '1px solid #999';
      				this.style.background = '#fff';
      				valid();
     			}
    		}

   		}// end for

   		$(function(){ //ready function
    		$('#forgotpass').click(function(){ //click event
         		alert("We have e-mailed your password reset link!");
    		});
		})