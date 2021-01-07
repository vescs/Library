using Library.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IFluentHandlerTask : IService
    {
        IFluentHandlerTask Always(Func<Task> always);
        IFluentHandlerTask OnCustomError(Func<LibraryException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IFluentHandlerTask OnError(Func<Exception, Task> onError,
            bool propagateException = false, bool executeOnError = false);
        IFluentHandlerTask PropagateException();
        IFluentHandlerTask DoNotPropagateException();
        IFluentHandler Next();
        IFluentHandlerTask OnSuccess(Func<Task> onSuccess);
        Task ExecuteAsync();
        
    }
}
