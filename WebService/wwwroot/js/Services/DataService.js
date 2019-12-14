define(["app"], function (app) {

    var SetToken = function (token) {
        app.token = "Bearer " + token;
    }

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
        SetToken(token);
        options.headers.Authorization += token;
        console.log(options.headers.Authorization);

        callback(data);
    }

    var SignUp = async function (user, callback) {
        var options = {
            method: 'POST', // or 'PUT'
            body: JSON.stringify(user), // data can be `string` or {object}!
            headers: {
                'Content-Type': 'application/json',
            }
        };
        const response = await fetch("api/signup", options);
        if (response.status !== 201) {
            console.log("Failed " + response.statusText);
            callback(undefined);
            return;
        }
        const data = await response.json();
        console.log("succes: New player created ", JSON.stringify(data));
        SetToken(data.token);
        callback(data);

    };

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

    var GetCharacter = async function (id, callback) {
        console.log("Retriving character information with id " + id);
        var url = "api/characters/" + id;
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

    var GetRaces = async function (callback) {
        var url ="api/races"
        var response = await fetch(url);
        var data = await response.json();
        callback(data);
    };
    var SetRace = async function (charid, raceName, callback) {
        var url = "api/characters/" + charid + "/races/" + raceName;
        var response = await fetch(url, options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        console.log("Race set status ", response.status, response.statusText);
        if (response.status !== 200) {
            callback(undefined);
            return;
        }
        var data = await response.json();
        callback(data);
    };

    var UpdateCharacter = async function (id, character, callback) {
        console.log("Updating character with id " + id);
        var url = "api/characters/" + id;
        var response = await fetch(url, options = {
            method: 'PUT',
            body: JSON.stringify(character),
            headers: {

                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        //console.log(character);
        console.log(response.status + " : " + response.statusText);
        if (response.status !== 200) {
            callback(undefined);
            return;
        }
        var data = await response.json();
        console.log(data);
        callback(data);
    };

    var GetSpell = async function (spellId, callback) {
        console.log("Trying to find spell with id: " + spellId);
        url = "api/spells/" + spellId;
        var response = await fetch(url);
        var data = await response.json();
        callback(data);
    };

    var LoadSpellbook = async function (characterId, callback) {
       // var url = "api/characters/" + characterId + "/spells/" + spellLevel;
        var url = "api/characters/" + characterId + "/spells/";
        /*var spell = {
            SpellId: spellId,
            SpellLevel: spellLevel
        };*/

        var response = await fetch(url, options = {
            method: 'GET',
            //body: JSON.stringify(spell),
            headers: {

                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        console.log("Spellbook response", response);
        if (response.status !== 200) {
            console.log(response.statusText);
            callback(null);
            return;
        }

        var data = await response.json();
        callback(data);
    };

    var AddSpell = async function (characterId, spellId, spellLevel, callback) {
        var url = "api/characters/" + characterId +"/spells";

        var spell = {
            SpellId: spellId,
            SpellLevel: spellLevel
        };
        console.log(spell);

        var response = await fetch(url, options = {
            method: 'POST',
            body: JSON.stringify(spell),
            headers: {

                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        console.log("Adding spell to spellbook: ", response.status, response.statusText);
        if (response.status === 201) {
            callback(true);
        }
        else {
            callback(false);
        }
    };

    var RemoveSpell = async function (characterId, spellId, spellLevel, callback) {
        var url = "api/characters/" + characterId + "/spells/" +spellLevel+"/"+spellId;
        var response = await fetch(url, options = {
            method: 'DELETE',
            headers: {

                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        console.log("Was removing spell from spellbook a success? ", response.status, response.statusText);
        if (response.status === 200) {
            callback(true);
        }
        else {
            callback(false);
        }
        
    };

    return {
        Login,
        SignUp,
        getPlayer,
        GetCharacter,
        UpdateCharacter,
        spellSearch,
        GetSpell,
        LoadSpellbook, AddSpell, RemoveSpell,
        GetRaces, SetRace
    }
});