var inputs = document.forms['Register'].getElementsByTagName('input');
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
          // Check other issue
           if(id == 'email'){
                if(reg_mail.test(value) == false){ span.innerHTML ='Email is not valid';}
                var email =value;
          }
       // Check password
       if(id == 'password'){
            if(value.length <6){span.innerHTML ='Password must have at least 6 letters';}
            var pass =value;
       }
       // Check repassword 
       if(id == 'confirm_pass' && value != pass){span.innerHTML ='Password didn not match';}
       // Check if phone number is valid
       if(id == 'phone' && isNaN(value) == true){span.innerHTML ='Phone number is not valid';}
      }

      // If have errors then run onchange, submit return false, highlight border
      if(span.innerHTML != ''){
           inputs[i].parentNode.appendChild(span);
           errors = true;
           run_onchange = true;
           inputs[i].style.border = '1px solid #c6807b';
           inputs[i].style.background = '#fffcf9';
      }
     }// end for

     if(errors == false) {
         alert('Successfully register! Now login to our page');
         window.location.href = "./login.html";
     }
     return !errors;
}// end valid()

// Chạy hàm kiểm tra valid()
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