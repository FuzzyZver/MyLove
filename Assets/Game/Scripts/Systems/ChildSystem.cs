using UnityEngine;
using Leopotam.Ecs;

public class ChildSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();
        if (playerEntity.Has<ChildFlag>())
        {
            playerEntity.Get<CombatComponent>() = new CombatComponent()
            {
                CombatStyleId = 3,
                ActiveWeapon = playerEntity.Get<InventoryComponent>().ChildWeapon,
                FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[3]
            };
        }
    }
}