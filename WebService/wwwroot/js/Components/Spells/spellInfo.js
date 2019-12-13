define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {

        var spell = ko.observable({name: "Name"});

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


        return {
            spell,
            school,
            GetSpell,
            castingTime, duration, components, range, target, area, effect, savingThrow
        };

    };
});