define(["knockout", "app", "dataService", "spellModal"], function (ko, app, ds, spellModal) {
    return function (params) {
        var id = undefined;
        /*Subscribes to changes to the Current character in the application.
        When a chagne is made, we call the GetCharacter dunction, to get the information.*/
        app.CurrentCharacter.subscribe(function (data) {
            console.log(JSON.stringify(data));
            GetSpellbook(data.id);
            id = data.id;
        });

        spellModal.changed.subscribe(function () {
            console.log("Changes were made to the spellbook");
            GetSpellbook(app.CurrentCharacter().id);
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
            spellModal.SpellLevel(spellLevel);
            spellModal.isOpen(true);
            spellModal.isAddingSpell(true);
        };

        var ShowSpellInfo = function(spellLevel, spell){
            console.log("Showing " + spell +" at level "+spellLevel);
            spellModal.isOpen(true);
            spellModal.SpellLevel(spellLevel);
            spellModal.CurrentSpell(spell);
            spellModal.isAddingSpell(false);
        }

        var CloseSpellModal = function () {
            spellModal.CloseModule();
        }

        return {
            GetSpellbook, ShowSpellInfo,
            AddSpellToSpellbook, isAddingSpell, CloseSpellModal,
            hasSpellbook, spellbook, spellLevels
        };
    };
});

