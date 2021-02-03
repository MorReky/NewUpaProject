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
using UpaProject.DataFilesApp;

namespace UpaProject.Views.Journals.Histories
{
    /// <summary>
    /// Логика взаимодействия для HistoriesPage.xaml
    /// </summary>
    public partial class HistoriesPage : Page
    {
        public HistoriesPage()
        {
            InitializeComponent();

            HistoryMtrGrid.ItemsSource = DBConnectHelper.DbObj.HistoryMTR.ToList();

            HistoryStoragesGrid.ItemsSource = DBConnectHelper.DbObj.HistoryStorages.ToList();
        }
    }
}
