define([], function () {


    var getPlayersWithFetchAsync = async function (callback) {
        var response = await fetch("api/players");
        var data = await response.json();
        callback(data);
    };

    var spellSearch = async function (query, callback) {
        //query = "";
        var path = "api/spells";
        if (query !== "") {
            path += "/search?query=" + query;
            
        }
        var response = await fetch(path);
        var data = await response.json();
        callback(data);
    };

    return {
        getPlayersWithFetchAsync,
        spellSearch
    }
});