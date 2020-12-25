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
using System.Windows.Shapes;
using UpaProject.DataFilesApp;
using UpaProject.Models.DataFilesApp;

namespace UpaProject.Catalogs.EqCatalog
{
    /// <summary>
    /// Логика взаимодействия для EditEq.xaml
    /// </summary>
    public partial class PageEditEq : Window
    {
        public PageEditEq(EqList eqList)
        {
            InitializeComponent();

            CmbTypeSet.SelectedValuePath = "IdEqType";
            CmbTypeSet.DisplayMemberPath = "EqTypeName";
            CmbTypeSet.ItemsSource = DBConnectHelper.DbObj.EqType.GroupBy(x => x.EqTypeName).ToList();

            GlobalIdSet.Text = eqList.GlobalId;
            NameAbbreviatedSet.Text=eqList.NameAbbreviated;
            NameSet.Text = eqList.FullName;
            CmbTypeSet.SelectedValue=eqList.IdEqType;
            TxbBaseUnitSet.Text = eqList.BaseUnit;
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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GlobalIdSet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
