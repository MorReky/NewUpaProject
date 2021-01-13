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
using UpaProject.Models.DataFilesApp;

namespace UpaProject.Views.Storages
{
    /// <summary>
    /// Логика взаимодействия для NewMTRPage.xaml
    /// </summary>
    public partial class NewMTRPage : Page
    {
        public NewMTRPage()
        {
            InitializeComponent();

            CmbStorage.ItemsSource = DBConnectHelper.DbObj.Storages.ToList();
            CmbStorage.Text = "Неопределено";
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(TxbQuantity.Text) || String.IsNullOrEmpty(TxbName.Text) || String.IsNullOrEmpty(TxbBaseEI.Text) || String.IsNullOrEmpty(CmbStorage.SelectedValue.ToString()) || String.IsNullOrEmpty(TxbComment.Text) || String.IsNullOrEmpty(TxbQuantity.Text)))
            {
                int IdObj;
                if (!DBConnectHelper.DbObj.MTR.Select(x => x.IdSap).Contains(Convert.ToInt32(TxbQuantity.Text)))
                {
                    MTR mtrObj = new MTR()
                    {
                        IdSap = Convert.ToInt32(TxbQuantity.Text),
                        Name = TxbName.Text,
                        Unit = TxbBaseEI.Text
                    };
                    DBConnectHelper.DbObj.MTR.Add(mtrObj);
                    IdObj = mtrObj.IDMTR;
                    HistoryMTR historyObj = new HistoryMTR()
                    {
                        IdMTR = IdObj,
                        IdUser = ClassUserHelper.ID,
                        DateEdit = DateTime.Now,
                        Activity = "Добавление нового МТР"
                    };
                    DBConnectHelper.DbObj.HistoryMTR.Add(historyObj);
                    // DBConnectHelper.DbObj.SaveChanges();
                }
                else
                    IdObj = DBConnectHelper.DbObj.MTR.FirstOrDefault(x => x.IdSap == Convert.ToInt32(TxbQuantity.Text)).IDMTR;
                Storage_MTR storage_MTR = new Storage_MTR()
                {
                    IdMTR = IdObj,
                    IdStorage = CmbStorage.SelectedValue.ToString(),
                    Comment = TxbComment.Text,
                    Quantity = Convert.ToInt32(TxbQuantity.Text)
                };
                DBConnectHelper.DbObj.Storage_MTR.Add(storage_MTR);
                HistoryStorages historyStorages = new HistoryStorages()
                {
                    IdUser = ClassUserHelper.ID,
                    IdStorage_MTR = storage_MTR.IDStorage_MTR,
                    DateEdit = DateTime.Now,
                    Activity = "Добавление новой позиции для материала"
                };
                DBConnectHelper.DbObj.HistoryStorages.Add(historyStorages);
                DBConnectHelper.DbObj.SaveChanges();
                MessageBox.Show("Запись добавлена!");
                FrameLoader.frmObj.GoBack();
            }
            else
                MessageBox.Show("Все поля Обязательны для заполнения");
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.GoBack();
        }
    }
}
