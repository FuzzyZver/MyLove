using UnityEngine;
using Leopotam.Ecs;

public class CriticSystem : Injects, IEcsInitSystem, IEcsRunSystem
{
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();
        if (playerEntity.Has<CriticFlag>())
        {
            playerEntity.Get<CombatComponent>() = new CombatComponent()
            {
                CombatStyleId = 1,
                ActiveWeapon = playerEntity.Get<InventoryComponent>().CriticWeapon,
                FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[1]
            };
        }
    }
}