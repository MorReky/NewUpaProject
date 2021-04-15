﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UpaProject.DataFilesApp;
using UpaProject.FrameApp;
using UpaProject.Infrastracture.Commands;
using UpaProject.Models.DataFilesApp;
using UpaProject.ViewModels.Base;
using UpaProject.Views.Catalogs.TagsCatalog;

namespace UpaProject.ViewModels
{
    class TagsCatalogPageViewModel:ViewModel
    {
        #region Свойства
        public IEnumerable<Place_MTR> TableSource
        {
            get => DBConnectHelper.DbObj.Place_MTR.ToList(); set => DBConnectHelper.DbObj.SaveChanges();
        }
        #endregion
            #region Комманды
            #region Обновление данных
        public ICommand ResourceUpdate { get; set; }
        public void OnResourceUpdateExecuted(object p) => TableSource = TableSource;
        public bool CanOnResourceUpdateExecute(object p) => true;
        #endregion
        #region Добавление новой позиции
        public ICommand NewPosition { get; set; }
        public void OnNewPositionExecuted(object p)
        {
            FrameLoader.frmObj.Navigate(new NewPositionPage());
        }
        public bool CanOnNewPositionExecute(object p) => true;
        #endregion
        #endregion
        public TagsCatalogPageViewModel()
        {
            #region Комманды
            ResourceUpdate = new LambdaCommand(OnResourceUpdateExecuted, CanOnResourceUpdateExecute);
            NewPosition = new LambdaCommand(OnNewPositionExecuted, CanOnNewPositionExecute);
            #endregion
        }
    }
}
