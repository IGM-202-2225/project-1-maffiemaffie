using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour, Collidable
{
    public void HandleCollision(ColliderType type, GameObject collidedWith)
    {
        switch(type)
        {
            case ColliderType.PlayerProjectile:
                GetComponent<HealthUWU>().Hurt(10);
                break;
            case ColliderType.EnemyProjectile:
                GetComponent<HealthUWU>().Hurt(20);
                break;
            default:
                return;
        }
    }
}
