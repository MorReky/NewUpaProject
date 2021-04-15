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
using UpaProject.Infrastracture.ClassHelper;
using UpaProject.Journals;
using UpaProject.Models.DataFilesApp;
using UpaProject.Views.Catalogs.EqCatalog;
using UpaProject.Views.Catalogs.MTRCatalog;
using UpaProject.Views.Catalogs.TagsCatalog;
using UpaProject.Views.Journals.Histories;
using UpaProject.Views.LogIN;
using UpaProject.Views.Storages;

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

            FrmMain.Navigate(new AutorizationFrame());

        }

        private void BtnCatalogs_Click(object sender, RoutedEventArgs e)
        {
           VoidContextMenu(sender, "ContextCatalogs");
        }
        

        private void BtnCatalogPers_Click(object sender, RoutedEventArgs e)
        {
           FrameLoader.frmObj.Navigate(new PersonsCatalogPage());
        }

        private void BtnJournal_Click(object sender, RoutedEventArgs e)
        {
            VoidContextMenu(sender, "ContextJournals");
        }

        private void BtnOpJournal_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new PageOpJournal());
        }

        private void BtnCatalogMTR_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new MTRCatalogsPage());
        }

        private void BtnStorage_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new StoragesPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void FrmMain_ContentRendered(object sender, EventArgs e)
        {
            if (ClassUserHelper.Role==1)
            {
                BtnCatalogs.IsEnabled = true;
                BtnJournal.IsEnabled = true;
                BtnStorage.IsEnabled = true;
                //BtnEq.IsEnabled = true;
            }
            if (ClassUserHelper.Role == 2)
            {
                BtnCatalogs.IsEnabled = true;
                BtnStorage.IsEnabled = true;
            }

        }

        private void BtnHistory_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new HistoriesPage());
        }

        private void BtnCatalogTag_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new TagsCatalogPage());
        }

    }
}
