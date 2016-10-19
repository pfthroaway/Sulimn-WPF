﻿using System;
using System.ComponentModel;

namespace Sulimn_WPF
{
    internal class Armor : Item, IEquatable<Armor>, INotifyPropertyChanged
    {
        private string _armorType;
        private int _defense;

        #region Data-Binding

        public sealed override event PropertyChangedEventHandler PropertyChanged;

        protected sealed override void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public sealed override string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public sealed override string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        public string ArmorType
        {
            get { return _armorType; }
            set { _armorType = value; OnPropertyChanged("ArmorType"); }
        }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public int Defense
        {
            get { return _defense; }
            set { _defense = value; OnPropertyChanged("DefenseToString"); OnPropertyChanged("DefenseToStringWithText"); }
        }

        public string DefenseToString
        {
            get { return Defense.ToString("N0"); }
        }

        public string DefenseToStringWithText
        {
            get { return "Defense: " + DefenseToString; }
        }

        public sealed override int Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged("Weight"); }
        }

        public sealed override int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        public sealed override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; OnPropertyChanged("CanSell"); }
        }

        public sealed override bool IsSold
        {
            get { return _isSold; }
            set { _isSold = value; OnPropertyChanged("IsSold"); }
        }

        #endregion Properties

        #region Override Operators

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Armor armr = obj as Armor;
            if ((System.Object)armr == null)
                return false;

            return (this.Name == armr.Name) && (this.Type == armr.Type) && (this.ArmorType == armr.ArmorType) && (this.Description == armr.Description) && (this.Defense == armr.Defense) && (this.Weight == armr.Weight) && (this.Value == armr.Value) && (this.CanSell == armr.CanSell) && (this.IsSold == armr.IsSold);
        }

        public bool Equals(Armor otherArmor)
        {
            if ((object)otherArmor == null)
                return false;

            return (this.Name == otherArmor.Name) && (this.Type == otherArmor.Type) && (this.ArmorType == otherArmor.ArmorType) && (this.Description == otherArmor.Description) && (this.Defense == otherArmor.Defense) && (this.Weight == otherArmor.Weight) && (this.Value == otherArmor.Value) && (this.CanSell == otherArmor.CanSell) && (this.IsSold == otherArmor.IsSold);
        }

        public static bool operator ==(Armor left, Armor right)
        {
            if (System.Object.ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return (left.Name == right.Name) && (left.Type == right.Type) && (left.ArmorType == right.ArmorType) && (left.Description == right.Description) && (left.Defense == right.Defense) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
        }

        public static bool operator !=(Armor left, Armor right)
        {
            return !(left == right);
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
        /// Initializes a default instance of Armor.
        /// </summary>
        internal Armor()
        {
        }

        /// <summary>
        /// Initializes an instance of Armor by setting Properties.
        /// </summary>
        /// <param name="armorName">Name of Armor</param>
        /// <param name="armorType">Type of Armor</param>
        /// <param name="armorDescription">Description of armor</param>
        /// <param name="armorDefense">Defense of Armor</param>
        /// <param name="armorWeight">Weight of Armor</param>
        /// <param name="armorValue">Value of Armor</param>
        /// <param name="armorCanSell">Can Sell Armor?</param>
        /// <param name="armorIsSold">Is Armor Sold?</param>
        internal Armor(string armorName, string armorType, string armorDescription, int armorDefense, int armorWeight, int armorValue, bool armorCanSell, bool armorIsSold)
        {
            Name = armorName;
            Type = "Armor";
            ArmorType = armorType;
            Description = armorDescription;
            Defense = armorDefense;
            Weight = armorWeight;
            Value = armorValue;
            CanSell = armorCanSell;
            IsSold = armorIsSold;
        }

        /// <summary>
        /// Replaces this instance of Armor with another instance.
        /// </summary>
        /// <param name="otherArmor">Instance of Armor to replace this one</param>
        internal Armor(Armor otherArmor)
        {
            Name = otherArmor.Name;
            Type = otherArmor.Type;
            ArmorType = otherArmor.Type;
            Description = otherArmor.Description;
            Defense = otherArmor.Defense;
            Weight = otherArmor.Weight;
            Value = otherArmor.Value;
            CanSell = otherArmor.CanSell;
            IsSold = otherArmor.IsSold;
        }

        #endregion Constructors
    }
}