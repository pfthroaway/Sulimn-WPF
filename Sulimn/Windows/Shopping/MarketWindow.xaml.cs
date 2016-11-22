﻿using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for MarketWindow.xaml
    /// </summary>
    public partial class MarketWindow : Window
    {
        internal CityWindow RefToCityWindow { get; set; }
        private string nl = Environment.NewLine;

        /// <summary>Adds text to the TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtMarket.Text += nl + nl + newText;
            txtMarket.Focus();
            txtMarket.CaretIndex = txtMarket.Text.Length;
            txtMarket.ScrollToEnd();
        }

        #region Button-Click Methods

        private void btnWeaponShop_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow();
            shopWindow.RefToMarketWindow = this;
            shopWindow.SetShopType(ItemTypes.Weapon);
            shopWindow.LoadAll();
            shopWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnArmorShop_Click(object sender, RoutedEventArgs e)
        {
            TheArmouryWindow theArmouryWindow = new TheArmouryWindow();
            theArmouryWindow.RefToMarketWindow = this;
            theArmouryWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnGeneralStore_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow();
            shopWindow.RefToMarketWindow = this;
            shopWindow.SetShopType(ItemTypes.Potion);
            shopWindow.LoadAll();
            shopWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnMagicShop_Click(object sender, RoutedEventArgs e)
        {
            MagickShoppeWindow magickShoppeWindow = new MagickShoppeWindow();
            magickShoppeWindow.RefToMarketWindow = this;
            magickShoppeWindow.LoadAll();
            magickShoppeWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public MarketWindow()
        {
            InitializeComponent();
            txtMarket.Text = "You enter a bustling market.There are many shops here, the most interesting being:" + nl + nl + "Weapons 'R' Us - A weapons shop." + nl + nl + "The Armoury - An armor shop." + nl + nl + "The General Store - A shop supplying general goods like potions." + nl + nl + "Ye Old Magick Shoppe -A shop selling magical spells and equipment.";
        }

        private void windowMarket_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}