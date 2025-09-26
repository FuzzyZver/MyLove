using UnityEngine;
using Leopotam.Ecs;

public class CriticSystem : Injects, IEcsInitSystem, IEcsRunSystem
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
        if (!playerEntity.Has<CriticFlag>()) return;
        playerEntity.Get<CombatComponent>() = new CombatComponent()
        {
            CombatStyleId = 1,
            ActiveWeapon = playerEntity.Get<InventoryComponent>().CriticWeapon,
            FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[1]
        };

        foreach (int i in _abilityInputEvent)
        {
            if (Time.time - _lastAbilityActivationTime < _abilityCooldown) return;
            _lastAbilityActivationTime = Time.time;
            int abilityId = playerEntity.Get<AbilitiesComponent>().CriticAbilityId;
            if (abilityId == 1) MistakesAbility();
        }
    }

    private void MistakesAbility()
    {
        Debug.Log("Способность активирована");
    }
}