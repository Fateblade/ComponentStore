using Fateblade.Components.Logic.Foundation.Orchestration.Contract.DataClasses;

namespace Fateblade.Components.Logic.Foundation.Orchestration.Contract
{
    public interface IOrchestrator
    {
        public void Orchestrate<T>(T orchestrationInfo) where T : OrchestrationBaseInfo;
        public void RegisterHandler<T>(IOrchestrationHandler<T> handler) where T : OrchestrationBaseInfo;
        public void UnRegisterHandler<T>(IOrchestrationHandler<T> handler) where T : OrchestrationBaseInfo;
    }
}
