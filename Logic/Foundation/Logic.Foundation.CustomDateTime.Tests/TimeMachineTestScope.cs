using System;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract;
using Fateblade.Components.Logic.Foundation.CustomDateTime.Contract.DataClasses;

namespace Fateblade.Components.Logic.Foundation.CustomDateTime.Tests;

internal class TimeMachineTestScope : IDisposable
{
    private readonly ITimeMachine _timeMachine;
    private readonly DateTimeStamp _previousTime;


    public TimeMachineTestScope(ITimeMachine timeMachine)
    {
        _timeMachine = timeMachine;
        _previousTime = _timeMachine.CurrentTime;
    }

    public void Dispose()
    {
        _timeMachine.SetTime(_previousTime);
    }
}