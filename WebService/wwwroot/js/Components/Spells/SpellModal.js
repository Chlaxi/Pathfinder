define(["knockout", "app"], function (ko, app) {


    var CurrentSpell = ko.observable({});
    var SpellLevel = ko.observable("");
    var isOpen = ko.observable(false);
    var isAddingSpell = ko.observable(false);

    var changed = ko.observable().extend({ notify: 'always' });

    var CloseModule = function() {
        CurrentSpell("");
        SpellLevel("");
        isOpen(false);
        isAddingSpell(false);
        changed("");
    };

    return {
        CurrentSpell,
        isOpen,
        SpellLevel,
        isAddingSpell,
        CloseModule,
        changed
    };
});