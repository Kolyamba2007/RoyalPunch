using System.Linq;

namespace Contexts.MainContext
{
    public class UnitService : IUnitService
    {
        [Inject] public IUnitState UnitState { get; set; }

        public void AddUnit(UnitView view, IUnitData data)
        {
            ushort id = GetID();

            view.SetID(id);
            UnitState.Health.Add(id, data.MaxHealth);
        }

        public void Remove(UnitView view)
        {
            if (UnitState.Health.TryGetValue(view.ID, out int _))
                UnitState.Health.Remove(view.ID);
        }

        public void SetDamage(UnitView view, int damage)
        {
            if (UnitState.Health.TryGetValue(view.ID, out int health))
                health = UnitState.Health[view.ID] -= damage;

            if (health < 0) UnitState.Health[view.ID] = 0;
        }

        public void SetDamage(UnitView view, int damage, out int remainingHealth)
        {
            if (UnitState.Health.TryGetValue(view.ID, out int health))
                health = UnitState.Health[view.ID] -= damage;

            remainingHealth = health > 0 ? health : 0;
        }

        private ushort GetID()
        {
            var id = UnitState.Health.Keys;

            if (id.Count != 0)
                return (ushort) (id.Max() + 1);
            else
                return 0;
        }
    }
}
