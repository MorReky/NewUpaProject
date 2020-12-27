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
        EqList EqListObj=new EqList();
        public PageAddNewEq(EqList eqList=null)
        {
            InitializeComponent();

            if (eqList != null)
                EqListObj = eqList;

            DataContext = EqListObj;

            CmbTypeSet.ItemsSource = DBConnectHelper.DbObj.EqType.ToList();
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
            StringBuilder StringError = new StringBuilder();

            if (String.IsNullOrEmpty(EqListObj.GlobalId))
                StringError.AppendLine("Заполните поле ГИД ");
            if(EqListObj.EqType==null)
                StringError.AppendLine("Выберите подкласс оборудования");
            if (StringError.Length > 0)
            {
                MessageBox.Show(
               StringError.ToString(),
               "Сбой",
               MessageBoxButton.OK,
               MessageBoxImage.Warning);
                return;
            }
            if (EqListObj.IdEqList == 0)
                DBConnectHelper.DbObj.EqList.Add(EqListObj);
            try
            {
                DBConnectHelper.DbObj.SaveChanges();
                MessageBox.Show(
                   "Данные успешно сохранены",
                   "Уведомление",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                this.Hide();


            }
            catch (Exception ex)
            {
                MessageBox.Show(
                   "Критическая работа с приложением. " + ex.Message.ToString(),
                   "Уведомление",
                   MessageBoxButton.OK,
                   MessageBoxImage.Warning);
            }
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
