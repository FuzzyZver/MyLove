using UnityEngine;
using Leopotam.Ecs;

public class ChildSystem: Injects, IEcsInitSystem, IEcsRunSystem
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
        if (!playerEntity.Has<ChildFlag>()) return;

        playerEntity.Get<CombatComponent>() = new CombatComponent()
        {
            CombatStyleId = 3,
            ActiveWeapon = playerEntity.Get<InventoryComponent>().ChildWeapon,
            FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[3]
        }; 

        foreach (int i in _abilityInputEvent)
        {
            if (Time.time - _lastAbilityActivationTime < _abilityCooldown) return;
            _lastAbilityActivationTime = Time.time;
            int abilityId = playerEntity.Get<AbilitiesComponent>().ChildAbilityId;
            if (abilityId == 1) AttackDashAbility();
        }
    }

    private void AttackDashAbility()
    {

    }
}