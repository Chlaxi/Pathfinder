define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {

        /*Subscribes to changes to the Current character in the application.
        When a chagne is made, we call the GetCharacter dunction, to get the information.*/
        app.CurrentCharacter.subscribe(function (data) {
            console.log(JSON.stringify(data));
            GetCharacter(data.id);
        });

        var name = ko.observable("");
        var age = ko.observable("");
        var alignment = ko.observable("");
        var diety = ko.observable("");
        var hair = ko.observable("");
        var eyes = ko.observable("");
        var weight = ko.observable("");
        var height = ko.observable("");
        var gender = ko.observable("");
        var homeland = ko.observable("");

        var race = ko.observable("");
        var _class = ko.observable("");

        var str_base = ko.observable("");
        var str_temp = ko.observable("");
        var str_baseMod = ko.computed(function () {
            if (str_base() == null || str_base() == "")
                return "";

            return Math.floor((str_base() - 10) / 2);
        });
        var str_tempMod = ko.computed(function () {
            if (str_temp() == null || str_temp() == "")
                return "";
            return Math.floor((str_temp() - 10) / 2);
        });

        var str_total = ko.computed(function () {
            if (str_temp() != "" && str_temp() != null) {
                return str_tempMod();
            }
            else {

                return str_baseMod();
            }
        });

        var dex_base = ko.observable("");
        var dex_temp = ko.observable("");
        var dex_baseMod = ko.computed(function () {
            if (dex_base() == null || dex_base() == "")
                return "";

            return Math.floor((dex_base() - 10) / 2);
        });
        var dex_tempMod = ko.computed(function () {
            if (dex_temp() == null || dex_temp() == "")
                return "";
            return Math.floor((dex_temp() - 10) / 2);
        });

        var dex_total = ko.computed(function () {
            if (dex_temp() != "" && dex_temp() != null) {
                return dex_tempMod();
            }
            else {

                return dex_baseMod();
            }
        });

        var con_base = ko.observable("");
        var con_temp = ko.observable("");
        var con_baseMod = ko.computed(function () {
            if (con_base() == null || con_base() == "")
                return "";

            return Math.floor((con_base() - 10) / 2);
        });
        var con_tempMod = ko.computed(function () {
            if (con_temp() == null || con_temp() == "")
                return "";
            return Math.floor((con_temp() - 10) / 2);
        });

        var con_total = ko.computed(function () {
            if (con_temp() != "" && con_temp() != null) {
                return con_tempMod();
            }
            else {

                return con_baseMod();
            }
        });

        var int_base = ko.observable("");
        var int_temp = ko.observable("");
        var int_baseMod = ko.computed(function () {
            if (int_base() == null || int_base() == "")
                return "";

            return Math.floor((int_base() - 10) / 2);
        });
        var int_tempMod = ko.computed(function () {
            if (int_temp() == null || int_temp() == "")
                return "";
            return Math.floor((int_temp() - 10) / 2);
        });

        var int_total = ko.computed(function () {
            if (int_temp() != "" && int_temp() != null) {
                return int_tempMod();
            }
            else {

                return int_baseMod();
            }
        });

        var wis_base = ko.observable("");
        var wis_temp = ko.observable("");
        var wis_baseMod = ko.computed(function () {
            if (wis_base() == null || wis_base() == "")
                return "";

            return Math.floor((wis_base() - 10) / 2);
        });
        var wis_tempMod = ko.computed(function () {
            if (wis_temp() == null || wis_temp() == "")
                return "";
            return Math.floor((wis_temp() - 10) / 2);
        });

        var wis_total = ko.computed(function () {
            if (wis_temp() != "" && wis_temp() != null) {
                return wis_tempMod();
            }
            else {

                return wis_baseMod();
            }
        });

        var cha_base = ko.observable("");
        var cha_temp = ko.observable("");
        var cha_baseMod = ko.computed(function () {
            if (cha_base() == null || cha_base() == "")
                return "";

            return Math.floor((cha_base() - 10) / 2);
        });
        var cha_tempMod = ko.computed(function () {
            if (cha_temp() == null || cha_temp() == "")
                return "";
            return Math.floor((cha_temp() - 10) / 2);
        });

        var cha_total = ko.computed(function () {
            if (cha_temp() != "" && cha_temp() != null) {
                return cha_tempMod();
            }
            else {

                return cha_baseMod();
            }
        });

        var bab = ko.observableArray("");
        var bab_highest = ko.computed(function () {
            if (bab == undefined || bab == "") {
                return "";
            } else {
                //TODO: Check what happens, when the bab is an array. Should only use the first index
                //return bab(); //Maybe do some string splitting, and take the first array value?
                if (bab.length < 0) {
                    console.log("highest bab is not set");
                    return "";
                }
                console.log("Base attack Bonus: " + bab()[0]);
                return bab()[0];
            }
        });

        var hp_current = ko.observable("");
        var hp_total = ko.observable("");
        var hp_non_lethal = ko.observable("");
        var hp_wounds = ko.observable("");

        var initiative_misc = ko.observable("");
        var initiative = ko.computed(function () {
            return dex_total() + Number(initiative_misc());
        });



        var size_defensive = ko.observable("");
        var size_offensive = ko.observable("");

        var ac_armour = ko.observable("");
        var ac_shield = ko.observable("");
        var ac_natural = ko.observable("");
        var ac_deflection = ko.observable("");
        var ac_misc = ko.observable("");

        var ac_total = ko.computed(function () {
            return 10 + Number(dex_total()) + Number(size_defensive()) + Number(ac_armour())
                + Number(ac_shield()) + Number(ac_natural()) + Number(ac_deflection())
                + Number(ac_misc());
        });

        var ac_touch_misc = ko.observable("");
        //TODO: Check calculation
        var ac_touch_total = ko.computed(function () {
            return 10 + Number(dex_total()) + Number(size_defensive()) + Number(ac_armour())
                + Number(ac_shield()) + Number(ac_natural()) + Number(ac_deflection())
                + Number(ac_touch_misc());
        });
        var ac_ff_misc = ko.observable("");
        //TODO: Check calculation
        var ac_ff_total = ko.computed(function () {
            return 10 + Number(size_defensive()) + Number(ac_armour())
                + Number(ac_shield()) + Number(ac_natural()) + Number(ac_deflection())
                + Number(ac_ff_misc());
        });

        var fort_base = ko.observable("");
        var fort_magic = ko.observable("");
        var fort_misc = ko.observable("");
        var fort_temp = ko.observable("");
        var fort_total = ko.computed(function () {
            return Number(fort_base()) + Number(con_total()) + Number(fort_magic()) + Number(fort_misc());
        });
        var fort_note = ko.observable("");

        var ref_base = ko.observable("");
        var ref_magic = ko.observable("");
        var ref_misc = ko.observable("");
        var ref_temp = ko.observable("");
        var ref_total = ko.computed(function () {
            return Number(ref_base()) + Number(dex_total()) + Number(ref_magic()) + Number(ref_misc());
        });
        var ref_note = ko.observable("");

        var will_base = ko.observable("");
        var will_magic = ko.observable("");
        var will_misc = ko.observable("");
        var will_temp = ko.observable("");
        var will_total = ko.computed(function () {
            return Number(will_base()) + Number(wis_total()) + Number(will_magic()) + Number(will_misc());
        });
        var will_note = ko.observable("");

        var speed_racial = ko.observable("");
        var speed_base = ko.observable("");
        var speed_mod = ko.observable("");
        var speed_total = ko.computed(function () {
            return Number(speed_base()) + Number(speed_mod());
        });
        var speed_armour_total = ko.observable("");
        var speed_fly = ko.observable("");
        var speed_climb = ko.observable("");
        var speed_burrow = ko.observable("");
        var speed_swim = ko.observable("");

        var cmd_misc = ko.observable("");
        var cmd_total = ko.computed(function () {
            return 10 + Number(bab_highest()) + Number(str_total()) + Number(dex_total()) + Number(size_defensive()) + Number(cmd_misc());
        });

        var cmb_misc = ko.observable("");
        var cmb_total = ko.computed(function () {
            return Number(bab_highest()) + Number(str_total()) + Number(size_defensive()) + Number(cmb_misc());
        });

        var GetCharacter = async function (id) {
            console.log("Getting character with id " + id);

            var character = undefined;
            var classInfo;
            await ds.GetCharacter(id, function callback(_character){
                character = _character;
            });
            classInfo = GetClassInfo(character.class);
            //TODO: Add all mapping from the character. See console for the character object's fields
            name(character.name); //Do this to all fields
            age(character.age);
            alignment(character.alignment); //From int to enum
            diety(character.diety);
            hair(character.hair);
            eyes(character.eyes);
            weight(character.weight);
            height(character.height);
            gender(character.gender);
            homeland(character.homeland);

            race(character.raceName);
            _class(GetClass(character.class));

        //Abilities
            str_base(character.strength.baseScore);
            str_temp(character.strength.tempScore);

            dex_base(character.dexterity.baseScore);
            dex_temp(character.dexterity.tempScore);

            con_base(character.constitution.baseScore);
            con_temp(character.constitution.tempScore);

            int_base(character.intelligence.baseScore);
            int_temp(character.intelligence.tempScore);

            wis_base(character.wisdom.baseScore);
            wis_temp(character.wisdom.tempScore);

            cha_base(character.charisma.baseScore);
            cha_temp(character.charisma.tempScore);

            bab(classInfo.bab);

            //initiative(character.initiative);
            initiative_misc(character.initiativeMiscModifier);

            hp_current(character.hitPoints.currentHitPoints);
            hp_total(character.hitPoints.maxHitPoints);
            hp_non_lethal(character.hitPoints.non_lethal);
            hp_wounds(character.hitPoints.wounds);

            size_defensive(character.ac.size);
            ac_armour(character.ac.armour);
            ac_shield(character.ac.shield);
            ac_natural(character.ac.naturalArmour);
            ac_deflection(character.ac.deflection);
            ac_misc(character.ac.misc);

            fort_base(classInfo.fortitude);
            fort_magic(character.fortitude.magic);
            fort_note(character.fortitude.misc);
            fort_temp(character.fortitude.temp);
            fort_note(character.fortitude.note);

            ref_base(classInfo.reflex);
            ref_magic(character.reflex.magic);
            ref_note(character.reflex.misc);
            ref_temp(character.reflex.temp);
            ref_note(character.reflex.note);

            will_base(classInfo.will);
            will_magic(character.will.magic);
            will_note(character.will.misc);
            will_temp(character.will.temp);
            will_note(character.will.note);

            speed_racial(character.speed.racialModifier);
            speed_base(character.speed.base);
            speed_mod(character.speed.baseModifier);
            speed_armour_total(character.speed.armour);
            speed_fly(character.speed.fly);
            speed_burrow(character.speed.burrow);
            speed_climb(character.speed.climb);
            speed_swim(character.speed.swim);

            cmd_misc(character.cmd.misc);
            cmb_misc(character.cmb.misc);

        };

        var GetClass = function (classes) {
            var _class = "";
            classes.forEach(function (item, index) {
                _class += item.className + " " + item.level;
                //Ensures we only add a , between the classes, if we aren't at the last class in the array
                if (index != classes.length -1) {
                    _class += ", ";
                }
            });
            return _class;
        }

        var GetClassInfo = function (classes){
            var classInfo = {
                fortitude: 0,
                reflex: 0,
                will: 0,
                bab: [],
                specials: []
            };
            classes.forEach(function (item, index) {

                var levelInfo = item.class.levelInfo[item.level-1]
                console.log(levelInfo);
                for (var i = 0; i < item.class.levelInfo.length; i++) {
                    classInfo.specials += item.class.levelInfo[i].specials;
                }
                classInfo.fortitude += levelInfo.baseFortitude;
                classInfo.reflex += levelInfo.baseReflex;
                classInfo.will += levelInfo.baseWill;
                for (var i = 0; i < levelInfo.baseAttackBonus.length; i++) {
                    //TODO: Remove debugs
                    if (classInfo.bab.length < levelInfo.baseAttackBonus.length) {
                        classInfo.bab.push(levelInfo.baseAttackBonus[i]);
                        console.log("bab at index " + i + " was set to " + classInfo.bab[i]);
                        continue;
                    }
                    classInfo.bab[i] += levelInfo.baseAttackBonus[i];
                    console.log("bab at index " + i + " was set to " + classInfo.bab[i]);
                }

            });
            console.log("The character had the following data from their classes:");
            console.log(classInfo);
            return classInfo;
        }

        return {
            name, age, alignment, diety, hair, eyes, weight, height, gender, homeland,
            race, _class,
            GetCharacter,
            str_base, str_temp, str_baseMod, str_tempMod, str_total,
            dex_base, dex_temp, dex_baseMod, dex_tempMod, dex_total,
            con_base, con_temp, con_baseMod, con_tempMod, con_total,
            int_base, int_temp, int_baseMod, int_tempMod, int_total,
            wis_base, wis_temp, wis_baseMod, wis_tempMod, wis_total,
            cha_base, cha_temp, cha_baseMod, cha_tempMod, cha_total,
            bab, bab_highest,
            hp_current, hp_total, hp_non_lethal, hp_wounds,
            initiative, initiative_misc,
            ac_total, size_defensive, ac_armour, ac_shield, ac_natural, ac_deflection, ac_misc,
            ac_touch_misc, ac_touch_total, ac_ff_misc, ac_ff_total,
            fort_base, fort_magic, fort_misc, fort_total, fort_temp, fort_note,
            ref_base, ref_magic, ref_misc, ref_total, ref_temp, ref_note,
            will_base, will_magic, will_misc, will_total, will_temp, will_note,
            speed_base, speed_mod, speed_total, speed_armour_total, speed_racial,
            speed_fly, speed_burrow, speed_climb, speed_swim,
            cmd_total, cmd_misc, cmb_total, cmb_misc
        }
    };
});