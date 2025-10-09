using UnityEngine;
using Leopotam.Ecs;
using System.Collections.Generic;

public class PlayerActor: Actor
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _stepPosition;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private Animator _animator;

    //Jump properties
    private float _coyoteTime;
    private float _jumpForce;
    private float _jumpVelocity;
    private float _jumpMultiply;
    private float _multiply;
    private float _lastGroundTime;

    private float _hangGravityScale;
    private float _normalGravityScale;
    private float _hangGravityMultiply;

    public override void ExpandEntity(EcsEntity entity)
    {
        entity.Get<RigidbodyRef>().Rigidbody2D = _rigidbody2D;
        entity.Get<TransformRef>().Transform = _transform;
        entity.Get<Collider2DRef>().Collider2D = _collider2D;
        entity.Get<StepPositionRef>().StepPosition = _stepPosition;
        entity.Get<GameObjectRef>().GameObject = this.gameObject;
        entity.Get<HealthComponent>() = new HealthComponent()
        {
            MaxHealthValue = _gameConfig.PlayerConfig.Health,
            HealthValue = _gameConfig.PlayerConfig.Health
        };
        entity.Get<ShieldComponent>() = new ShieldComponent()
        {
            MaxShieldValue = _gameConfig.PlayerConfig.Shield,
            ShieldValue = _gameConfig.PlayerConfig.Shield
        };
        entity.Get<InventoryComponent>() = new InventoryComponent()
        {
            RuneSlots = new List<Rune>(),
            MaxRunes = 20
        };
        entity.Get<CombatComponent>() = new CombatComponent()
        {
            CombatStyleId = 1
        };
        entity.Get<AbilitiesComponent>() = new AbilitiesComponent()
        {
            CriticAbilityId = 1,
            AlarmistAbilityId = 1,
            ChildAbilityId = 1,
            RebelAbilityId = 1,
            SaverAbilityId = 1
        };
        entity.Get<AnimatorRef>().Animator = _animator;
    }

    private void Start()
    {
        _coyoteTime = _gameConfig.PlayerConfig.CoyoteTime;
        _jumpForce = _gameConfig.PlayerConfig.JumpForce;
        _jumpVelocity = _gameConfig.PlayerConfig.JumpVelocity;
        _jumpMultiply = _gameConfig.PlayerConfig.JumpMultiply;
        _multiply = _gameConfig.PlayerConfig.Multiply;
        _lastGroundTime = _gameConfig.PlayerConfig.LastGroundTime;
        _hangGravityScale = _gameConfig.PlayerConfig.HangGravityScale;
        _normalGravityScale = _gameConfig.PlayerConfig.NormalGravityScale;
        _hangGravityMultiply = _gameConfig.PlayerConfig.HangGravityMultiply;
    }

    public void FixedUpdate()
    {
        var playerEntity = GetEntity();
        if (playerEntity.Has<JumpFlag>())
        {
            _lastGroundTime = Time.time;
            playerEntity.Del<JumpFlag>();
        }

        bool canUseCoyote = (Time.time - _lastGroundTime) <= _coyoteTime;

        if (canUseCoyote)
        {
            _rigidbody2D.gravityScale = _hangGravityScale;
            _hangGravityMultiply = _multiply;
            _jumpVelocity = _jumpForce;
            _jumpMultiply = _multiply;
        }
        if (_jumpVelocity > 0f)
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, _jumpVelocity);
            _jumpVelocity -= _jumpMultiply;
            _jumpMultiply = _jumpMultiply * _multiply;
        }

        if (_rigidbody2D.linearVelocity.y == 0f && _rigidbody2D.gravityScale < _normalGravityScale)
        {
            _rigidbody2D.gravityScale += _hangGravityMultiply;
            _hangGravityMultiply = _hangGravityMultiply * _multiply;
        }
    }
}