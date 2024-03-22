document.getElementById("signup-link").addEventListener("click", function() {
    document.getElementById("login-form").style.display = "none";
    document.getElementById("signup-form").style.display = "block";
  });
  
  document.getElementById("login-link").addEventListener("click", function() {
    document.getElementById("signup-form").style.display = "none";
    document.getElementById("login-form").style.display = "block";
  });
  
  document.getElementById("login-form").addEventListener("submit", function(e) {
    e.preventDefault();
    // TODO: Handle login logic here
  });
  
  document.getElementById("signup-form").addEventListener("submit", function(e) {
    e.preventDefault();
    // Get form values
    var name = document.getElementById("signup-name").value;
    var email = document.getElementById("signup-email").value;
    var password = document.getElementById("signup-password").value;
    var phone = document.getElementById("signup-phone").value;
    var address = document.getElementById("signup-address").value;
    
    // TODO: Validate and process the form data
    // Example validation - checking if required fields are not empty
    if (name && email && password && phone && address) {
      // Form is valid, proceed with signup logic
      // TODO: Add your signup logic here
      
      // Clear form fields
      document.getElementById("signup-name").value = "";
      document.getElementById("signup-email").value = "";
      document.getElementById("signup-password").value = "";
      document.getElementById("signup-phone").value = "";
      document.getElementById("signup-address").value = "";
      
      // Display success message or redirect to a new page
      alert("Signup successful!");
    } else {
      // Display error message or handle invalid form data
      alert("Please fill in all the required fields!");
    }
  });
  