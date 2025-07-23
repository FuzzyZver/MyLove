using UnityEngine;
using Leopotam.Ecs;

public class LevelInitSystem: Injects, IEcsInitSystem
{
    public void Init()
    {
        SceneData.PlayerOnScene.Init(EcsWorld);
    }
}
