define(["knockout"], function (ko) {


    var LoggedIn = ko.observable(false);
    var currentParams = ko.observable({});
    var Token = ko.observable();
    var CurrentPlayer = ko.observable({id:undefined,name:""});
    
    return {
        LoggedIn,
        currentParams,
        Token,
        CurrentPlayer
        
    };
});