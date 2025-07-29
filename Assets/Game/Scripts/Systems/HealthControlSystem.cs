using UnityEngine;
using Leopotam.Ecs;

public class HealthControlSystem: Injects, IEcsRunSystem
{
    private EcsFilter<HealthComponent> _healthComponentFilter;
    
    public void Run()
    {
        foreach(int i in _healthComponentFilter)
        {
            var healthEntity = _healthComponentFilter.GetEntity(i);
            if (healthEntity.Get<HealthComponent>().HealthValue <= 0 && !healthEntity.Has<DeadFlag>())
            {
                UnityEngine.Object.Destroy(healthEntity.Get<GameObjectRef>().GameObject);
                healthEntity.Get<DeadFlag>();
            }
        }
    }
}
