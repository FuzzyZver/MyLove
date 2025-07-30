using UnityEngine;
using Leopotam.Ecs;

public class CloseAttackSystem: Injects, IEcsRunSystem
{
    private EcsFilter<CloseAttackInputEvent> _closeAttackInputEventFilter;
    private EcsFilter<CloseEyeRaycastEvent> _closeEyeRaycastEventFilter;
    private float _playerDamage;
    
    public void Run()
    {
        _playerDamage = GameConfig.PlayerConfig.Damage;

        foreach (int i in _closeAttackInputEventFilter)
        {
            foreach(int j in _closeEyeRaycastEventFilter)
            {
                EcsWorld.NewEntity().Get<DamageEvent>() = new DamageEvent
                {
                    DamageValue = _playerDamage,
                    Enemy = _closeEyeRaycastEventFilter.Get1(j).HitInfo.collider.GetComponent<Actor>()
                };
            }
        }
    }
}
