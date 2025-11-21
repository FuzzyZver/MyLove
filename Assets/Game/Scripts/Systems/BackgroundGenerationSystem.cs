using UnityEngine;
using Leopotam.Ecs;

public class BackgroundGenerationSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<MoveInputEvent> _modeInputEventFilter;
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;

    }

    public void Run()
    {
        //foreach(int i in _modeInputEventFilter)
        //{
        //    Vector2 vector2 = _modeInputEventFilter.Get1(i).Vector2;
            
        //}
    }
}
