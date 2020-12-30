using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Models.DataFilesApp;

namespace UpaProject.Catalogs
{
    /// <summary>
    /// Логика взаимодействия для AddNewEq.xaml
    /// </summary>
    public partial class PageAddNewEq : Window
    {
       
        public PageAddNewEq()
        {
            InitializeComponent();

        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBManufacturerGlobalIentifierSet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TBGlobalIdSet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void GlobalIdSet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

    }
}
