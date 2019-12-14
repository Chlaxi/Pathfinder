define(["knockout", "app", "dataService"], function (ko, app, ds) {


    return function (params) {

        var charId = undefined;

        var character = ko.observable({});
        var ownedClasses = ko.observableArray([]);

        params.character.subscribe(function (data) {
            console.log("CLASSES PARAM", data);
            character(data);
            ownedClasses(data);
            console.log(ownedClasses);
            
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
                console.log("Level: "+level())
                await ds.SetClass(charId, currentClass().name, level(), function (data) {
                    console.log("Class set: ", data);
                   // app.RaceModalState(false);
                   // app.SetCharacter({ id: charId, race: data.name });
                    level(1);
                    var newClass = {
                        characterId: charId,
                        className: data.name,
                        level: data.level
                    }

                    ownedClasses.push(newClass);
                });
            }

        };

        var RemoveClass = async function (data) {
            await ds.RemoveClass(charId, data.className, function (response) {
                if (response.ok) {
                    console.log("Sucessfully removed");
                    ownedClasses.remove(data);
                }
            });
        };

        var LevelUp = async function (data) {

            await ds.LevelUpClass(charId, data.className, function (callback) {
                console.log("level up callback", callback);
                if (callback !== null) {

                    var updatedClass = data;
                    updatedClass.level = callback.level;
                    console.log("Sucessfully leveld up", updatedClass);
                    ownedClasses.remove(data)
                    ownedClasses.push(updatedClass);
                }
            });
        };

        return {
            classes, currentClass, level, character, ownedClasses,
            GetClasses, SetClasses, RemoveClass, LevelUp
        };

    };
});