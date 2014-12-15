$(document).ready(
    function () {

        Go_Login();

    }
)

function Go_Login() {

    Auth.Reset();
    Pool.Reset();

    $("article").hide();
    $("#login").show();

    $("#login-submit").off().on("click", function () {

        var info = {
            grant_type: "password", // it needs to be this, don't change
            Username: $("#login-email").val(),
            Password: $("#login-password").val()
        }

        Pool.Core("token", "", info, function (data) {
            Auth.Auth(data.access_token, $("#login-email").val(), function () {
                Go_Homepage();
            })
        });

    })

    $("#login-register").off().on("click", function () {

        var info = {
            User: {
                Email: $("#login-email").val() // the email address
            },
            Name: $("#login-name").val(), // the name
        }

        Pool.Core("profile", "register", info, function () {
            alert("Successfully Registered!");
        });

    })

}

function Go_Homepage() {

    $("article").hide();
    $("#homepage").show();

    if (Auth.Current.Profile().IsSuperintendent == false) {
        $("#homepage-building").hide();
        $("#homepage-apartment").hide();
        $("#homepage-user").hide();
    }

    $("#homepage-building").off().on("click", function () {
        Go_Building();
    });

    $("#homepage-apartment").off().on("click", function () {
        Go_Apartment();
    });

    $("#homepage-user").off().on("click", function () {
        Go_User();
    });

    $("#homepage-request").off().on("click", function () {
        Go_Request();
    });

    $("#homepage-logout").off().on("click", function () {
        Go_Login();
    });

    $("#homepage-lvlup").off().on("click", function () {

        var info = {
            ID: Auth.Current.Profile().ID
        }

        Pool.Core("profile", "lvlup", info, function () {
            Go_Login();
        });
    });

}

function Go_Building() {

    function Render_Table() {
        var ticket = {
            ID: Auth.Current.Profile().ID
        };
        Pool.Core("building", "multiple/profile", ticket, function (data) {
            $("#building-table tbody").html("");
            for (var i = 0; i < data.length; i++) {
                var TR = document.createElement("tr");
                $(TR).html("<td>" + data[i].Name + "</td><td>" + data[i].Address + "</td><td>" + data[i].Apartments.length + "</td><td class='delete' data-id='" + data[i].ID + "'>delete</td>").appendTo("#building-table tbody");
            }
            $("#building-table td.delete").on("click", function () {
                var info = {
                    ID: $(this).attr("data-id")
                }
                Pool.Core("building", "delete", info, function () {
                    Render_Table();
                });
            });
        });
    }

    Render_Table();

    $("article").hide();
    $("#building").show();

    $("#building-create-back").off().on("click", function () {
        Go_Homepage();
    });

    $("#building-create-submit").off().on("click", function () {

        var info = {
            Name: $("#building-create-name").val(),
            Address: $("#building-create-address").val(),
            Superintendents: [
                {
                    ID: Auth.Current.Profile().ID
                }
            ]
        }

        Pool.Core("building", "create", info, function (data) {
            Render_Table();
        });

    });

}

function Go_Apartment() {

    function Render_Table() {
        $("#apartment-create-building").html("");
        var ticket = {
            ID: Auth.Current.Profile().ID
        };
        Pool.Core("building", "multiple/profile", ticket, function (data) {
            for (var i = 0; i < data.length; i++) {
                var O = document.createElement("option");
                $(O).text(data[i].Name).val(data[i].ID).appendTo("#apartment-create-building");
            }
        });
        $("#apartment-table tbody").html("");
        Pool.Core("apartment", "multiple/profile", ticket, function (data) {
            for (var i = 0; i < data.length; i++) {
                var TR = document.createElement("tr");
                $(TR).html("<td>" + data[i].Number + "</td><td>" + data[i].Building.Name + "</td><td class='delete' data-id='" + data[i].ID + "'>delete</td>").appendTo("#apartment-table tbody");
            }
            $("#apartment-table td.delete").on("click", function () {
                var info = {
                    ID: $(this).attr("data-id")
                }
                Pool.Core("apartment", "delete", info, function () {
                    Render_Table();
                });
            });
        });
    }

    Render_Table();

    $("article").hide();
    $("#apartment").show();

    $("#apartment-create-back").off().on("click", function () {
        Go_Homepage();
    });

    $("#apartment-create-submit").off().on("click", function () {

        var info = {
            Number: $("#apartment-create-number").val(),
            BuildingID: $("#apartment-create-building :selected").val()
        }

        Pool.Core("apartment", "create", info, function (data) {
            Render_Table();
        });
    });
}

function Go_User() {

    function Render_Table() {
        $("#user-assign-apartment").html("");
        $("#user-assign-building").html("");
        var ticket = {
            ID: Auth.Current.Profile().ID
        };
        Pool.Core("building", "multiple/profile", ticket, function (data) {
            for (var i = 0; i < data.length; i++) {
                var B = document.createElement("option");
                $(B).text(data[i].Name).val(data[i].ID).appendTo("#user-assign-building");

                for (var j = 0; j < data[i].Apartments.length; j++) {
                    var O = document.createElement("option");
                    $(O).text(data[i].Name + " - " + data[i].Apartments[j].Number).val(data[i].Apartments[j].ID).appendTo("#user-assign-apartment");
                }
            }
        })
        $("#user-table tbody").html("");
        Pool.Core("profile", "multiple", null, function (data) {
            for (var i = 0; i < data.length; i++) {
                var TR = document.createElement("tr");
                $(TR).html("<td>" + data[i].Name + "</td><td>" + data[i].Apartments.length + "</td><td>" + data[i].Buildings.length + "</td><td><input type='checkbox' value='" + data[i].ID + "' /></td>").appendTo("#user-table tbody");
            }
        })
    }

    Render_Table();

    $("article").hide();
    $("#user").show();

    $("#user-assign-apartment-assign").off().on("click", function () {
        var _hit = 0;
        function _hitter() {
            _hit++;
            if (_hit >= $("#user-table input:checked").length) {
                Render_Table();
            }
        }
        $("#user-table input:checked").each(function () {
            var info = {
                ID: $(this).val(),
                Apartments: [
                    {
                        ID: $("#user-assign-apartment :selected").val()
                    },
                ]
            }

            Pool.Core("profile", "assign/apartment", info, function () {
                _hitter();
            })
        })
    });

    $("#user-assign-building-assign").off().on("click", function () {
        var _hit = 0;
        function _hitter() {
            _hit++;
            if (_hit >= $("#user-table input:checked").length) {
                Render_Table();
            }
        }
        $("#user-table input:checked").each(function () {
            var info = {
                ID: $(this).val(),
                Buildings: [
                    {
                        ID: $("#user-assign-building :selected").val()
                    },
                ]
            }

            Pool.Core("profile", "assign/building", info, function () {
                _hitter();
            })
        })
    });

    $("#user-assign-back").off().on("click", function () {
        Go_Homepage();
    });

}

function Go_Request() {

    function Render_Table() {
        if (Auth.Current.Profile().IsSuperintendent == false) {
            $("#request-table-my").hide();
        }
        $("#request-create-apartment").html("");
        var ticket = {
            ID: Auth.Current.Profile().ID
        };
        Pool.Core("apartment", "multiple/profile", ticket, function (data) {
            for (var i = 0; i < data.length; i++) {
                var O = document.createElement("option");
                $(O).text(data[i].Building.Name + " - " + data[i].Number).val(data[i].ID).appendTo("#request-create-apartment");
            }
            $("#request-table-new tbody, #request-table-responsed tbody, #request-table-done tbody, #request-table-my tbody").html("");
            Pool.Core("request", "multiple", ticket, function (data) {
                for (var i = 0; i < data.length; i++) {
                    var TR = document.createElement("tr");
                    $(TR).html("<td>" + $("#request-create-apartment option[value='" + data[i].ApartmentID + "']").text() + "</td><td>" + $("#request-create-category option[value='" + data[i].Category + "']").text() + "</td><td>" + data[i].RequestTime + "</td>");
                    switch (data[i].Status) {
                        case 1: {
                            $(TR).appendTo("#request-table-new tbody");
                            break;
                        }
                        case 2: {
                            $(TR).appendTo("#request-table-responsed tbody");
                            break;
                        }
                        case 3: {
                            $(TR).appendTo("#request-table-done tbody");
                            break;
                        }
                    }
                }
            })

            Pool.Core("request", "multiple/profile", ticket, function (data) {
                for (var i = 0; i < data.length; i++) {
                    var TR = document.createElement("tr");
                    $(TR).html("<td>" + $("#request-create-apartment option[value='" + data[i].ApartmentID + "']").text() + "</td><td>" + $("#request-create-category option[value='" + data[i].Category + "']").text() + "</td><td>" + data[i].RequestTime + "</td><td>" + data[i].Status + "</td>").appendTo("#request-table-my tbody");
                    if (data[i].Status == 1) {
                        $(TR).append("<td class='response' data-id='" + data[i].ID + "'>Response</td>");
                    }
                    else if (data[i].Status == 2) {
                        $(TR).append("<td class='done' data-id='" + data[i].ID + "'>Done</td>");
                    }
                }
                $("#request-table-my td.response").on("click", function () {
                    var info = {
                        ID: $(this).attr("data-id"),
                        ResponseSuperintenentID: Auth.Current.Profile().ID
                    }
                    Pool.Core("request", "response", info, function () {
                        Render_Table();
                    })
                })
                $("#request-table-my td.done").on("click", function () {
                    var info = {
                        ID: $(this).attr("data-id"),
                    }
                    Pool.Core("request", "done", info, function () {
                        Render_Table();
                    })
                })
            })
        })
    }

    Render_Table();

    $("article").hide();
    $("#request").show();

    $("#request-create-submit").off().on("click", function () {
        var info = {
            RequestTenantID: Auth.Current.Profile().ID,
            ApartmentID: $("#request-create-apartment :selected").val(),
            Category: $("#request-create-category :selected").val(),
        }
        Pool.Core("request", "create", info, function (data) {
            Render_Table();
        }
    )

    })

    $("#request-create-back").off().on("click", function () {
        Go_Homepage();
    });

}