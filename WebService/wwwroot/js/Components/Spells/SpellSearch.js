define(["knockout", "app", "spellModal", "dataService"], function (ko, app, spellModal, ds) {
    return function (params) {

        var spellQuery = ko.observable("");

        var spells = ko.observableArray([]);
        var currentSpell = ko.observable("");

        spellQuery.subscribe(function (data) {
            if (data.length === 0) {
                spells([]);
                return;
            }

            var result = ds.spellSearch(data, function (data) {
                spells(data);
            });
            spells(result);
        });

        var chooseSpell = function (spell) {
            currentSpell(spell);
            console.log("Choose a new spell", spell);
            spellModal.CurrentSpell(spell);
        }

        return {

            spellQuery,
            spells,
            currentSpell,
            chooseSpell,

        };
    };
});