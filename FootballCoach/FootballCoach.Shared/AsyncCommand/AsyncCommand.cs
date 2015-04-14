using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Isah.Core.AsyncCommand
{
    [ExcludeFromCodeCoverage]
    public sealed class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<CancellationToken, Task> _command;
        private readonly CancelAsyncCommand _cancelCommand;
        private readonly Func<bool> _canExecute;
        private NotifyTaskCompletion _execution;

        public AsyncCommand(Func<Task> command)
            : this(_ => command(), () => true) { }

        public AsyncCommand(Func<Task> command, Func<bool> canExecute)
            : this(_ => command(), canExecute) { }

        public AsyncCommand(Func<CancellationToken, Task> command)
            : this(command, () => true) { }

        public AsyncCommand(Func<CancellationToken, Task> command, Func<bool> canExecute)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
            _canExecute = canExecute;
        }

        public NotifyTaskCompletion Execution
        {
            get { return _execution; }
            private set
            {
                _execution = value;
                OnPropertyChanged();
            }
        }
        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }

        public override bool CanExecute(object parameter)
        {
            return (Execution == null || Execution.IsCompleted) && _canExecute();
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion(_command(_cancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            if (!Execution.IsSuccessfullyCompleted)
            {
                var capturedException = ExceptionDispatchInfo.Capture(Execution.InnerException);
                capturedException.Throw();
            }
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private sealed class CancelAsyncCommand : ICommand
        {
            private CancellationTokenSource _cts = new CancellationTokenSource();
            private bool _commandExecuting;

            public CancellationToken Token
            {
                get { return _cts.Token; }
            }

            public void NotifyCommandStarting()
            {
                _commandExecuting = true;
                if (!_cts.IsCancellationRequested)
                    return;
                _cts = new CancellationTokenSource();
                RaiseCanExecuteChanged();
            }

            public void NotifyCommandFinished()
            {
                _commandExecuting = false;
                RaiseCanExecuteChanged();
            }

            bool ICommand.CanExecute(object parameter)
            {
                return _commandExecuting && !_cts.IsCancellationRequested;
            }

            void ICommand.Execute(object parameter)
            {
                _cts.Cancel();
                RaiseCanExecuteChanged();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            private void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public sealed class AsyncCommand<TParameter> : AsyncCommandBase<TParameter>, IAsyncCommand
    {
        private readonly Func<TParameter, CancellationToken, Task> _command;
        private readonly CancelAsyncCommand _cancelCommand;
        private readonly Func<TParameter, bool> _canExecute;
        private NotifyTaskCompletion _execution;

        public AsyncCommand(Func<Task> command)
            : this((parameter, cancellation) => command(), _ => true) { }

        public AsyncCommand(Func<Task> command, Func<TParameter, bool> canExecute)
            : this((parameter, cancellation) => command(), canExecute) { }

        public AsyncCommand(Func<TParameter, Task> command)
            : this((parameter, cancellation) => command(parameter), _ => true) { }

        public AsyncCommand(Func<TParameter, Task> command, Func<TParameter, bool> canExecute)
            : this((parameter, cancellation) => command(parameter), canExecute) { }

        public AsyncCommand(Func<TParameter, CancellationToken, Task> command)
            : this(command, _ => true) { }

        public AsyncCommand(Func<TParameter, CancellationToken, Task> command, Func<TParameter, bool> canExecute)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
            _canExecute = canExecute;
        }

        public NotifyTaskCompletion Execution
        {
            get { return _execution; }
            private set
            {
                _execution = value;
                OnPropertyChanged();
            }
        }
        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }

        public override bool CanExecute(object parameter)
        {
            return (Execution == null || Execution.IsCompleted) && _canExecute(parameter is TParameter ? (TParameter)parameter : default(TParameter));
        }

        public async Task ExecuteAsync(object parameter)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion(_command(parameter is TParameter ? (TParameter)parameter : default(TParameter), _cancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            if (!Execution.IsSuccessfullyCompleted)
            {
                //log here !?
                if (Execution.Exception.InnerExceptions.Count == 1)
                {
                    var capturedException = ExceptionDispatchInfo.Capture(Execution.InnerException);
                    capturedException.Throw();
                }
                {
                    var capturedException = ExceptionDispatchInfo.Capture(Execution.Exception);
                    capturedException.Throw();
                }
            }
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public override async Task ExecuteAsync(TParameter parameter)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion(_command(parameter, _cancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            if (!Execution.IsSuccessfullyCompleted)
            {
                //log here !?
                if (Execution.Exception.InnerExceptions.Count == 1)
                {
                    var capturedException = ExceptionDispatchInfo.Capture(Execution.InnerException);
                    capturedException.Throw();
                }
                {
                    var capturedException = ExceptionDispatchInfo.Capture(Execution.Exception);
                    capturedException.Throw();
                }
            }
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private sealed class CancelAsyncCommand : ICommand
        {
            private CancellationTokenSource _cts = new CancellationTokenSource();
            private bool _commandExecuting;

            public CancellationToken Token
            {
                get { return _cts.Token; }
            }

            public void NotifyCommandStarting()
            {
                _commandExecuting = true;
                if (!_cts.IsCancellationRequested)
                    return;
                _cts = new CancellationTokenSource();
                RaiseCanExecuteChanged();
            }

            public void NotifyCommandFinished()
            {
                _commandExecuting = false;
                RaiseCanExecuteChanged();
            }

            bool ICommand.CanExecute(object parameter)
            {
                return _commandExecuting && !_cts.IsCancellationRequested;
            }

            void ICommand.Execute(object parameter)
            {
                _cts.Cancel();
                RaiseCanExecuteChanged();
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            private void RaiseCanExecuteChanged()
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}