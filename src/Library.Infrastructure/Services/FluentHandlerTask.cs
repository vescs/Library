using Library.Core.Models;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class FluentHandlerTask : IFluentHandlerTask
    {
        private readonly IFluentHandler _fluentHandler;
        private readonly Func<Task> _run;
        private Func<Task> _validate;
        private Func<Task> _always;
        private Func<Task> _onSuccess;
        private Func<LibraryException, Task> _onCustomError;
        private Func<Exception, Task> _onError;
        private bool _propagateException = true;
        private bool _executeOnError = true;

        public FluentHandlerTask(IFluentHandler fluentHandler, Func<Task> run, Func<Task> validate = null)
        {
            _fluentHandler = fluentHandler;
            _run = run;
            _validate = validate;
        }

        public IFluentHandlerTask Always(Func<Task> always)
        {
            _always = always;
            
            return this;
        }

        public IFluentHandlerTask DoNotPropagateException()
        {
            _propagateException = false;
            return this;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                if(_validate != null)
                {
                    await _validate();
                }
                await _run();
                if(_onSuccess != null)
                {
                    await _onSuccess();
                }
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(ex);
                if (_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                if(_always != null)
                {
                    await _always();
                }
            }
        }

        public IFluentHandler Next()
        {
            return _fluentHandler;
        }

        public IFluentHandlerTask OnCustomError(Func<LibraryException, Task> onCustomError, bool propagateException = false, bool executeOnError = false)
        {
            _onCustomError = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IFluentHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false, bool executeOnError = false)
        {
            _onError = onError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IFluentHandlerTask OnSuccess(Func<Task> onSuccess)
        {
            _onSuccess = onSuccess;

            return this;
        }

        public IFluentHandlerTask PropagateException()
        {
            _propagateException = true;

            return this;
        }

        private async Task HandleExceptionAsync(Exception ex)
        {
            var customEx = ex as LibraryException;
            if(customEx != null)
            {
                if(_onCustomError != null)
                {
                    await _onCustomError(customEx);
                }
            }
            var executeOnError = _executeOnError || customEx == null;
            if (!executeOnError)
            {
                return;
            }
            if (_onError != null)
            {
                await _onError(ex);
            }
        }
    }
}
