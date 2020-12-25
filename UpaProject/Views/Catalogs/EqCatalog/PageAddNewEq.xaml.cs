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

namespace UpaProject.Catalogs
{
    /// <summary>
    /// Логика взаимодействия для AddNewEq.xaml
    /// </summary>
    public partial class PageAddNewEq : Window
    {
        static EqList EqListobj;
        public PageAddNewEq()
        {
            InitializeComponent();

            CmbTypeSet.SelectedValuePath = "IdEqType";
            CmbTypeSet.DisplayMemberPath = "EqTypeName";
            CmbTypeSet.ItemsSource = DBConnectHelper.DbObj.EqType.GroupBy(x => x.EqTypeName).ToList();
        }


        public PageAddNewEq(EqList eqList)
        {
            InitializeComponent();

            CmbTypeSet.SelectedValuePath = "IdEqType";
            CmbTypeSet.DisplayMemberPath = "EqTypeName";
            CmbTypeSet.ItemsSource = DBConnectHelper.DbObj.EqType.GroupBy(x => x.EqTypeName).ToList();

            EqListobj = eqList;
            TxbGlobalIdSet.Text = eqList.GlobalId;
            TxbNameAbbreviatedSet.Text = eqList.NameAbbreviated;
            TxbNameSet.Text = eqList.FullName;
            CmbTypeSet.SelectedValue = eqList.IdEqType;
            TxbUnitSet.Text = eqList.BaseUnit;
            TxbNameAsuMtrSet.Text = eqList.NameASU_MTR;
            TxbBrandAndSizeSet.Text = eqList.BrandAndSize;
            TxbCatalogNumberSet.Text = eqList.CatalogNumberAndGost;
            TxbMaterialGradeSet.Text = eqList.MaterialGrade;
            TxbDrawingNumberSet.Text = eqList.DrawingNumber;
            TxbTechCharacterSet.Text = eqList.TechnicalSpecifications;
            TxbEquipmentSet.Text = eqList.Equipment;
            TxbTypeMTRNameSet.Text = eqList.TypeMTRName;
            TxbManufacturerGlobalIentifierSet.Text = eqList.ManufacturerGlobalIentifier;
            TxbManufacturerNameSet.Text = eqList.ManufacturerName;
            TxbMTPClassClassCodeSet.Text = eqList.MTPClassClassCode;
            TxbMTPClassClassNameSet.Text = eqList.MTPClassClassName;
            TxbCommentsSet.Text = eqList.Comments;
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

            //try
            //{
            if (String.IsNullOrEmpty(TxbGlobalIdSet.Text))
                MessageBox.Show(
               "Поле ГИД не может быть пустым",
               "Уведомление",
               MessageBoxButton.OK,
               MessageBoxImage.Warning);
            else
            {
                EqListobj = new EqList
                {
                    IdEqList = EqListobj.IdEqList,
                    GlobalId = TxbGlobalIdSet.Text,
                    NameAbbreviated = TxbNameAbbreviatedSet.Text,
                    FullName = TxbNameSet.Text,
                    IdEqType = DBConnectHelper.DbObj.EqType.FirstOrDefault(x=>x.EqTypeName==CmbTypeSet.Text).IdEqType,
                    BaseUnit = TxbUnitSet.Text,
                    NameASU_MTR = TxbNameAsuMtrSet.Text,
                    BrandAndSize = TxbBrandAndSizeSet.Text,
                    CatalogNumberAndGost = TxbCatalogNumberSet.Text,
                    MaterialGrade = TxbMaterialGradeSet.Text,
                    DrawingNumber = TxbDrawingNumberSet.Text,
                    TechnicalSpecifications = TxbTechCharacterSet.Text,
                    Equipment = TxbEquipmentSet.Text,
                    TypeMTRName = TxbTypeMTRNameSet.Text,
                    ManufacturerGlobalIentifier = TxbManufacturerGlobalIentifierSet.Text,
                    ManufacturerName = TxbManufacturerNameSet.Text,
                    MTPClassClassCode = TxbMTPClassClassCodeSet.Text,
                    MTPClassClassName = TxbMTPClassClassNameSet.Text,
                    Comments = TxbCommentsSet.Text,
                    OperationOfEquipmentMPP = MPP.IsChecked,
                    OperationOfEquipmentOF = OF.IsChecked
                };                
                DBConnectHelper.DbObj.EqList.Add(EqListobj);
                DBConnectHelper.DbObj.SaveChanges();
            }


            //catch(Exception ex)
            //{
            //    MessageBox.Show(
            //       "Критическая работа с приложением. " + ex.Message.ToString(),
            //       "Уведомление",
            //       MessageBoxButton.OK,
            //       MessageBoxImage.Warning);
            //}


            //*****************************************************************************************//
            //try
            //{
            //    if (String.IsNullOrEmpty(TxbGlobalIdSet.Text))
            //        MessageBox.Show(
            //       "Поле ГИД не может быть пустым",
            //       "Уведомление",
            //       MessageBoxButton.OK,
            //       MessageBoxImage.Warning);
            //    else               
            //    {
            //        if (DBConnectHelper.DbObj.EqList.Where(x => x.GlobalId.Contains(TxbGlobalIdSet.Text.Trim())).Count() > 0)
            //            MessageBox.Show(
            //           "Элемент с индентификатором " + TxbGlobalIdSet.Text + " уже имеется в таблице",
            //           "Уведомление",
            //           MessageBoxButton.OK,
            //           MessageBoxImage.Warning);
            //        else
            //        {
            //            EqListobj = new EqList()
            //            {
            //                GlobalId = TxbGlobalIdSet.Text.Trim(),
            //                NameAbbreviated = TxbNameAbbreviatedSet.Text.Trim(),
            //                FullName = TxbNameSet.Text.Trim(),
            //                BaseUnit = TxbUnitSet.Text.Trim(),
            //                NameASU_MTR = TxbNameAsuMtrSet.Text.Trim(),
            //                BrandAndSize = TxbBrandAndSizeSet.Text.Trim(),
            //                CatalogNumberAndGost = TxbCatalogNumberSet.Text.Trim(),
            //                MaterialGrade = TxbMaterialGradeSet.Text.Trim(),
            //                DrawingNumber = TxbDrawingNumberSet.Text.Trim(),
            //                TechnicalSpecifications = TxbTechCharacterSet.Text.Trim(),
            //                Equipment = TxbEquipmentSet.Text.Trim(),
            //                TypeMTRName = TxbTypeMTRNameSet.Text.Trim(),
            //                ManufacturerGlobalIentifier = TxbManufacturerGlobalIentifierSet.Text.Trim(),
            //                ManufacturerName = TxbManufacturerNameSet.Text.Trim(),
            //                MTPClassClassCode = TxbMTPClassClassCodeSet.Text.Trim(),
            //                MTPClassClassName = TxbMTPClassClassNameSet.Text.Trim(),
            //                Comments = TxbCommentsSet.Text.Trim(),
            //                OperationOfEquipmentMPP = MPP.IsEnabled,
            //                OperationOfEquipmentOF = OF.IsEnabled,
            //            };
            //            DBConnectHelper.DbObj.EqList.Add(EqListobj);
            //        }
            //        DBConnectHelper.DbObj.SaveChanges();
            //    }
            //    MessageBox.Show(
            //           "Объект " + EqListobj.NameAbbreviated + " Успешно добавлен",
            //           "Уведомление",
            //           MessageBoxButton.OK,
            //           MessageBoxImage.Information
            //           );
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(
            //        "Критическая работа с приложением. " + ex.Message.ToString(),
            //        "Уведомление",
            //        MessageBoxButton.OK,
            //        MessageBoxImage.Warning);
            //}


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
