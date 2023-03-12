using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour, Collidable
{
    public void HandleCollision(ColliderType type, GameObject CollidedWith)
    {
        Destroy(gameObject);
    }
}
