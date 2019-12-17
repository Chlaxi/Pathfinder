define(["knockout", "app", "dataService", "spellModal"], function (ko, app, ds, spellModal) {
    return function (params) {

        var charId = undefined;
        app.CurrentCharacter.subscribe(function (data) {
            charId = data.id;
        });

        var spellFound = ko.observable(false);
        var spell = ko.observable({});

        var school = ko.observable("");
        var castingTime = ko.observable(false);
        var duration = ko.observable(false);
        var components = ko.observable(false);
        var range = ko.observable(false);
        var target = ko.observable(false);
        var area = ko.observable(false);
        var effect = ko.observable(false);
        var duration = ko.observable(false);
        var savingThrow = ko.observable(false);

        spellModal.CurrentSpell.subscribe(function (data) {
            if (data ==="" || data === null) {
                Clear();
                return;
            }
            GetSpell(data.spellId);
        });

        spellModal.SpellLevel.subscribe(function (data) {
        });

        var GetSpell = async function (spellId) {

            await ds.GetSpell(spellId, function (_spell) {
                spell(_spell);
                spellFound(true);
                var schoolString = _spell.school;

                if (_spell.subSchool !== "" && _spell.subSchool !== null) {
                    schoolString += " (" + _spell.subSchool + ")";
                }

                if (_spell.element !== "" && _spell.element !== null) {
                    schoolString += " [" + _spell.element + "]";
                }
                school(schoolString);

                var _castingTime = (_spell.castingTime === null) ? false : true;
                castingTime(_castingTime);

                var _components = (_spell.components === null) ? false : true;
                components(_components);

                var _range = (_spell.range === null) ? false : true;
                range(_range);

                var _target = (_spell.target === null) ? false : true;
                target(_target);

                var _area = (_spell.area === null) ? false : true;
                area(_area);

                var _effect = (_spell.effect === null) ? false : true;
                effect(_effect);

                var _savingThrow = (_spell.savingThrow === null) ? false : true;
                savingThrow(_savingThrow);

                var _duration = (_spell.duration === null) ? false : true;
                duration(_duration);
            });
        };

        var Clear = function () {
            spellFound(false);
            school("");
            spell({});
            castingTime(false);
            duration(false);
            components(false);
            range(false);
            target(false);
            area(false);
            effect(false);
            duration(false);
            savingThrow(false);
        };

        var AddSpell = async function () {
            if (charId === null || charId === undefined) {
                return;
            }
            if (spell().id === null || spell().id === undefined) {
                return;
            }
            if (spellModal.SpellLevel === null || spellModal.SpellLevel === undefined) {
                return;
            }
            var spellLevel = Number(spellModal.SpellLevel());

            await ds.AddSpell(charId, spell().id, spellLevel, function (success) {
                if (success) {
                    var status = "Successfully added " + spell().name;
                }
                else {
                    var status = "Failed to add spell";
                }
                CloseModule(status);
                //Show "spell added" message, close module, refresh the spell level
            });
            
        };

        var RemoveSpell = async function () {
            if (charId === null || charId === undefined) {
                return;
            }
            if (spell().id === null || spell().id === undefined) {
                return;
            }
            if (spellModal.SpellLevel === null || spellModal.SpellLevel === undefined) {
                return;
            }
            var spellLevel = Number(spellModal.SpellLevel());
            await ds.RemoveSpell(charId, spell().id, spellLevel, function (success) {
                if (success) {
                    var status = "Successfully removed " + spell().name;
                } else {
                    var status = "Failed to remove spell";
                }
                CloseModule(status);
                //Show "spell removed" message, close module, refresh the spell level
            });
        };

        var CloseModule = function (status) {
            
            alert(status);
            Clear();
            //refresh spell list
            spellModal.CloseModule();
        };

        return {
            spell, spellFound,
            school,
            GetSpell, Clear, AddSpell, RemoveSpell, CloseModule,
            castingTime, duration, components, range, target, area, effect, savingThrow
        };

    };
});