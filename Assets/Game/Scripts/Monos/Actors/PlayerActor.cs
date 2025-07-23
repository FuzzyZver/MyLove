using UnityEngine;
using Leopotam.Ecs;

public class PlayerActor: Actor
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public override void ExpandEntity(EcsEntity entity)
    {
        entity.Get<RigidbodyRef>().Rigidbody2D = _rigidbody2D;
    }
}
