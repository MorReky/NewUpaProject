using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.Infrastracture.ClassHelper;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;
using UpaProject.Views;

namespace UpaProject.ViewModels
{
   internal class AutorizationPageViewModel:ViewModel
    {
        #region Свойства
        private string _Login = Properties.Settings.Default.LastLogin;
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }
        private bool _RememberMe;
        public bool RememberMe
        {
            get => _RememberMe;
            set => Set(ref _RememberMe, value);
        }
        #endregion
        #region Комманды 
        public ICommand LogInCommand { get; }
        private bool CanLogInCommandExecute(object p) => true;
        private void OnLogInCommandExecuted(object p)
        {
            //нарушение концепции MVVM
            var passB = p as PasswordBox;
            var password = passB.Password;
            var User = DBConnectHelper.DbObj.Users.FirstOrDefault(x=>x.Login==Login&&x.Password==password);
            try
            {
                if (User != null)
                {
                    ClassUserHelper.ID = User.IDUser;
                    ClassUserHelper.Name = User.Login;
                    ClassUserHelper.Role = User.Role;
                    HistoryLoginUsers HistoryLoginUsersObj = new HistoryLoginUsers
                    {
                        IdUser = ClassUserHelper.ID,
                        DateLogin = DateTime.Now,
                    };
                    DBConnectHelper.DbObj.HistoryLoginUsers.Add(HistoryLoginUsersObj);
                    DBConnectHelper.DbObj.SaveChanges();

                    if (RememberMe)
                    {
                        Properties.Settings.Default.LastLogin = Login;
                        Properties.Settings.Default.Save();
                    }
                    FrameApp.FrameLoader.frmObj.Navigate(new StartPage());
                }
                else
                    MessageBox.Show("Не удалось авторизироваться.\nПроверьте правильность введенных данных", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Исключение:" + ex, "Сбой", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion
        public AutorizationPageViewModel()
        {
            #region комманды
            LogInCommand = new LambdaCommand(OnLogInCommandExecuted, CanLogInCommandExecute);
            #endregion
        }
    }
}
