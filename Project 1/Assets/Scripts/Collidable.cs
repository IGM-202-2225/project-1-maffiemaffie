using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Collidable
{
    public void HandleCollision(ColliderType type, GameObject CollidedWith);
}

public enum ColliderType
{
    Bounds,
    PlayerProjectile,
    EnemyProjectile,
    Enemy,
    Bitch,
    Player
}