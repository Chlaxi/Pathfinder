
    require.config({
        baseUrl: "js",
        paths: {
            //     jquery: "../lib/jquery/dist/jquery",
            knockout: "../lib/knockout/build/output/knockout-latest.debug",
            dataService: "services/dataservice"

        }
    });

    require(["knockout", "app", "dataService"], function (ko, app, ds) {
        //console.log(app.name);
        
        ko.applyBindings(app);
    });
