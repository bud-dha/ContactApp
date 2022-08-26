using System.Windows;
using ContactApp.Model;
using System.Windows.Media;
using System.Windows.Input;
using ContactApp.ViewModel.Base;
using ContactApp.Infrastructure.Comands;

namespace ContactApp.ViewModel
{
    class ContactWindowViewModel : ViewModelBase
    {
        #region Свойства

        #endregion

        #region Команды

        #region Команда принятия изменений

        public ICommand AceptChangesCommand { get; }

        private void OnAceptChangesCommandExecuted(object p)
        {
            Window win = p as Window;
            win.Close();
        }

        private bool CanAceptChangesCommandExecuted(object p) => true;

        #endregion

        #endregion

        public ContactWindowViewModel()
        {
            AceptChangesCommand = new LambdaCommand(OnAceptChangesCommandExecuted, CanAceptChangesCommandExecuted);
        }
    }
}
