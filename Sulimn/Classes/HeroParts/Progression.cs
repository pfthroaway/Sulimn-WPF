﻿namespace Sulimn.Classes.HeroParts
{
    /// <summary>This class represents a Hero's progression through the game.</summary>
    internal class Progression
    {
        #region Modifying Properties

        /// <summary>Has the Hero completed Fields?</summary>
        internal bool Fields { get; set; }

        /// <summary>Has the Hero completed Forest?</summary>
        internal bool Forest { get; set; }

        /// <summary>Has the Hero completed Cathedral?</summary>
        internal bool Cathedral { get; set; }

        /// <summary>Has the Hero completed Mines?</summary>
        internal bool Mines { get; set; }

        /// <summary>Has the Hero completed Catacombs?</summary>
        internal bool Catacombs { get; set; }

        /// <summary>Has the Hero completed Courtyard?</summary>
        internal bool Courtyard { get; set; }

        /// <summary>Has the Hero completed Battlements?</summary>
        internal bool Battlements { get; set; }

        /// <summary>Has the Hero completed Armoury?</summary>
        internal bool Armoury { get; set; }

        /// <summary>Has the Hero completed Spire?</summary>
        internal bool Spire { get; set; }

        /// <summary>Has the Hero completed ThroneRoom?</summary>
        internal bool ThroneRoom { get; set; }

        #endregion Modifying Properties

        #region Override Operators

        private static bool Equals(Progression left, Progression right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return left.Fields == right.Fields && left.Forest == right.Forest && left.Cathedral == right.Cathedral && left.Mines == right.Mines && left.Catacombs == right.Catacombs && left.Courtyard == right.Courtyard && left.Battlements == right.Battlements && left.Armoury == right.Armoury && left.Spire == right.Spire && left.ThroneRoom == right.ThroneRoom;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Progression);

        public bool Equals(Progression otherProgression) => Equals(this, otherProgression);

        public static bool operator ==(Progression left, Progression right) => Equals(left, right);

        public static bool operator !=(Progression left, Progression right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        #endregion Override Operators

        #region Constructors

        /// <summary>Iniitalizes a default instance of Progression.</summary>
        public Progression()
        {
        }

        /// <summary>Initializes an instance of Progression by assigning properties.</summary>
        /// <param name="fields">Has the Hero completed Fields?</param>
        /// <param name="forest">Has the Hero completed Forest?</param>
        /// <param name="cathedral">Has the Hero completed Cathedral?</param>
        /// <param name="mines">Has the Hero completed Mines?</param>
        /// <param name="catacombs">Has the Hero completed Catacombs?</param>
        /// <param name="courtyard">Has the Hero completed Courtyard?</param>
        /// <param name="battlements">Has the Hero completed Battlements?</param>
        /// <param name="armoury">Has the Hero completed Armoury?</param>
        /// <param name="spire">Has the Hero completed Spire?</param>
        /// <param name="throneRoom">Has the Hero completed ThroneRoom?</param>
        public Progression(bool fields, bool forest, bool cathedral, bool mines, bool catacombs, bool courtyard, bool battlements, bool armoury, bool spire, bool throneRoom)
        {
            Fields = fields;
            Forest = forest;
            Cathedral = cathedral;
            Mines = mines;
            Catacombs = catacombs;
            Courtyard = courtyard;
            Battlements = battlements;
            Armoury = armoury;
            Spire = spire;
            ThroneRoom = throneRoom;
        }

        /// <summary>Replaces this instance of Progression with another instance.</summary>
        /// <param name="other">Instance of Progression to replace this instance</param>
        public Progression(Progression other) : this(other.Fields, other.Forest, other.Cathedral, other.Mines, other.Catacombs, other.Courtyard, other.Battlements, other.Armoury, other.Spire, other.ThroneRoom)
        {
        }

        #endregion Constructors
    }
}