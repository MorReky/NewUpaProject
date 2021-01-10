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

namespace UpaProject.Views.LogIN
{
    /// <summary>
    /// Логика взаимодействия для AutorizationFrame.xaml
    /// </summary>
    public partial class AutorizationFrame : Page
    {
        public AutorizationFrame()
        {
            InitializeComponent();
        }

        private void BtnAutoriz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var User = DBConnectHelper.DbObj.Users.FirstOrDefault(x => x.Login == TxbLogin.Text && x.Password == PsbPass.Password);
                if (User != null)
                {
                    ClassUserHelper.ID = User.IDUser;
                    ClassUserHelper.Name = User.Name;
                    ClassUserHelper.Role = User.Role;
                   
                    FrameLoader.frmObj.Navigate(new StartPage());
                }
                else
                    MessageBox.Show("Не удалось авторизироваться.\nПроверьте правильность введенных данных", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Исключение:" + ex, "Сбой", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
