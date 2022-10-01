using System.Collections.Generic;

public interface IUnitState
{
    Dictionary<ushort, int> Health { get; }
}
