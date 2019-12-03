define(["app"], function (app) {

    var Login = async function (user, callback) {
        var options = {
            method: 'POST', // or 'PUT'
            body: JSON.stringify(user), // data can be `string` or {object}!
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer '
            }
        };
 //       console.log("Received the user " + JSON.stringify(user));

        const response = await fetch("api/tokens", options);
        if (response.status !== 200) {
            console.log(response.statusText);
            callback(undefined);
            return;
        }
        const data = await response.json();
        console.log('Success:', JSON.stringify(data));
        
        var token = await data.token;
        app.token = "Bearer "+token;
        options.headers.Authorization += token;
        console.log(options.headers.Authorization);

        callback(data);
    }


    var getPlayer = async function (id, callback) {

        console.log("Retriving characters from player with id " + id);
        var url = "api/players/" + id;
        var response = await fetch(url, options = {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        console.log(response.status + " : " + response.statusText);
        if (response.status !== 200) {
            callback(undefined);
            return;
        }
        var data = await response.json();
        console.log(data);
        callback(data);
    };

    var spellSearch = async function (query, callback) {
        var path = "api/spells";
        if (query !== "") {
            path += "/search?query=" + query;
            
        }
        var response = await fetch(path);
        var data = await response.json();
        callback(data);
    };

    return {
        Login,
        getPlayer,
        spellSearch
    }
});