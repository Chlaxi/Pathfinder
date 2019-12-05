define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {

        var SignUpFailed = ko.observable("");
        var SignUpUsername = ko.observable();
        var SignUpPassword = ko.observable();

        var SignUp = async function(){
            var username = SignUpUsername();
            var password = SignUpPassword();

            if (username === undefined) {
                console.log("undefined username");
                SignUpFailed("No username");
                return;
            }
            if (password === undefined) {
                console.log("undefined password");
                SignUpFailed("No password");
                return;
            }

            var creds = {
                Username: username,
                Password: password,
            }
            console.log("Attempting to create a new user for with the information: " + creds);
            await ds.SignUp(creds, function (result) {
                console.log("Checking log in result: " + JSON.stringify(result));
                if (result !== undefined) {
                    console.log("SUCCESS!");
                    SignUpFailed("Success");
                   
                    app.CurrentPlayer({
                        id: result.id,
                        name: result.username
                    });
                    app.LoggedIn(true);
                }
                else {
                    console.log("failed");
                    SignUpFailed("User already exists");
                    app.LoggedIn(false);
                }
            });
        };

        return {
            SignUp,
            SignUpUsername,
            SignUpPassword,
            SignUpFailed

        };


    };
});