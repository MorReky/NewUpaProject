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

            CmbTypeSet.DisplayMemberPath = "EqTypeName";
            CmbTypeSet.ItemsSource = DBConnectHelper.DbObj.EqType.GroupBy(x => x.EqTypeName).ToList();
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
            try
            {
                if (DBConnectHelper.DbObj.EqList.Where(x => x.GlobalId.Contains(GlobalIdSet.Text.Trim())).Count() > 0)
                    throw new Exception("Элемент с индентификатором " + GlobalIdSet.Text + " уже имеется в таблице");
                if (String.IsNullOrEmpty(GlobalIdSet.Text))
                    throw new Exception("Поле ГИД не может быть пустым");
                EqList EqListobj = new EqList()
                {
                    GlobalId = GlobalIdSet.Text.Trim(),
                    NameAbbreviated = NameAbbreviatedSet.Text.Trim(),
                    FullName = NameSet.Text.Trim(),
                    BaseUnit = BaseUnitSet.Text.Trim(),
                    NameASU_MTR = NameAsuMtrSet.Text.Trim(),
                    BrandAndSize = BrandAndSizeSet.Text.Trim(),
                    CatalogNumberAndGost = CatalogNumberSet.Text.Trim(),
                    MaterialGrade = MaterialGradeSet.Text.Trim(),
                    DrawingNumber = DrawingNumberSet.Text.Trim(),
                    TechnicalSpecifications = TechCharacterSet.Text.Trim(),
                    Equipment = EquipmentSet.Text.Trim(),
                    TypeMTRName = TypeMTRNameSet.Text.Trim(),
                    ManufacturerGlobalIentifier = ManufacturerGlobalIentifierSet.Text.Trim(),
                    ManufacturerName = ManufacturerNameSet.Text.Trim(),
                    MTPClassClassCode = MTPClassClassCodeSet.Text.Trim(),
                    MTPClassClassName = MTPClassClassNameSet.Text.Trim(),
                    Comments = CommentsSet.Text.Trim(),
                    OperationOfEquipmentMPP = MPP.IsEnabled,
                    OperationOfEquipmentOF = OF.IsEnabled,
                };
                DBConnectHelper.DbObj.EqList.Add(EqListobj);
                DBConnectHelper.DbObj.SaveChanges();
                MessageBox.Show(
                       "Объект " + EqListobj.NameAbbreviated + " Успешно добавлен",
                       "Уведомление",
                       MessageBoxButton.OK,
                       MessageBoxImage.Information
                       );
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
