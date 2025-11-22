using UnityEngine;
using Leopotam.Ecs;

public class DoorActor: Actor
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _barrierCollider2D;

    public override void ExpandEntity(EcsEntity entity)
    {
        entity.Get<SpriteRef>().SpriteRenderer = _spriteRenderer;
        entity.Get<Collider2DRef>().Collider2D = _barrierCollider2D;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerActor>() != null)
        {
            GetEntity().Get<NearDoorFlag>().DoorActor = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerActor>() != null)
        {
            GetEntity().Del<NearDoorFlag>();
        }
    }
}
