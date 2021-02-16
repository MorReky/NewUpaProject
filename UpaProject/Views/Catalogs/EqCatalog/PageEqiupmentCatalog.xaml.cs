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
using UpaProject.FrameApp;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using UpaProject.Models.DataFilesApp;
using System.Windows.Threading;

namespace UpaProject.Catalogs
{
    /// <summary>
    /// Логика взаимодействия для EqiupmentCatalog.xaml
    /// </summary>
    public partial class PageEqiupmentCatalog : Page
    {
        public PageEqiupmentCatalog()
        {
            InitializeComponent();

            CmbLoader();
          

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += UpdateContext;
            GridList.ItemsSource = DBConnectHelper.DbObj.MTR.ToList();
        }

        private void UpdateContext(object sender,EventArgs e)
        {
            GridList.ItemsSource = DBConnectHelper.DbObj.MTR.ToList();           
        }

        private void CmbLoader()
        {           
           
            
    }

        /// <summary>
        /// Логика обработки фильтров поиска для GridList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            
        }



        private void BtnUpdate_Click(object sender, RoutedEventArgs e) => UpdateContext(sender,e);

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnSetCondition_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            PageAddNewEq addNewEq = new PageAddNewEq();
            addNewEq.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            PageAddNewEq addNewEq = new PageAddNewEq();
            addNewEq.Show();
        }

        private void CmbNameAbbreviatedSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbNameAbbreviatedSearch.Text = CmbNameAbbreviatedSearch.Text.Trim();
        }
    }
}
