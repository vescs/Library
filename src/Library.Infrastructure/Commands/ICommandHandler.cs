using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T: ICommand
    {
        Task HandleAsync(T command);
    }
}
