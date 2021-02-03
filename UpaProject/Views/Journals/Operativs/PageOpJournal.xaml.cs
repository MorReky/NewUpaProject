using System;
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
using UpaProject.Models.DataFilesApp;

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

            GridList.ItemsSource = DBConnectHelper.DbObj.OpShifts_OpRecord.OrderByDescending(x=>x.OpRececord.DateOccurence).ToList();
            txtDate.Text = DBConnectHelper.DbObj.OpShifts.OrderByDescending(x => x.DateStartShift).First().DateStartShift.ToShortDateString().ToString();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += UpdateContext;
        }

        private void UpdateContext(object sender, EventArgs e)
        {
            GridList.ItemsSource = DBConnectHelper.DbObj.OpShifts_OpRecord.OrderByDescending(x => x.OpRececord.DateOccurence).ToList();
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
