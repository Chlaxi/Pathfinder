define(["knockout", "spellModal"], function (ko, spellModal) {


    var LoggedIn = ko.observable(false);
    var currentParams = ko.observable({});
    var Token = ko.observable();
    var CurrentPlayer = ko.observable({ id: undefined, name: "" });
    var CurrentCharacter = ko.observable({ id: undefined });

    var spellModal = ko.observable(spellModal);

    var SetCharacter = function (_id) {
        console.log("Current character set to: " + _id);
        CurrentCharacter({ id : _id });
    }

    return {
        LoggedIn,
        currentParams,
        Token,
        CurrentPlayer,
        CurrentCharacter,
        SetCharacter,
        spellModal
    };
});