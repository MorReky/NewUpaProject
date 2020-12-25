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
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using UpaProject.Catalogs.EqCatalog;
using UpaProject.Models.DataFilesApp;

namespace UpaProject.Catalogs
{
    /// <summary>
    /// Логика взаимодействия для EqiupmentCatalog.xaml
    /// </summary>
    public partial class PageEqiupmentCatalog : Page
    {
        public PageEqiupmentCatalog()
        {
            InitializeComponent();

            CmbLoader();

            GridList.ItemsSource = DBConnectHelper.DbObj.EqList.ToList();
            GridList.SelectedIndex = 0;

            TypeSet.DisplayMemberPath = "EqTypeName";
            TypeSet.ItemsSource = DBConnectHelper.DbObj.EqType.GroupBy(x => x.EqTypeName).ToList();
        }

        private void CmbLoader()
        {           
            CmbGlobalId.DisplayMemberPath = "GlobalId";
             CmbGlobalId.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.GlobalId).ToList();

             CmbNameAbbreviatedSearch.DisplayMemberPath = "NameAbbreviated";
             CmbNameAbbreviatedSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.NameAbbreviated).ToList();

             CmbTypeSearch.SelectedValuePath = "IdEqType";
             CmbTypeSearch.DisplayMemberPath = "EqTypeName";
             CmbTypeSearch.ItemsSource = DBConnectHelper.DbObj.EqType.GroupBy(x => x.EqTypeName).ToList();

             CmbBaseUnitSearch.DisplayMemberPath = "BaseUnit";
             CmbBaseUnitSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.BaseUnit).ToList();

             CmbNameAsuMtrSearch.DisplayMemberPath = "NameASU_MTR";
             CmbNameAsuMtrSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.NameASU_MTR).ToList();

             CmbBrandAndSizeSearch.DisplayMemberPath = "BrandAndSize";
             CmbBrandAndSizeSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.BrandAndSize).ToList();

             CmbCatalogNumberSearch.DisplayMemberPath = "CatalogNumberAndGost";
             CmbCatalogNumberSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.CatalogNumberAndGost).ToList();

             CmbMaterialGradeSearch.DisplayMemberPath = "MaterialGrade";
             CmbMaterialGradeSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.MaterialGrade).ToList();

             CmbDrawingNumberSearch.DisplayMemberPath = "DrawingNumber";
             CmbDrawingNumberSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.DrawingNumber).ToList();

             CmbTechCharacterSearch.DisplayMemberPath = "TechnicalSpecifications";
             CmbTechCharacterSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.TechnicalSpecifications).ToList();

             CmbEquipmentSearch.DisplayMemberPath = "Equipment";
             CmbEquipmentSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.Equipment).ToList();

             CmbTypeMTRNameSearch.DisplayMemberPath = "TypeMTRName";
             CmbTypeMTRNameSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.TypeMTRName).ToList();

             CmbManufacturerGlobalIentifierSearch.DisplayMemberPath = "ManufacturerGlobalIentifier";
             CmbManufacturerGlobalIentifierSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.ManufacturerGlobalIentifier).ToList();

             CmbManufacterSearch.DisplayMemberPath = "ManufacturerName";
             CmbManufacterSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.ManufacturerName).ToList();

             CmbMTPClassClassCodeSearch.DisplayMemberPath = "MTPClassClassCode";
             CmbMTPClassClassCodeSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.MTPClassClassCode).ToList();

             CmbMTPClassClassNameSearch.DisplayMemberPath = "MTPClassClassName";
             CmbMTPClassClassNameSearch.ItemsSource = DBConnectHelper.DbObj.EqList.GroupBy(x => x.MTPClassClassName).ToList();
            
    }

        /// <summary>
        /// Логика обработки фильтров поиска для GridList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Источник таблицы
            var source = DBConnectHelper.DbObj.EqList.ToList();
            //Сделать отдельный метод на досуге
            if (!String.IsNullOrEmpty(CmbGlobalId.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.GlobalId == CmbGlobalId.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbNameAbbreviatedSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.NameAbbreviated == CmbNameAbbreviatedSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(TxbNameFullSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.FullName.Contains(TxbNameFullSearch.Text))).ToList();
            if (!String.IsNullOrEmpty(CmbTypeSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.EqType.EqTypeName == CmbTypeSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbBaseUnitSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.BaseUnit == CmbBaseUnitSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbNameAsuMtrSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.NameASU_MTR == CmbNameAsuMtrSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbBrandAndSizeSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.BrandAndSize == CmbBrandAndSizeSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbCatalogNumberSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.CatalogNumberAndGost == CmbCatalogNumberSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbMaterialGradeSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.MaterialGrade == CmbMaterialGradeSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbDrawingNumberSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.DrawingNumber == CmbDrawingNumberSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbTechCharacterSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.TechnicalSpecifications == CmbTechCharacterSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbEquipmentSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.Equipment == CmbEquipmentSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbTypeMTRNameSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.TypeMTRName == CmbTypeMTRNameSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbManufacturerGlobalIentifierSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.ManufacturerGlobalIentifier == CmbManufacturerGlobalIentifierSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbManufacterSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.ManufacturerName == CmbManufacterSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbMTPClassClassCodeSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.MTPClassClassCode == CmbMTPClassClassCodeSearch.Text)).ToList();
            if (!String.IsNullOrEmpty(CmbMTPClassClassNameSearch.Text))
                source = source.Intersect(DBConnectHelper.DbObj.EqList.Where(x => x.MTPClassClassName == CmbMTPClassClassNameSearch.Text)).ToList();

            GridList.ItemsSource = source;
        }

        

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GridList.ItemsSource = DBConnectHelper.DbObj.EqList.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(GlobalIdSet.Text))
                {
                    var mbResult = MessageBox.Show("Вы уверены, что хотите удалить выделенную строку?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (mbResult == MessageBoxResult.Yes)
                    {
                        for (int i=0;i< GridList.SelectedItems.Count;i++)
                        {
                            EqList EqListobj = GridList.SelectedItems[i] as EqList;
                            DBConnectHelper.DbObj.EqList.Remove(EqListobj);
                        }
                        DBConnectHelper.DbObj.SaveChanges();
                        MessageBox.Show("Строка успешно удалена", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        BtnUpdate_Click(null, null);
                    }
                }
                else
                    MessageBox.Show("Для данного действия необходимо выбрать строку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                   "Критическая работа с приложением " + ex.Message.ToString(),
                   "Уведомление",
                   MessageBoxButton.OK,
                   MessageBoxImage.Warning
                   );
            }
        }

        private void BtnSetCondition_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            PageAddNewEq addNewEq = new PageAddNewEq();
            addNewEq.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            PageAddNewEq addNewEq = new PageAddNewEq(GridList.SelectedValue as EqList);
            addNewEq.Show();
        }

        private void CmbNameAbbreviatedSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CmbNameAbbreviatedSearch.Text = CmbNameAbbreviatedSearch.Text.Trim();
        }
    }
}
