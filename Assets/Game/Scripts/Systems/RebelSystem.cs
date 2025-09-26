using UnityEngine;
using Leopotam.Ecs;

public class RebelSystem: Injects, IEcsInitSystem, IEcsRunSystem
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
        if (!playerEntity.Has<RebelFlag>()) return;
        playerEntity.Get<CombatComponent>() = new CombatComponent()
        {
            CombatStyleId = 4,
            ActiveWeapon = playerEntity.Get<InventoryComponent>().RebelWeapon,
            FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[4]
        };

        foreach (int i in _abilityInputEvent)
        {
            if (Time.time - _lastAbilityActivationTime < _abilityCooldown) return;
            _lastAbilityActivationTime = Time.time;
            int abilityId = playerEntity.Get<AbilitiesComponent>().RebelAbilityId;
            if (abilityId == 1) MultiAttackAbility();
        }
    }

    private void MultiAttackAbility()
    {

    }
}
