
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;
using UpaProject.Views.Catalogs.EqCatalog;

namespace UpaProject.ViewModels
{
    internal class PersonsCatalogPageViewModel : ViewModel
    {
        #region Свойства
        #region TableSource
        public IEnumerable<Persons> TableSource
        {
            get => DBConnectHelper.DbObj.Persons.ToList();
            set
            {
                DBConnectHelper.DbObj.SaveChanges();
                OnPropertyChanged("TableSource");
            }
        }
        #endregion        
        #region SelectedRow
        private Persons _SelectedRow;
        public Persons SelectedRow
        {
            get => _SelectedRow;
            set
            {
                if (!Equals(_SelectedRow, value))
                {
                    _SelectedRow = value;
                    DBConnectHelper.DbObj.SaveChanges();
                    OnPropertyChanged("SelectedRow");
                }
                else
                    _SelectedRow = value;
            }
        }
        #endregion
        public IEnumerable<StatusValues> StatusValue { get => DBConnectHelper.DbObj.StatusValues.ToList(); }
        public IEnumerable<WorkGroupValues> WorkGroupValue { get => DBConnectHelper.DbObj.WorkGroupValues.ToList(); }
        #endregion

        #region Комманды
        #region Открытие окна Добавления нового сотрудника
        public ICommand AddPerson { get; }
        public void OnAddPersonExecuted(object p) => FrameLoader.frmObj.Navigate(new PageNewCatalogPage());
        public bool CanOnAddPersonExecute(object p) => true;
        #endregion
        #region Удаление позиции
        public ICommand DeleteSelectedRow { get; }

        public void OnDeleteSelectedRowExecuted(object p)
        {
            MessageBoxResult result = MessageBox.Show($"Сотрудник {SelectedRow.Name} Будет удален из списка", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    DBConnectHelper.DbObj.Persons.Remove(SelectedRow as Persons);
                    TableSource = TableSource;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла непредвиденная ошибка: " + ex);
                }
            }
        }
        public bool CanOnDeleteSelectedRowExecute(object p)
        {
            if (SelectedRow != null)
                return true;
            return false;
        }
        #endregion
        #region Обновление данных
        public ICommand ResourceUpdate { get; set; }
        public void OnResourceUpdateExecuted(object p) => TableSource = TableSource;
        public bool CanOnResourceUpdateExecute(object p) => true;
        #endregion

        #endregion
        public PersonsCatalogPageViewModel()
        {
            #region Комманды
            AddPerson = new LambdaCommand(OnAddPersonExecuted, CanOnAddPersonExecute);
            DeleteSelectedRow = new LambdaCommand(OnDeleteSelectedRowExecuted, CanOnDeleteSelectedRowExecute);
            ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
            #endregion

            // TableSource = DBConnectHelper.DbObj.Persons.ToList();
        }
    }
}
