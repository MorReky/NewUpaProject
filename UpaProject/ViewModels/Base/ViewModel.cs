using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UpaProject.ViewModels.Base
{
    /// <summary>
    /// Базовый класс для реализации паттерна MVVM
    /// </summary>
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string PropertyName=null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(PropertyName));
        }
        /// <summary>
        /// Сеттер для удобства
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Ссылка на изменяемый параметр</param>
        /// <param name="value">Устанавливаемое значение</param>
        /// <param name="PropertyName">Название объекта вызвавшего метод</param>
        /// <returns></returns>
        public bool Set<T>(ref T field,T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return false;
        }
    }
}
