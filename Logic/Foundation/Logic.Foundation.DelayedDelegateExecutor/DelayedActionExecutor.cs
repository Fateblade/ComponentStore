using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Fateblade.Components.CrossCutting.ExceptionFormatter.Contract;
using Fateblade.Components.CrossCutting.Logging.Contract;
using Fateblade.Components.CrossCutting.Logging.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.DelayedActionExecution.Contract;

namespace Fateblade.Components.Logic.Foundation.DelayedActionExecution
{
    class DelayedActionExecutor : IDelayedActionExecutor
    {
        private readonly struct ActionQueueItem
        {
            public Action ActionToExecute { get; }
            public DateTime EarliestExecutionTime { get; }

            public ActionQueueItem(Action actionToExecute, DateTime earliestExecutionTime)
            {
                ActionToExecute = actionToExecute;
                EarliestExecutionTime = earliestExecutionTime;
            }
        }

        private readonly Timer _timer;
        private readonly DelayedActionExecutionConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IExceptionMessageFormatter _exceptionFormatter;
        private readonly ConcurrentQueue<ActionQueueItem> _actionQueue;
        private bool _running;

        public DelayedActionExecutor(DelayedActionExecutionConfiguration configuration, ILogger logger, IExceptionMessageFormatter exceptionFormatter)
        {
            _configuration = configuration;
            _logger = logger;
            _exceptionFormatter = exceptionFormatter;
            _timer = new Timer(executeQueuedActions, this, Timeout.Infinite, Timeout.Infinite);
            _actionQueue = new ConcurrentQueue<ActionQueueItem>();
        }

        public void DelayExecution(Action actionToExecute, int millisecondsToDelayAtLeast)
        {
            _actionQueue.Enqueue(new ActionQueueItem(actionToExecute, DateTime.Now.AddMilliseconds(-millisecondsToDelayAtLeast)));
            
            if (!_running)
            {
                _running = true;
                _timer.Change(_configuration.ExecutionCheckDelay, _configuration.ExecutionCheckDelay);
            }
        }

        private void processActionQueue()
        {
            List<ActionQueueItem> itemsToRequeue = new List<ActionQueueItem>();
            while (_actionQueue.TryDequeue(out ActionQueueItem currentItem))
            {
                if (currentItem.EarliestExecutionTime <= DateTime.Now)
                {
                    try
                    {
                        currentItem.ActionToExecute.Invoke();
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(
                            LoggingPriority.High, 
                            LoggingType.Exception, 
                            $"Exception in {nameof(DelayedActionExecutor)}: {Environment.NewLine}{_exceptionFormatter.FormatMessagesAndStackTracesToString(ex)}");
                    }
                }
                else
                {
                    itemsToRequeue.Add(currentItem);
                }
            }

            itemsToRequeue.ForEach(t => _actionQueue.Enqueue((t)));
        }

        private void executeQueuedActions(object args)
        {
            DelayedActionExecutor actionExecutor = (DelayedActionExecutor) args;
            if (actionExecutor == null)
            {
                return;
            }

            actionExecutor._timer.Change(Timeout.Infinite, Timeout.Infinite);

            actionExecutor.processActionQueue();

            if (actionExecutor._actionQueue.IsEmpty)
            {
                actionExecutor._running = false;
            }
            else
            {
                actionExecutor._timer.Change(
                    actionExecutor._configuration.ExecutionCheckDelay,
                    actionExecutor._configuration.ExecutionCheckDelay);
            }
        }
    }
}
