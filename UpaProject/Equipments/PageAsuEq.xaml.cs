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

namespace UpaProject.Equipments
{
    /// <summary>
    /// Логика взаимодействия для PageAsuEq.xaml
    /// </summary>
    public partial class PageAsuEq : Page
    {
        public PageAsuEq()
        {
            InitializeComponent();
            GridList.ItemsSource = DBConnectHelper.DbObj.ItemsOfEq.ToList();
            GridList.SelectedIndex = 0;
            
            CmbDepartment.DisplayMemberPath = "DepartmentName";
            CmbDepartment.ItemsSource = DBConnectHelper.DbObj.DepartmentEq.ToList();            
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            GridList.ItemsSource = DBConnectHelper.DbObj.ItemsOfEq.Where(x => x.DepartmentEq.DepartmentName == CmbDepartment.Text).ToList();

        }
    }
}
