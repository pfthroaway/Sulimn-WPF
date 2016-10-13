﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window, INotifyPropertyChanged
    {
        internal CharacterWindow RefToCharacterWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        private void BindLabels()
        {
            lblEquippedWeapon.DataContext = GameState.CurrentHero.Weapon;
            lblEquippedWeaponDamage.DataContext = GameState.CurrentHero.Weapon;
            lblEquippedHead.DataContext = GameState.CurrentHero.Head;
            lblEquippedHeadDefense.DataContext = GameState.CurrentHero.Head;
            lblEquippedBody.DataContext = GameState.CurrentHero.Body;
            lblEquippedBodyDefense.DataContext = GameState.CurrentHero.Body;
            lblEquippedLegs.DataContext = GameState.CurrentHero.Legs;
            lblEquippedLegsDefense.DataContext = GameState.CurrentHero.Legs;
            lblEquippedFeet.DataContext = GameState.CurrentHero.Feet;
            lblEquippedFeetDefense.DataContext = GameState.CurrentHero.Feet;
            DataContext = GameState.CurrentHero;
            lstInventory.ItemsSource = GameState.CurrentHero.Inventory;
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>
        /// Clears all text from the Selected Item labels.
        /// </summary>
        private void ClearSelectedInfo()
        {
            lblSelectedName.Text = "";
            lblSelectedType.Text = "";
            lblDescription.Text = "";
            lblValue.Text = "";
            lblAmount.Text = "";
            lblSellable.Text = "";
        }

        /// <summary>
        /// Displays all relevant information.
        /// </summary>
        internal void DisplayAllInfo()
        {
            lstInventory.Items.Refresh();
            CheckUnequipButtons();
            DisplayInventoryList();
        }

        /// <summary>
        /// Displays information about the currently equipped Weapon and Armors.
        /// </summary>
        private void CheckUnequipButtons()
        {
            if (GameState.CurrentHero.Weapon.Name == "Fists")
                btnUnequipWeapon.IsEnabled = false;
            else
                btnUnequipWeapon.IsEnabled = true;

            if (GameState.CurrentHero.Head.Name == "Cloth Helmet")
                btnUnequipHead.IsEnabled = false;
            else
                btnUnequipHead.IsEnabled = true;

            if (GameState.CurrentHero.Body.Name == "Cloth Shirt")
                btnUnequipBody.IsEnabled = false;
            else
                btnUnequipBody.IsEnabled = true;

            if (GameState.CurrentHero.Legs.Name == "Cloth Pants")
                btnUnequipLegs.IsEnabled = false;
            else
                btnUnequipLegs.IsEnabled = true;

            if (GameState.CurrentHero.Feet.Name == "Cloth Shoes")
                btnUnequipFeet.IsEnabled = false;
            else
                btnUnequipFeet.IsEnabled = true;
        }

        /// <summary>
        /// Displays the current Hero's information to the lstInventory ListBox.
        /// </summary>
        private void DisplayInventoryList()
        {
            ClearSelectedInfo();
            //lstInventory.Items.Clear();
            btnEquip.IsEnabled = false;
            btnConsume.IsEnabled = false;
            btnDrop.IsEnabled = false;

            //if (GameState.currentHero.Inventory.Items.Count > 0)
            //{
            //  foreach (Item itm in GameState.currentHero.Inventory.Items)
            //    lstInventory.Items.Add(itm.Name);
            //}
        }

        /// <summary>
        /// Display information about the currently selected Item in the lstInventory ListBox.
        /// </summary>
        private void DisplaySelectedInfo()
        {
            if (lstInventory.SelectedIndex >= 0)
            {
                Item selectedItem = GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex];

                switch (selectedItem.Type)
                {
                    case "Weapon":
                        btnEquip.IsEnabled = true;
                        btnConsume.IsEnabled = false;
                        btnDrop.IsEnabled = true;

                        Weapon selectedWeapon = (Weapon)GameState.AllItems.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedWeapon.Name;
                        lblSelectedType.Text = "Weapon: " + selectedWeapon.WeaponType;
                        lblDescription.Text = selectedWeapon.Description;
                        lblValue.Text = "Value: " + selectedWeapon.Value.ToString("N0");
                        lblAmount.Text = "Damage: " + selectedWeapon.Damage.ToString("N0");
                        if (selectedWeapon.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case "Armor":
                        btnEquip.IsEnabled = true;
                        btnConsume.IsEnabled = false;
                        btnDrop.IsEnabled = true;

                        Armor selectedArmor = (Armor)GameState.AllItems.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedArmor.Name;
                        lblSelectedType.Text = "Armor: " + selectedArmor.ArmorType;
                        lblDescription.Text = selectedArmor.Description;
                        lblValue.Text = "Value: " + selectedArmor.Value.ToString("N0");
                        lblAmount.Text = "Defense: " + selectedArmor.Defense.ToString("N0");
                        if (selectedArmor.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case "Potion":
                        btnEquip.IsEnabled = false;
                        btnConsume.IsEnabled = true;
                        btnDrop.IsEnabled = true;

                        Potion selectedPotion = (Potion)GameState.AllItems.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedPotion.Name;
                        lblSelectedType.Text = "Potion: " + selectedPotion.PotionType;
                        lblDescription.Text = selectedPotion.Description;
                        lblValue.Text = "Value: " + selectedPotion.Value.ToString("N0");
                        lblAmount.Text = selectedPotion.PotionType + ": " + selectedPotion.Amount.ToString("N0");
                        //FUTURE SET SWITCH FOR POTION TYPES
                        if (selectedPotion.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case "Food":
                        btnEquip.IsEnabled = false;
                        btnConsume.IsEnabled = true;
                        btnDrop.IsEnabled = true;

                        Food selectedFood = (Food)GameState.AllItems.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedFood.Name;
                        lblSelectedType.Text = "Food: " + selectedFood.FoodType;
                        lblDescription.Text = selectedFood.Description;
                        lblValue.Text = "Value: " + selectedFood.Value.ToString("N0");
                        lblAmount.Text = selectedFood.FoodType + ": " + selectedFood.Amount.ToString("N0");
                        //FUTURE SET SWITCH FOR POTION TYPES
                        if (selectedFood.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;
                    //FUTURE MORE ITEM TYPES
                    default:
                        MessageBox.Show("Somehow you managed to screw up and find an item type named '" + selectedItem.Type + "'.", "Sulimn", MessageBoxButton.OK);
                        break;
                }
            }
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItemAt(lstInventory.SelectedIndex);
            }

            DisplayInventoryList();
        }

        private void btnEquip_Click(object sender, RoutedEventArgs e)
        {
            switch (GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Type)
            {
                case "Weapon":
                    Weapon selectedWeapon = (Weapon)(GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex]);
                    GameState.CurrentHero.Inventory.RemoveItem(selectedWeapon);
                    if (GameState.CurrentHero.Weapon.Name != "Fists")
                    {
                        GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Weapon);
                        lstInventory.ItemsSource = GameState.CurrentHero.Inventory;
                    }
                    GameState.CurrentHero.Weapon = selectedWeapon;
                    lblEquippedWeapon.DataContext = GameState.CurrentHero.Weapon;
                    lblEquippedWeaponDamage.DataContext = GameState.CurrentHero.Weapon;
                    break;

                case "Armor":
                    Armor selectedArmor = (Armor)(GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex]);
                    GameState.CurrentHero.Inventory.RemoveItem(selectedArmor);

                    switch (selectedArmor.ArmorType)
                    {
                        case "Head":
                            if (GameState.CurrentHero.Head.Name != "Cloth Helmet")
                                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Head);

                            GameState.CurrentHero.Head = selectedArmor;
                            lblEquippedHead.DataContext = GameState.CurrentHero.Head;
                            lblEquippedHeadDefense.DataContext = GameState.CurrentHero.Head;
                            break;

                        case "Body":
                            if (GameState.CurrentHero.Body.Name != "Cloth Shirt")
                                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Body);

                            GameState.CurrentHero.Body = selectedArmor;
                            lblEquippedBody.DataContext = GameState.CurrentHero.Body;
                            lblEquippedBodyDefense.DataContext = GameState.CurrentHero.Body;
                            break;

                        case "Legs":
                            if (GameState.CurrentHero.Legs.Name != "Cloth Pants")
                                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Legs);

                            GameState.CurrentHero.Legs = selectedArmor;
                            lblEquippedLegs.DataContext = GameState.CurrentHero.Legs;
                            lblEquippedLegsDefense.DataContext = GameState.CurrentHero.Legs;
                            break;

                        case "Feet":
                            if (GameState.CurrentHero.Feet.Name != "Cloth Shoes")
                                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Feet);

                            GameState.CurrentHero.Feet = selectedArmor;
                            lblEquippedFeet.DataContext = GameState.CurrentHero.Feet;
                            lblEquippedFeetDefense.DataContext = GameState.CurrentHero.Feet;
                            break;

                        default:
                            MessageBox.Show("Somehow you managed to screw up and find an armor type named '" + selectedArmor.ArmorType + "'.", "Sulimn", MessageBoxButton.OK);
                            break;
                    }
                    break;

                default:
                    MessageBox.Show("Somehow you managed to screw up and find an item type named '" + GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Type + "'.", "Sulimn", MessageBoxButton.OK);
                    break;
            }
            //FUTURE MORE ITEM TYPES

            DisplayAllInfo();
            btnDrop.IsEnabled = false;
            btnConsume.IsEnabled = false;
            btnEquip.IsEnabled = false;
        }

        private void btnConsume_Click(object sender, RoutedEventArgs e)
        {
            switch (GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Type)
            {
                case "Potion":
                    Potion selectedPotion = (Potion)GameState.AllItems.Find(x => x.Name == GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Name);
                    GameState.CurrentHero.Inventory.RemoveItem(selectedPotion);

                    switch (selectedPotion.PotionType)
                    {
                        case "Healing":
                            GameState.CurrentHero.Heal(selectedPotion.Amount);
                            break;

                        case "Magic":
                            GameState.CurrentHero.RestoreMagic(selectedPotion.Amount);
                            break;

                        case "Curing":
                            break;

                        default:
                            //FUTURE POTION TYPES
                            break;
                    }
                    break;

                case "Food":
                    Food selectedFood = (Food)GameState.AllItems.Find(x => x.Name == GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Name);
                    GameState.CurrentHero.Inventory.RemoveItem(selectedFood);

                    switch (selectedFood.FoodType)
                    {
                        case "Food":
                            GameState.CurrentHero.Heal(selectedFood.Amount);
                            break;

                        case "Drink":
                            GameState.CurrentHero.RestoreMagic(selectedFood.Amount);
                            break;
                    }
                    break;
            }

            DisplayAllInfo();
        }

        #endregion Button-Click Methods

        #region Unequip Button Clicks

        private void btnUnequipBody_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Body.Name != "Cloth Shirt")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Body);
            GameState.CurrentHero.Body = (Armor)GameState.AllItems.Find(x => x.Name == "Cloth Shirt");
            DisplayAllInfo();
        }

        private void btnUnequipHead_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Head.Name != "Cloth Helmet")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Head);
            GameState.CurrentHero.Head = (Armor)GameState.AllItems.Find(x => x.Name == "Cloth Helmet");
            DisplayAllInfo();
        }

        private void btnUnequipFeet_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Feet.Name != "Cloth Shoes")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Feet);
            GameState.CurrentHero.Feet = (Armor)GameState.AllItems.Find(x => x.Name == "Cloth Shoes");
            DisplayAllInfo();
        }

        private void btnUnequipLegs_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Legs.Name != "Cloth Pants")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Legs);
            GameState.CurrentHero.Legs = (Armor)GameState.AllItems.Find(x => x.Name == "Cloth Pants");
            DisplayAllInfo();
        }

        private void btnUnequipWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Weapon.Name != "Fists")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Weapon);
            GameState.CurrentHero.Weapon = (Weapon)GameState.AllItems.Find(x => x.Name == "Fists");
            DisplayAllInfo();
        }

        #endregion Unequip Button Clicks

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public InventoryWindow()
        {
            InitializeComponent();
            BindLabels();
        }

        private void windowInventory_Closing(object sender, CancelEventArgs e)
        {
            GameState.SaveHero();
            RefToCharacterWindow.Show();
        }

        #endregion Window-Manipulation Methods

        private void lstInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplaySelectedInfo();
        }
    }
}