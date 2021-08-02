using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;

namespace Fateblade.Components.Data.GenericDataStoring.Contract
{
    public interface IPropertyUpdater<TEntity> where TEntity : IIdentifiableGuidEntity
    {
        void UpdateProperties(TEntity source, TEntity target);
    }
}
