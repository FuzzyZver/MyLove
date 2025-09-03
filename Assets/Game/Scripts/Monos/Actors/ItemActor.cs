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
        entity.Get<GameObjectRef>().GameObject = this.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerActor>())
        {
            GetWorld().NewEntity().Get<ItemEnterEvent>().Entity = GetEntity();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerActor>())
        {
            GetWorld().NewEntity().Get<ItemExitEvent>().Entity = GetEntity();
        }
    }
}
