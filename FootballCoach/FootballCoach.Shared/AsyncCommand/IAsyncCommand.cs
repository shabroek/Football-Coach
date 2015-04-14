using System.Threading.Tasks;
using System.Windows.Input;

namespace Isah.Core.AsyncCommand
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }

    public interface IAsyncCommand<in T> : ICommand
    {
        Task ExecuteAsync(T parameter);
    }
}
