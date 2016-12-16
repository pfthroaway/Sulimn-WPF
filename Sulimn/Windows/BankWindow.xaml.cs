﻿using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for BankWindow.xaml
    /// </summary>
    public partial class BankWindow : INotifyPropertyChanged
    {
        private readonly string nl = Environment.NewLine;
        private int _goldInBank;
        private int _loanAvailable;
        private int _loanTaken;

        internal CityWindow RefToCityWindow { get; set; }

        #region Modifying Properties

        public int GoldInBank
        {
            get { return _goldInBank; }
            set
            {
                _goldInBank = value;
                OnPropertyChanged("GoldInBankToString");
            }
        }

        public int LoanAvailable
        {
            get { return _loanAvailable; }
            set
            {
                _loanAvailable = value;
                OnPropertyChanged("LoanAvailableToString");
            }
        }

        public int LoanTaken
        {
            get { return _loanTaken; }
            set
            {
                _loanTaken = value;
                OnPropertyChanged("LoanTakenToString");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        public string GoldInBankToString => GoldInBank.ToString("N0");

        public string LoanAvailableToString => LoanAvailable.ToString("N0");

        public string LoanTakenToString => LoanTaken.ToString("N0");

        #endregion Helper Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds text to the labels.
        /// </summary>
        internal void BindLabels()
        {
            DataContext = this;
            lblGoldOnHand.DataContext = GameState.CurrentHero.Inventory;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>
        /// Adds text to the txtBank Textbox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        internal void AddTextTT(string newText)
        {
            txtBank.Text += nl + nl + newText;
            txtBank.Focus();
            txtBank.CaretIndex = txtBank.Text.Length;
            txtBank.ScrollToEnd();
        }

        /// <summary>
        /// Checks what buttons should be enabled.
        /// </summary>
        internal void CheckButtons()
        {
            btnDeposit.IsEnabled = GameState.CurrentHero.Inventory.Gold > 0;

            btnWithdraw.IsEnabled = GoldInBank > 0;

            btnTakeLoan.IsEnabled = LoanAvailable > 0;

            btnRepayLoan.IsEnabled = LoanTaken > 0;
        }

        /// <summary>
        /// Displays the Bank Dialog Window.
        /// </summary>
        /// <param name="maximum">Maximum amount of gold permitted</param>
        /// <param name="type">Type of Window information to be displayed</param>
        private void DisplayBankDialog(int maximum, BankAction type)
        {
            BankDialogWindow bankDialogWindow = new BankDialogWindow();
            bankDialogWindow.LoadWindow(maximum, type);
            bankDialogWindow.RefToBankWindow = this;
            bankDialogWindow.Show();
            Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Loads the initial Bank state and Hero's Bank information..
        /// </summary>
        internal async void LoadBank()
        {
            DataSet ds =
            await Functions.FillDataSet(
            "SELECT * FROM Bank WHERE [CharacterName]='" + GameState.CurrentHero.Name + "'", "Bank");

            if (ds.Tables[0].Rows.Count > 0)
            {
                GoldInBank = Int32Helper.Parse(ds.Tables[0].Rows[0]["Gold"]);
                LoanTaken = Int32Helper.Parse(ds.Tables[0].Rows[0]["LoanTaken"]);
            }
            else
            {
                MessageBox.Show("No such user exists in the bank.", "Sulimn", MessageBoxButton.OK);
            }

            LoanAvailable = GameState.CurrentHero.Level * 250 - LoanTaken;

            txtBank.Text =
            "You enter the Bank. A teller beckons to you and you approach him. You tell him your name, and he rummages through a few papers. He finds one, and pulls it out." +
            nl + nl + "You have " + GoldInBank.ToString("N0") +
            " gold available to withdraw. You also have an open credit line of " + LoanAvailable.ToString("N0") +
            " gold.";
            BindLabels();
            CheckButtons();
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            DisplayBankDialog(GameState.CurrentHero.Inventory.Gold, BankAction.Deposit);
        }

        private void btnRepayLoan_Click(object sender, RoutedEventArgs e)
        {
            DisplayBankDialog(LoanTaken, BankAction.Repay);
        }

        private void btnTakeLoan_Click(object sender, RoutedEventArgs e)
        {
            DisplayBankDialog(LoanAvailable, BankAction.Borrow);
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            DisplayBankDialog(GoldInBank, BankAction.Withdrawal);
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            Close();
        }

        public BankWindow()
        {
            InitializeComponent();
        }

        private async void windowBank_Closing(object sender, CancelEventArgs e)
        {
            await Task.Factory.StartNew(() => GameState.SaveHero(GameState.CurrentHero));
            await Task.Factory.StartNew(() => GameState.SaveHeroBank(GoldInBank, LoanTaken));
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}