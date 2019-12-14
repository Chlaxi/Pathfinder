define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {
        var id = undefined;
        /*Subscribes to changes to the Current character in the application.
        When a chagne is made, we call the GetCharacter dunction, to get the information.*/
        app.CurrentCharacter.subscribe(function (data) {
            console.log(JSON.stringify(data));
            GetSpellbook(data.id);
            id = data.id;
        });
        var hasSpellbook = ko.observable(false);
        var spellbook = ko.observableArray([]);
        var spellLevels = ko.observableArray([]);
        var currentSpellLevel = ko.observable("");
        var isAddingSpell = ko.observable(false);

        var GetSpellbook = async function (charId) {
            console.log("Trying to fetch spellbook from character with id" + charId);
            await ds.LoadSpellbook(charId, function (data) {
                console.log("Spellbook: ", data);
                if (data === null) {
                    hasSpellbook(false);
                    return;
                }
                spellbook(data);
                spellLevels(data.spellLevels);
            });
        };

        //Rename to "OpenModal" or something
        var AddSpellToSpellbook = function (spellLevel) {
            console.log("Trying to add new spell to spell level " + spellLevel);
            //This should set current spell level, and open up the spell search modal 
            app.SpellLevel(spellLevel);
            isAddingSpell(true);
        };

        var CloseSpellModal = function () {
            isAddingSpell(false);
            app.SpellLevel(null);
            currentSpellLevel(null);
        }

        return {
            GetSpellbook,
            AddSpellToSpellbook, isAddingSpell, CloseSpellModal,
            hasSpellbook, spellbook, spellLevels
        };
    };
});

