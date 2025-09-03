using UnityEngine;
using Leopotam.Ecs;

public class ItemActor: Actor
{
    [SerializeField] private Transform _transform;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public override void ExpandEntity(EcsEntity entity)
    {
        entity.Get<TransformRef>().Transform = _transform;
        entity.Get<SpriteRef>().SpriteRenderer = _spriteRenderer;
    }
}
