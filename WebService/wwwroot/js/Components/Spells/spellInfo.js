﻿define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {

        var charId = undefined;
        app.CurrentCharacter.subscribe(function (data) {
            console.log("Spells will be added to: ", JSON.stringify(data))
            charId = data.id;
        });

        var spell = ko.observable({ name: "Name" });

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



        params.spell.subscribe(function (data) {
            console.log(JSON.stringify(data));
            GetSpell(data.spellId);
        });
        app.SpellLevel.subscribe(function (data) {
            console.log("Spell level set to ", data);
        });

        var GetSpell = async function (spellId) {
            await ds.GetSpell(spellId, function (_spell) {
                console.log("Done", _spell);
                spell(_spell);

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
            spell({ name: "Name" });
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
            console.log(charId);
            if (charId === null || charId === undefined) {
                console.log("character not defined");
                return;
            }
            console.log("Spell to add ", spell());
            if (spell().id === null || spell().id === undefined) {
                console.log("Spell not defined");
                return;
            }
            if (app.SpellLevel === null || app.SpellLevel === undefined) {
                console.log("No spell level set");
            }
            var spellLevel = Number(app.SpellLevel());
            console.log(spellLevel);
            console.log("Adding spell" + spell().id + " at level ", spellLevel ," to " + charId);

            await ds.AddSpell(charId, spell().id, spellLevel, function (data) {
                console.log(data);
                //Show "spell added" message, close module, refresh the spell level
            });
            
        };

        return {
            spell,
            school,
            GetSpell, Clear, AddSpell,
            castingTime, duration, components, range, target, area, effect, savingThrow
        };

    };
});