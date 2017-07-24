﻿using Extensions;
using Extensions.Encryption;
using Sulimn.Classes;
using System.Windows;

namespace Sulimn.Pages.Admin
{
    /// <summary>Interaction logic for AdminChangePasswordPage.xaml</summary>
    public partial class AdminChangePasswordPage
    {
        #region Button-Click Methods

        private async void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PswdCurrentPassword.Password.Length >= 1 && PswdNewPassword.Password.Length >= 1 &&
            PswdConfirmPassword.Password.Length >= 1)
            {
                if (Argon2.ValidatePassword(GameState.AdminPassword, PswdCurrentPassword.Password))
                    if (PswdNewPassword.Password == PswdConfirmPassword.Password)
                        if (PswdCurrentPassword.Password != PswdNewPassword.Password)
                        {
                            string newPassword = Argon2.HashPassword(PswdNewPassword.Password);

                            if (await GameState.ChangeAdminPassword(newPassword))
                            {
                                GameState.AdminPassword = newPassword;
                                GameState.DisplayNotification("Successfully changed administrator password.", "Sulimn");
                                ClosePage();
                            }
                            else
                                GameState.DisplayNotification("Unable to change administrator password.", "Sulimn");
                        }
                        else
                        {
                            GameState.DisplayNotification("The new password can't be the same as the current password.", "Sulimn");
                        }
                    else
                        GameState.DisplayNotification("Please ensure the new passwords match.", "Sulimn");
                else
                    GameState.DisplayNotification("Invalid current administrator password.", "Sulimn")
                    ;
            }
            else
                GameState.DisplayNotification("The old and new passwords must be at least 4 characters in length.", "Sulimn");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.MainWindow.MainFrame.GoBack();
        }

        public AdminChangePasswordPage()
        {
            InitializeComponent();
            PswdCurrentPassword.Focus();
        }

        private void PswdChanged(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = PswdCurrentPassword.Password.Length >= 1 && PswdNewPassword.Password.Length >= 1 &&
            PswdConfirmPassword.Password.Length >= 1;
        }

        private void Pswd_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.PasswordBoxGotFocus(sender);
        }

        #endregion Page-Manipulation Methods

        private void AdminChangePasswordPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}