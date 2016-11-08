﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sulimn_WPF
{
    internal static class Functions
    {
        private const string _DBPROVIDERANDSOURCE = "Data Source = Sulimn.sqlite;Version=3";

        #region Random Number Generation

        /// <summary>
        /// Generates a random number between min and max (inclusive).
        /// </summary>
        /// <param name="min">Inclusive minimum number</param>
        /// <param name="max">Inclusive maximum number</param>
        /// <returns>Returns randomly generated integer between min and max.</returns>
        internal static int GenerateRandomNumber(int min, int max)
        {
            return GenerateRandomNumber(min, max, Int32.MaxValue);
        }

        /// <summary>
        /// Generates a random number between min and max (inclusive).
        /// </summary>
        /// <param name="min">Inclusive minimum number</param>
        /// <param name="max">Inclusive maximum number</param>
        /// <param name="upperLimit">Maximum limit for the method, regardless of min and max.</param>
        /// <returns>Returns randomly generated integer between min and max with an upper limit of upperLimit.</returns>
        internal static int GenerateRandomNumber(int min, int max, int upperLimit)
        {
            int result;

            if (min < max)
                result = ThreadSafeRandom.ThisThreadsRandom.Next(min, max + 1);
            else
                result = ThreadSafeRandom.ThisThreadsRandom.Next(max, min + 1);

            if (result > upperLimit)
                return upperLimit;

            return result;
        }

        #endregion Random Number Generation

        /// <summary>
        /// Turns several Keyboard.Keys into a list of Keys which can be tested using List.Any.
        /// </summary>
        /// <param name="keys">Array of Keys</param>
        /// <returns></returns>
        internal static List<bool> GetListOfKeys(params Key[] keys)
        {
            List<bool> allKeys = new List<bool>();
            foreach (Key key in keys)
                allKeys.Add(Keyboard.IsKeyDown(key));
            return allKeys;
        }

        #region General Database Manipulation

        // This method fills a DataSet with data from a table.
        internal static async Task<DataSet> FillDataSet(string sql, string tableName)
        {
            SQLiteConnection con = new SQLiteConnection();
            SQLiteDataAdapter da = new SQLiteDataAdapter();
            DataSet ds = new DataSet();
            con.ConnectionString = _DBPROVIDERANDSOURCE;

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, tableName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Filling DataSet", MessageBoxButton.OK);
                }
                finally { con.Close(); }
            });
            return ds;
        }

        //// This method deletes a record in a database.
        //internal void DeleteRecord(string sql, string tableName, DataSet ds)
        //{
        //  string dbProvider = "Provider=Microsoft.ACE.oledb.12.0;";
        //  string dbSource = "Data Source=Players.accdb";
        //  SQLiteConnection con = new SQLiteConnection();
        //  SQLiteDataAdapter da = new SQLiteDataAdapter();
        //  SQLiteCommandBuilder cb = new SQLiteCommandBuilder(da);

        //  try
        //  {
        //    con.ConnectionString = dbProvider + dbSource;
        //    da = new SQLiteDataAdapter(sql, con);
        //    ds.Tables[tableName].Rows[0].Delete();
        //    da.Update(ds, tableName);
        //  }
        //  catch (Exception e)
        //  {
        //    MessageBox.Show(e.Message, "Sulimn", MessageBoxButtons.OK);
        //  }
        //  finally { con.Close(); }
        //}

        //// This method updates a record in a database.
        //internal void UpdateRecord(string sql, string tableName, DataSet ds)
        //{
        //  string dbProvider = "Provider=Microsoft.ACE.oledb.12.0;";
        //  string dbSource = "Data Source=Players.accdb";
        //  SQLiteConnection con = new SQLiteConnection();
        //  SQLiteDataAdapter da = new SQLiteDataAdapter();
        //  SQLiteCommandBuilder cb = new SQLiteCommandBuilder(da);

        //  try
        //  {
        //    con.ConnectionString = dbProvider + dbSource;
        //    da = new SQLiteDataAdapter(sql, con);
        //    da.UpdateCommand.CommandText = sql;
        //    da.Update(ds, tableName);
        //  }
        //  catch (Exception e)
        //  {
        //    MessageBox.Show(e.Message, "Sulimn", MessageBoxButtons.OK);
        //  }
        //  finally { con.Close(); }
        //}

        #endregion General Database Manipulation
    }
}