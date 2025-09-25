using UnityEngine;
using Leopotam.Ecs;

public class RebelSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();
        if (playerEntity.Has<RebelFlag>())
        {
            playerEntity.Get<CombatComponent>() = new CombatComponent()
            {
                CombatStyleId = 4,
                ActiveWeapon = playerEntity.Get<InventoryComponent>().RebelWeapon,
                FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[4]
            };
        }
    }
}
