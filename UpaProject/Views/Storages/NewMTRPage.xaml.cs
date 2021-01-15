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
            if (!(String.IsNullOrEmpty(TxbIdSap.Text) || String.IsNullOrEmpty(TxbName.Text) || String.IsNullOrEmpty(TxbBaseEI.Text) || String.IsNullOrEmpty(CmbStorage.SelectedValue.ToString()) || String.IsNullOrEmpty(TxbComment.Text) || String.IsNullOrEmpty(TxbQuantity.Text)))
            {
                try
                {
                    var obj = DBConnectHelper.DbObj.Storage_MTR.FirstOrDefault(x => x.MTR.IdSap.ToString() == TxbIdSap.Text && x.IdStorage == CmbStorage.SelectedValue.ToString());
                    //Если совпадений на запись МТР в этом  контейнере нет,то...
                    if (obj == null)
                    {
                        int IdObj;
                        //Если указанного МТР нет,то добавляет записи в соответствующие таблицы. Иначе находит ID МТР
                        if (!DBConnectHelper.DbObj.MTR.Select(x => x.IdSap).Contains(Convert.ToInt32(TxbIdSap.Text)))
                        {
                            MTR mtrObj = new MTR()
                            {
                                IdSap = Convert.ToInt32(TxbIdSap.Text),
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
                        }
                        else
                            IdObj = DBConnectHelper.DbObj.MTR.FirstOrDefault(x => x.IdSap.ToString() == TxbIdSap.Text).IDMTR;
                        //Создаем записи в таблице учета и в таблице истории внесения МТР
                        Storage_MTR storage_MTR = new Storage_MTR()
                        {
                            IdMTR = IdObj,
                            IdStorage = CmbStorage.SelectedValue.ToString(),
                            Quantity = Convert.ToInt32(TxbQuantity.Text),
                            Comment = TxbComment.Text,
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
                    }
                    //...если запись находится,то обновляем данные и делаем запись в журнале
                    else
                    {
                        obj.Quantity = Convert.ToInt32(TxbQuantity.Text);
                        obj.Comment = TxbComment.Text;
                        HistoryStorages historyStorages = new HistoryStorages()
                        {
                            IdUser = ClassUserHelper.ID,
                            IdStorage_MTR = obj.IDStorage_MTR,
                            DateEdit = DateTime.Now,
                            Activity = "Внесение изменений позиции материала"
                        };
                        DBConnectHelper.DbObj.HistoryStorages.Add(historyStorages);
                        DBConnectHelper.DbObj.SaveChanges();
                    }
                    MessageBox.Show("Запись успешно добавлена или изменена");
                    FrameLoader.frmObj.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сбой программы. Пожалуйста,обратитесь к администратору:" + ex, "Сбой", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
