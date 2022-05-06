using Fateblade.Components.Logic.Foundation.Orchestration.Contract;
using Fateblade.Components.Logic.Foundation.Orchestration.Contract.DataClasses;
using Fateblade.Components.Logic.Foundation.Orchestration.Contract.Exceptions;
using System;
using System.Collections.Generic;

namespace Fateblade.Components.Logic.Foundation.Orchestration
{
    class Orchestrator : IOrchestrator
    {
        private readonly Dictionary<Type, object> _handlerDictionary;

        public Orchestrator()
        {
            _handlerDictionary = new Dictionary<Type, object>();
        }


        public void Orchestrate<T>(T orchestrationInfo) where T : OrchestrationBaseInfo
        {
            throwIfOrchestratorDoesNotExist(typeof(T));

            var handler = (IOrchestrationHandler<T>) _handlerDictionary[typeof(T)];
            handler.Handle(orchestrationInfo);
        }

        public void RegisterHandler<T>(IOrchestrationHandler<T> handler) where T : OrchestrationBaseInfo
        {
            if (_handlerDictionary.ContainsKey(typeof(T)))
            {
                throw new OrchestratorAlreadyRegisteredException(
                    $"There is already an orchestration handler registered for type '{typeof(T)}'");
            }

            _handlerDictionary.Add(typeof(T), handler);
        }

        public void UnRegisterHandler<T>(IOrchestrationHandler<T> handler) where T : OrchestrationBaseInfo
        {
            throwIfOrchestratorDoesNotExist(typeof(T));

            _handlerDictionary.Remove(typeof(T));
        }

        private void throwIfOrchestratorDoesNotExist(Type orchestratorType)
        {
            if (!_handlerDictionary.ContainsKey(orchestratorType))
            {
                throw new OrchestratorNotRegisteredException(
                    $"There is no orchestration handler for type '{orchestratorType}");
            }
        }
    }
}
