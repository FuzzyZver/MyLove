using UnityEngine;
using Leopotam.Ecs;

public class SaverSystem : Injects, IEcsInitSystem, IEcsRunSystem
{
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();
        if (playerEntity.Has<SaverFlag>())
        {
            playerEntity.Get<CombatComponent>() = new CombatComponent()
            {
                CombatStyleId = 5,
                ActiveWeapon = playerEntity.Get<InventoryComponent>().SaverWeapon,
                FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[5]
            };
        }
    }
}