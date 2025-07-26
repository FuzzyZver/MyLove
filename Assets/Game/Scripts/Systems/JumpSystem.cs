using UnityEngine;
using Leopotam.Ecs;

public class JumpSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<JumpInputEvent> _jumpInputEventFilter;
    private PlayerActor _playerRef;
    private float _jumpForce;

    public void Init()
    {
        _playerRef = SceneData.PlayerOnScene;
        _jumpForce = GameConfig.PlayerConfig.JumpForce;
    }

    public void Run()
    {
        foreach (int i in _jumpInputEventFilter)
        {
            var playerEntity = _playerRef.GetEntity();
            if (!playerEntity.Has<IsGroundFlag>()) return;
            if (playerEntity.Has<DeadFlag>()) return;
            if (playerEntity.Has<FreezeFlag>()) return;

            var playerRigidbody = playerEntity.Get<RigidbodyRef>().Rigidbody2D;
            playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
