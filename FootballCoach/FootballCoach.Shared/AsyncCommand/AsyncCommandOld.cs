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
    public sealed class AsyncCommand<TResult> : AsyncCommandBase
    {
        private readonly Func<CancellationToken, Task<TResult>> _command;
        private readonly CancelAsyncCommand _cancelCommand;
        private NotifyTaskCompletion<TResult> _execution;
        private readonly Func<bool> _canExecute;

        public AsyncCommand(Func<CancellationToken, Task<TResult>> command)
            : this(command, () => true)
        {

        }

        public AsyncCommand(Func<CancellationToken, Task<TResult>> command, Func<bool> canExecute)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
            _canExecute = canExecute;
        }

        public NotifyTaskCompletion<TResult> Execution
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
            Execution = new NotifyTaskCompletion<TResult>(_command(_cancelCommand.Token));
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

    [ExcludeFromCodeCoverage]
    public static class AsyncCommand
    {
        public static AsyncCommand<TResult> Create<TResult>(Func<Task<TResult>> command)
        {
            return new AsyncCommand<TResult>(_ => command());
        }
        public static AsyncCommand<TResult> Create<TResult>(Func<Task<TResult>> command, Func<bool> canExceute)
        {
            return new AsyncCommand<TResult>(_ => command(), canExceute);
        }
        public static AsyncCommand<TResult> Create<TResult>(Func<CancellationToken, Task<TResult>> command)
        {
            return new AsyncCommand<TResult>(command);
        }
        public static AsyncCommand<TResult> Create<TResult>(Func<CancellationToken, Task<TResult>> command, Func<bool> canExceute)
        {
            return new AsyncCommand<TResult>(command, canExceute);
        }
    }
}