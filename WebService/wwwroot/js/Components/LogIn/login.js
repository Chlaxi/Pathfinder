define(["knockout", "dataService"], function (ko, ds) {
    return function () {
        var LoggedIn = ko.observable(false);
        var LoginFailed = ko.observable(false);
        //Remove the default value. used for testing.
        var loginUsername = ko.observable("Dummy");
        var loginPassword = ko.observable("tester123");

        //TODO: Fix playername
        var player = ko.observable("Unknown's");
        var characters = ko.observableArray([]);

        var LoginHandler = async function (formElement) {
            var username = loginUsername();
            var password = loginPassword();

            if (username === undefined) {
                console.log("undefined username");
                return;
            }
            if (password === undefined) {
                console.log("undefined password");
                return;
            }

            var creds = {
                Username: username,
                Password: password,
            }
            console.log(creds);
            await ds.Login(creds, function (result) {
                if (result) {
                    LoginFailed(false);
                    LoggedIn(true);
                }
                else {
                    LoginFailed(true);
                    LoggedIn(false);
                }
            });
        };

       

        var GetPlayerInfo = function (id) {
            id = 2;
            console.log("Getting characters");

            ds.getPlayer(id, function (data) {
                characters(data.characters);
                player(data.username);
            });
        };

        var Logout = function () {
            LoggedIn(false);
            //TODO: Remove authorisation
        };

        return {
            LoggedIn,
            LoginHandler,
            LoginFailed,
            Logout,
            loginUsername,
            loginPassword,
            player,
            characters,
            GetPlayerInfo
        };
    };
});