$(document).ready(
    function () {

        Pool.Reset();
        Auth.Reset();

        $("#go").click(function () {

            var info = {
                User: {
                    Email: $("#email").val()
                },
                Name: "Hello",
            };

            Pool.Core("profile", "register", info, function () {

                alert("Great");
            });

        })

        $("#login").click(function () {
            Login();
        })

        $("#bcreate").click(function () {
            BCreate();
        })

        $("#acreate").click(function () {
            ACreate();
        })

        $("#rcreate").click(function () {
            RCreate();
        })

        $("#FDSA").click(function () {

            Pool.Core("profile", "multiple", null, function (data) {
                alert(JSON.stringify(data));
            })

            Pool.Core("apartment", "multiple", null, function (data) {
                alert(JSON.stringify(data));
            })

            Pool.Core("building", "multiple", null, function (data) {
                alert(JSON.stringify(data));
            })

        })
    }
)

function Login() {

    var identity = {
        grant_type: "password",
        Username: $("#email").val(),
        Password: $("#password").val()
    }

    Pool.Core("token", "", identity, function (data) {
        Auth.Auth(data.access_token, $("#email").val(), function () { alert("YES!") })
    })

}

function BCreate() {

    var info = {
        Name: $("#bname").val(),
        Address: $("#baddress").val(),
        Superintendents: [
            {
                ID: Auth.Current.Profile().ID
            }
        ]
    }

    Pool.Core("building", "create", info, function (data) {
        alert("Building!");
    })

}

function ACreate() {

    var info = {
        Number: $("#anumber").val(),
        BuildingID: $("#abid").val()
    }

    Pool.Core("apartment", "create", info, function (data) {
        alert("App!!!!!");
    })

}

function RCreate() {

    var info = {
        ApartmentID: $("#raid").val(),
        RequestTenantID: $("#rpid").val(),
        Category: 0,
        Sub: 0
    }

    Pool.Core("request", "create", info, function (data) {
        alert("Request!!!!!!!!!!!!!!");
    })

}