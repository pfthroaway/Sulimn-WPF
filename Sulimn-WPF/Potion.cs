﻿using System;

namespace Sulimn_WPF
{
    internal class Potion : Item, IEquatable<Potion>
    {
        private PotionTypes _potionType;
        private int _amount;

        #region Properties

        public sealed override string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public sealed override string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public PotionTypes PotionType
        {
            get { return _potionType; }
            set { _potionType = value; }
        }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public sealed override int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public sealed override int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public sealed override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; }
        }

        public sealed override bool IsSold
        {
            get { return _isSold; }
            set { _isSold = value; OnPropertyChanged("IsSold"); }
        }

        #endregion Properties

        #region Override Operators

        public static bool Equals(Potion left, Potion right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(left.Type, right.Type, StringComparison.OrdinalIgnoreCase) && left.PotionType == right.PotionType && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.Amount == right.Amount) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
        }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as Potion);
        }

        public bool Equals(Potion otherPotion)
        {
            return Equals(this, otherPotion);
        }

        public static bool operator ==(Potion left, Potion right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Potion left, Potion right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ 17;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion Override Operators

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Potion.
        /// </summary>
        internal Potion()
        {
        }

        /// <summary>
        /// Initializes an instance of Potion by assigning Properties.
        /// </summary>
        /// <param name="potionName">Name of Potion</param>
        /// <param name="potionType">Type of Potion</param>
        /// <param name="potionDescription">Description of Potion</param>
        /// <param name="potionAmount">Amount of Potion</param>
        /// <param name="potionValue">Value of Potion</param>
        /// <param name="potionCanSell">Can Potion be sold?</param>
        /// <param name="potionIsSold">Is Potion sold?</param>
        internal Potion(string potionName, PotionTypes potionType, string potionDescription, int potionAmount, int potionValue, bool potionCanSell, bool potionIsSold)
        {
            Name = potionName;
            Type = "Potion";
            PotionType = potionType;
            Description = potionDescription;
            Weight = 0;
            Value = potionValue;
            Amount = potionAmount;
            CanSell = potionCanSell;
            IsSold = potionIsSold;
        }

        #endregion Constructors
    }
}