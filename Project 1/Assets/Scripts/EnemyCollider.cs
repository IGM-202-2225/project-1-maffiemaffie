using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour, Collidable
{
    [SerializeField]
    private GameObject plusFive;

    public void HandleCollision(ColliderType type, GameObject collidedWith)
    {
        switch(type)
        {
            case ColliderType.PlayerProjectile:
                GetComponent<HealthUWU>().Hurt(10);
                break;
            case ColliderType.EnemyProjectile:
                GetComponent<HealthUWU>().Hurt(20);
                GameObject _plusFive = Instantiate(plusFive, transform.position, Quaternion.identity);
                _plusFive.GetComponent<SpriteRenderer>().color = Color.cyan;
                FindObjectOfType<PowerSlider>().increasePower(10);
                break;
            default:
                return;
        }
    }
}
