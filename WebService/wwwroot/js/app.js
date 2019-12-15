define(["knockout", "spellModal"], function (ko, spellModal) {


    var menuElements = ko.observableArray([
        {
            name: "Login",
            component: "login-comp"
        },
        {
            name: "Sign Up",
            component: "signup-comp"
        }
    ]);
    var LoggedIn = ko.observable(false);

     LoggedIn.subscribe(function (state) {
         console.log("beep bop", state);
         if (!state) {
             menuElements([
                 {
                     name: "Login",
                     component: "login-comp"
                 },
                 {
                     name: "Sign Up",
                     component: "signup-comp"
                 }
             ]);
             ChangeContent(menu = { name: "Login", component: "login-comp"});
         }
         else {
             menuElements([
                 {
                     name: "Characters",
                     component: "player-front"
                 }

             ]);
             ChangeContent(menu = {name: "character",component: "player-front"});
         }
     });

    var currentMenu = ko.observable(menuElements()[0]);
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

    var Logout = function () {
        LoggedIn(false);
        Token = "";
        CurrentPlayer({});
        CurrentCharacter({});
        sheetInfo(sheet = { active: false, id: null });
    };

    return {
        currentComponent, menuElements, ChangeContent, isSelected,
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