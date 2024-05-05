using System.ComponentModel.DataAnnotations.Schema;
using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using Fateblade.Components.Data.GenericDataStoring.Contract;

namespace Fateblade.Components.Data.GenericDataStoring.SqLite.Tests;

[Table(nameof(GuidTestDataClass))]
public class GuidTestDataClass : IIdentifiableGuidEntity
{
    public Guid Id { get; set; }
    public string StringValue { get; set; }
    public float FloatValue { get; set; }
    public decimal DecimalValue { get; set; }
    public double DoubleValue { get; set; }
    public char CharValue { get; set; }
    public EnumTestDataClass EnumValue { get; set; }

}

public class GuidTestDataClassPropertyUpdater : IPropertyUpdater<GuidTestDataClass>
{
    public void UpdateProperties(GuidTestDataClass source, GuidTestDataClass target)
    {
        target.EnumValue = source.EnumValue;
        target.CharValue = source.CharValue;
        target.StringValue = source.StringValue;
        target.FloatValue = source.FloatValue;
        target.DoubleValue = source.DoubleValue;
        target.DecimalValue = source.DecimalValue;
    }
}

public enum EnumTestDataClass
{
    Value1,
    Value2,
    Value3
}

[Table(nameof(GuidForeignTestDataClass))]
public class GuidForeignTestDataClass : IIdentifiableGuidEntity
{
    public Guid Id { get; set; }
    public GuidTestDataClass ForeignValue { get; set; }
}

[Table(nameof(GuidCrossTestDataClass))]
public class GuidCrossTestDataClass : IIdentifiableGuidEntity
{
    public Guid Id { get; set; }
    public List<GuidTestDataClass> CrossValue { get; set; }
}