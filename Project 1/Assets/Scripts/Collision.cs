using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private (float Right, float Top, float Left, float Bottom) bounds;

    public List<GameObject> Projectiles;
    public List<GameObject> Enemies;
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
        if (Enemies.Count == 0) return;

        List<GameObject> trash = new();

        foreach (GameObject projectile in Projectiles)
        {
            if (isCollidingBounds(projectile))
            {
                trash.Add(projectile);
            }
        }
        foreach (GameObject item in trash)
        {
            Projectiles.Remove(item);
            Destroy(item);
        }

        for (int i = 0; i < Projectiles.Count; i++)
        {
            for (int j = i; j < Enemies.Count; j++)
            {
                if (isCollidingCircles(Projectiles[i], Enemies[j]))
                {
                    trash.Add(Projectiles[i]);
                    trash.Add(Enemies[j]);
                }
            }
        }
        foreach (GameObject item in trash)
        {
            Projectiles.Remove(item);
            Enemies.Remove(item);
            Destroy(item);
        }

        if (Enemies.Count == 0)
        {
            Spawner.GetComponent<EnemySpawner>().SpawnEnemyCircle(5, 8);
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
