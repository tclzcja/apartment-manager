$(document).ready(
    function () {
        $("#go").click(function () {
            alert();
            $.ajax({
                url: 'http://localhost:8080/api/profile/register',
                type: "POST",
                data: {
                    User: {
                        Email: $("#email").val()
                    },
                    Name: "Hello",
                    Role: 0
                },
                statusCode: {
                    200: function (data) {
                        alert("Great");
                    }
                }
            })
        })
    }
)