﻿using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents a piece of Armor worn on the head.
    /// </summary>
    internal class HeadArmor : Item, IEquatable<HeadArmor>, INotifyPropertyChanged
    {
        protected int _defense;

        #region Data-Binding

        public sealed override event PropertyChangedEventHandler PropertyChanged;

        protected sealed override void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Name of the armor</summary>
        public sealed override string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>Type of the armor</summary>
        public sealed override ItemTypes Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        /// <summary>Description of the armor</summary>
        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        /// <summary>How much damage the armor can defend against</summary>
        public int Defense
        {
            get { return _defense; }
            set { _defense = value; OnPropertyChanged("DefenseToString"); OnPropertyChanged("DefenseToStringWithText"); }
        }

        /// <summary>How much the armor weighs</summary>
        public sealed override int Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged("Weight"); }
        }

        /// <summary>How much the armor is worth</summary>
        public sealed override int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        /// <summary>Can the armor be sold to a shop?</summary>
        public sealed override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; OnPropertyChanged("CanSell"); }
        }

        /// <summary>Can the armor be sold in a shop?</summary>
        public sealed override bool IsSold
        {
            get { return _isSold; }
            set { _isSold = value; OnPropertyChanged("IsSold"); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>The value of the armor with thousands separators</summary>
        public sealed override string ValueToString
        {
            get { return Value.ToString("N0"); }
        }

        /// <summary>The value of the armor with thousands separators and preceding text</summary>
        public sealed override string ValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return "Value: " + ValueToString;
                return "";
            }
        }

        /// <summary>The value of the armor</summary>
        public sealed override int SellValue
        {
            get { return Value / 2; }
        }

        /// <summary>The value of the armor with thousands separators</summary>
        public sealed override string SellValueToString
        {
            get { return SellValue.ToString("N0"); }
        }

        /// <summary>The value of the armor with thousands separators with preceding text</summary>
        public sealed override string SellValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return "Sell Value: " + SellValueToString;
                return "";
            }
        }

        /// <summary>Returns text relating to the sellability of the armor</summary>
        public sealed override string CanSellToString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (CanSell)
                        return "Sellable";
                    return "Not Sellable";
                }
                return "";
            }
        }

        /// <summary>Returns the defense with a comma separating thousands.</summary>
        public string DefenseToString
        {
            get { return Defense.ToString("N0"); }
        }

        /// <summary>Returns the defense with a comma separating thousands and preceding text.</summary>
        public string DefenseToStringWithText
        {
            get
            {
                if (Defense > 0)
                    return "Defense: " + DefenseToString;
                return "";
            }
        }

        #endregion Helper Properties

        #region Override Operators

        public static bool Equals(HeadArmor left, HeadArmor right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.Defense == right.Defense) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as HeadArmor);
        }

        public bool Equals(HeadArmor otherArmor)
        {
            return Equals(this, otherArmor);
        }

        public static bool operator ==(HeadArmor left, HeadArmor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HeadArmor left, HeadArmor right)
        {
            return !Equals(left, right);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode() ^ 17;
        }

        public sealed override string ToString()
        {
            return Name;
        }

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of HeadArmor.</summary>
        internal HeadArmor()
        {
        }

        /// <summary>Initializes an instance of HeadArmor by assigning Properties.</summary>
        /// <param name="name">Name of HeadArmor</param>
        /// <param name="armorType">Type of Item</param>
        /// <param name="description">Description of HeadArmor</param>
        /// <param name="defense">Defense of HeadArmor</param>
        /// <param name="weight">Weight of HeadArmor</param>
        /// <param name="value">Value of HeadArmor</param>
        /// <param name="canSell">Can Sell HeadArmor?</param>
        /// <param name="isSold">Is HeadArmor Sold?</param>
        internal HeadArmor(string name, ItemTypes itemType, string description, int defense, int weight, int value, bool canSell, bool isSold)
        {
            Name = name;
            Type = itemType;
            Description = description;
            Defense = defense;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of HeadArmor with another instance.</summary>
        /// <param name="otherArmor">Instance of HeadArmor to replace this one</param>
        internal HeadArmor(HeadArmor otherArmor)
        {
            Name = otherArmor.Name;
            Type = otherArmor.Type;
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