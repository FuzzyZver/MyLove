using UnityEngine;
using Leopotam.Ecs;

public class EyeRaycastSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<MoveInputEvent> _moveInputEventFilter;
    private Transform _playerTransform;
    private float _eyeDistanceThreshold;
    private Vector2 _lookVector = Vector2.right;

    public void Init()
    {
        _playerTransform = SceneData.PlayerOnScene.GetEntity().Get<TransformRef>().Transform;
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
        RaycastHit2D closeHitInfo = Physics2D.Raycast(_playerTransform.position, _lookVector, _eyeDistanceThreshold);
        RaycastHit2D farHitInfo = Physics2D.Raycast(_playerTransform.position, _lookVector);
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
