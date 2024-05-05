﻿using Fateblade.Components.CrossCutting.Base.Identifiable.DataClasses;
using System;
using System.Collections.Generic;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses
{
    public class DateTimeUnit : IIdentifiableGuidEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public char FormatKey { get; set; } 
        public List<DateTimeUnitRelation> AllRelations { get; set; }
        public List<DateTimeUnitRelation> LeavingRelations { get; set; }
        public List<DateTimeUnitRelation> IncomingRelations { get; set; }
    }
}