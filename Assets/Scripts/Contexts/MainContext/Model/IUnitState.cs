using System.Collections.Generic;

namespace Contexts.MainContext
{
    public interface IUnitState
    {
        Dictionary<ushort, int> Health { get; }
    }
}
