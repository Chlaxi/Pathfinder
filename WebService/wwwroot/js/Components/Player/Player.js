define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function () {

        var characters = ko.observableArray([]);
        var playerInfo = ko.observable({});

        var GetPlayerInfo = function (id) {
            if (id === undefined) {
                return;
            }
            ds.getPlayer(id, function (data) {
                if (data === undefined) {
                    characters.removeAll();
                    return;
                }
                console.log("Player info: ", data);
                playerInfo(data);
                characters(data.characters);
            });
        };

        app.LoggedIn.subscribe(function (state) {
            characters.removeAll();
        });

        var NewCharName = ko.observable("");
        var AddCharacter = async function () {
            console.log("Attempting to add character", NewCharName(), " for player", playerInfo().id);
            await ds.AddCharacter(playerInfo().id, NewCharName(), function (data) {
                console.log("Character Created:", data);
                characters.push(data);
                app.GoToSheet(data);
                
            });
        };

        var RemoveCharacter = async function(data){

            var confirmed = confirm("Are you sure you want to delete "+ data.name + "?");
            if (confirmed) {
                await ds.RemoveCharacter(playerInfo().id, data.id, function (result) {
                    if (result) {
                        console.log("Successfully removed", data);
                        characters.remove(data);
                    }
                });
            }
        };

        return {
            GetPlayerInfo, playerInfo,
            characters, 
            AddCharacter, NewCharName, RemoveCharacter
        };
    };
});