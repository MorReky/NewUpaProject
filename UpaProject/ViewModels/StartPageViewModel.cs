using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
    public class StartPageViewModel : ViewModel
    {
        #region Свойства
        #region Выбранная вахта
        public int DeafaultChange
        {
            get => Properties.Settings.Default.DeafaultChange;
            set
            {
                if (value <= 2 && Properties.Settings.Default.DeafaultChange != value)
                {
                    Properties.Settings.Default.DeafaultChange = value;
                    Properties.Settings.Default.Save();
                }
            }
        }
        #endregion

        #endregion
        #region Комманды
        #endregion
        public StartPageViewModel()
        {
            #region Комманды
            #endregion
        }
    }
}
