using UnityEngine;
using Leopotam.Ecs;

public class AlarmistSystem : Injects, IEcsInitSystem, IEcsRunSystem
{
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();
        if (playerEntity.Has<AlarmistFlag>())
        {
            playerEntity.Get<CombatComponent>() = new CombatComponent()
            {
                CombatStyleId = 2,
                ActiveWeapon = playerEntity.Get<InventoryComponent>().AlarmistWeapon,
                FightStyleBuffs = GameConfig.FightStylesConfig.FightStyleBuffs[2]
            };
        }
    }
}