using UnityEngine;
using Leopotam.Ecs;

public class SaverSystem : Injects, IEcsInitSystem, IEcsRunSystem
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
        if (!playerEntity.Has<SaverFlag>()) return;
        playerEntity.Get<CombatComponent>() = new CombatComponent()
        {
            CombatStyleId = 5,
            ActiveWeapon = playerEntity.Get<InventoryComponent>().SaverWeapon,
            FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[5]
        };

        foreach (int i in _abilityInputEvent)
        {
            if (Time.time - _lastAbilityActivationTime < _abilityCooldown) return;
            _lastAbilityActivationTime = Time.time;
            int abilityId = playerEntity.Get<AbilitiesComponent>().SaverAbilityId;
            if (abilityId == 1) HeallerAbility();
        }
    }

    private void HeallerAbility()
    {

    }
}