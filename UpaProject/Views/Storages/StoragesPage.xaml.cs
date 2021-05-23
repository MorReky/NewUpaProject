using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Infrastracture.ClassHelper;
using UpaProject.Models.DataFilesApp;
using Excel = Microsoft.Office.Interop.Excel;

namespace UpaProject.Views.Storages
{
    /// <summary>
    /// Логика взаимодействия для StoragesPage.xaml
    /// </summary>
    public partial class StoragesPage : System.Windows.Controls.Page
    {
        private Storage_MTR storage_MTR = new Storage_MTR();
        private IEnumerable Source = DBConnectHelper.DbObj.Storage_MTR.ToList();
        private static IEnumerable StoragesCollection;


        public StoragesPage()
        {
            InitializeComponent();

            if (ClassUserHelper.Role == 1)
            {
                MTRGrid.IsReadOnly = false;
            }

            StoragesCollection = DBConnectHelper.DbObj.Storages.ToList();

            MTRGrid.ItemsSource = Source;
            CmbGrid.ItemsSource = StoragesCollection;

            CmbStorage.ItemsSource = StoragesCollection;
            CmbStorage.SelectedValue = 10;

            CmbId.ItemsSource = DBConnectHelper.DbObj.MTR.OrderBy(x => x.IdSap).ToList();
            CmbId.SelectedIndex = -1;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += FrameLoad;
            timer.Start();
        }

        private void CmbStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32(CmbStorage.SelectedValue) == 10)
                Source = DBConnectHelper.DbObj.Storage_MTR.ToList();
            else
                // MTRGrid.ItemsSource = (MTRGrid.ItemsSource as IEnumerable<Storage_MTR>).Where(x=>x.Storage==CmbStorage.SelectedValue.ToString()).ToList();
                //null System.NotSupportedException: "Не удалось создать константу с типом "System.Object" и значением NULL. В этом контексте поддерживаются только типы сущностей, типы перечисления и типы-примитивы
                Source = DBConnectHelper.DbObj.Storage_MTR.Where(x => x.IdStorage.ToString() == CmbStorage.SelectedValue.ToString()).ToList();
            MTRGrid.ItemsSource = Source;
        }

        private void FrameLoad(object sender, object e)
        {
            MTRGrid.ItemsSource = MTRGrid.ItemsSource;
        }

        private void CmbId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbId.SelectedValue != null)
                MTRGrid.ItemsSource = (Source as IEnumerable<Storage_MTR>).Where(x => x.MTR.IdSap.ToString() == CmbId.SelectedValue.ToString()).ToList();
            else
                MTRGrid.ItemsSource = Source;
        }

        private void TxbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            MTRGrid.ItemsSource = (Source as IEnumerable<Storage_MTR>).Where(x => x.MTR.Name.ToString().Contains(TxbName.Text));
        }

        private void TxbComments_TextChanged(object sender, TextChangedEventArgs e)
        {
            MTRGrid.ItemsSource = (MTRGrid.ItemsSource as IEnumerable<Storage_MTR>).Where(x => x.Comment.ToString().Contains(TxbName.Text));
        }

        private void MTRGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            int IdStorage_mtr;
            if (((sender as DataGrid).CurrentItem as Storage_MTR) != null)
            {
                IdStorage_mtr = ((sender as DataGrid).CurrentItem as Storage_MTR).IDStorage_MTR;
                HistoryStorages historyObj = new HistoryStorages()
                {
                    IdStorage_MTR = ((sender as DataGrid).CurrentItem as Storage_MTR).IDStorage_MTR,
                    IdUser = ClassUserHelper.ID,
                    DateEdit = DateTime.Now,
                    Activity = "Редактирование записи"
                };
                DBConnectHelper.DbObj.HistoryStorages.Add(historyObj);
            }
            DBConnectHelper.DbObj.SaveChanges();
        }

        private void BtnToExcel_Click(object sender, RoutedEventArgs e)
        {
            //Creating a new workbook (Создание новой книги)
            var wb = new XLWorkbook();
            //Adding a woksheet (добавление рабочего листа)
            var ws = wb.Worksheets.Add("МТР");

            MessageBox.Show("Загрузка данных началась. Пожалуйста, подождите", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            //try
            //{
            string data = "";
            ws.Cell(1, 1).Value = "#п/п";
            ws.Cell(1, 2).Value = "Гид";
            ws.Cell(1, 3).Value = "Наименование";
            ws.Cell(1, 4).Value = "Ед.Изм.";
            ws.Cell(1, 5).Value = "Итого";
            ws.Cell(1, 6).Value = "Контейнер 1";
            ws.Cell(1, 7).Value = "Контейнер 2";
            ws.Cell(1, 8).Value = "Контейнер 3";
            ws.Cell(1, 9).Value = "Контейнер 4";
            ws.Cell(1, 10).Value = "Пом. 185";
            ws.Cell(1, 11).Value = "Пом. 303";
            ws.Cell(1, 12).Value = "Пом. 328";
            ws.Cell(1, 13).Value = "Открытый склад";
            ws.Cell(1, 14).Value = "ХАЛ";
            ws.Cell(1, 15).Value = "Безвозмездная поставка";
            ws.Cell(1, 16).Value = "PLC119";
            ws.Cell(1, 17).Value = "Оперативный";
            ws.Cell(1, 18).Value = "Несортированное";
            ws.Cell(1, 19).Value = "Дата последнего обновления";


            IEnumerable<Storage_MTR> records = DBConnectHelper.DbObj.Storage_MTR;
            IEnumerable<MTR> mtrs = DBConnectHelper.DbObj.MTR;
            IEnumerable<HistoryStorages> historyStorages = DBConnectHelper.DbObj.HistoryStorages;

            //ws.Cell(4, 2).Value = mtrs.SelectMany(x => x.Name);


            for (int i = 0; i < records.Count(); i++)
            {
                data = records.ElementAt(i).MTR.IDMTR.ToString();
                ws.Cell(i + 2, 1).Value = i;
                var mtr = mtrs.FirstOrDefault(x => x.IDMTR.ToString() == data);
                ws.Cell(i + 2, 2).Value = mtr.IdSap;
                ws.Cell(i + 2, 3).Value = mtr.Name;
                ws.Cell(i + 2, 4).Value = mtr.Unit;

                ws.Cell(i + 2, 5).Value = records.Where(x => x.IdMTR.ToString() == data).Sum(x => x.Quantity);
                var record = records.Where(x => x.IdMTR.ToString() == data);
                ws.Cell(i + 2, 6).Value = record.FirstOrDefault(x => x.IdStorage == 1)?.Quantity;
                ws.Cell(i + 2, 7).Value = record.FirstOrDefault(x => x.IdStorage == 3)?.Quantity ;
                ws.Cell(i + 2, 8).Value = record.FirstOrDefault(x => x.IdStorage == 4)?.Quantity ;
                ws.Cell(i + 2, 9).Value = record.FirstOrDefault(x => x.IdStorage == 7)?.Quantity ;
                ws.Cell(i + 2, 10).Value = record.FirstOrDefault(x => x.IdStorage == 2)?.Quantity;
                ws.Cell(i + 2, 11).Value = record.FirstOrDefault(x => x.IdStorage == 5)?.Quantity;
                ws.Cell(i + 2, 12).Value = record.FirstOrDefault(x => x.IdStorage == 6)?.Quantity;
                ws.Cell(i + 2, 13).Value = record.FirstOrDefault(x => x.IdStorage == 11)?.Quantity;
                ws.Cell(i + 2, 14).Value = record.FirstOrDefault(x => x.IdStorage == 12)?.Quantity;
                ws.Cell(i + 2, 15).Value = record.FirstOrDefault(x => x.IdStorage == 8)?.Quantity ;
                ws.Cell(i + 2, 16).Value = record.FirstOrDefault(x => x.IdStorage == 13)?.Quantity;
                ws.Cell(i + 2, 17).Value = record.FirstOrDefault(x => x.IdStorage == 15)?.Quantity;
                ws.Cell(i + 2, 18).Value = record.FirstOrDefault(x => x.IdStorage == 9)?.Quantity;

                ws.Cell(i + 2, 19).Value = historyStorages.FirstOrDefault(x => x.Storage_MTR.MTR.IDMTR.ToString() == data)?.DateEdit.ToString();
            }
            //ws.Range((ws.Cell(2,19)),(ws.Cell(2000,19))).DataType = XLDataType.DateTime;
            MessageBox.Show("Готово! StorageTable.xlsx находится на вашем рабочем столе");
            //ws.Columns().AdjustToContents();
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            wb.SaveAs($@"C:\Users\{wi.Name.Substring(4)}\Desktop\StorageTable.xlsx");
            //Marshal.ReleaseComObject(ws);
            //Marshal.ReleaseComObject(wb);

        }

        private void BtnNewPosition_Click(object sender, RoutedEventArgs e)
        {
            FrameLoader.frmObj.Navigate(new NewMTRPage());
        }
    }
}
