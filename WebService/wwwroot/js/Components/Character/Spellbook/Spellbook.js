define(["knockout", "app", "dataService", "spellModal"], function (ko, app, ds, spellModal) {
    return function (params) {
        var id = undefined;
        /*Subscribes to changes to the Current character in the application.
        When a chagne is made, we call the GetCharacter dunction, to get the information.*/
        app.CurrentCharacter.subscribe(function (data) {
            GetSpellbook(data.id);
            id = data.id;
        });

        spellModal.changed.subscribe(function () {
            GetSpellbook(app.CurrentCharacter().id);
        });

        var hasSpellbook = ko.observable(false);
        var spellbook = ko.observableArray([]);
        var spellLevels = ko.observableArray([]);
        var currentSpellLevel = ko.observable("");
        var isAddingSpell = ko.observable(false);

        var GetSpellbook = async function (charId) {
            await ds.LoadSpellbook(charId, function (data) {
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
            //This should set current spell level, and open up the spell search modal 
            spellModal.SpellLevel(spellLevel);
            spellModal.isOpen(true);
            spellModal.isAddingSpell(true);
        };

        var ShowSpellInfo = function(spellLevel, spell){
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

