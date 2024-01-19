function myFunction() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
      td = tr[i].getElementsByTagName("td")[1];
      if (td) {
        txtValue = td.textContent || td.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
          tr[i].style.display = "";
        } else {
          tr[i].style.display = "none";
        }
      }       
    }
  }

  function myFunction() {
    var x = document.getElementById("myInput");
    if (x.type === "password") {
      x.type = "text";
    } else {
      x.type = "password";
    }
  }

  function sendemail(){
    Email.send({
    Host : "smtp.elasticemail.com",
    Username : "ingjesuscabrera@gmail.com",
    Password : "8F953C21A7384C95A31C76A8FE8418CA81D4",
    To : 'ingjesuscabrera@gmail.com',
    From : "ingjesuscabrera@gmail.com",
    Subject : "Job application",
    Body : "A job application has been processed"
}).then(
message => alert("We have sent your application to the human resources department, please close the window to continue applying for job offers")
);
}

