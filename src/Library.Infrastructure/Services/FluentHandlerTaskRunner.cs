using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class FluentHandlerTaskRunner : IFluentHandlerTaskRunner
    {
        private readonly IFluentHandler _fluentHandler;
        private readonly Func<Task> _validate;
        private readonly ISet<IFluentHandlerTask> _fluentHandlerTasks = new HashSet<IFluentHandlerTask>();

        public FluentHandlerTaskRunner(IFluentHandler fluentHandler, 
            Func<Task> validate, ISet<IFluentHandlerTask> fluentHandlerTasks)
        {
            _fluentHandler = fluentHandler;
            _fluentHandlerTasks = fluentHandlerTasks;
            _validate = validate;
        }

        public IFluentHandlerTask Run(Func<Task> run)
        {
            var handlerTask = new FluentHandlerTask(_fluentHandler, run, _validate);
            _fluentHandlerTasks.Add(handlerTask);

            return handlerTask;
        }
    }
}
