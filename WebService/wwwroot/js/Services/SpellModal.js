define(["knockout", "app"], function (ko, app) {

    var CurrentSpell = ko.observable({});
    var SpellLevel = ko.observable("");
    var isOpen = ko.observable(false);
    var isAddingSpell = ko.observable(false);
    var showSearch = ko.observable(true);
    var isRemovingSpells = ko.observable(false);

    var changed = ko.observable().extend({ notify: 'always' });

    var CloseModule = function () {
        CurrentSpell("");
        SpellLevel("");
        isOpen(false);
        isAddingSpell(false);
        isRemovingSpells(false);
        showSearch(true);
        changed("");
    };
        
    var Clear = function () {
        CurrentSpell("");
    }

        return {
            CurrentSpell, showSearch,
            isOpen,
            SpellLevel,
            isAddingSpell, isRemovingSpells, Clear,
            CloseModule,
            changed
        };
});