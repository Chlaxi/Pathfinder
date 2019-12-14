define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {
        
        var charId = undefined;


        var races = ko.observableArray([]);
        var currentRace = ko.observableArray();
        var selectedRace = ko.observableArray();


        app.CurrentCharacter.subscribe(function (data) {
            charId = data.id;
            GetRaces()
            console.log("character's race is :", data);
            currentRace(data.race);
        });

        var GetRaces = async function () {
            console.log("Getting races");
            await ds.GetRaces(function (data) {
                races(data);
                console.log(data);
            });
        };

        var SetRace = async function () {
            if (charId !== undefined && selectedRace() !== undefined) {
                console.log("Adding race to character", charId, selectedRace().name);
                await ds.SetRace(charId, selectedRace().name, function (data) {
                    console.log("Race set: ", data);
                    app.RaceModalState(false);
                    app.SetCharacter({ id: charId, race: data.name });
                    
                });
            }
            
        };

        return {
            races, GetRaces, SetRace, currentRace, selectedRace
        };

    };
});