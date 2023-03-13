using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private (float Right, float Top, float Left, float Bottom) bounds;

    public List<GameObject> PlayerProjectiles;
    public List<GameObject> EnemyProjectiles;
    public List<GameObject> Enemies;
    public List<GameObject> Bitches;
    public GameObject Player;
    public GameObject Explodemy;
    public GameObject plusOne;
    public GameObject plusFive;

    [SerializeField]
    private GameObject Spawner;

    // Start is called before the first frame update
    void Start()
    {
        bounds.Top = Camera.main.orthographicSize;
        bounds.Bottom = -Camera.main.orthographicSize;
        bounds.Left = -Camera.main.orthographicSize * Camera.main.aspect;
        bounds.Right = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        CollectTrash();
        DoPlayerOnEnemyCollision();
        DoProjectileBoundsCollision();
        DoProjectileOnEnemyCollision();
        DoProjectileOnPlayerCollision();
        DoEnemyOnEnemyViolence();
    }

    private void CollectTrash()
    {
        foreach (GameObject projectile in new List<GameObject>(EnemyProjectiles))
        {
            if (projectile == null) EnemyProjectiles.Remove(projectile);
        }
        foreach (GameObject projectile in new List<GameObject>(PlayerProjectiles))
        {
            if (projectile == null) PlayerProjectiles.Remove(projectile);
        }
        foreach (GameObject enemy in new List<GameObject>(Enemies))
        {
            if (enemy == null) Enemies.Remove(enemy);
        }
        foreach (GameObject enemy in new List<GameObject>(Bitches))
        {
            if (enemy == null) Bitches.Remove(enemy);
        }
    }
    
    private void DoProjectileBoundsCollision()
    {
        foreach (GameObject projectile in PlayerProjectiles)
        {
            if (isCollidingBounds(projectile))
            {
                projectile.GetComponent<Collidable>().HandleCollision(ColliderType.Bounds, null);
            }
        }
        foreach (GameObject projectile in EnemyProjectiles)
        {
            if (isCollidingBounds(projectile))
            {
                projectile.GetComponent<Collidable>().HandleCollision(ColliderType.Bounds, null);
            }
        }
    }

    private void DoPlayerOnEnemyCollision()
    {
        if (Player == null) return;
        foreach (GameObject enemy in Enemies)
        {
            if (isCollidingCircles(Player, enemy))
            {
                Player.GetComponent<PlayerCollider>().HandleCollision(ColliderType.Enemy, enemy);
            }
        }
    }

    private void DoProjectileOnEnemyCollision()
    {
        foreach (GameObject projectile in PlayerProjectiles)
        {
            foreach (GameObject enemy in Enemies)
            {
                if (isCollidingCircles(projectile, enemy))
                {
                    enemy.GetComponent<Collidable>().HandleCollision(ColliderType.PlayerProjectile, projectile);
                    projectile.GetComponent<Collidable>().HandleCollision(ColliderType.Enemy, enemy);
                }
            }

            if (projectile == null) return;
            foreach (GameObject enemy in Bitches)
            {
                if (isCollidingCircles(projectile, enemy))
                {
                    enemy.GetComponent<Collidable>().HandleCollision(ColliderType.PlayerProjectile, projectile);
                    projectile.GetComponent<Collidable>().HandleCollision(ColliderType.Bitch, enemy);
                }
            }
        }
    }

    private void DoProjectileOnPlayerCollision()
    {
        if (Player == null) return;

        foreach (GameObject projectile in EnemyProjectiles)
        {
            if (isCollidingCircles(Player, projectile))
            {
                Player.GetComponent<Collidable>().HandleCollision(ColliderType.EnemyProjectile, projectile);
            }
        }
    }

    private void DoEnemyOnEnemyViolence()
    {
        foreach (GameObject projectile in EnemyProjectiles)
        {
            foreach (GameObject enemy in Enemies)
            {
                if (isCollidingCircles(projectile, enemy))
                {
                    projectile.GetComponent<Collidable>().HandleCollision(ColliderType.Enemy, enemy);
                    enemy.GetComponent<Collidable>().HandleCollision(ColliderType.EnemyProjectile, projectile);
                }
            }
        }
    }

    private bool isCollidingBounds(GameObject gameObject)
    {
        if (gameObject.transform.position.x < bounds.Left) return true;
        if (gameObject.transform.position.x > bounds.Right) return true;
        if (gameObject.transform.position.y < bounds.Bottom) return true;
        if (gameObject.transform.position.y > bounds.Top) return true;
        return false;
    }

    private bool isCollidingCircles(GameObject o1, GameObject o2)
    {
        Bounds bounds1 = o1.GetComponent<SpriteRenderer>().bounds;
        Bounds bounds2 = o2.GetComponent<SpriteRenderer>().bounds;

        Vector2 center1 = bounds1.center;
        Vector2 center2 = bounds2.center;

        Vector2 extents1 = bounds1.extents;
        Vector2 extents2 = bounds2.extents;

        float distance = (center1 - center2).sqrMagnitude;
        float minDistanceWithoutColliding = (extents1.sqrMagnitude) + (extents2.sqrMagnitude) ;

        return distance < minDistanceWithoutColliding;
    }
}
