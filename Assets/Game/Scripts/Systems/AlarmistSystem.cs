using UnityEngine;
using Leopotam.Ecs;
using System.Security.Cryptography.X509Certificates;

public class AlarmistSystem : Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<AbilityInputEvent> _abilityInputEvent;
    private PlayerActor _player;

    private float _abilityCooldown = 5f;
    private float _lastAbilityActivationTime = 0f;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();
        if (!playerEntity.Has<AlarmistFlag>()) return;
        playerEntity.Get<CombatComponent>() = new CombatComponent()
        {
            CombatStyleId = 2,
            ActiveWeapon = playerEntity.Get<InventoryComponent>().AlarmistWeapon,
            FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[2]
        };

        foreach (int i in _abilityInputEvent)
        {
            if (Time.time - _lastAbilityActivationTime < _abilityCooldown) return;
            _lastAbilityActivationTime = Time.time;
            int abilityId = playerEntity.Get<AbilitiesComponent>().AlarmistAbilityId; ;
            if (abilityId == 1) ResistAbility();
        }
    }

    private void ResistAbility()
    {

    }
}