define(["knockout", "spellModal"], function (ko, spellModal) {


    var LoggedIn = ko.observable(false);
    var currentParams = ko.observable({});
    var Token = ko.observable();
    var CurrentPlayer = ko.observable({ id: undefined, name: "" });
    var CurrentCharacter = ko.observable({ id: undefined });

    var RaceModalState = ko.observable(false);
    var ClassModalState = ko.observable(false);

    var spellModal = ko.observable(spellModal);

    var SetCharacter = function (char) {
        console.log("Current character set to: " + char);
        CurrentCharacter({ id : char.id, race: char.race});
    }

    return {
        LoggedIn,
        currentParams,
        Token,
        CurrentPlayer,
        CurrentCharacter,
        SetCharacter,
        spellModal,
        RaceModalState,
        ClassModalState
    };
});