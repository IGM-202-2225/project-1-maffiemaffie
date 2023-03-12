using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour, Collidable
{
    public void HandleCollision(ColliderType type, GameObject collidedWith)
    {
        switch(type)
        {
            case ColliderType.EnemyProjectile:
                //GetComponent<HealthUWU>().Hurt(10);
                GetComponent<TurnRedButThenDontAnymore>().Flash();
                break;
            case ColliderType.Enemy:
                //GetComponent<HealthUWU>().Hurt(20);
                GetComponent<TurnRedButThenDontAnymore>().Flash();
                break;
            default:
                return;
        }
    }
}
