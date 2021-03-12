using System;
using System.Collections.Generic;
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

namespace UpaProject.ViewModels
{
    internal class PageNewCatalogPersonsViewModel : ViewModel
    {

        #region Свойства
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                Set(ref _Name, value);
            }
        }

        private string _Role;
        public string Role
        {
            get => _Role;
            set => Set(ref _Role, value);
        }

        private int _WorkGroup;
        public int WorkGroup
        {
            get => _WorkGroup;
            set => Set(ref _WorkGroup, value);
        }


        #endregion

        #region Комманды
        #region Добавить нового сторудника в бд
        public ICommand AddPersons { get; }
        public void OnAddPersonsExecuted(object p)
        {
            try
            {
                Persons personsObj = new Persons
                {
                    Name = Name,
                    Role = Role,
                    IdWorkGroup = WorkGroup
                };
                DBConnectHelper.DbObj.Persons.Add(personsObj);
                DBConnectHelper.DbObj.SaveChanges();
                MessageBox.Show($"{Name} ,{Role} добавлен");
                FrameLoader.frmObj.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Возникла непредвиденная ошибка: " + ex,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        
        public bool CanOnAddPersonsExecute(object p)
        {
            if (Name!= ""&&Role!="")
                return true;
            return false;
        }
        #endregion

        #region Вернуться назад
        public ICommand Back { get; }

        public void OnBackExecuted(object p)
        {
            FrameLoader.frmObj.GoBack();
        }

        public bool CanOnBackExecute(object p) => true;
        #endregion
        #endregion
        public PageNewCatalogPersonsViewModel()
        {
            #region Комманды
            AddPersons = new LambdaCommand(OnAddPersonsExecuted, CanOnAddPersonsExecute);
            Back = new LambdaCommand(OnBackExecuted, CanOnBackExecute);
            #endregion

        }
        
    }
}
