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
using UpaProject.Models.DataFilesApp;

namespace UpaProject.Journals
{
    /// <summary>
    /// Логика взаимодействия для WindowNewSubj.xaml
    /// </summary>
    public partial class WindowNewSubj : Window
    {

        public WindowNewSubj()
        {
            InitializeComponent();
        }
        private void TxbTimeStart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890:".IndexOf(e.Text)<0;
        }


    }
}
