define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function () {
        var LoggedIn = ko.observable(false);
        var LoginFailed = ko.observable(false);
        //Remove the default value. used for testing.
        var loginUsername = ko.observable("Dummy");
        var loginPassword = ko.observable("tester123");

        //TODO: Fix playername
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
                console.log("Checking log in result: " + JSON.stringify(result));
                if (result !== undefined) {
                    LoginFailed(false);
                    LoggedIn(true);
                    app.CurrentPlayer({
                        id: result.id,
                        name: result.username
                    });
                }
                else {
                    LoginFailed(true);
                    LoggedIn(false);
                }
            });
        };

       

        var GetPlayerInfo = function (id) {
            console.log("Getting characters for " + id);
            if (id === undefined) {
                console.log("id undefined");
                return;
            }
            ds.getPlayer(id, function (data) {
                if (data === undefined) {
                    characters.removeAll();
                    return;
                }
                characters(data.characters);
            });
        };

        var Logout = function () {
            console.log("Logging out");
            LoggedIn(false);
            characters.removeAll();
            app.Token = "";
            app.CurrentPlayer({});
            //TODO: Remove authorisation
        };

        return {
            LoggedIn,
            LoginHandler,
            LoginFailed,
            Logout,
            loginUsername,
            loginPassword,
            characters,
            GetPlayerInfo
        };
    };
});