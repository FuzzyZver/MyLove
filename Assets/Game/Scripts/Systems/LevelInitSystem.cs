using UnityEngine;
using Leopotam.Ecs;

public class LevelInitSystem: Injects, IEcsPreInitSystem
{
    public void PreInit()
    {
        SceneData.PlayerOnScene.Init(EcsWorld);
        SceneData.EnemyOnScene.Init(EcsWorld);
        UI.Console.Init(EcsWorld);

        foreach (DoorActor door in SceneData.Doors)
        {
            door.Init(EcsWorld);
        }
    }
}
