using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IFluentHandler : IService
    {
        IFluentHandlerTask Run(Func<Task> run);
        IFluentHandlerTaskRunner Validate(Func<Task> validate);
        Task ExecuteAllAsync();
    }
}
