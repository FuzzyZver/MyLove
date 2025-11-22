using UnityEngine;
using Leopotam.Ecs;
using System.Collections;

public class DoorsSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<NearDoorFlag> _nearDoorFlagFilter;
    private EcsFilter<InteractInputEvent> _interactInputEventFilter;
    private Sprite _openDoorSprite;
    private Sprite _closeDoorSprite;
    private float _doorCloseTime;
    
    public void Init()
    {
        _openDoorSprite = GameConfig.CommonConfig.OpenDoorSprite;
        _closeDoorSprite = GameConfig.CommonConfig.CloseDoorSprite;
        _doorCloseTime = GameConfig.CommonConfig.DoorCloseTime;
    }

    public void Run()
    {
        foreach(int i in _nearDoorFlagFilter)
        {
            DoorActor door = _nearDoorFlagFilter.Get1(i).DoorActor;
            if (door == null) return;
            var doorEntity = door.GetEntity();

            foreach (int j in _interactInputEventFilter)
            {
                if (!doorEntity.Has<OpenDoorFlag>())
                {
                    doorEntity.Get<OpenDoorFlag>();
                    doorEntity.Get<SpriteRef>().SpriteRenderer.sprite = _openDoorSprite;
                    doorEntity.Get<Collider2DRef>().Collider2D.enabled = false;
                    CoroutineRunner.Instance.StartCoroutine(TimerCloseDoor(door));
                }
            }
        }
    }

    private IEnumerator TimerCloseDoor(DoorActor door)
    {
        yield return new WaitForSeconds(_doorCloseTime);
        var doorEntity = door.GetEntity();
        doorEntity.Get<SpriteRef>().SpriteRenderer.sprite = _closeDoorSprite;
        doorEntity.Get<Collider2DRef>().Collider2D.enabled = true;
        doorEntity.Del<OpenDoorFlag>();
    }
}
