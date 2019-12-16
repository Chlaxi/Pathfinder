define(["knockout", "app", "spellModal", "dataService"], function (ko, app, spellModal, ds) {
    return function (params) {

        var spellQuery = ko.observable("");

        var spells = ko.observableArray([]);
        var currentSpell = ko.observable("");

        spellQuery.subscribe(async function (data) {
            if (data.length === 0) {
                spells([]);

                return;
            }

            var result = await ds.spellSearch(data, function (data) {
                spells(data);
                currentPage = 1
                numberOfPages = getNumberOfPages();
                loadList();
            });
          //  spells(result);

        });

        var chooseSpell = function (spell) {
            currentSpell(spell);
            console.log("Choose a new spell", spell);
            spellModal.CurrentSpell(spell);
        };

        var pageList = ko.observableArray([]);
        var currentPage = 1;
        var numberPerPage = 10;
        var numberOfPages = 0;

        var getNumberOfPages = function () {
            console.log("Spell length:", spells().length);
            return Math.ceil(spells().length / 10);
        };

        var loadList = function() {

            var begin = ((currentPage - 1) * 10);
            var end = begin + numberPerPage;
            pageList(spells().slice(begin, end));
           // check();         // determines the states of the pagination buttons
        };

        var firstPage = function () {
            currentPage = 1;
            loadList();
        };

        var nextPage = function() {
            currentPage += 1;
            loadList();
        }

        var previousPage = function() {
            currentPage -= 1;
            loadList();
        }

        var lastPage = function() {
            currentPage =numberOfPages;
            loadList();
        }

        return {

            spellQuery,
            spells, pageList, getNumberOfPages, currentPage,
            currentSpell,
            chooseSpell,
            firstPage, nextPage, previousPage, lastPage
        };
    };
});