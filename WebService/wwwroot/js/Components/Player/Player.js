define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function () {

        var characters = ko.observableArray([]);

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
            app.LoggedIn(false);
            characters.removeAll();
            app.Token = "";
            app.CurrentPlayer({});
            app.CurrentCharacter({});

        };

        return {
            GetPlayerInfo,
            Logout,
            characters
        };
    };
});