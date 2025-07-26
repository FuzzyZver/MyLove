using UnityEngine;
using Leopotam.Ecs;

public class JumpSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<JumpInputEvent> _jumpInputEventFilter;
    private PlayerActor _playerRef;
    private float _jumpForce;
    private bool _doubleJumpCheck;

    public void Init()
    {
        _playerRef = SceneData.PlayerOnScene;
        _jumpForce = GameConfig.PlayerConfig.JumpForce;
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
                var playerRigidbody = playerEntity.Get<RigidbodyRef>().Rigidbody2D;
                playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            }
            if (!playerEntity.Has<IsGroundFlag>() && _doubleJumpCheck)
            {
                var playerRigidbody = playerEntity.Get<RigidbodyRef>().Rigidbody2D;
                playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Force);
                _doubleJumpCheck = false;
            }
        }
    }
}
