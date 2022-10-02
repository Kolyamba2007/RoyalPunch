using System.Collections.Generic;

namespace Contexts.MainContext
{
    public class UnitState : IUnitState
    {
        public Dictionary<ushort, int> Health { get; } = new Dictionary<ushort, int>();
    }
}
