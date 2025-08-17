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
            if (entity.Has<ShieldComponent>() && entity.Get<ShieldComponent>().ShieldValue > 0)
            {
                entity.Get<ShieldComponent>().ShieldValue -= damage;
                Debug.Log($"{entity.Get<ShieldComponent>().ShieldValue}");
            }
            else
            {
                entity.Get<HealthComponent>().HealthValue -= damage;
                Debug.Log($"{entity.Get<HealthComponent>().HealthValue}");
            }
        }
    }
}
