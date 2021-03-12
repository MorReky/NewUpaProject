
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
    internal class PersonsCatalogPageViewModel:ViewModel
    {
        #region Свойства
        public ObservableCollection<Persons> TableSource
        {
            get => new ObservableCollection<Persons>(DBConnectHelper.DbObj.Persons);
            set
            {
                OnPropertyChanged("Update");
                DBConnectHelper.DbObj.SaveChanges();
            }
        }
       // public ObservableCollection<WorkGroupValues> WorkGroups { get => new ObservableCollection<WorkGroupValues>(DBConnectHelper.DbObj.WorkGroupValues.ToList()); set => DBConnectHelper.DbObj.SaveChanges(); }

        private object _SelectedRow;
        public object SelectedRow
        { 
            get => _SelectedRow;
            set => Set(ref _SelectedRow, value);
        }        
        #endregion

        #region Комманды
        #region Открытие окна Добавления нового сотрудника
        public ICommand AddPerson { get; }
        public void OnAddPersonExecuted(object p)=> FrameLoader.frmObj.Navigate(new PageNewCatalogPage());
        public bool CanOnAddPersonExecute(object p) => true;
        #endregion
        #region Удаление позиции
        public ICommand DeletePosition { get; }

        public void OnDeletePositionExecuted(object p)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить выбранного сотрудника?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result==MessageBoxResult.Yes)
            {
                try
                {
                    DBConnectHelper.DbObj.Persons.Remove(SelectedRow as Persons);
                    DBConnectHelper.DbObj.SaveChanges();
                    MessageBox.Show("Сотрудник удален!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Возникла непредвиденная ошибка: " + ex);
                }
            }
        }
        public bool CanOnDeletePositionExecute(object p)
        {
            if (SelectedRow != null)
                return true;
            return false;
        }
        #endregion

        #endregion
        public PersonsCatalogPageViewModel()
        {
            #region Комманды
            AddPerson = new LambdaCommand(OnAddPersonExecuted, CanOnAddPersonExecute);
            DeletePosition = new LambdaCommand(OnDeletePositionExecuted, CanOnDeletePositionExecute);
            #endregion
            
           // TableSource = DBConnectHelper.DbObj.Persons.ToList();
        }
    }
}
