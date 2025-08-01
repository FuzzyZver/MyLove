using UnityEngine;
using Leopotam.Ecs;

public class PlayerActor: Actor
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _stepPosition;
    [SerializeField] private Collider2D _collider2D;

    public override void ExpandEntity(EcsEntity entity)
    {
        entity.Get<RigidbodyRef>().Rigidbody2D = _rigidbody2D;
        entity.Get<TransformRef>().Transform = _transform;
        entity.Get<Collider2DRef>().Collider2D = _collider2D;
        entity.Get<StepPositionRef>().StepPosition = _stepPosition;
        entity.Get<GameObjectRef>().GameObject = this.gameObject;
        entity.Get<HealthComponent>().HealthValue = _gameConfig.PlayerConfig.Health;
    }
}
