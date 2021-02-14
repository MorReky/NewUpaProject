using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using UpaProject.Infrastracture.Commands;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels.HelpersClass
{
    class TimeDate:ViewModel
    {
        public void InitializeTimer(int Time)
        {

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(Time);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
