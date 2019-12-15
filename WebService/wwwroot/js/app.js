define(["knockout", "spellModal"], function (ko, spellModal) {


    var menuElements = ko.observableArray([

        ]);

    var sheetMenuElements = ko.observableArray([
        {
            name: "Character",
            link: "#CharacterInfo",
            icon: "glyphicon glyphicon-log-in"
        },
        {
            name: "Abilities",
            link: "#Abilities",
            icon: "glyphicon glyphicon-log-in"
        },
        {
            name: "Offense",
            link: "#Offense",
            icon: "glyphicon glyphicon-log-in"
        },
        {
            name: "Defensive",
            link: "#Defensive",
            icon: "glyphicon glyphicon-log-in"
        },
        {
            name: "Feats",
            link: "#Feats",
            icon: "glyphicon glyphicon-book"
        },
        {
            name: "Items",
            link: "#Items",
            icon: "glyphicon glyphicon-book"
        },
        {
            name: "Spellsbook",
            link: "#Spellbook",
            icon: "glyphicon glyphicon-book"
        },
    ]);

    var loginMenuElements = ko.observableArray([
        {
            name: "Login",
            component: "login-comp",
            icon: "glyphicon glyphicon-log-in"
        },
        {
            name: "Sign Up",
            component: "signup-comp",
            icon: "glyphicon glyphicon-user"
        }
    ]);

    var LoggedIn = ko.observable(false);

     LoggedIn.subscribe(function (state) {
         console.log("beep bop", state);
         if (!state) {
             ChangeContent(menu = { name: "Login", component: "login-comp"});
         }
         else {
             ChangeContent(menu = {name: "character",component: "player-front"});
         }
     });

    var currentMenu = ko.observable(loginMenuElements()[0]);
    var currentComponent = ko.observable(currentMenu().component);

    var ChangeContent = function (menu) {
        console.log("MENU", menu);
        currentMenu(menu.name);
        currentComponent(menu.component);
    }

    var isSelected = function (menu) {
        return menu === currentMenu() ? "active" : "";
    };


    var currentParams = ko.observable({});
    var Token = ko.observable();
    var CurrentPlayer = ko.observable({ id: undefined, name: "" });
    var CurrentCharacter = ko.observable({ id: undefined });

    var RaceModalState = ko.observable(false);
    var ClassModalState = ko.observable(false);

    var spellModal = ko.observable(spellModal);

    var sheetInfo = ko.observable({});
    var GoToSheet = function (data) {
        //ChangeContent(menu = { name: "character sheet", component: "character" });
        sheetInfo(sheet = { active: true, id: data.id });
       

    };

    var SetCharacter = function (char) {
        CurrentCharacter({ id: char.id, race: char.race });
    };

    var CloseSheet = function () {
        CurrentCharacter({});
        sheetInfo(sheet = { active: false, id: null });
    };

    var Logout = function () {
        LoggedIn(false);
        Token = "";
        CurrentPlayer({});
        CurrentCharacter({});
        sheetInfo(sheet = { active: false, id: null });
    };

    return {
        currentComponent, menuElements, loginMenuElements, ChangeContent, isSelected,
        sheetMenuElements,
        LoggedIn,
        currentParams,
        Token,
        CurrentPlayer,
        CurrentCharacter,
        SetCharacter,
        spellModal,
        RaceModalState,
        ClassModalState,
        sheetInfo, GoToSheet,
        Logout, CloseSheet
    };
});