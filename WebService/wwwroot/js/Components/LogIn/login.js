define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function () {

        var LoginFailed = ko.observable("");
        //Remove the default value. used for testing.
        var loginUsername = ko.observable("Dummy");
        var loginPassword = ko.observable("tester123");

        var LoginHandler = async function (formElement) {
            var username = loginUsername();
            var password = loginPassword();

            if (username === undefined) {
                console.log("undefined username");
                LoginFailed("Please insert a Username");
                return;
            }
            if (password === undefined) {
                console.log("undefined password");
                LoginFailed("Please insert a password");
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
                    LoginFailed("");
                    app.LoggedIn(true);
                    app.CurrentPlayer({
                        id: result.id,
                        name: result.username
                    });
                }
                else {
                    LoginFailed("Wrong username or password");
                    app.LoggedIn(false);
                }
            });
        };

       

      

        return {
            
            LoginHandler,
            LoginFailed,
            loginUsername,
            loginPassword
        };
    };
});