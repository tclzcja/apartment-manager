$(document).ready(
    function () {
        $("#go").click(function () {
            $.ajax({
                url: 'http://localhost:8080/api/profile/register',
                type: "POST",
                data: {
                    User: {
                        Email: $("#email").val()
                    },
                    Name: "Hello",
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