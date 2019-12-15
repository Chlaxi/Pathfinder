define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {
        
        var charId = undefined;


        var races = ko.observableArray([]);
        var currentRace = ko.observableArray();
        var selectedRace = ko.observableArray();


        app.CurrentCharacter.subscribe(function (data) {
            charId = data.id;
            GetRaces()
            currentRace(data.race);
        });

        var GetRaces = async function () {
            await ds.GetRaces(function (data) {
                races(data);
            });
        };

        var SetRace = async function () {
            if (charId !== undefined && selectedRace() !== undefined) {
                await ds.SetRace(charId, selectedRace().name, function (data) {
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