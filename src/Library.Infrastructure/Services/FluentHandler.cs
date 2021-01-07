using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class FluentHandler : IFluentHandler
    {
        private readonly ISet<IFluentHandlerTask> _fluentHandlerTasks = new HashSet<IFluentHandlerTask>();
        public async Task ExecuteAllAsync()
        {
            foreach (var t in _fluentHandlerTasks)
            {
                await t.ExecuteAsync();
            }
            _fluentHandlerTasks.Clear();
        }

        public IFluentHandlerTask Run(Func<Task> run)
        {
            var handlerTask = new FluentHandlerTask(this, run);
            _fluentHandlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IFluentHandlerTaskRunner Validate(Func<Task> validate)
            => new FluentHandlerTaskRunner(this, validate, _fluentHandlerTasks);
    }
}
