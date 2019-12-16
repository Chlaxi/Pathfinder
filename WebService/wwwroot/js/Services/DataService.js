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
        var token = await data.token;
        SetToken(token);
        options.headers.Authorization += token;

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
        if (response.status !== 200) {
            callback(undefined);
            return;
        }
        var data = await response.json();

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

    var AddCharacter = async function (id, characterName, callback) {
        url= "api/players/" + id + "/character";
        url += "?characterName=" + characterName;
        var response = await fetch(url, options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        if (response.status !== 201) {
            callback(undefined);
            return;
        }
        var data = await response.json();
        callback(data);
    };
    var RemoveCharacter = async function (playerId, characterId, callback) {
        url = "api/players/" + playerId + "/character/"+characterId;
        var response = await fetch(url, options = {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        if (response.status !== 200) {
            callback(false);
            return;
        }
        callback(true);
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
        if (response.status !== 200) {
            callback(undefined);
            return;
        }
        var data = await response.json();
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

    var GetClasses = async function (callback) {
        var url = "api/classes"
        var response = await fetch(url);
        var data = await response.json();
        callback(data);
    };

    var SetClass = async function (charid, className, level, callback) {
        var url = "api/characters/" + charid + "/classes/" + className;
        var param = "?level=" + level;
        url += param;
        console.log("Trying to set a class using the url"+url)
        var response = await fetch(url, options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        console.log("class set status ", response.status, response.statusText);
        if (response.status !== 200) {
            callback(undefined);
            return;
        }
        var data = await response.json();
        callback(data);
    };

    var LevelUpClass = async function (charid, className, callback) {
        var url = "api/characters/" + charid + "/classes/" + className +"/levelup";
        var response = await fetch(url, options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        console.log("class set status ", response.status, response.statusText);
        if (response.status !== 200) {
            callback(undefined);
            return;
        }
        var data = await response.json(); 
        callback(data);
    };

    var RemoveClass = async function (charid, className, callback) {
        var url = "api/characters/" + charid + "/classes/" + className;
        var response = await fetch(url, options = {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': app.token
            }
        });
        console.log("class set status ", response.status, response.statusText);
        if (response.status !== 200) {
            callback(undefined);
            return;
        }
        callback(response);
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
        GetCharacter, AddCharacter, RemoveCharacter,
        UpdateCharacter,
        spellSearch,
        GetSpell,
        LoadSpellbook, AddSpell, RemoveSpell,
        GetRaces, SetRace,
        GetClasses, SetClass, LevelUpClass, RemoveClass
    }
});