﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DeepDungeonDex
{
    [Flags]
    public enum Vulnerabilities
    {
        None = 0x00,
        Stun = 0x01,
        Heavy = 0x02,
        Slow = 0x04,
        Sleep = 0x08,
        Bind = 0x10,
        Undead = 0x20
    }

    public enum ThreatLevel
    {
        Unspecified,
        Easy,
        Caution,
        Dangerous,
        Vicious
    }

    public enum AggroType
    {
        Unspecified,
        Sight,
        Sound,
        Proximity,
        Boss
    }

    public struct MobData
    {
        public Vulnerabilities Vuln { get; set; }
        public string MobNotes { get; set; }
        public ThreatLevel Threat { get; set; }
        public AggroType Aggro { get; set; }

        #region Constructors
        /// <summary>
        /// Mob data struct init
        /// </summary>
        /// <param name="level">The threat level</param>
        /// <param name="aggro">The aggro type</param>
        /// <param name="mobNotes">Notes for the mob</param>
        /// <param name="vuln">The vulnerabilities for the mob</param>
        public MobData(ThreatLevel level, AggroType aggro, Vulnerabilities vuln, string mobNotes)
        {
            Threat = level;
            Aggro = aggro;
            MobNotes = mobNotes;
            Vuln = vuln;
        }
        /// <summary>
        /// Mob data struct init
        /// </summary>
        /// <param name="level">The threat level</param>
        /// <param name="aggro">The aggro type</param>
        /// <param name="mobNotes">Notes for the mob</param>
        public MobData(ThreatLevel level, AggroType aggro, string mobNotes) : this(level, aggro, Vulnerabilities.None, mobNotes) { }
        /// <summary>
        /// Mob data struct init
        /// </summary>
        /// <param name="level">The threat level</param>
        /// <param name="aggro">The aggro type</param>
        public MobData(ThreatLevel level, AggroType aggro) : this(level, aggro, "") { }
        /// <summary>
        /// Mob data struct init
        /// </summary>
        /// <param name="level">The threat level</param>
        /// <param name="aggro">The aggro type</param>
        /// <param name="stun">Can the mob be stunned</param>
        public MobData(ThreatLevel level, AggroType aggro, bool stun) : this(level, aggro, stun, "") { }
        /// <summary>
        /// Mob data struct init
        /// </summary>
        /// <param name="level">The threat level</param>
        /// <param name="aggro">The aggro type</param>
        /// <param name="vuln">The vulnerabilities for the mob</param>
        public MobData(ThreatLevel level, AggroType aggro, Vulnerabilities vuln) : this(level, aggro, vuln, "") { }
        /// <summary>
        /// Mob data struct init
        /// </summary>
        /// <param name="level">The threat level</param>
        /// <param name="aggro">The aggro type</param>
        /// <param name="vulnTuple">Can the mob be afflicted with stun, heavy, slow, sleep and bind and is the mob undead. (stun, heavy, slow, sleep, bind, undead)</param>
        public MobData(ThreatLevel level, AggroType aggro, ITuple vulnTuple) : this(level, aggro, vulnTuple, "") { }
        /// <summary>
        /// Mob data struct init
        /// </summary>
        /// <param name="level">The threat level</param>
        /// <param name="aggro">The aggro type</param>
        /// <param name="mobNotes">Notes for the mob</param>
        /// <param name="stun">Can the mob be stunned</param>
        public MobData(ThreatLevel level, AggroType aggro, bool stun, string mobNotes) : this(level, aggro, Tuple.Create(stun, false), mobNotes) { }
        /// <summary>
        /// Mob data struct init
        /// </summary>
        /// <param name="level">The threat level</param>
        /// <param name="aggro">The aggro type</param>
        /// <param name="vulnTuple">Can the mob be afflicted with stun, heavy, slow, sleep and bind and is the mob undead. (stun, heavy, slow, sleep, bind, undead)</param>
        /// <param name="mobNotes">Notes for the mob</param>
        public MobData(ThreatLevel level, AggroType aggro, ITuple vulnTuple, string mobNotes) : this(level, aggro, vulnTuple.Get(), mobNotes) { }
        #endregion
    }

    public class DataHandler
    {
        public static MobData? Mobs(uint nameId)
        {
            if (_mobs.TryGetValue(nameId, out var value)) return value;
            return null;
        }

        private static readonly Dictionary<uint, MobData> _mobs = new Dictionary<uint, MobData>
        {
			// HoH floors 1-9
            { 7262, new MobData(ThreatLevel.Easy, AggroType.Sight, true, "Auto inflicts Heavy debuff")},
            { 7263, new MobData(ThreatLevel.Easy, AggroType.Sight, true, "Auto applies Physical Vuln Up every 10s")},
            { 7264, new MobData(ThreatLevel.Easy, AggroType.Sight, true, "AoE applies Paralysis")},
            { 7265, new MobData(ThreatLevel.Dangerous, AggroType.Proximity, true, "Triple auto inflicts Bleed")},
            { 7266, new MobData(ThreatLevel.Caution, AggroType.Sight, true, "Untelegraphed Sleep followed by AoE")},
            { 7267, new MobData(ThreatLevel.Easy, AggroType.Sight, "AoE applies Bleed")},
            { 7268, new MobData(ThreatLevel.Easy, AggroType.Sight, true, "Gaze")},
            { 7269, new MobData(ThreatLevel.Easy, AggroType.Proximity, true)},
            { 7270, new MobData(ThreatLevel.Easy, AggroType.Sight, true, "AoE inflicts knockback")},
            { 7271, new MobData(ThreatLevel.Easy, AggroType.Sight, true, "Conal AoE inflicts Bleed\nCircle AoE inflicts knockback")},
            { 7272, new MobData(ThreatLevel.Dangerous, AggroType.Sight, true, "Unavoidable tankbuster-like \"Jaws\"")},
            { 7273, new MobData(ThreatLevel.Caution, AggroType.Sight, true, "Untelegraphed buster inflicts Bleed and knockback")},
            { 7274, new MobData(ThreatLevel.Easy, AggroType.Sight, true)},
			// HoH floors 11-19
            //{ 7275, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7276, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7277, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7278, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7279, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Lite buster \"Scissor Run\" followed by AoE" } },
            //{ 7280, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7281, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Gaze inflicts Seduce, followed by large AoE that inflicts Minimum" } },
            //{ 7282, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7283, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7284, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="" } },
            //{ 7285, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Buster and triple auto" } },
            //{ 7286, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Room wide ENRAGE" } },
            //{ 7287, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
			// HoH floors 21-29
            //{ 7288, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanHeavy = true, CanBind = false}, MobNotes="Gaze inflicts Blind" } },
            //{ 7289, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanHeavy = true, CanSleep = true}, MobNotes="Cures self and allies" } },
            //{ 7290, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false}, MobNotes="Casts AoEs with knockback unaggroed\nLine AoE inflicts Bleed" } },
            //{ 7291, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true}, MobNotes="Buffs own damage" } },
            //{ 7292, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false}, MobNotes="Untelegraphed conal AoE with knockback, buster" } },
            //{ 7293, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="" } },
            //{ 7294, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true}, MobNotes="" } },
            //{ 7295, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Draw-in followed by cleave" } },
            //{ 7296, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Gaze" } },
            //{ 7297, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true}, MobNotes="Line AoE inflicts Bleed" } },
            //{ 7298, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanHeavy = true}, MobNotes="Cross AoE inflicts Suppuration" } },
            //{ 7299, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Large AoE inflicts Paralysis" } },
            //{ 7300, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Circle AoE inflicts Suppuration" } }, 
            //HoH floors 31-39
            //{ 7301, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7302, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Casts AoEs unaggroed" } },
            //{ 7303, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Double auto inflicts Bleed\nLow health ENRAGE" } },
            //{ 7304, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Low health ENRAGE" } },
            //{ 7305, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Line AoE inflicts Bleed\nLow health ENRAGE" } },
            //{ 7306, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Cleaves every other auto" } },
            //{ 7307, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7308, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Weak stack attack" } },
            //{ 7309, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7310, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Extremely large AoE" } },
            //{ 7311, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Line AoE inflicts Bleed\nLow health ENRAGE" } },
            //{ 7312, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Frontal cleave without cast or telegraph" } },
            //{ 7313, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Gaze inflicts Otter" } },
			// HoH floors 41-49
            //{ 7314, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Casts AoEs unaggroed" } },
            //{ 7315, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7316, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7317, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7318, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Large line AoE\nEventual ENRAGE" } },
            //{ 7319, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Casts AoEs unaggroed" } },
            //{ 7320, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Purple: double auto" } },
            //{ 7321, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Large cone AoE" } },
            //{ 7322, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7323, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Green: Casts AoEs unaggroed" } },
            //{ 7324, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Very wide line AoE" } },
            //{ 7325, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7326, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Eventual ENRAGE" } },
			//HoH floors 51-59
            //{ 7327, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Autos inflict stacking vuln up" } },
            //{ 7328, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Buster inflicts Bleed" } },
            //{ 7329, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Buffs own damage" } },
            //{ 7330, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Eventual instant ENRAGE" } },
            //{ 7331, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Cone AoE inflicts Bleed" } },
            //{ 7332, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Exclusively fatal line AoEs" } },
            //{ 7333, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7334, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7335, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Draw-in attack" } },
            //{ 7336, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Instant AoEs on targeted player unaggroed" } },
            //{ 7337, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Conal gaze, very quick low health ENRAGE" } },
            //{ 7338, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="" } },
            //{ 7339, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="" } },
			// HoH floors 61-69
            //{ 7340, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Inflicts stacking Poison that lasts 30s" } },
            //{ 7341, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Inflicts stacking vuln up" } },
            //{ 7342, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7343, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Fast alternating line AoEs that inflict Paralysis" } },
            //{ 7344, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Caster, double auto" } },
            //{ 7345, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Conal AoE inflicts Paralysis" } },
            //{ 7346, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Cleave and potent Poison" } },
            //{ 7347, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Large doughnut AoE, gaze attack inflicts Fear" } },
            //{ 7348, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Large circular AoE inflicts Bleed" } },
            //{ 7349, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Buffs own or ally's defense" } },
            //{ 7350, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="AoE inflicts numerous debuffs at once" } },
            //{ 7351, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
			// HoH floors 71-79
            //{ 7352, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7353, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Casts large AoE unaggroed\nExtremely large circular AoE" } },
            //{ 7354, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Untelegraphed knockback on rear" } },
            //{ 7355, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Double auto inflicts Bleed" } },
            //{ 7356, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Casts AoEs unaggroed that inflict Deep Freeze" } },
            //{ 7357, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Casts room wide AoEs unaggroed\nLarge conal draw-in attack followed by heavy damage" } },
            //{ 7358, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Buffs own damage" } },
            //{ 7359, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Haste, eventual ENRAGE" } },
            //{ 7360, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Very large AoEs" } },
            //{ 7361, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Draw-in attack, extremely large AoE, eventual ENRAGE" } },
            //{ 7362, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Extremely large conal AoE, gaze inflicts Fear" } },
            //{ 7363, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="" } },
            //{ 7364, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Double auto and very large AoE" } },
			// HoH floors 81-89
            //{ 7365, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Ram's Voice - get out\nDragon's Voice - get in\nTelegraphed cleaves" } },
            //{ 7366, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Buffs own damage unaggroed\nLarge AoE unaggroed that inflicts vuln up and stacks" } },
            //{ 7367, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Charges on aggro" } },
            //{ 7368, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Untelegraphed conal AoE on random player, gaze attack" } },
            //{ 7369, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Casts AoEs unaggroed" } },
            //{ 7370, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Double autos, very strong rear cleave if behind" } },
            //{ 7371, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Alternates line and circle AoEs untelegraphed" } },
            //{ 7372, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Buffs own damage and double autos" } },
            //{ 7373, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Draw-in attack, tons of bleed, and a stacking poison" } },
            //{ 7374, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Large doughnut AoE" } },
            //{ 7375, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Cone AoE, circle AoE, party wide damage" } },
            //{ 7376, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Charges, buffs own damage, double autos, electricity Bleed" } },
            //{ 7377, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Charges, buffs own damage, untelegraphed buster \"Ripper Claw\"" } },
			// HoH floors 91-99
            //{ 7378, new MobData { Threat=MobData.ThreatLevel.Vicious, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="WAR: Triple knockback with heavy damage\nBuffs own attack\nExtremely high damage cleave with knockback" } },
            //{ 7379, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="MNK: Haste buff, short invuln" } },
            //{ 7380, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="WHM: double autos\n\"Stone\" can be line of sighted" } },
            //{ 7381, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Cleave\nLarge line AoE that can be line of sighted" } },
            //{ 7382, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="\"Charybdis\" AoE that leaves tornadoes on random players" } },
            //{ 7383, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="" } },
            //{ 7384, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Casts targeted AoEs unaggroed, buffs own defense" } },
            //{ 7385, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Targeted AoEs, cleaves" } },
            //{ 7386, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Extremely quick line AoE \"Death's Door\" that instantly kills" } },
            //{ 7387, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Deals heavy damage to random players" } },
            //{ 7388, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Charges\nUntelegraphed line AoE \"Swipe\"\nUntelegraphed wide circle AoE \"Swing\"" } },
            //{ 7389, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Repeatedly cleaves for high damage, lifesteal, buffs own damage, three stacks of damage up casts ENRAGE \"Black Nebula\"" } },
            //{ 7390, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Rapid double autos and untelegraphed line AoE \"Quasar\"" } },
            //{ 7391, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Double autos, cone AoE inflicts Sleep" } },
            //{ 7584, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Permanent stacking damage buff\nMassive enrage on random player\"Allagan Meteor\"\nGaze attack" } }, 
            // HoH bosses and misc.
            //{ 7392, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false}, MobNotes="Floors 1-30: Bronze chests only\nHigh damage autos and instant kill AoE\n\"Malice\" can be interrupted with silence/stun/knockback/witching/\ninterject" } },
            //{ 7393, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true}, MobNotes="Floors 31-60: Silver chests only\nHigh damage autos and instant kill AoE\n\"Malice\" can be interrupted with silence/stun/interject" } },
            //{ 7394, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Floors 61+: Gold chests only\nHigh damage autos and instant kill AoE\n\"Malice\" can only be interrupted with interject\nCANNOT STUN" } },
            //{ 7478, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="Summons lightning clouds that inflict stacking vuln up when they explode\nBoss does proximity AoE under itself that knocks players into the air\nGet knocked into a cloud to dispel it and avoid vuln\nHalf-room wide AoE" } },
            //{ 7480, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="Goes to center of arena and casts knockback to wall (cannot be knockback invulned)\nFollows immediately with a half-room wide AoE" } },
            //{ 7481, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="Summons butterflies on edges of arena\nDoes gaze mechanic that inflicts Fear\nButterflies explode untelegraphed" } },
            //{ 7483, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="Summons clouds on edge of arena\nDoes to-wall knockback that ignores knockback invuln (look for safe spot in clouds!)\nFollows immediately with targeted line AoE" } },
            //{ 7485, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="1) Untelegraphed swipe\n2) Untelegraphed line AoE on random player\n3) Gaze mechanic that inflicts Fear\n4) Summons pulsating bombs over arena and does a proximity AoE\n5) Repeats after bombs explode for the last time" } },
            //{ 7487, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="Summons staffs that do various AoEs\nStaffs then do line AoEs targeting players\nRoom wide AoE" } },
            //{ 7489, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="1) Untelegraphed frontal cleave\n2) Targets random player with \"Innerspace\" puddle (standing in puddle inflicts Minimum)\n3) Targets random player with \"Hound out of Hell\"\n4) Targeted player must stand in puddle to dodge \"Hound out of Hell\" and \"Devour\" (will instant-kill if not in puddle and give the boss a stack of damage up)\n5) Repeat" } },
            //{ 7490, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="1) Summons balls of ice\n2) Summons icicle that pierces through room, detonating any ice balls it hits\n3) \"Lunar Cry\" detonates remaining ice balls\n4) Exploding ice balls inflict Deep Freeze if they hit a player\n5) Boss jumps to random player, instantly killing if player is frozen (light damage otherwise)" } },
            //{ 7493, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false}, MobNotes="1) Heavy room wide damage \"Ancient Quaga\"\n2) Pulsing rocks appear over arena, causing moderate damage and Heavy debuff if player is hit by one\n3) \"Meteor Impact\" summons proximity AoE at boss's current location\n4) Line AoE \"Aura Cannon\"\n5) Targeted circle AoE \"Burning Rave\"\n6) Point-blank circle AoE \"Knuckle Press\"\n7) Repeat" } },
            //{ 7610, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Does not interact, wide stun and immediately dies when attacked" } },

            //PotD data
            //PotD 1-10
            //{ 4975, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Casts Haste on itself" } },
            //{ 4976, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 4977, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 4978, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 4979, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 4980, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Inflicts Poison" } },
            //{ 4981, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="High damage \"Final Sting\"" } },
            //{ 4982, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = true, IsUndead = false}, MobNotes="Inflicts vulnerability up" } },
            //{ 4983, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 4984, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 4985, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Mini buster \"Rhino Charge\"" } },
            //{ 4986, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false, CanSlow = false}, MobNotes="1) \"Whipcrack\" - light tankbuster\n2) \"Stormwind\" - conal AOE\n3) \"Bombination\" - circular AOE on boss inflicts Slow\n4) \"Lumisphere\" - targeted AOE on random player\n5) \"Aeroblast\" - room wide AOE inflicts Bleed" } },
            //PotD 11-20
            //{ 4987, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 4988, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="Inflicts poison" } },
            //{ 4989, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="\"Sticky Tongue\" does not stun if facing towards" } },
            //{ 4990, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="Eventual ENRAGE" } },
            //{ 4991, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 4992, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 4993, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Area of effect Slow" } },
            //{ 4994, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 4995, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 4996, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="Buffs own damage" } },
            //{ 4997, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = true, IsUndead = false}, MobNotes="Gaze attack inflicts Petrify, \"Devour\" instantly kills players inflicted with Toad" } },
            //{ 4998, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 4999, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false, CanSlow = false}, MobNotes="1) \"Bloody Caress\" - high damage cleave\n2) Two telegraphed AOEs and a room wide AOE\n3) Summons two hornets that must be killed before they \"Final Sting\"\n4) \"Rotten Stench\" - high damage line AOE" } },
            //PotD 21-30
            //{ 5000, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5001, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5002, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5003, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5004, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5005, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5006, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5007, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5008, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5009, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5010, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Untelegraphed AOE does moderate damage and knockback" } },
            //{ 5011, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="\"Chirp\" inflicts Sleep for 15s" } },
            //{ 5012, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false, CanSlow = false}, MobNotes="1) Spread out fire and ice AOEs and don't drop them in center because: \n2) Get inside boss's hit box for \"Fear Itself\" - will inflict high damage and Terror if not avoided" } },
            //PotD 31-40
            //{ 5013, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5014, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5015, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5016, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = true}, MobNotes="" } },
            //{ 5017, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5018, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5019, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5020, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5021, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5022, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="\"Dark Mist\" inflicts Terror" } },
            //{ 5023, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5024, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = true}, MobNotes="" } },
            //{ 5025, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = true, CanSlow = false}, MobNotes="1) Summons four lingering AoEs\n2) Summons two adds -- they must be killed before boss casts \"Scream\", adds will target player with high damage AoEs if not dead" } },
            // PotD 41-50
            //{ 5026, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5027, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5028, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5029, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5030, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="Inflicts Paralysis" } },
            //{ 5031, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5032, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Inflicts Paralysis" } },
            //{ 5033, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5034, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5035, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5036, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5037, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = true}, MobNotes="" } },
            //{ 5038, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false, CanSlow = false}, MobNotes="FOLLOW MECHANICS -- failed mechanics power up an unavoidable room AoE\nBoss will occasionally inflict Disease which slows\n1) \"In Health\" -- can be room wide AoE with safe spot on boss or targeted AoE under boss \n2) \"Cold Feet\" -- Gaze" } },
            // PotD special NPCs and misc.
            //{ 5039, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="" } },
            //{ 5040, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="" } },
            //{ 5041, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Does not interact, wide stun and immediately dies when attacked" } },
            //{ 5046, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5047, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5048, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5049, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5050, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5051, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5052, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5053, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5283, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5284, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5285, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5286, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5287, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5288, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5289, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5290, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5291, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5292, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5293, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5294, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5295, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5296, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5297, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            //{ 5298, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{}, MobNotes="Immune to Pomander of Witching" } },
            // PotD 51-60
            //{ 5299, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="Gaze inflicts Paralysis" } },
            //{ 5300, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5301, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = true}, MobNotes="" } },
            //{ 5302, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="" } },
            //{ 5303, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5304, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5305, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5306, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5307, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5308, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Gaze inflicts Blind and does high damage" } },
            //{ 5309, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false, CanSlow = false}, MobNotes="Drops large puddle AoEs that inflict Bleed if stood in\n\"Valfodr\" -- targeted unavoidable line AoE centered on player that causes strong knockback, avoid AoEs surrounding outer edge" } },
            // PotD 61-70
            //{ 5311, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5312, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5313, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5314, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5315, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5316, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="" } },
            //{ 5317, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5318, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5319, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5320, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5321, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false, CanSlow = false}, MobNotes="\"Douse\" -- lingering ground AoE that inflicts Bleed if stood in and buffs boss with Haste if left in it\nOccasionally casts targeted ground AoEs" } },
            // PotD 71-80
            //{ 5322, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5323, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5324, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5325, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5326, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5327, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5328, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5329, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5330, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5331, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5332, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5333, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="\"Charybdis\" -- lingering ground tornadoes that cause high damage if sucked into\nBoss will run to edge of arena and cast \"Trounce\" - wide conal AoE\nAt 17%% casts \"Ecliptic Meteor\" - HIGH DAMAGE room wide with long cast that deals 80%% of total health damage" } },
            // PotD 81-90
            //{ 5334, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="Casts wide \"Self Destruct\" if not killed in time" } },
            //{ 5335, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5336, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5337, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5338, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5339, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5340, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5341, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5342, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5343, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5344, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5345, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false, CanSlow = false}, MobNotes="Casts large AoEs\nSummons \"Grey Bomb\" - must be killed before it does high room wide damage\nBegins long cast \"Massive Burst\" and summons \"Giddy Bomb\" that must be knocked towards the boss to interrupt cast" } },
            // PotD 91-100
            //{ 5346, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = true}, MobNotes="" } },
            //{ 5347, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = true}, MobNotes="" } },
            //{ 5348, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, IsUndead = false}, MobNotes="" } },
            //{ 5349, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5350, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5351, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5352, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = true}, MobNotes="" } },
            //{ 5353, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{IsUndead = true}, MobNotes="" } },
            //{ 5354, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5355, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = true}, MobNotes="" } },
            //{ 5356, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false, CanSlow = false}, MobNotes="Summons adds and does large targeted AoEs -- adds are vulnerable to Pomander of Resolution's attacks" } },
            // PotD 101-110
            //{ 5360, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5361, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5362, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5363, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5364, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="Floors 101-110: \nNothing notable (ignore threat level)\nFloors 191-200: \nDouble autos" } },
            //{ 5365, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Inflicts Poison" } },
            //{ 5366, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="High damage \"Final Sting\"" } },
            //{ 5367, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Inflicts vulnerability up" } },
            //{ 5368, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5369, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5370, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Mini buster \"Rhino Charge\"" } },
            //{ 5371, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="1) \"Whipcrack\" - light tankbuster\n2) \"Stormwind\" - conal AOE\n3) \"Bombination\" - circular AOE on boss inflicts Slow\n4) \"Lumisphere\" - targeted AOE on random player\n5) \"Aeroblast\" - room wide AOE inflicts Bleed" } },
            // PotD 111-120
            //{ 5372, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{IsUndead = false}, MobNotes="" } },
            //{ 5373, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="" } },
            //{ 5374, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="\"Sticky Tongue\" draw-in and stun attack if not facing, followed by \"Labored Leap\" AoE centered on enemy" } },
            //{ 5375, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Eventual ENRAGE" } },
            //{ 5376, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5377, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanSleep = true, IsUndead = false}, MobNotes="Casts invuln buff on itself" } },
            //{ 5378, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Area of effect Slow" } },
            //{ 5379, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5380, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="Will inflict Sleep before casting \"Bad Breath\"" } },
            //{ 5381, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Buffs own damage" } },
            //{ 5382, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="Gaze attack inflicts Petrify, \"Regorge\" inflicts Poison\nWill one-shot kill anyone inflicted with Toad" } },
            //{ 5383, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, IsUndead = false}, MobNotes="" } },
            //{ 5384, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="1) \"Bloody Caress\" - high damage cleave\n2) Two telegraphed AOEs and a room wide AOE\n3) Summons two hornets that must be killed before they \"Final Sting\"\n4) \"Rotten Stench\" - high damage line AOE" } },
            // PotD 121-130
            //{ 5385, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5386, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5387, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5388, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="" } },
            //{ 5389, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="Double autos" } },
            //{ 5390, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5391, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5392, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5393, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5394, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5395, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="\"11-Tonze Swing\" - point-blank untelegraphed AoE that does high damage and knockback" } },
            //{ 5396, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="\"Chirp\" inflicts Sleep for 15s" } },
            //{ 5397, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="1) Spread out fire and ice AOEs and don't drop them in center because: \n2) Get inside boss's hit box for fast cast \"Fear Itself\" - will inflict high damage and Terror if not avoided" } },
            // PotD 131-140
            //{ 5398, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5399, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5400, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5401, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = true}, MobNotes="" } },
            //{ 5402, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="Untelegraphed conal AoE \"Level 5 Petrify\" inflicts Petrify" } },
            //{ 5403, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5404, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5405, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5406, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = false}, MobNotes="Casts targeted AoE that inflicts Bleed" } },
            //{ 5407, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5408, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5409, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanSleep = true, IsUndead = true}, MobNotes="Floors 131-140: \nNothing notable (ignore threat level)\nFloors 191-200: \nDouble autos" } },
            //{ 5410, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = true}, MobNotes="1) Summons four lingering AoEs\n2) Summons two adds -- they must be killed before boss casts \"Scream\", adds will target player with high damage AoEs if not dead" } },
            // PotD 141-150
            //{ 5411, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{IsUndead = false}, MobNotes="" } },
            //{ 5412, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{IsUndead = false}, MobNotes="" } },
            //{ 5413, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="\"Charybdis\" - semi-enrage that drops party health to 1%%" } },
            //{ 5414, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="Very high damage, inflicts Poison" } },
            //{ 5415, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="Floors 141-150: \nNothing notable (ignore threat level)\nFloors 191-200: \nCasts large doughnut AoE \"Death Spiral\" that deals heavy damage\nHas soft enrage of a strong damage buff" } },
            //{ 5416, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="" } },
            //{ 5417, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="" } },
            //{ 5418, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="" } },
            //{ 5419, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = false}, MobNotes="" } },
            //{ 5420, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, IsUndead = false}, MobNotes="Casts Gaze \"Evil Eye\"" } },
            //{ 5421, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{IsUndead = false}, MobNotes="Buffs own damage, untelegraphed high damage \"Ripper Claw\" - can be avoided by walking behind" } },
            //{ 5422, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, IsUndead = true}, MobNotes="High health and very large AoE \"Scream\"" } },
            //{ 5423, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = false, IsUndead = true}, MobNotes="Very high damage for the floors it appears on" } },
            //{ 5424, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="Summons adds\n\"Fanatic Zombie\" will grab player and root in place until killed\n\"Fanatic Succubus\" will heal boss if it reaches it" } },
            // PotD 151-160
            //{ 5429, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Double autos\nGaze inflicts Paralysis" } },
            //{ 5430, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="Inflicts Vuln Up debuff" } },
            //{ 5431, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="\"Ice Spikes\" reflects damage\n\"Void Blizzard\" inflicts Slow" } },
            //{ 5432, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5433, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Double autos that lifesteal" } },
            //{ 5434, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5435, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5436, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="Double autos" } },
            //{ 5437, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Double autos\nGaze inflicts heavy damage and Blind" } },
            //{ 5438, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="Drops lingering AoEs that cause heavy Bleed if stood in\n\"Valfodr\" -- targeted unavoidable line AoE centered on player that causes strong knockback, avoid AoEs surrounding outer edge" } },
            // PotD 161-170
            //{ 5439, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5440, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5441, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5442, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Double autos" } },
            //{ 5443, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Double autos" } },
            //{ 5444, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5445, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5446, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5447, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5448, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5449, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="\"Douse\" -- lingering ground AoE that inflicts Bleed if stood in and buffs boss with Haste and Damage Up if left in it\nOccasionally inflicts Heavy and casts targeted ground AoEs " } },
            // PotD 171-180
            //{ 5450, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Double autos" } },
            //{ 5451, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = true, IsUndead = false}, MobNotes="Has semi-enrage around 30s in combat" } },
            //{ 5452, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="\"Revelation\" inflicts Confusion\n\"Tropical Wind\" gives enemy a large Haste and damage buff" } },
            //{ 5453, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="\"Glower\" - untelegraphed line AoE\n\"100-Tonze Swing\" - untelegraphed point-blank AoE" } },
            //{ 5454, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Buffs own damage and inflicts Physical Vuln Up with AoE damage out of combat" } },
            //{ 5455, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5456, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5457, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5458, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Double autos" } },
            //{ 5459, new MobData { Threat=MobData.ThreatLevel.Vicious, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="Cleave inflicts Bleed\n\"Flying Frenzy\" targets a player and does heavy damage, Vuln Down, and stuns" } },
            //{ 5460, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="Cleave does heavy damage and inflicts potent Bleed" } },
            //{ 5461, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="\"Charybdis\" -- lingering ground tornadoes cast twice in a row that cause high damage if sucked into\nBoss will run to top or bottom of arena and cast \"Trounce\" - wide conal AoE\nAt 15%% casts FAST CAST \"Ecliptic Meteor\" - HIGH DAMAGE room wide with long cast that deals 80%% of total health damage every 9 seconds" } },
            // PotD 181-190
            //{ 5462, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="" } },
            //{ 5463, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Double autos" } },
            //{ 5464, new MobData { Threat=MobData.ThreatLevel.Vicious, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Instant AoE that inflicts heavy Bleed" } },
            //{ 5465, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Instant AoE on pull, double autos\nAt 30 seconds will cast semi-enrage" } },
            //{ 5466, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Sucks in player and does heavy damage\n\"Tail Screw\" does damage and inflicts Slow" } },
            //{ 5467, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="Instant AoE burst does heavy damage and inflicts Slow\nInstant cone inflicts Poison" } },
            //{ 5468, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5469, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = true, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5470, new MobData { Threat=MobData.ThreatLevel.Vicious, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = true, CanHeavy = true, CanSleep = true, IsUndead = false}, MobNotes="If familiar with chimera mechanics can be engaged\n\"The Dragon's Voice\" - be inside hit box\n\"The Ram's Voice\" - be outside of melee range" } },
            //{ 5471, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Boss, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, CanSlow = false, IsUndead = false}, MobNotes="Kill blue bomb when it appears\nPush red bomb into boss during \"Massive Burst\" cast, will wipe party if not stunned\nBoss has cleave that does heavy damage" } },
            // PotD 191-200
            //{ 5472, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Sound, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5473, new MobData { Threat=MobData.ThreatLevel.Dangerous, Aggro=MobData.AggroType.Sight, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="Casts untelegraphed cone \"Level 5 Death\"" } },
            //{ 5474, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false, CanBind = false, CanHeavy = true, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5475, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = true}, MobNotes="Double auto" } },
            // PotD misc.
            //{ 5479, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 5480, new MobData { Threat=MobData.ThreatLevel.Easy, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = true, CanBind = false, CanHeavy = false, CanSleep = false, IsUndead = false}, MobNotes="" } },
            //{ 2566, new MobData { Threat=MobData.ThreatLevel.Caution, Aggro=MobData.AggroType.Proximity, Vuln = new MobData.Vulnerabilities{CanStun = false}, MobNotes="High damage autos and instant kill AoE\n\"Infatuation\" can only be interrupted with interject" } },
        };
    }

    public static class Extensions
    {
        //public enum Vulnerabilities
        //{
        //    None = 0x00,
        //    Stun = 0x01,
        //    Heavy = 0x02,
        //    Slow = 0x04,
        //    Sleep = 0x08,
        //    Bind = 0x10,
        //    Undead = 0x20
        //}
        public static Vulnerabilities Get(this ITuple vulnTuple)
        {
            unsafe
            {
                bool stun, heavy, slow, sleep, bind;
                switch (vulnTuple)
                {
                    case Tuple<bool, bool> tuple when tuple.GetType() == typeof(Tuple<bool, bool>):
                        (stun, heavy) = tuple;
                        return (Vulnerabilities)(*(byte*)&stun + (*(byte*)&heavy << 1));
                    case Tuple<bool, bool, bool> tuple when tuple.GetType() == typeof(Tuple<bool, bool, bool>):
                        (stun, heavy, slow) = tuple;
                        return (Vulnerabilities)(*(byte*)&stun + (*(byte*)&heavy << 1) + (*(byte*)&slow << 2));
                    case Tuple<bool, bool, bool, bool> tuple when tuple.GetType() == typeof(Tuple<bool, bool, bool, bool>):
                        (stun, heavy, slow, sleep) = tuple;
                        return (Vulnerabilities)(*(byte*)&stun + (*(byte*)&heavy << 1) + (*(byte*)&slow << 2) + (*(byte*)&sleep << 3));
                    case Tuple<bool, bool, bool, bool, bool> tuple when tuple.GetType() == typeof(Tuple<bool, bool, bool, bool, bool>):
                        (stun, heavy, slow, sleep, bind) = tuple;
                        return (Vulnerabilities)(*(byte*)&stun + (*(byte*)&heavy << 1) + (*(byte*)&slow << 2) + (*(byte*)&sleep << 3) + (*(byte*)&bind << 4));
                    case Tuple<bool, bool, bool, bool, bool, bool> tuple when tuple.GetType() == typeof(Tuple<bool, bool, bool, bool, bool, bool>):
                        bool undead;
                        (stun, heavy, slow, sleep, bind, undead) = tuple;
                        return (Vulnerabilities)(*(byte*)&stun + (*(byte*)&heavy << 1) + (*(byte*)&slow << 2) + (*(byte*)&sleep << 3) + (*(byte*)&bind << 4) + (*(byte*)&undead << 5));
                }
            }

            return Vulnerabilities.None;
        }
    }
}
