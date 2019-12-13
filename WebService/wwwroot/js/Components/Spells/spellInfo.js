define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {

        var spell = ko.observable({name: "Name"});

        var schoolInfo = ko.observable("");

        params.spell.subscribe(function (data) {
            console.log(JSON.stringify(data));
            GetSpell(data.spellId);
        });
        

        var GetSpell = async function (spellId) {
             await ds.GetSpell(spellId, function (callback) {
                 console.log("Done", callback);
                 spell(callback);
            });
        }


        return {
            spell,
            schoolInfo,
            GetSpell
        };

    };
});