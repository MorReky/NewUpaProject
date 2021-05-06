using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;

namespace UpaProject.ViewModels
{
    internal class WindowNewSubjViewModel : ViewModel
    {

        #region Свойства
        public readonly OpShifts LastShift;
        StringBuilder error = new StringBuilder();
        #region Источник участков
        private ObservableCollection<Departments> _DepartmentsList;
        public ObservableCollection<Departments> DepartmentsList { get => _DepartmentsList; set => Set(ref _DepartmentsList, value); }
        #endregion
        #region Участок события
        private Departments _SelectedDepartment;
        public Departments SelectedDepartment { get => _SelectedDepartment; set => Set(ref _SelectedDepartment, value); }
        #endregion
        #region Система события
        private SystemsAsu _SelectedSystemsAsu;
        public SystemsAsu SelectedSystemsAsu { get => _SelectedSystemsAsu; set => Set(ref _SelectedSystemsAsu, value); }
        #endregion
        #region Тег события
        private Place _SelectedTag;
        public Place SelectedTag { get => _SelectedTag; set => Set(ref _SelectedTag, value); }
        #endregion
        #region Дежурные по смене
        private ObservableCollection<Persons> _PersonsList;
        public ObservableCollection<Persons> PersonsList { get => _PersonsList; set => Set(ref _PersonsList, value); }
        #endregion
        #region Выделенный исполнитель
        private Persons _SelectedPerson;
        public Persons SelectedPerson { get => _SelectedPerson; set => Set(ref _SelectedPerson, value); }
        #endregion
        #region Выбранные исполнители
        private ObservableCollection<Persons> _SelectedPersonsList = new ObservableCollection<Persons>();
        public ObservableCollection<Persons> SelectedPersonsList { get => _SelectedPersonsList; set => Set(ref _SelectedPersonsList, value); }
        #endregion
        #region Время начала события
        private TimeSpan _TimeStart;
        public TimeSpan TimeStart
        {
            get => _TimeStart;
            set
            {
                if (value.TotalDays <= 1)
                    Set(ref _TimeStart, value);
                DownTime = TimeEnd.Subtract(TimeStart);

            }
        }
        #endregion
        #region Время окнончания события
        private TimeSpan _TimeEnd;
        public TimeSpan TimeEnd
        {
            get => _TimeEnd;
            set
            {
                if (value.TotalDays <= 1)
                    Set(ref _TimeEnd, value);
                DownTime = TimeEnd.Subtract(TimeStart);


            }
        }
        #endregion
        #region Время простоя из-за события
        private TimeSpan _DownTime;
        public TimeSpan DownTime
        {
            get => _DownTime;
            set
            {
                if (value.TotalDays <= 1)
                    Set(ref _DownTime, value);
            }
        }
        #endregion
        #region Критичность события
        private int _Criticality;
        public int Criticality
        {
            get => _Criticality;
            set => Set(ref _Criticality, value);
        }
        #endregion
        #region Описание события
        private string _Event;
        public string Event
        {
            get => _Event;
            set => Set(ref _Event, value);
        }
        #endregion
        #region Причина события
        private string _Cause;
        public string Cause
        {
            get => _Cause;
            set => Set(ref _Cause, value);
        }
        #endregion
        #region Решение события
        private string _Elimination;
        public string Elimination
        {
            get => _Elimination;
            set => Set(ref _Elimination, value);
        }
        #endregion
        #region Решение события
        private string _Comment;
        public string Comment
        {
            get => _Comment;
            set => Set(ref _Comment, value);
        }
        #endregion
        #endregion
        #region Комманды
        #region Комманда выбора исполнителей
        public ICommand ChagedRealizer { get; }
        public void OnChagedRealizersExecuted(object p)
        {
            if (p is null) return;
            if (Equals(p, "+"))
                SelectedPersonsList.Add(SelectedPerson);
            if (Equals(p, "-"))
                SelectedPersonsList.Remove(SelectedPerson);
        }
        public bool CanOnCnagedRealizersExecute(object p) => _SelectedPersonsList.Count >= 0 || _SelectedPersonsList.Count <= PersonsList.Count;
        #endregion
        #region Комманда создания события
        public ICommand AddNewSubj { get; }
        public void OnAddNewSubjExecuted(object p)
        {
            //Создали запись в таблице записей
            OpRececord opRececordObj = new OpRececord
            {
                DateOccurence = LastShift.DateStartShift,
                TimeOccurence = TimeStart,
                TimeSolution = TimeEnd,
                DownTime = DownTime,
                IdDepartment = SelectedDepartment.IDDepartment,
                Criticality = Criticality,
                IdPlace = SelectedTag.IDPlace,
                Occurence = Event,
                Account = Cause,
                Solution = Elimination,
                Comments = Comment
            };
            DBConnectHelper.DbObj.OpRececord.Add(opRececordObj);
            //Создали запись в сводной таблице запись-смена
            OpShifts_OpRecord opShifts_opRecordObj = new OpShifts_OpRecord()
            {
                IdOpRecord = opRececordObj.IDOpRecord,
                IdShift = LastShift.IDShift
            };
            DBConnectHelper.DbObj.OpShifts_OpRecord.Add(opShifts_opRecordObj);
            //Добавляем исполнителей работу в сводную таблицу запись-исполнители
            foreach (object selected in SelectedPersonsList)
            {
                Realizers realizers = new Realizers()
                {
                    IdOpRecord = opRececordObj.IDOpRecord,
                    IdPerson = (selected as Persons).IDPerson
                };
                DBConnectHelper.DbObj.Realizers.Add(realizers);
            }
            DBConnectHelper.DbObj.SaveChanges();
            MessageBox.Show("Данные успешно внесены", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public bool CanOnAddNewSubjExecute(object p)
        {
            if (TimeStart == null || TimeEnd == null)
                return false;
            if (SelectedDepartment == null)
                return false;
            if (SelectedTag == null)
                return false;
            if (SelectedPersonsList.Count == 0)
                return false;
            if (Event == null || Event.Length == 0)
                return false;
            return true;
        }
        #endregion
        #endregion
        public WindowNewSubjViewModel()
        {
            #region Комманды
            ChagedRealizer = new LambdaCommand(OnChagedRealizersExecuted, CanOnCnagedRealizersExecute);
            AddNewSubj = new LambdaCommand(OnAddNewSubjExecuted, CanOnAddNewSubjExecute);
            #endregion
            LastShift = DBConnectHelper.DbObj.OpShifts.OrderByDescending(x => x.DateStartShift).First();
            DepartmentsList = new ObservableCollection<Departments>(DBConnectHelper.DbObj.Departments);
            PersonsList = new ObservableCollection<Persons>(DBConnectHelper.DbObj.Shifts_Persons.Where(y => y.IdShift == LastShift.IDShift).Select(x => x.Persons));
            Criticality = 1;
        }

    }
}
