define(["knockout", "spellModal"], function (ko, spellModal) {


    var LoggedIn = ko.observable(false);
    var currentParams = ko.observable({});
    var Token = ko.observable();
    var CurrentPlayer = ko.observable({ id: undefined, name: "" });
    var CurrentCharacter = ko.observable({ id: undefined });

    var RaceModalState = ko.observable(false);
    var ClassModalState = ko.observable(false);

    var spellModal = ko.observable(spellModal);

    var sheetInfo = ko.observable({});
    var GoToSheet = function (data) {
        sheetInfo(sheet = { active: true, id: data.id });
        //ChangeContent(menu = {name: "character sheet",component: "character"});

    };

    var SetCharacter = function (char) {
        CurrentCharacter({ id: char.id, race: char.race });
    };

    var Logout = function () {
        LoggedIn(false);
        Token = "";
        CurrentPlayer({});
        CurrentCharacter({});
        sheetInfo(sheet = { active: false, id: null });
    };

    return {
        LoggedIn,
        currentParams,
        Token,
        CurrentPlayer,
        CurrentCharacter,
        SetCharacter,
        spellModal,
        RaceModalState,
        ClassModalState,
        sheetInfo, GoToSheet
        , Logout
    };
});