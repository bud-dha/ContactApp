using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ContactApp.Infrastructure.Comands.Base
{
    internal abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Событие.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Возвращает возможность возможность выполнения команды.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true-возможно, false-невозможно</returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Логика команды.
        /// </summary>
        public abstract void Execute(object parameter);
    }
}
