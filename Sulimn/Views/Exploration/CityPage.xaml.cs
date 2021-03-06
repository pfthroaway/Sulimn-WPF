﻿using Extensions;
using Sulimn.Classes;
using Sulimn.Views.BankPages;
using Sulimn.Views.Characters;
using Sulimn.Views.Options;
using Sulimn.Views.Shopping;
using System.Windows;

namespace Sulimn.Views.Exploration
{
    /// <summary>Interaction logic for CityPage.xaml</summary>
    public partial class CityPage
    {
        /// <summary>Is a newly created <see cref="Hero"/> logging in for the first time?</summary>
        internal bool NewHero { get; set; }

        #region Button-Click Methods

        private void BtnBank_Click(object sender, RoutedEventArgs e)
        {
            BankPage bankPage = new BankPage();
            bankPage.LoadBank();
            GameState.Navigate(bankPage);
        }

        private void BtnChapel_Click(object sender, RoutedEventArgs e)
        {
            if (
            decimal.Divide(GameState.CurrentHero.Statistics.CurrentHealth,
            GameState.CurrentHero.Statistics.MaximumHealth) <= 0.25M)
            {
                Functions.AddTextToTextBox(TxtCity, "You enter a local chapel and approach the altar. A priest approaches you.\n" +
                "\"Let me heal your wounds. You look like you've been through a tough battle.\"\n" +
                "The priest gives you a potion which heals you to full health!\n" +
                "You thank the priest and return to the streets.");
                GameState.CurrentHero.Statistics.CurrentHealth = GameState.CurrentHero.Statistics.MaximumHealth;

                GameState.SaveHero(GameState.CurrentHero);
            }
            else
            {
                Functions.AddTextToTextBox(TxtCity, "You enter a local chapel. A priest approaches you.\n" +
                "\"You look healthy to me. If you ever need healing, don't hesitate to come see me.\"\n\n" +
                "You thank the priest and return to the streets.");
            }
        }

        private void BtnCharacter_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new CharacterPage());

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.YesNoNotification("Are you sure you want to exit?", "Sulimn"))
            {
                GameState.GoBack();
                GameState.MainWindow.StatsFrame.Visibility = Visibility.Collapsed;
                GameState.MainWindow.StatsFrame.Content = null;
                while (GameState.MainWindow.StatsFrame.NavigationService.RemoveBackEntry() != null) ;
            }
        }

        private void BtnExplore_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new ExplorePage());

        private void BtnMarket_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new MarketPage());

        private void BtnOptions_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new HeroChangePasswordPage());

        private void BtnTavern_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new TavernPage());

        #endregion Button-Click Methods

        #region Page-Generated Methods

        /// <summary>Closes the Page.</summary>

        public CityPage()
        {
            InitializeComponent();
            TxtCity.Text =
            "You are in the city of Sulimn. There is a bustling market in the center of the city, a dilapidated cathedral sprawling over the cityscape at the north end, an abandoned mining complex on the south side, crop fields to the east, and a forest to the west.";
        }

        private void CityPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (NewHero)
            {
                GameState.MainWindow.MainFrame.RemoveBackEntry();
                NewHero = false;
            }
            GameState.MainWindow.StatsFrame.Navigate(new StatsPage());
            GameState.MainWindow.StatsFrame.Visibility = Visibility.Visible;
        }

        #endregion Page-Generated Methods
    }
}