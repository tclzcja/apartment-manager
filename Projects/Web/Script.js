// JavaScript Document

//Variable indicating the role of the user. 0: Not logged in, 1: Tenant, 2: Superintendent, 3: Owner

var role;

var main=function() {
	role=0;
	$("div").hide();
	$("#login").show();
	}

function connection() {
	//To add: Test of password relative to e-mail in the user base.
	if(true /*'true' to be replaced by actual test result*/) {
		//To add: Test to determine the role of the user, and potential case of multi-role.
		if (true /*'true' to be replaced by multi-role test result*/) {
			$("div").hide();
			$("#roleChoice").show();
			}
		else {
			//To add: case in which the user only has one role - then he connects to the appropriate role. MUST CALL accessXXXXMain() to adjust the role variable.
			}
		}
	else {
		//Case in which the user's e-mail/password don't match
		$("#deny").show();
		}
	}

function callHeader() {
	if (role==1) $("#headerTenant").show();
	if (role==2) $("#headerSuper").show();
	if (role==3) $("#headerOwner").show();
	}

function accessTenantMain() {
	role=1;
	$("div").hide();
	$("#headerTenant").show();
	$("#tenantMain").show();
	}

function accessSuperMain() {
	role=2;
	$("div").hide();
	$("#headerSuper").show();
	$("#superMain").show();
	}

function accessOwnerMain() {
	role = 3;
	$("div").hide();
	$("#headerOwner").show();
	$("#ownerMain").show();
	}

function WRCreationForm() {
	$("div").hide();
	callHeader();
	$("#WRCreationForm").show();
	}

function WRCreate() {
	//To add: code to save the work request content into the work request table
	displayOngoingRequests();
	}

function displayOngoingRequests() {
	$("div").hide();
	callHeader();
	$("#displayOngoingRequests").show();
	}

function backToMain() {
	if (role==1) accessTenantMain();
	if (role==2) accessSuperMain();
	if (role==3) accessOwnerMain();
	}

function detailViewWR() {
	$("div").hide();
	callHeader();
	$("#detailViewWR").show();
	if (role==2) $("#UpdateWR").show();
	}

function updateWR() {
	if (true /*To be replaced by a test to know whether the work request status is 2:First response or 3::Resolution*/) firstResponseScreen();
	else resolutionScreen();
	}

function firstResponseScreen() {
	$("div").hide();
	callHeader();
	$("#firstResponse").show();
	}

function resolutionScreen() {
	$("div").hide();
	callHeader();
	$("#resolution").show();
	}

function firstResponseSubmit() {
	//Add code to take into account the content of the submitted form and update the work request
	displayOngoingRequests();
	}

function resolutionSubmit() {
	//Add code to take into account the content of the submitted form and update the work request
	displayOngoingRequests();
	}

$(document).ready(main);