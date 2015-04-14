using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Isah.Core.AsyncCommand
{
    [ExcludeFromCodeCoverage]
    internal static class AsyncCommandFactory
    {
        public static AsyncCommand Create(Func<Task> command)
        {
            return new AsyncCommand(command);
        }
        public static AsyncCommand Create(Func<Task> command, Func<bool> canExecute)
        {
            return new AsyncCommand(command, canExecute);
        }
        public static AsyncCommand Create(Func<CancellationToken, Task> command)
        {
            return new AsyncCommand(command);
        }
        public static AsyncCommand Create(Func<CancellationToken, Task> command, Func<bool> canExecute)
        {
            return new AsyncCommand(command, canExecute);
        }
    }

    [ExcludeFromCodeCoverage]
    internal static class AsyncCommandFactory<TParameter>
    {
        public static AsyncCommand<TParameter> Create(Func<Task> command)
        {
            return new AsyncCommand<TParameter>(command);
        }
        public static AsyncCommand<TParameter> Create(Func<Task> command, Func<TParameter, bool> canExecute)
        {
            return new AsyncCommand<TParameter>(command, canExecute);
        }
        public static AsyncCommand<TParameter> Create(Func<TParameter, Task> command)
        {
            return new AsyncCommand<TParameter>(command);
        }
        public static AsyncCommand<TParameter> Create(Func<TParameter, Task> command, Func<TParameter, bool> canExecute)
        {
            return new AsyncCommand<TParameter>(command, canExecute);
        }
        public static AsyncCommand<TParameter> Create(Func<TParameter, CancellationToken, Task> command)
        {
            return new AsyncCommand<TParameter>(command);
        }
        public static AsyncCommand<TParameter> Create(Func<TParameter, CancellationToken, Task> command, Func<TParameter, bool> canExecute)
        {
            return new AsyncCommand<TParameter>(command, canExecute);
        }
    }
}