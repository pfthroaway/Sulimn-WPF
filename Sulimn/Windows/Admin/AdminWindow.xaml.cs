﻿using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for AdminWindow.xaml</summary>
    public partial class AdminWindow
    {
        internal MainWindow RefToMainWindow { get; set; }

        #region Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersWindow manageUsersWindow = new ManageUsersWindow { RefToAdminWindow = this };
            manageUsersWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnChangeAdminPassword_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePasswordWindow adminChangePasswordWindow = new AdminChangePasswordWindow
            {
                RefToAdminWindow = this
            };
            adminChangePasswordWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnManageArmor_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnManageEnemies_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnManageFood_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnManageHeroClass_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnManagePotion_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnManageWeapons_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void windowAdmin_Closing(object sender, CancelEventArgs e)
        {
            RefToMainWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}