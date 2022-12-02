


function ValidateTeacher() {

	var IsValid = true;
	var ErrorMsg = "";
	var ErrorBox = document.getElementById("ErrorBox");
	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var Employeenumber = document.getElementById('Employeenumber').value;
	var Salary = document.getElementById('Salary').value;

	if (TeacherFname.length < 2) {
		IsValid = false;
		ErrorMsg += "First Name Must be 2 or more characters.<br>";
	}
	if (TeacherLname.length < 2) {
		IsValid = false;
		ErrorMsg += "Last Name Must be 2 or more characters.<br>";
	}
	if (!ValidateEmpNum(Employeenumber)) {
		IsValid = false;
		ErrorMsg += "Please Enter a valid Employeenumber.<br>";
	}

	if (isNaN(Salary)) {
		IsValid = false;
		ErrorMsg += "Please Enter a valid Salary.<br>";

	}
	if (!IsValid) {
		ErrorBox.style.display = "block";
		ErrorBox.innerHTML = ErrorMsg;
	} else {
		ErrorBox.style.display = "none";
		ErrorBox.innerHTML = "";
	}


	return IsValid;
}



function AddTeacher() {

	//check for validation straight away
	var IsValid = ValidateTeacher();
	if (!IsValid) return;
	//goal: send a request which looks like this:
	//POST : http://localhost:53613/api/TeacherData/AddTeacher
	//with POST data of authorname, bio, email, etc.

	var URL = "http://localhost:53613/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var Employeenumber = document.getElementById('Employeenumber').value;
	var Salary = document.getElementById('Salary').value;


	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"Employeenumber": Employeenumber,
		"Salary": Salary
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished
			
			//nothing to render, the method returns nothing.
		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));
	if (window.confirm("New teacher information has added. Do you want to go back?")) {
		window.open("List"); 
		
	}
}



function UpdateTeacher(TeacherId) {

	//check for validation straight away
	var IsValid = ValidateTeacher();
	if (!IsValid) return;

	//goal: send a request which looks like this:
	//POST : http://localhost:53613/api/TeacherData/UpdateTeacher/{id}
	//with POST data of authorname, bio, email, etc.

	var URL = "http://localhost:53613/api/TeacherData/UpdateTeacher/" + TeacherId;

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?


	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var HireDate = document.getElementById('HireDate').value;
	var Employeenumber = document.getElementById('Employeenumber').value;
	var Salary = document.getElementById('Salary').value;



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"HireDate": HireDate,
		"Employeenumber": Employeenumber,
		"Salary": Salary
	};

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished
			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));
	if (window.confirm("Teacher information has updateded. Do you want see the changes?")) {

		window.open("http://localhost:53613/Teacher/Show/" + TeacherId); 


	}
	
}


function ValidateEmpNum(emp) {
	const re = /\w\d{3}/;
	return re.test(String(emp).toLowerCase());
}

