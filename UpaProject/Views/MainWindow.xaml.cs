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
using UpaProject.Catalogs;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Journals;
using UpaProject.Models.DataFilesApp;

namespace UpaProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FrameLoader.frmObj = FrmMain;

            DBConnectHelper.DbObj = new UpaDBEntities();

        }

        private void BtnCatalogs_Click(object sender, RoutedEventArgs e)
        {
           VoidContextMenu(sender, "ContextCatalogs");
        }
        /// <summary>
        /// Логика взаимодействия с контекстным меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Resource"></param>
        private void VoidContextMenu(object sender, string Resource)
        {
            ContextMenu contextMenu = this.FindResource(Resource) as ContextMenu;
            contextMenu.PlacementTarget = sender as Button;
            contextMenu.IsOpen = true;
        }

        private void BtnCatalogEq_Click(object sender, RoutedEventArgs e)
        {
            FrmMain.Navigate(new PageEqiupmentCatalog());
        }

        private void BtnJournal_Click(object sender, RoutedEventArgs e)
        {
            VoidContextMenu(sender, "ContextJournals");
        }

        private void BtnOpJournal_Click(object sender, RoutedEventArgs e)
        {
            FrmMain.Navigate(new PageOpJournal());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnEq_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
