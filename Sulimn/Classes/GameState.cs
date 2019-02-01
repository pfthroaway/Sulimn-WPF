﻿using Extensions;
using Extensions.Encryption;
using Extensions.Enums;
using Sulimn.Classes.Database;
using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using Sulimn.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Classes
{
    /// <summary>Represents the current state of the game.</summary>
    internal static class GameState
    {
        internal static string AdminPassword = "";
        internal static Hero CurrentHero = new Hero();
        internal static Enemy CurrentEnemy = new Enemy();
        internal static Hero MaximumStatsHero = new Hero();
        internal static List<Enemy> AllEnemies = new List<Enemy>();
        internal static List<Item> AllItems = new List<Item>();
        internal static List<HeadArmor> AllHeadArmor = new List<HeadArmor>();
        internal static List<BodyArmor> AllBodyArmor = new List<BodyArmor>();
        internal static List<HandArmor> AllHandArmor = new List<HandArmor>();
        internal static List<LegArmor> AllLegArmor = new List<LegArmor>();
        internal static List<FeetArmor> AllFeetArmor = new List<FeetArmor>();
        internal static List<Ring> AllRings = new List<Ring>();
        internal static List<Weapon> AllWeapons = new List<Weapon>();
        internal static List<Food> AllFood = new List<Food>();
        internal static List<Drink> AllDrinks = new List<Drink>();
        internal static List<Potion> AllPotions = new List<Potion>();
        internal static List<Spell> AllSpells = new List<Spell>();
        internal static List<Hero> AllHeroes = new List<Hero>();
        internal static List<HeroClass> AllClasses = new List<HeroClass>();
        internal static Weapon DefaultWeapon = new Weapon();
        internal static HeadArmor DefaultHead = new HeadArmor();
        internal static BodyArmor DefaultBody = new BodyArmor();
        internal static HandArmor DefaultHands = new HandArmor();
        internal static LegArmor DefaultLegs = new LegArmor();
        internal static FeetArmor DefaultFeet = new FeetArmor();

        internal static SqLiteDatabaseInteraction DatabaseInteraction = new SqLiteDatabaseInteraction();

        #region Navigation

        /// <summary>Instance of MainWindow currently loaded</summary>
        internal static MainWindow MainWindow { get; set; }

        /// <summary>Navigates to selected Page.</summary>
        /// <param name="newPage">Page to navigate to.</param>
        internal static void Navigate(Page newPage) => MainWindow.MainFrame.Navigate(newPage);

        /// <summary>Navigates to the previous Page.</summary>
        internal static void GoBack()
        {
            if (MainWindow.MainFrame.CanGoBack)
                MainWindow.MainFrame.GoBack();
        }

        #endregion Navigation

        /// <summary>Changes the administrator password in the database.</summary>
        /// <param name="newPassword">New administrator password</param>
        /// <returns>Returns true if password successfully updated</returns>
        internal static Task<bool> ChangeAdminPassword(string newPassword) => DatabaseInteraction
            .ChangeAdminPassword(newPassword);

        /// <summary>Determines whether a Hero's credentials are authentic.</summary>
        /// <param name="username">Hero's name</param>
        /// <param name="password">Hero's password</param>
        /// <returns>Returns true if valid login</returns>
        internal static bool CheckLogin(string username, string password)
        {
            try
            {
                Hero checkHero = AllHeroes.Find(hero => hero.Name == username);
                if (checkHero != null && checkHero != new Hero())
                    if (Argon2.ValidatePassword(checkHero.Password, password))
                    {
                        CurrentHero = checkHero;
                        return true;
                    }
                DisplayNotification("Invalid login.", "Sulimn");
                return false;
            }
            catch (Exception ex)
            {
                DisplayNotification(ex.Message, "Sulimn");
                return false;
            }
        }

        /// <summary>Loads the current theme from the database.</summary>
        /// <returns>Current theme</returns>
        internal static Task<string> LoadTheme() => DatabaseInteraction.LoadTheme();

        /// <summary>Changes the current theme in the database.</summary>
        /// <param name="theme">Current theme</param>
        /// <returns>True if successful</returns>
        internal static Task<bool> ChangeTheme(string theme) => DatabaseInteraction.ChangeTheme(theme);

        /// <summary>Loads almost everything from the database.</summary>
        internal static async Task LoadAll()
        {
            DatabaseInteraction.VerifyDatabaseIntegrity();

            AdminPassword = await DatabaseInteraction.LoadAdminPassword();
            AllClasses = XMLInteraction.LoadClasses();
            AllHeadArmor = XMLInteraction.LoadHeadArmor();
            AllBodyArmor = XMLInteraction.LoadBodyArmor();
            AllHandArmor = XMLInteraction.LoadHandArmor();
            AllLegArmor = XMLInteraction.LoadLegArmor();
            AllFeetArmor = XMLInteraction.LoadFeetArmor();
            AllRings = XMLInteraction.LoadRings();
            AllDrinks = XMLInteraction.LoadDrinks();
            AllFood = XMLInteraction.LoadFood();
            AllPotions = XMLInteraction.LoadPotions();
            AllSpells = XMLInteraction.LoadSpells();
            AllWeapons = XMLInteraction.LoadWeapons();
            AllItems.AddRanges(AllHeadArmor, AllBodyArmor, AllHandArmor, AllLegArmor, AllFeetArmor, AllRings, AllFood, AllDrinks, AllPotions, AllWeapons);
            AllEnemies = XMLInteraction.LoadEnemies();
            AllHeroes = XMLInteraction.LoadHeroes();
            MaximumStatsHero = await DatabaseInteraction.LoadMaxHeroStats();

            AllItems = AllItems.OrderBy(item => item.Name).ToList();
            AllEnemies = AllEnemies.OrderBy(enemy => enemy.Name).ToList();
            AllSpells = AllSpells.OrderBy(spell => spell.Name).ToList();
            AllClasses = AllClasses.OrderBy(heroClass => heroClass.Name).ToList();
            AllRings = AllRings.OrderBy(ring => ring.Name).ToList();

            DefaultWeapon = AllWeapons.Find(weapon => weapon.Name == "Fists");
            DefaultHead = AllHeadArmor.Find(armor => armor.Name == "Cloth Helmet");
            DefaultBody = AllBodyArmor.Find(armor => armor.Name == "Cloth Shirt");
            DefaultHands = AllHandArmor.Find(armor => armor.Name == "Cloth Gloves");
            DefaultLegs = AllLegArmor.Find(armor => armor.Name == "Cloth Pants");
            DefaultFeet = AllFeetArmor.Find(armor => armor.Name == "Cloth Shoes");
        }

        internal static async Task WriteAll()
        {
            DatabaseInteraction.VerifyDatabaseIntegrity();

            AdminPassword = await DatabaseInteraction.LoadAdminPassword();
            AllClasses = await DatabaseInteraction.LoadClasses();
            AllHeadArmor = await DatabaseInteraction.LoadHeadArmor();
            AllBodyArmor = await DatabaseInteraction.LoadBodyArmor();
            AllHandArmor = await DatabaseInteraction.LoadHandArmor();
            AllLegArmor = await DatabaseInteraction.LoadLegArmor();
            AllFeetArmor = await DatabaseInteraction.LoadFeetArmor();
            AllRings = await DatabaseInteraction.LoadRings();
            AllFood = await DatabaseInteraction.LoadFood();
            AllPotions = await DatabaseInteraction.LoadPotions();
            AllSpells = await DatabaseInteraction.LoadSpells();
            AllWeapons = await DatabaseInteraction.LoadWeapons();
            AllItems.AddRanges(AllHeadArmor, AllBodyArmor, AllHandArmor, AllLegArmor, AllFeetArmor, AllRings, AllFood, AllPotions, AllWeapons);
            AllEnemies = await DatabaseInteraction.LoadEnemies();
            AllHeroes = await DatabaseInteraction.LoadHeroes();
            MaximumStatsHero = await DatabaseInteraction.LoadMaxHeroStats();

            AllItems = AllItems.OrderBy(item => item.Name).ToList();
            AllEnemies = AllEnemies.OrderBy(enemy => enemy.Name).ToList();
            AllSpells = AllSpells.OrderBy(spell => spell.Name).ToList();
            AllClasses = AllClasses.OrderBy(heroClass => heroClass.Name).ToList();
            AllRings = AllRings.OrderBy(ring => ring.Name).ToList();

            DefaultWeapon = AllWeapons.Find(weapon => weapon.Name == "Fists");
            DefaultHead = AllHeadArmor.Find(armor => armor.Name == "Cloth Helmet");
            DefaultBody = AllBodyArmor.Find(armor => armor.Name == "Cloth Shirt");
            DefaultHands = AllHandArmor.Find(armor => armor.Name == "Cloth Gloves");
            DefaultLegs = AllLegArmor.Find(armor => armor.Name == "Cloth Pants");
            DefaultFeet = AllFeetArmor.Find(armor => armor.Name == "Cloth Shoes");
        }

        /// <summary>Ensures all necessary folders are created.</summary>
        internal static void VerifyFolders()
        {
            Directory.CreateDirectory("Data");
            Directory.CreateDirectory("Data/Armor");
            Directory.CreateDirectory("Data/Classes");
            Directory.CreateDirectory("Data/Drinks");
            Directory.CreateDirectory("Data/Enemies");
            Directory.CreateDirectory("Data/Food");
            Directory.CreateDirectory("Data/Heroes");
            Directory.CreateDirectory("Data/Potions");
            Directory.CreateDirectory("Data/Rings");
            Directory.CreateDirectory("Data/Spells");
            Directory.CreateDirectory("Data/StatusEffects");
            Directory.CreateDirectory("Data/Weapons");
            Directory.CreateDirectory("Mods");
            Directory.CreateDirectory("Mods/Armor");
            Directory.CreateDirectory("Mods/Classes");
            Directory.CreateDirectory("Mods/Drinks");
            Directory.CreateDirectory("Mods/Enemies");
            Directory.CreateDirectory("Mods/Food");
            Directory.CreateDirectory("Mods/Potions");
            Directory.CreateDirectory("Mods/Rings");
            Directory.CreateDirectory("Mods/Spells");
            Directory.CreateDirectory("Mods/StatusEffects");
            Directory.CreateDirectory("Mods/Weapons");
        }

        /// <summary>Gets a specific Enemy based on its name.</summary>
        /// <param name="name">Name of Enemy</param>
        /// <returns>Enemy</returns>
        private static Enemy GetEnemy(string name) => new Enemy(AllEnemies.Find(enemy => enemy.Name == name));

        /// <summary>Gets a specific Item based on its name.</summary>
        /// <param name="name">Item name</param>
        /// <returns>Item</returns>
        private static Item GetItem(string name) => AllItems.Find(itm => itm.Name == name);

        #region Item Management

        /// <summary>Retrieves a List of all Items of specified Type.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>List of specified Type.</returns>
        public static List<T> GetItemsOfType<T>() => AllItems.OfType<T>().ToList();

        /// <summary>Sets allowed <see cref="HeroClass"/>es based on a string.</summary>
        /// <param name="classes">AllowedClasses to be converted</param>
        /// <returns>List of allowed <see cref="HeroClass"/>es</returns>
        internal static List<HeroClass> SetAllowedClasses(string classes)
        {
            List<HeroClass> newAllowedClasses = new List<HeroClass>();

            if (classes.Length > 0)
            {
                string[] arrAllowedClasses = classes.Split(',');
                newAllowedClasses.AddRange(arrAllowedClasses.Select(str => AllClasses.Find(x => x.Name == str.Trim())));
            }
            return newAllowedClasses;
        }

        /// <summary>Sets the Hero's inventory.</summary>
        /// <param name="inventory">Inventory to be converted</param>
        /// <returns>Inventory List</returns>
        internal static List<Item> SetInventory(string inventory)
        {
            List<Item> newInventory = new List<Item>();

            if (inventory.Length > 0)
            {
                string[] arrInventory = inventory.Split(',');
                newInventory.AddRange(arrInventory.Select(str => AllItems.Find(x => x.Name == str.Trim())));
            }
            return newInventory;
        }

        /// <summary>Sets the list of the Hero's known spells.</summary>
        /// <param name="spells">String list of spells</param>
        /// <returns>List of known Spells</returns>
        internal static Spellbook SetSpellbook(string spells)
        {
            List<Spell> spellList = new List<Spell>();

            if (spells.Length > 0)
            {
                string[] arrSpell = spells.Split(',');

                foreach (string str in arrSpell)
                    spellList.Add(AllSpells.Find(x => x.Name == str.Trim()));
            }
            return new Spellbook(spellList);
        }

        #endregion Item Management

        #region Hero Management

        /// <summary>Modifies a Hero's details in the database.</summary>
        /// <param name="oldHero">Hero whose details need to be modified</param>
        /// <param name="newHero">Hero with new details</param>
        /// <returns>True if successful</returns>
        internal static async Task<bool> ChangeHeroDetails(Hero oldHero, Hero newHero)
        {
            bool success = false;
            if (await DatabaseInteraction.ChangeHeroDetails(oldHero, newHero))
            {
                Hero currentHero = AllHeroes.Find(hero => hero.Name == oldHero.Name);
                currentHero.Name = newHero.Name;
                currentHero.Password = newHero.Password;
                //AllHeroes.Replace(oldHero, currentHero);
                success = true;
            }
            return success;
        }

        /// <summary>Deletes a Hero from the game and database.</summary>
        /// <param name="deleteHero">Hero to be deleted</param>
        /// <returns>Whether deletion was successful</returns>
        internal static async Task<bool> DeleteHero(Hero deleteHero)
        {
            bool success = false;
            if (await DatabaseInteraction.DeleteHero(deleteHero))
            {
                AllHeroes.Remove(deleteHero);
                if (CurrentHero == deleteHero)
                    CurrentHero = new Hero();
                success = true;
            }
            return success;
        }

        /// <summary>Loads a Hero's Bank.</summary>
        /// <param name="bankHero">Hero whose Bank is to be loaded.</param>
        /// <returns>Hero's Bank</returns>
        internal static Task<Bank> LoadHeroBank(Hero bankHero) => DatabaseInteraction.LoadBank(bankHero);

        /// <summary>Creates a new Hero and adds it to the database.</summary>
        /// <param name="newHero">New Hero</param>
        /// <returns>Returns true if successfully created</returns>
        internal static async Task<bool> NewHero(Hero newHero)
        {
            bool success = false;
            if (newHero.Equipment.Head == null || newHero.Equipment.Head == new HeadArmor())
                newHero.Equipment.Head = AllHeadArmor.Find(armor => armor.Name == DefaultHead.Name);
            if (newHero.Equipment.Body == null || newHero.Equipment.Body == new BodyArmor())
                newHero.Equipment.Body = AllBodyArmor.Find(armor => armor.Name == DefaultBody.Name);
            if (newHero.Equipment.Hands == null || newHero.Equipment.Hands == new HandArmor())
                newHero.Equipment.Hands = AllHandArmor.Find(armor => armor.Name == DefaultHands.Name);
            if (newHero.Equipment.Legs == null || newHero.Equipment.Legs == new LegArmor())
                newHero.Equipment.Legs = AllLegArmor.Find(armor => armor.Name == DefaultLegs.Name);
            if (newHero.Equipment.Feet == null || newHero.Equipment.Feet == new FeetArmor())
                newHero.Equipment.Feet = AllFeetArmor.Find(armor => armor.Name == DefaultFeet.Name);
            if (newHero.Equipment.Weapon == null || newHero.Equipment.Weapon == new Weapon())
            {
                switch (newHero.Class.Name)
                {
                    case "Wizard":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Staff");
                        if (newHero.Spellbook == null || newHero.Spellbook == new Spellbook())
                            newHero.Spellbook?.LearnSpell(AllSpells.Find(spell => spell.Name == "Fire Bolt"));
                        break;

                    case "Cleric":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Staff");
                        if (newHero.Spellbook == null || newHero.Spellbook == new Spellbook())
                            newHero.Spellbook?.LearnSpell(AllSpells.Find(spell => spell.Name == "Heal Self"));
                        break;

                    case "Warrior":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Stone Dagger");
                        break;

                    case "Rogue":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Bow");
                        break;

                    default:
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Stone Dagger");
                        break;
                }
            }

            newHero.Gold = 250;
            for (int i = 0; i < 3; i++)
                newHero.AddItem(AllPotions.Find(itm => itm.Name == "Minor Healing Potion"));

            if (await DatabaseInteraction.NewHero(newHero))
            {
                AllHeroes.Add(newHero);
                success = true;
            }

            return success;
        }

        /// <summary>Saves Hero to database.</summary>
        /// <param name="saveHero">Hero to be saved</param>
        /// <returns>Returns true if successfully saved</returns>
        internal static bool SaveHero(Hero saveHero)
        {
            XMLInteraction.SaveHero(saveHero);

            int index = AllHeroes.FindIndex(hero => hero.Name == saveHero.Name);
            AllHeroes[index] = saveHero;

            return true;
        }

        /// <summary>Saves the Hero's bank information.</summary>
        /// <param name="goldInBank">Gold in the bank</param>
        /// <param name="loanTaken">Loan taken out</param>
        internal static Task<bool> SaveHeroBank(int goldInBank, int loanTaken) => DatabaseInteraction
            .SaveHeroBank(CurrentHero, goldInBank, loanTaken);

        /// <summary>Saves the Hero's password to the database.</summary>
        /// <param name="saveHero">Hero whose password needs to be saved</param>
        internal static async Task<bool> SaveHeroPassword(Hero saveHero)
        {
            bool success = false;
            if (await DatabaseInteraction.SaveHeroPassword(saveHero))
            {
                int index = AllHeroes.FindIndex(hero => hero.Name == CurrentHero.Name);
                AllHeroes[index] = CurrentHero;
                success = true;
            }
            return success;
        }

        #endregion Hero Management

        #region Exploration Events

        /// <summary>Event where the Hero finds gold.</summary>
        /// <param name="minGold">Minimum amount of gold to be found</param>
        /// <param name="maxGold">Maximum amount of gold to be found</param>
        /// <returns>Returns text regarding gold found</returns>
        internal static string EventFindGold(int minGold, int maxGold)
        {
            int foundGold = minGold == maxGold ? minGold : Functions.GenerateRandomNumber(minGold, maxGold);
            CurrentHero.Gold += foundGold;
            SaveHero(CurrentHero);
            return $"You find {foundGold:N0} gold!";
        }

        /// <summary>Event where the Hero finds an item.</summary>
        /// <param name="minValue">Minimum value of Item</param>
        /// <param name="maxValue">Maximum value of Item</param>
        /// <param name="canSell">Can the item be sold?</param>
        /// <returns>Returns text about found Item</returns>
        internal static string EventFindItem(int minValue, int maxValue, bool canSell = true)
        {
            List<Item> availableItems = AllItems.Where(x => x.Value >= minValue && x.Value <= maxValue && x.IsSold).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.AddItem(availableItems[item]);
            SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero finds an item.</summary>
        /// <param name="names">List of names of available Items</param>
        /// <returns>Returns text about found Item</returns>
        internal static string EventFindItem(params string[] names)
        {
            List<Item> availableItems = new List<Item>();
            foreach (string name in names)
                availableItems.Add(GetItem(name));
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.AddItem(availableItems[item]);

            SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero finds an item.</summary>
        /// <typeparam name="T">Type of Item to be found</typeparam>
        /// <param name="minValue">Minimum value of Item</param>
        /// <param name="maxValue">Maximum value of Item</param>
        /// <param name="canSell">Can the item be sold?</param>
        /// <returns>Returns text about found Item</returns>
        internal static string EventFindItem<T>(int minValue, int maxValue, bool canSell = true) where T : Item
        {
            List<T> availableItems = new List<T>(GetItemsOfType<T>());
            availableItems = availableItems.FindAll(itm => itm.Value >= minValue && itm.Value <= maxValue).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);
            CurrentHero.AddItem(availableItems[item]);
            SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero encounters a hostile animal.</summary>
        /// <param name="minLevel">Minimum level of animal</param>
        /// <param name="maxLevel">Maximum level of animal</param>
        internal static void EventEncounterAnimal(int minLevel, int maxLevel) => EventEncounterEnemy(AllEnemies.Where(enemy => enemy.Level >= minLevel && enemy.Level <= maxLevel && enemy.Type == "Animal").ToList());

        /// <summary>Event where the Hero encounters a hostile Enemy.</summary>
        /// <param name="minLevel">Minimum level of Enemy.</param>
        /// <param name="maxLevel">Maximum level of Enemy.</param>
        internal static void EventEncounterEnemy(int minLevel, int maxLevel) => EventEncounterEnemy(AllEnemies.Where(enemy => enemy.Level >= minLevel && enemy.Level <= maxLevel && enemy.Type != "Boss").ToList());

        /// <summary>Event where the Hero encounters a hostile Enemy.</summary>
        /// <param name="names">Array of names</param>
        internal static void EventEncounterEnemy(params string[] names) =>
            EventEncounterEnemy(names.Select(GetEnemy).ToList());

        internal static void EventEncounterEnemy(List<Enemy> availableEnemies)
        {
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            CurrentEnemy = new Enemy(availableEnemies[enemyNum]);
            if (CurrentEnemy.Gold > 0)
                CurrentEnemy.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Gold / 2, CurrentEnemy.Gold);
        }

        /// <summary>Event where the Hero encounters a water stream and restores health and magic.</summary>
        /// <returns>String saying Hero has been healed</returns>
        internal static string EventEncounterStream()
        {
            CurrentHero.Statistics.CurrentHealth = CurrentHero.Statistics.MaximumHealth;
            CurrentHero.Statistics.CurrentMagic = CurrentHero.Statistics.MaximumMagic;

            return
            "You stumble across a stream. You stop to drink some of the water and rest a while. You feel recharged!";
        }

        #endregion Exploration Events

        #region Notification Management

        /// <summary>Displays a new Notification in a thread-safe way.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        internal static void DisplayNotification(string message, string title) => Application.Current.Dispatcher.Invoke(() => new Notification(message, title, NotificationButtons.OK, MainWindow).ShowDialog());

        /// <summary>Displays a new Notification in a thread-safe way and retrieves a boolean result upon its closing.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        /// <returns>Returns value of clicked button on Notification.</returns>
        internal static bool YesNoNotification(string message, string title) => Application.Current.Dispatcher.Invoke(() => (new Notification(message, title, NotificationButtons.YesNo, MainWindow).ShowDialog() == true));

        #endregion Notification Management
    }
}