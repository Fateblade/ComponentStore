using System;

namespace Fateblade.Components.Data.GenericDataStoring.Contract.Messages
{
    public class EntityChangedMessage
    {
        public Type Type { get; set; }
        public ChangeType ChangeType { get; set; }
        public object Entity { get; set; }
    }

    public class EntityChangedMessage<TEntity> : EntityChangedMessage
    {
        public new Type Type { get; } = typeof(TEntity);

        public new TEntity Entity
        {
            get => (TEntity) base.Entity;
            set => base.Entity = value;
        }
    }
}
