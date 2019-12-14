define(["knockout", "app", "dataService"], function (ko, app, ds) {


    return function (params) {

        var charId = undefined;

        var character = ko.observable({});

        params.character.subscribe(function (data) {
            console.log("CLASSES PARAM", data);
            character(data);
        });

        var classes = ko.observableArray([]);
        var currentClass = ko.observableArray();

        var level = ko.observable(1);
        app.CurrentCharacter.subscribe(function (data) {
            charId = data.id;
            GetClasses()
            console.log("character's race is :", data);
        });

        var GetClasses = async function () {
            console.log("Getting races from ", character);
            await ds.GetClasses(function (data) {
                classes(data);
                console.log(data);
            });
        };

        var SetClasses = async function (level) {
            if (charId !== undefined && currentClass() !== undefined) {
                console.log("Adding race to character", charId, currentClass().name);
                console.log("Level: "+level)
                await ds.SetClass(charId, currentClass().name, level, function (data) {
                    console.log("Class set: ", data);
                   // app.RaceModalState(false);
                   // app.SetCharacter({ id: charId, race: data.name });
                    level(1);
                });
            }

        };

        return {
            classes, currentClass, level, character,
            GetClasses, SetClasses
        };

    };
});