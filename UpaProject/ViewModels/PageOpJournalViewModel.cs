using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using UpaProject.DataFilesApp;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;
using System.Windows.Input;
using System.Windows.Threading;
using UpaProject.Infrastracture.Commands;
using UpaProject.Journals;

namespace UpaProject.ViewModels
{
    internal class PageOpJournalViewModel : ViewModel
    {
        DispatcherTimer timer;
        #region Свойства
        private IEnumerable<OpShifts_OpRecord> _DataTable;
        public IEnumerable<OpShifts_OpRecord> DataTable
        {
            get => _DataTable;
            set => Set(ref _DataTable, value);
        }
        private string _LastOpenTurn;
        public string LastOpenTurn
        {
            get => _LastOpenTurn;
            set => Set(ref _LastOpenTurn, value);
        }


        #endregion
        #region Комманды
        #region Обновление данных на странице
        public ICommand ResourceUpdate { get; set; }
        public void OnResourceUpdateExecuted(object p)
        {
            DataTable = DBConnectHelper.DbObj.OpShifts_OpRecord.OrderByDescending(x => x.OpRececord.DateOccurence).ToList();
            LastOpenTurn= DBConnectHelper.DbObj.OpShifts.OrderByDescending(x => x.DateStartShift).First().DateStartShift.ToShortDateString().ToString();
        }
        public bool CanOnResourceUpdateExecute(object p) => true;
        #endregion
        #region Объявление нового события
        public ICommand NewSubject { get; }
        public void OnNewSubjectExecuted(object p)
        {
            WindowNewSubj windowNewSubj = new WindowNewSubj();
            windowNewSubj.ShowDialog();
        }
        #endregion
        #region Объявление новой смены
        public ICommand NewTurn { get; }
        public void OnNewTurnExecuted(object p)
        {
            WindowNewOpTurn newOpTurn = new WindowNewOpTurn();
            newOpTurn.ShowDialog();
        }
        public bool CanOnNewTurnExecute(object p) => true;
        #endregion
        public bool CanOnNewSubjectExecute(object p) => true;

        #endregion
        public PageOpJournalViewModel()
        {
            #region Комманды
            ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
            NewTurn = new LambdaCommand(OnNewTurnExecuted, CanOnNewTurnExecute);
            NewSubject = new LambdaCommand(OnNewSubjectExecuted, CanOnNewSubjectExecute);
            #endregion
            // StartTimer();
            LoadData();

        }
        private void LoadData()
        {
            DataTable = DBConnectHelper.DbObj.OpShifts_OpRecord.OrderByDescending(x => x.OpRececord.DateOccurence).ToList();
            LastOpenTurn = DBConnectHelper.DbObj.OpShifts.OrderByDescending(x => x.DateStartShift).First().DateStartShift.ToShortDateString().ToString();
        }
        private void StartTimer()
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromSeconds(10);
            //timer.Tick += new EventHandler(Tick);        
            timer.Tick += (object sender, EventArgs e) => ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
            timer.Start();
        }
        //private void Tick(object sender, EventArgs e)
        //{
        //    //Здесь пишем что должен делать таймер
        //    ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
        //}
    }
}
