var Auth = (function () {

    var _body = {};
    _body.Current = {};

    _body.Test = function () {
        if (sessionStorage.getItem("Auth_Pass") != "1") {
            window.location = "login.html";
        }
    }

    _body.Reset = function () {
        sessionStorage.removeItem("Auth_Pass");
        sessionStorage.removeItem("Auth_Header");
        sessionStorage.removeItem("Auth_Profile");
    }

    _body.Auth = function (token, email, callback) {

        sessionStorage.setItem("Auth_Pass", "1");
        sessionStorage.setItem("Auth_Header", JSON.stringify({ "Authorization": "Bearer " + token }));

        Pool.Core("profile", "single/email", { User: { Email: email } }, function (data) {
            sessionStorage.setItem("Auth_Profile", JSON.stringify(data));
            callback();
        })

    }

    _body.Current.Header = function () {
        if (sessionStorage.getItem("Auth_Pass") != "1") {
            return {};
        }
        else {
            return JSON.parse(sessionStorage.getItem("Auth_Header"));
        }
    }

    _body.Current.Profile = function () {
        return JSON.parse(sessionStorage.getItem("Auth_Profile"));
    }

    return _body;
}());