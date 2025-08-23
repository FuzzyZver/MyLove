using UnityEngine;
using Leopotam.Ecs;

public class ResistanceSystem: Injects, IEcsRunSystem
{
    private EcsFilter<ResistanceEvent> _resistanceEventFilter;
    private EcsFilter<ResistanceFlag> _resistanceFlagFilter;

    public void Run()
    {
        foreach(int i in _resistanceEventFilter)
        {
            var entity = _resistanceEventFilter.Get1(i).Entity;
            if (entity.Has<DeadFlag>()) return;
            if (entity.Has<ResistanceFlag>()) return;
            entity.Get<ResistanceFlag>().Time = _resistanceEventFilter.Get1(i).Time;
        }
        foreach(int i in _resistanceFlagFilter)
        {
            if (_resistanceFlagFilter.Get1(i).Time <= 0)
            {
                _resistanceFlagFilter.GetEntity(i).Del<ResistanceFlag>();
                UI.Console.SetConsoleText("Resist has been delete");
            }
            else
            {
                _resistanceFlagFilter.Get1(i).Time -= Time.deltaTime;
            }
        }
    }
}
