var Pool = (function () {

    var _body = {};

    _body.Reset = function () {
        sessionStorage.clear();
        sessionStorage.setItem("Path_Api", "http://localhost:8080");
    }

    _body.Core = function (type, action, info, callback) {
        $.ajax({
            url: sessionStorage.getItem("Path_Api") + (type == "token" ? "/token" : "/api/" + type.toLowerCase() + "/" + action.toLowerCase() + ""),
            type: "POST",
            dataType: "json",
            data: info,
            headers: Auth.Current.Header(),
            statusCode: {
                200: function (data) {
                    callback(data);
                },
            }
        })
    };

    return _body;
}());