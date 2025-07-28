using UnityEngine;
using Leopotam.Ecs;

public class TriggerSystem: Injects, IEcsRunSystem
{
    private EcsFilter<OnTriggerEnterEvent> _onTriggerEnterEventFilter;

    public void Run()
    {
        foreach(int i in _onTriggerEnterEventFilter)
        {
            var thisGO = _onTriggerEnterEventFilter.Get1(i).ThisGameObject;
            var otherGO = _onTriggerEnterEventFilter.Get1(i).OtherGameObject;

            if (thisGO == null || otherGO == null)
                continue;

            if (!thisGO.TryGetComponent<Actor>(out var thisActor))
            {
                Debug.Log("���� ������");
                continue;
            }

            if (!otherGO.TryGetComponent<Actor>(out var otherActor))
            {
                Debug.Log("������ ������");
                continue;
            }

            var thisObject = thisActor.GetEntity();
            var otherObject = otherActor.GetEntity();
        }
    }
}
