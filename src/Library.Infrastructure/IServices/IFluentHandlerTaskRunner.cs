using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.IServices
{
    public interface IFluentHandlerTaskRunner : IService
    {
        IFluentHandlerTask Run(Func<Task> run);
    }
}
