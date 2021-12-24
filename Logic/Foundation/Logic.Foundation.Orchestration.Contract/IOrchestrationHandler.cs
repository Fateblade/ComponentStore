using Fateblade.Components.Logic.Foundation.Orchestration.Contract.DataClasses;

namespace Fateblade.Components.Logic.Foundation.Orchestration.Contract
{
    public interface IOrchestrationHandler<T> where T : OrchestrationBaseInfo
    {
        void Handle(T orchestrationInfo);
    }
}