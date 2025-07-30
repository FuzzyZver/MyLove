using UnityEngine;
using Leopotam.Ecs;

public class EyeRaycastSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<MoveInputEvent> _moveInputEventFilter;
    private Transform _playerTransform;
    private Collider2D _playerCollider2D;
    private float _eyeDistanceThreshold;
    private float _skin = 0.01f;
    private Vector2 _lookVector = Vector2.right;

    public void Init()
    {
        _playerTransform = SceneData.PlayerOnScene.GetEntity().Get<TransformRef>().Transform;
        _playerCollider2D = SceneData.PlayerOnScene.GetEntity().Get<Collider2DRef>().Collider2D;
        _eyeDistanceThreshold = GameConfig.InputConfig.EyeDistanceThreshold;
    }
    
    public void Run()
    {
        foreach (int i in _moveInputEventFilter)
        {
            Vector2 moveVector = _moveInputEventFilter.Get1(i).Vector2;
            if (moveVector.x > 0)
            {
                _lookVector = Vector2.right;
            }
            else if (moveVector.x < 0)
            {
                _lookVector = Vector2.left;
            }
            else continue;
        }
        if (_playerTransform  == null) return;
        Vector2 origin = (Vector2)_playerTransform.position + _lookVector * (_playerCollider2D.bounds.extents.magnitude + _skin);
        RaycastHit2D closeHitInfo = Physics2D.Raycast(origin, _lookVector, _eyeDistanceThreshold);
        RaycastHit2D farHitInfo = Physics2D.Raycast(origin, _lookVector);

        if(closeHitInfo.collider != null)
        {
            EcsWorld.NewEntity().Get<CloseEyeRaycastEvent>().HitInfo = closeHitInfo;
        }
        if(farHitInfo.collider != null)
        {
            EcsWorld.NewEntity().Get<FarEyeRaycastEvent>().HitInfo = farHitInfo;
        }
    }
}
