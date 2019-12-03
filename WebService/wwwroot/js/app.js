define(["knockout"], function (ko) {

    var LoggedIn = ko.observable(false);
    var currentParams = ko.observable({});
    var Token = ko.observable();
     
    return {
        LoggedIn,
        currentParams,
        Token
        
    };
});