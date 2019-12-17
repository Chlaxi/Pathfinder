define(["knockout", "app", "dataService"], function (ko, app, ds) {


    return function (params) {

        var charId = undefined;

        var character = ko.observable({});
        var ownedClasses = ko.observableArray([]);

        params.character.subscribe(function (data) {
            character(data);
            ownedClasses(data);
           // console.log("Classes:", ownedClasses());
            
        });

        var classes = ko.observableArray([]);

        var currentClass = ko.observableArray();

        var level = ko.observable(1);
        app.CurrentCharacter.subscribe(function (data) {
            charId = data.id;
            GetClasses()
        });

        var GetClasses = async function () {
            await ds.GetClasses(function (data) {
                classes(data);
            });
        };

        var SetClasses = async function (level) {
            if (charId !== undefined && currentClass() !== undefined) {
                await ds.SetClass(charId, currentClass().name, level(), function (data) {
                   // app.RaceModalState(false);
                   // app.SetCharacter({ id: charId, race: data.name });
                    level(1);
                    console.log("DATA", data);
                    console.log(classes());
                    var newClass = undefined;

                    newClass = GetLevelInfo(data.name, data.level);

                    var newClass = {
                        characterId: charId,
                        className: data.name,
                        level: data.level,
                        class: newClass
                    };

                    ownedClasses.push(newClass);
                 
                });
            }

        };

        var RemoveClass = async function (data) {
            await ds.RemoveClass(charId, data.className, function (response) {
                if (response.ok) {
                    ownedClasses.remove(data);
                }
            });
        };

        var LevelUp = async function (data) {

            await ds.LevelUpClass(charId, data.className, function (callback) {
                if (callback !== null) {
                    data.level = callback.level;
                    data.class = GetLevelInfo(data.className,data.level);
                    ownedClasses.remove(data)
                    ownedClasses.push(data);
                }
            });
        };

        var GetLevelInfo = function (className, level) {
            for (var i = 0; i < classes().length; i++) {
                var newClass = undefined;
                if (classes()[i].name == className) {
                    console.log("class found!", classes()[i]);
                    newClass = classes()[i];

                    for (var i = newClass.levelInfo.length; i > level; i--) {
                        var removed = newClass.levelInfo.pop();
                        console.log("Pop:", removed.level);
                    }
                    console.log(newClass);
                    return newClass;

                }
            }
        }

        return {
            classes, currentClass, level, character, ownedClasses,
            GetClasses, SetClasses, RemoveClass, LevelUp
        };

    };
});