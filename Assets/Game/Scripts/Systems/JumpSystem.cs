using UnityEngine;
using Leopotam.Ecs;

public class JumpSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<JumpInputEvent> _jumpInputEventFilter;
    private PlayerActor _playerRef;
    private bool _doubleJumpCheck;

    public void Init()
    {
        _playerRef = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _playerRef.GetEntity();
        
        if (playerEntity.Has<IsGroundFlag>())
        {
            _doubleJumpCheck = true;
        }
        foreach (int i in _jumpInputEventFilter)
        {
            if (playerEntity.Has<DeadFlag>()) return;
            if (playerEntity.Has<FreezeFlag>()) return;
            if (playerEntity.Has<IsGroundFlag>())
            {
                playerEntity.Get<JumpFlag>();
            }
            if (!playerEntity.Has<IsGroundFlag>() && _doubleJumpCheck)
            {
                playerEntity.Get<JumpFlag>();
                _doubleJumpCheck = false;
            }
        }
    }
}
