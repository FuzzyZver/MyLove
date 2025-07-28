using UnityEngine;
using Leopotam.Ecs;

public class EnemyActor: Actor
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _transform;

    public override void ExpandEntity(EcsEntity entity)
    {
        entity.Get<RigidbodyRef>().Rigidbody2D = _rigidbody2D;
        entity.Get<TransformRef>().Transform = _transform;
    }
}
