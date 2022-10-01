using System.Collections.Generic;

public class UnitState : IUnitState
{
    public Dictionary<ushort, int> Health { get; } = new Dictionary<ushort, int>();
}
