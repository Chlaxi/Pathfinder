﻿

require.config({
    baseUrl: "js",
    paths: {
        //     jquery: "../lib/jquery/dist/jquery",
        knockout: "../lib/knockout/build/output/knockout-latest.debug",
        text: "../lib/requirejs-text/text",
        dataService: "services/dataservice",
        spellModal: "components/spells/spellmodal",
        
    }
});

require(["knockout"], function (ko) {

    ko.components.register('signup-comp', {
        viewModel: { require: "components/login/signup" },
        template: { require: "text!components/login/signup.html" }
    });

    ko.components.register('login-comp', {
        viewModel: { require: "components/login/login" },
        template: { require: "text!components/login/login.html" }
    });

    ko.components.register('player-front', {
        viewModel: { require: "components/player/player" },
        template: { require: "text!components/player/player.html" }
    });

    ko.components.register('race', {
        viewModel: { require: "components/character/race/raceinfo" },
        template: { require: "text!components/character/race/raceinfo.html" }
    });

    ko.components.register('character', {
        viewModel: { require: "components/character/character" },
        template: { require: "text!components/character/character.html" }
    });


    ko.components.register('spell', {
        viewModel: { require: "components/spells/spellsearch" },
        template: { require: "text!components/spells/spellsearch.html" }
    });


    ko.components.register('spell-info', {
        viewModel: { require: "components/spells/spellinfo" },
        template: { require: "text!components/spells/spellinfo.html" }
    });


    ko.components.register('spellbook', {
        viewModel: { require: "components/character/spellbook/spellbook" },
        template: { require: "text!components/character/spellbook/spellbook.html" }
    });


});

require(["knockout", "app"], function (ko, app) {
    //console.log(app.name);


    ko.applyBindings(app);
});
