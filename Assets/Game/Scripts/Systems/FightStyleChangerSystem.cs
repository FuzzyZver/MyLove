using UnityEngine;
using Leopotam.Ecs;

public class FightStyleChangerSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<NumbersInputEvent> _numbersInputEventFilter;
    private PlayerActor _player;
    
    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();

        foreach(int i in _numbersInputEventFilter)
        {
            playerEntity.Del<CriticFlag>();
            playerEntity.Del<AlarmistFlag>();
            playerEntity.Del<ChildFlag>();
            playerEntity.Del<RebelFlag>();
            playerEntity.Del<SaverFlag>();
            int fightStyleId = _numbersInputEventFilter.Get1(i).Number;
            if(fightStyleId == 1) playerEntity.Get<CriticFlag>();
            if (fightStyleId == 2) playerEntity.Get<AlarmistFlag>();
            if (fightStyleId == 3) playerEntity.Get<ChildFlag>();
            if (fightStyleId == 4) playerEntity.Get<RebelFlag>();
            if (fightStyleId == 5) playerEntity.Get<SaverFlag>();
            playerEntity.Get<CombatComponent>().CombatStyleId = fightStyleId;
            Debug.Log($"Стиль боя сейчас:{fightStyleId}");
        }
    }
}
