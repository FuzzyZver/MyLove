using UnityEngine;
using Leopotam.Ecs;

public class PlayerAnimationSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<CloseAttackInputEvent> _closeAttackInputEventFilter;
    private EcsFilter<DamageEvent> _damageEventFilter;
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }
    
    public void Run()
    {
        var playerEntity = _player.GetEntity();
        if (playerEntity.Has<MoveFlag>())
        {
            playerEntity.Get<AnimatorRef>().Animator.SetBool("IsRunning", true);
        }
        else
        {
            playerEntity.Get<AnimatorRef>().Animator.SetBool("IsRunning", false);
        }

        foreach(int i in _closeAttackInputEventFilter)
        {
            playerEntity.Get<AnimatorRef>().Animator.SetTrigger("Kick");
        }
        foreach(int i in _damageEventFilter)
        {
            var entity = _damageEventFilter.Get1(i).Entity;
            if(entity == playerEntity)
            {
                playerEntity.Get<AnimatorRef>().Animator.SetTrigger("Hurt");
            }
        }
    }
}
