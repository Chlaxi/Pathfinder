define(["knockout", "app"], function (ko, app) {


    var CurrentSpell = ko.observable({});
    var SpellLevel = ko.observable("");
    var isOpen = ko.observable(false);
    var isAddingSpell = ko.observable(false);
    

    return {
        CurrentSpell,
        isOpen,
        SpellLevel,
        isAddingSpell
    };
});