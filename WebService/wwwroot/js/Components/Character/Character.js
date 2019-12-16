define(["knockout", "app", "dataService"], function (ko, app, ds) {
    return function (params) {
        var id = undefined;
        /*Subscribes to changes to the Current character in the application.
        When a chagne is made, we call the GetCharacter dunction, to get the information.*/

        app.sheetInfo.subscribe(function (data) {
            console.log("Sheet info", data);
            if (data.active) {
                GetCharacter(data.id);
                id = data.id;
            }
        });

        var setNull = function (value) {
            if (value === -100 || value === null) {
                return null
            }
            return Number(value);
        };


        var Character = ko.observable({});

        var name = ko.observable("");
        var age = ko.observable("");
        var alignment = ko.observable("");
        var deity = ko.observable("");
        var hair = ko.observable("");
        var eyes = ko.observable("");
        var weight = ko.observable("");
        var height = ko.observable("");
        var gender = ko.observable("");
        var homeland = ko.observable("");

        var race = ko.observable("Race");
        var _class = ko.observable("");

        var str_base = ko.observable("");
        var str_temp = ko.observable("");
        var str_baseMod = ko.computed(function () {
            if (str_base() == null || str_base() == "" || str_base === -100)
                return "";

            return Math.floor((str_base() - 10) / 2);
        });
        var str_tempMod = ko.computed(function () {
            if (str_temp() == null || str_temp() == "" || str_temp === -100)
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
            if (dex_base() == null || dex_base() == "" || dex_base === -100)
                return "";

            return Math.floor((dex_base() - 10) / 2);
        });
        var dex_tempMod = ko.computed(function () {
            if (dex_temp() == null || dex_temp() == "" || dex_temp === -100)
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
            if (con_base() == null || con_base() == "" || con_base === -100)
                return "";

            return Math.floor((con_base() - 10) / 2);
        });
        var con_tempMod = ko.computed(function () {
            if (con_temp() == null || con_temp() == "" || con_temp === -100)
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
            if (int_base() == null || int_base() == "" || int_base === -100)
                return "";

            return Math.floor((int_base() - 10) / 2);
        });
        var int_tempMod = ko.computed(function () {
            if (int_temp() == null || int_temp() == "" || int_temp === -100)
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
            if (wis_base() == null || wis_base() == "" || wis_base === -100)
                return "";

            return Math.floor((wis_base() - 10) / 2);
        });
        var wis_tempMod = ko.computed(function () {
            if (wis_temp() == null || wis_temp() == "" || wis_temp === -100)
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
            if (cha_base() == null || cha_base() == "" || cha_base === -100)
                return "";

            return Math.floor((cha_base() - 10) / 2);
        });
        var cha_tempMod = ko.computed(function () {
            if (cha_temp() == null || cha_temp() == "" || cha_temp === -100)
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

        var bab = ko.observableArray([]);
        var bab_highest = ko.computed(function () {
            console.log("Bab", bab());
            if (bab == undefined || bab == "") {
                return "";
            } else {
                //TODO: Check what happens, when the bab is an array. Should only use the first index
                //return bab(); //Maybe do some string splitting, and take the first array value?
                console.log("There's ", bab().length, "eleemnt in bab");
                if (bab.length < 0) {
                    console.log("highest bab is not set");
                    return "";
                }
                console.log("Base attack Bonus: " + bab()[bab().length-1]);
                return bab()[bab().length - 1];
            }
        });
        var bab_ranged = ko.computed(function () {
            var result = "";
            var dex = dex_total();
            if (dex_total() === null) dex = 0;
            bab().forEach(function (item, index) {
                result += Number(item + dex).toString();
                if (index !== bab.length -1) {
                    result += ", ";
                }
            });
                
            return result;
        });
        var bab_melee = ko.computed(function () {
            var result = "";
            var str = str_total();
            if (str_total() === null) str = 0;
            bab().forEach(function (item, index) {
                result += Number(item + str).toString();
                if (index !== bab.length - 1) {
                    result += ", ";
                }
            });

            return result;
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
            //if temp is not null, set total to that.
            return Number(fort_base()) + Number(con_total()) + Number(fort_magic()) + Number(fort_misc());
        });
        var fort_note = ko.observable("");

        var ref_base = ko.observable("");
        var ref_magic = ko.observable("");
        var ref_misc = ko.observable("");
        var ref_temp = ko.observable("");
        var ref_total = ko.computed(function () {
            //if temp is not null, set total to that.
            return Number(ref_base()) + Number(dex_total()) + Number(ref_magic()) + Number(ref_misc());
        });
        var ref_note = ko.observable("");

        var will_base = ko.observable("");
        var will_magic = ko.observable("");
        var will_misc = ko.observable("");
        var will_temp = ko.observable("");
        var will_total = ko.computed(function () {
            //if temp is not null, set total to that.
            return Number(will_base()) + Number(wis_total()) + Number(will_magic()) + Number(will_misc());
        });
        var will_note = ko.observable("");

        var damage_reduction = ko.observable("");
        var spell_resistance = ko.observable("");
        var resistance = ko.observable("");
        var immunity = ko.observable("");

        var speed_racial = ko.observable("");
        var speed_base = ko.observable("");
        var speed_mod = ko.observable("");
        var speed_tempBase = ko.observable("");
        var speed_temporary = ko.observable("");
        var speed_total = ko.computed(function () {
            return Number(speed_base()) + Number(speed_mod());
        });
        var speed_armour_total = ko.observable("");
        var speed_fly = ko.observable("");
        var speed_climb = ko.observable("");
        var speed_burrow = ko.observable("");
        var speed_swim = ko.observable("");

        var cmd_misc = ko.observable("");
        var cmd_temp = ko.observable("");
        var cmd_note = ko.observable("");
        var cmd_total = ko.computed(function () {
            return 10 + Number(bab_highest()) + Number(str_total()) + Number(dex_total()) + Number(size_defensive()) + Number(cmd_misc());
        });

        var cmb_misc = ko.observable("");
        var cmb_temp = ko.observable("");
        var cmb_note = ko.observable("");
        var cmb_total = ko.computed(function () {
            return Number(bab_highest()) + Number(str_total()) + Number(size_defensive()) + Number(cmb_misc());
        });

        var copper = ko.observable("");
        var silver = ko.observable("");
        var gold = ko.observable("");
        var platinum = ko.observable("");

        var languages = ko.observable("");
        var note = ko.observable("");



        var GetCharacter = async function (id) {
            var character = undefined;
            var classInfo;
            await ds.GetCharacter(id, function callback(_character) {
                character = _character;

            });
            classInfo = GetClassInfo(character.class);
            //TODO: Add all mapping from the character. See console for the character object's fields
            name(character.name); //Do this to all fields
            age(character.age);
            alignment(character.alignment); //From int to enum
            deity(character.deity);
            hair(character.hair);
            eyes(character.eyes);
            weight(character.weight);
            height(character.height);
            gender(character.gender);
            homeland(character.homeland);

            race(character.raceName);
            _class(GetClass(character.class));

            //Abilities
            str_base(setNull(character.strength.baseScore));
            str_temp(setNull(character.strength.tempScore));

            dex_base(setNull(character.dexterity.baseScore));
            dex_temp(setNull(character.dexterity.tempScore));

            con_base(setNull(character.constitution.baseScore));
            con_temp(setNull(character.constitution.tempScore));

            int_base(setNull(character.intelligence.baseScore));
            int_temp(setNull(character.intelligence.tempScore));

            wis_base(setNull(character.wisdom.baseScore));
            wis_temp(setNull(character.wisdom.tempScore));

            cha_base(setNull(character.charisma.baseScore));
            cha_temp(setNull(character.charisma.tempScore));

            bab(classInfo.bab);

            //initiative(character.initiative);
            initiative_misc(setNull(character.initiativeMiscModifier));

            hp_current(setNull(character.hitPoints.currentHitPoints));
            hp_total(setNull(character.hitPoints.maxHitPoints));
            hp_non_lethal(setNull(character.hitPoints.nonLethalDamage));
            hp_wounds(character.hitPoints.wounds);

            size_defensive(character.ac.size); //Set null?
            ac_armour(setNull(character.ac.armour));
            ac_shield(setNull(character.ac.shield));
            ac_natural(setNull(character.ac.naturalArmour));
            ac_deflection(setNull(character.ac.deflection));
            ac_misc(setNull(character.ac.misc));

            fort_base(classInfo.fortitude);
            fort_magic(setNull(character.fortitude.magic));
            fort_misc(setNull(character.fortitude.misc));
            fort_temp(setNull(character.fortitude.temporary));
            fort_note(character.fortitude.note);

            ref_base(classInfo.reflex);
            ref_magic(setNull(character.reflex.magic));
            ref_misc(setNull(character.reflex.misc));
            ref_temp(setNull(character.reflex.temporary));
            ref_note(character.reflex.note);

            will_base(classInfo.will);
            will_magic(setNull(character.will.magic));
            will_misc(setNull(character.will.misc));
            will_temp(setNull(character.will.temporary));
            will_note(character.will.note);

            damage_reduction(character.damageReduction);
            spell_resistance(character.spellResistance);
            resistance(character.resistance);
            immunity(character.immunity);

            speed_racial(character.speed.racialModifier);
            speed_base(character.speed.base);
            speed_mod(setNull(character.speed.baseModifier));
            speed_armour_total(setNull(character.speed.armour));
            speed_fly(setNull(character.speed.fly));
            speed_burrow(setNull(character.speed.burrow));
            speed_climb(setNull(character.speed.climb));
            speed_swim(setNull(character.speed.swim));
            speed_tempBase(setNull(character.speed.baseTempModifier));
            speed_temporary(setNull(character.speed.temporary));

            cmd_misc(setNull(character.cmd.misc));
            cmd_temp(setNull(character.cmd.temp));
            cmd_note(character.cmd.note);

            cmb_misc(setNull(character.cmb.misc));
            cmb_temp(setNull(character.cmb.temp));
            cmb_note(character.cmb.note);

            copper(character.copper);
            silver(character.silver);
            gold(character.gold);
            platinum(character.platinum);

            languages(character.languages);
            note(character.note);
            Character(character);

            app.CurrentCharacter(character);
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
                        continue;
                    }
                    classInfo.bab[i] += levelInfo.baseAttackBonus[i];
                }

            });
            return classInfo;
        }

        var SaveChanges = async function () {
            var character = {
                Name: name(),
                Gender: gender(),

                Age: age(),
                Hair: hair(),
                Eyes: eyes(),
                Homeland: homeland(),
                Deity: deity(),
                //                Alignment: alignment(),
                Weight: weight(),
                Height: height(),
                //Race: race(),
                //Class: _class(),
                Strength: {
                    BaseScore: checkNull(str_base()),
                    TempScore: checkNull(str_temp()),
                },
                Dexterity: {
                    BaseScore: checkNull(dex_base()),
                    TempScore: checkNull(dex_temp()),
                },
                Constitution: {
                    BaseScore: checkNull(con_base()),
                    TempScore: checkNull(con_temp()),
                },
                Intelligence: {
                    BaseScore: checkNull(int_base()),
                    TempScore: checkNull(int_temp()),
                },
                Wisdom: {
                    BaseScore: checkNull(wis_base()),
                    TempScore: checkNull(wis_temp()),
                },
                Charisma: {
                    BaseScore: checkNull(cha_base()),
                    TempScore: checkNull(cha_temp()),
                },

                InitiativeMiscModifier: checkNull(initiative_misc()),

                HitPoints: {
                    CurrentHitPoints: checkNull(hp_current()),
                    MaxHitPoints: checkNull(hp_total()),
                    NonLethalDamage: checkNull(hp_non_lethal()),
                    Wounds: hp_wounds(),
                },

                AC: {

                    Armour: checkNull(ac_armour()),
                    Shield: checkNull(ac_shield()),
                    NaturalArmour: checkNull(ac_natural()),
                    Deflection: checkNull(ac_deflection()),
                    Misc: checkNull(ac_misc())
                    //TouchMisc
                    //FlatFootedMisc
                },

                Fortitude: {
                    Magic: checkNull(fort_magic()),
                    Temporary: checkNull(fort_temp()),
                    Misc: checkNull(fort_misc()),
                    Note: fort_note()
                },
                Reflex: {
                    Magic: checkNull(ref_magic()),
                    Temporary: checkNull(ref_temp()),
                    Misc: checkNull(ref_misc()),
                    Note: ref_note()
                },

                Will: {
                    Magic: checkNull(will_magic()),
                    Temporary: checkNull(will_temp()),
                    Misc: checkNull(will_misc()),
                    Note: will_note()
                },

                DamageReduction: damage_reduction(),
                SpellResistance: spell_resistance(),
                Resistance: resistance(),
                Immunity: immunity(),

                Speed: {
                    BaseModifier: checkNull(speed_mod()),
                    BaseTempModifier: checkNull(speed_tempBase()),
                    Armour: checkNull(speed_armour_total()),
                    Fly: checkNull(speed_fly()),
                    Swim: checkNull(speed_swim()),
                    Climb: checkNull(speed_climb()),
                    Burrow: checkNull(speed_burrow()),
                    Temporary: checkNull(speed_temporary())
                },
                CMB: {
                    Misc: checkNull(cmb_misc())
                },
                CMD: {
                    Misc: checkNull(cmd_misc())
                },

                Copper: Number(copper()),
                Silver: Number(silver()),
                Gold: Number(gold()),
                Platinum: Number(platinum()),
                Languages: languages(),
                Note: note()
            };

            await ds.UpdateCharacter(id, character, function (result) {
                console.log(result);
            });
        }

        var checkNull = function (value) {
            //us typeof === number? 
            if (value === "" || value === null) {
                value = -100;
            }
            value=Number(value);
            if (isNaN(value)) {
                value = -100;
            }
            return value;
        };


        var RaceModal = function () {
            app.RaceModalState(!app.RaceModalState());
        };

        var ClassModal = function () {
            app.ClassModalState(!app.ClassModalState());
        };

        return {
            name, age, alignment, deity, hair, eyes, weight, height, gender, homeland,
            race, _class,
            GetCharacter,
            str_base, str_temp, str_baseMod, str_tempMod, str_total,
            dex_base, dex_temp, dex_baseMod, dex_tempMod, dex_total,
            con_base, con_temp, con_baseMod, con_tempMod, con_total,
            int_base, int_temp, int_baseMod, int_tempMod, int_total,
            wis_base, wis_temp, wis_baseMod, wis_tempMod, wis_total,
            cha_base, cha_temp, cha_baseMod, cha_tempMod, cha_total,
            bab, bab_highest, bab_ranged, bab_melee,
            hp_current, hp_total, hp_non_lethal, hp_wounds,
            initiative, initiative_misc,
            ac_total, size_defensive, ac_armour, ac_shield, ac_natural, ac_deflection, ac_misc,
            ac_touch_misc, ac_touch_total, ac_ff_misc, ac_ff_total,
            fort_base, fort_magic, fort_misc, fort_total, fort_temp, fort_note,
            ref_base, ref_magic, ref_misc, ref_total, ref_temp, ref_note,
            will_base, will_magic, will_misc, will_total, will_temp, will_note,
            speed_base, speed_mod, speed_total, speed_armour_total, speed_racial, speed_tempBase, speed_temporary,
            speed_fly, speed_burrow, speed_climb, speed_swim,
            cmd_total, cmd_misc, cmd_temp, cmd_note, cmb_total, cmb_misc, cmb_temp, cmb_note,
            SaveChanges, checkNull,
            copper, silver, gold, platinum, 
            damage_reduction, spell_resistance, resistance, immunity,
            languages, note,
            Character,
            RaceModal, ClassModal
        }
    };
});