define(["knockout", "dataService"], function (ko, ds) {

    var spellQuery = ko.observable("");

    var spells = ko.observableArray([]);

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


    return {
        spellQuery,
        spells,

    };
});