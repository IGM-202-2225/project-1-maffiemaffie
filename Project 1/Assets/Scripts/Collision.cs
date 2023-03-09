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
        DoPlayerOnEnemyCollision();
        DoProjectileBoundsCollision();
        DoProjectileOnEnemyCollision();
        DoProjectileOnPlayerCollision();
        DoEnemyOnEnemyViolence();
    }
    
    private void DoProjectileBoundsCollision()
    {
        List<GameObject> trash = new();

        foreach (GameObject projectile in PlayerProjectiles)
        {
            if (isCollidingBounds(projectile))
            {
                trash.Add(projectile);
            }
        }
        foreach (GameObject projectile in EnemyProjectiles)
        {
            if (isCollidingBounds(projectile))
            {
                trash.Add(projectile);
            }
        }
        foreach (GameObject item in trash)
        {
            PlayerProjectiles.Remove(item);
            EnemyProjectiles.Remove(item);
            Destroy(item);
        }
    }

    private void DoPlayerOnEnemyCollision()
    {
        Player.GetComponent<SpriteRenderer>().color = Color.white;
        foreach (GameObject enemy in Enemies)
        {
            if (!isCollidingCircles(Player, enemy)) continue;
            Player.GetComponent<SpriteRenderer>().color = Color.red;
            break;
        }
    }

    private void DoProjectileOnEnemyCollision()
    {
        if (Enemies.Count == 0) return;

        List<GameObject> trash = new();
        for (int i = 0; i < PlayerProjectiles.Count; i++)
        {
            for (int j = i; j < Enemies.Count; j++)
            {
                if (isCollidingCircles(PlayerProjectiles[i], Enemies[j]))
                {
                    trash.Add(PlayerProjectiles[i]);
                    Instantiate(Explodemy, Enemies[j].transform.position, Quaternion.identity);
                    Instantiate(plusOne, Enemies[j].transform.position + Vector3.right * 1f, Quaternion.identity);
                    trash.Add(Enemies[j]);
                }
            }
        }
        foreach (GameObject item in trash)
        {
            PlayerProjectiles.Remove(item);
            Enemies.Remove(item);
            Destroy(item);
        }
    }

    private void DoProjectileOnPlayerCollision()
    {
        List<GameObject> trash = new();
        for (int i = 0; i < EnemyProjectiles.Count; i++)
        {
            if (isCollidingCircles(EnemyProjectiles[i], Player))
            {
                trash.Add(EnemyProjectiles[i]);
            }
        }
        foreach (GameObject item in trash)
        {
            EnemyProjectiles.Remove(item);
            Destroy(item);
        }
    }

    private void DoEnemyOnEnemyViolence()
    {
        if (Enemies.Count == 0) return;

        List<GameObject> trash = new();
        for (int i = 0; i < EnemyProjectiles.Count; i++)
        {
            for (int j = i; j < Enemies.Count; j++)
            {
                if (isCollidingCircles(EnemyProjectiles[i], Enemies[j]))
                {
                    trash.Add(EnemyProjectiles[i]);
                    Instantiate(Explodemy, Enemies[j].transform.position, Quaternion.identity);
                    Instantiate(plusFive, Enemies[j].transform.position + Vector3.right * 1f, Quaternion.identity);
                    trash.Add(Enemies[j]);
                }
            }
        }
        foreach (GameObject item in trash)
        {
            EnemyProjectiles.Remove(item);
            Enemies.Remove(item);
            Destroy(item);
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
        float minDistanceWithoutColliding = (extents1.sqrMagnitude) + (extents2.sqrMagnitude);

        return distance < minDistanceWithoutColliding;
    }
}
