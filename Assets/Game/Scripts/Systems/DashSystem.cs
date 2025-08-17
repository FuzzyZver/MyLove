using UnityEngine;
using Leopotam.Ecs;

public class DashSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<DashInputEvent> _dashInputEventFilter;
    private EcsFilter<MoveInputEvent> _moveInuputEventFilter;
    private PlayerActor _playerRef;
    private float _dashDistance;
    private float _dashTime;
    private float _lastDashTime = 0f;
    private Vector2 _lastDirection = new Vector2(1, 0);
    
    public void Init()
    {
        _playerRef = SceneData.PlayerOnScene;
        _dashDistance = GameConfig.PlayerConfig.DashDistance;
        _dashTime = GameConfig.PlayerConfig.DashTime;
    }

    public void Run()
    {
        foreach (int i in _moveInuputEventFilter)
        {
            var Vector2 = _moveInuputEventFilter.Get1(i).Vector2;
            if (Vector2.sqrMagnitude > 0.01f)
            {
                _lastDirection.x = Vector2.x;
                _lastDirection.Normalize();
            }
        }
        var playerEntity = _playerRef.GetEntity();
        foreach (int i in _dashInputEventFilter)
        {
            if (playerEntity.Has<DeadFlag>()) return;
            if (playerEntity.Has<FreezeFlag>()) return;
            if (!playerEntity.Has<IsDashingFlag>() && Time.time - _lastDashTime >= _dashTime)
            {
                playerEntity.Get<IsDashingFlag>();
                _lastDashTime = Time.time;
            }
        }
        if (playerEntity.Has<IsDashingFlag>())
        {
            if (Time.time - _lastDashTime >= _dashTime)
            {
                playerEntity.Del<IsDashingFlag>();
                playerEntity.Get<RigidbodyRef>().Rigidbody2D.linearVelocity = Vector2.zero;
            }
            else
            {
                Vector2 dashVelocity = new Vector2(_lastDirection.x * _dashDistance, _lastDirection.y);
                playerEntity.Get<RigidbodyRef>().Rigidbody2D.linearVelocity = dashVelocity;
            }
        }
    }
}