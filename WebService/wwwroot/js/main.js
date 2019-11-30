

require.config({
    baseUrl: "js",
    paths: {
        //     jquery: "../lib/jquery/dist/jquery",
        knockout: "../lib/knockout/build/output/knockout-latest.debug",
        text: "../lib/requirejs-text/text",
        dataService: "services/dataservice"

    }
});

require(["knockout"], function (ko) {

    ko.components.register('login', {
        viewModel: { require: "components/login/login" },
        template: { require: "text!components/login/login.html" }
    });

    ko.components.register('spell', {
        viewModel: { require: "components/spells/spellsearch" },
        template: { require: "text!components/spells/spellsearch.html" }
    });
});

require(["knockout", "app"], function (ko, app) {
    //console.log(app.name);


    ko.applyBindings(app);
});
