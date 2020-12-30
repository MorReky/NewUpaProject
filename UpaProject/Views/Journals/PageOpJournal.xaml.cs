﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UpaProject.DataFilesApp;

namespace UpaProject.Journals
{
    /// <summary>
    /// Логика взаимодействия для PageOpJournal.xaml
    /// </summary>
    public partial class PageOpJournal : Page
    {
        public PageOpJournal()
        {
            InitializeComponent();
                     
            //сделатьь реализацию через объект
            GridList.ItemsSource = DBConnectHelper.DbObj.OpLogJournal.ToList();
            GridList.SelectedIndex = 0;

            txtDate.Text = DBConnectHelper.DbObj.Shifts.OrderByDescending(x => x.IdShift).First().DateStartShift.ToString();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += UpdateContext;
        }

        private void UpdateContext(object sender, EventArgs e)
        {
            //Разобрать строку
            // DBConnectHelper.DbObj.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            GridList.ItemsSource = DBConnectHelper.DbObj.EqList.ToList();
        }

        private void BtnNewTurn_Click(object sender, RoutedEventArgs e)
        {
            WindowNewOpTurn newOpTurn = new WindowNewOpTurn();
            newOpTurn.ShowDialog();
        }

        private void BtnNewSubj_Click(object sender, RoutedEventArgs e)
        {
            WindowNewSubj windowNewSubj = new WindowNewSubj();
            windowNewSubj.ShowDialog();
        }
    }
}