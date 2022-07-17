using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactApp.ViewModel.Base
{    
    /// <summary>
    /// Шаблон класса ViewModel.
    /// </summary>
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие изменеия свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Гененрирует событие. 
        /// </summary>
        /// <param name="PropertyName">Имя свойства.</param>
        protected virtual void OnPropertyChanged([CallerMemberName]string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        /// <summary>
        /// Обновляет значение свойства.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Ссылка на поле свойства.</param>
        /// <param name="value">Значение которое хотим установить.</param>
        /// <param name="PropertyName">Имя свойства.</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
