using UnityEngine;
using Leopotam.Ecs;

public class DamageSystem: Injects, IEcsRunSystem
{
    private EcsFilter<DamageEvent> _damageEventFilter;

    public void Run()
    {
        foreach(int i in _damageEventFilter)
        {
            float damage = _damageEventFilter.Get1(i).DamageValue;
            var entity = _damageEventFilter.Get1(i).Entity;
            if (entity.Has<DeadFlag>()) return;
            if (entity.Has<FreezeFlag>()) return;
            if (entity.Has<ResistanceFlag>())return;

            if (entity.Has<ShieldComponent>() && entity.Get<ShieldComponent>().ShieldValue > 0)
            {
                entity.Get<ShieldComponent>().ShieldValue -= damage;
            }
            else
            {
                entity.Get<HealthComponent>().HealthValue -= damage;
            }
        }
    }
}
