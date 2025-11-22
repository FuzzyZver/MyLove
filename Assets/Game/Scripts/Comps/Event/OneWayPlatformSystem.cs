using UnityEngine;
using Leopotam.Ecs;
using System.Collections;

public class OneWayPlatformSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<MoveInputEvent> _moveInputEventFilter;
    private PlatformEffector2D _oneWayPlatforms;
    private string _playerLayer = "Player";
    private int _defaultMask;
    private float _fallFrames = 0.2f;

    public void Init()
    {
        _oneWayPlatforms = SceneData.OneWayPlatforms;
        _defaultMask = _oneWayPlatforms.colliderMask;
    }

    public void Run()
    {
        foreach (int i in _moveInputEventFilter)
        {
            if (_moveInputEventFilter.Get1(i).Vector2.y < 0)
            {
                _oneWayPlatforms.colliderMask &= ~(1 << LayerMask.NameToLayer(_playerLayer));
                CoroutineRunner.Instance.StartCoroutine(PlatformSwitch());
            }
        }
    }

    private IEnumerator PlatformSwitch()
    {
        yield return new WaitForSeconds(_fallFrames);
        _oneWayPlatforms.colliderMask = _defaultMask;
    }
}