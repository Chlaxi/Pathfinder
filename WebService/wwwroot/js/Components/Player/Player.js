define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function () {

        var characters = ko.observableArray([]);

        var GetPlayerInfo = function (id) {
            if (id === undefined) {
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

        app.LoggedIn.subscribe(function (state) {
            characters.removeAll();
        });

        return {
            GetPlayerInfo,
            characters
        };
    };
});