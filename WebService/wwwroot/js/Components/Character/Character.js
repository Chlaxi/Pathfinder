define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {

        /*Subscribes to changes to the Current character in the application.
        When a chagne is made, we call the GetCharacter dunction, to get the information.*/
        app.CurrentCharacter.subscribe(function (data) {
            console.log(JSON.stringify(data));
            GetCharacter(data.id);
        });

        var characterName = ko.observable("");
        var char_str_base = ko.observable("");
        
        var GetCharacter = async function (id) {
            console.log("Getting character with id " + id);

            var character = undefined;

            await ds.GetCharacter(id, function callback(_character){
                character = _character;
            });

            //TODO: Add all mapping from the character. See console for the character object's fields
            characterName(character.name); //Do this to all fields
            char_str_base(character.strength.baseScore);
            

        };

        return {
            characterName,
            GetCharacter,
            char_str_base
        }
    };
});