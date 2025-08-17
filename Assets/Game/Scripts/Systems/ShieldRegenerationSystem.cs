using UnityEngine;
using Leopotam.Ecs;
using System.Collections;

public class ShieldRegenerationSystem : Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<DamageEvent> _damageEventFilter;
    private PlayerActor _playerRef;
    private float _timeUntilRegenShield;
    private float _lastHitTime;

    private float _regenCooldown = 0.5f;
    private float _lastRegenTime = 0;

    public void Init()
    {
        _playerRef = SceneData.PlayerOnScene;
        _timeUntilRegenShield = GameConfig.PlayerConfig.TimeUntilRegenerationShield;
        _lastHitTime = _timeUntilRegenShield;
    }

    public void Run()
    {
        var playerEntity = _playerRef.GetEntity();
        if (playerEntity.Has<DeadFlag>()) return;
        if (playerEntity.Has<FreezeFlag>()) return;
        foreach (int i in _damageEventFilter)
        {
            var entity = _damageEventFilter.Get1(i).Entity;
            if (entity.Has<ShieldComponent>()) _lastHitTime = 0;
        }

        if (Time.time - _lastRegenTime < _regenCooldown) return;
        _lastRegenTime = Time.time;
        if (_lastHitTime == _timeUntilRegenShield && playerEntity.Get<ShieldComponent>().ShieldValue < playerEntity.Get<ShieldComponent>().MaxShieldValue)
        {
            playerEntity.Get<ShieldComponent>().ShieldValue++;
        }
        if (_lastHitTime < _timeUntilRegenShield) _lastHitTime++;

    }

}
