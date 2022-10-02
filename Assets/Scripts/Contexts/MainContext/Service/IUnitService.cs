namespace Contexts.MainContext
{
    public interface IUnitService
    {
        void AddUnit(UnitView view, IUnitData data);

        void Remove(UnitView view);

        void SetDamage(UnitView view, int damage);

        void SetDamage(UnitView view, int damage, out int remainingHealth);
    }
}
