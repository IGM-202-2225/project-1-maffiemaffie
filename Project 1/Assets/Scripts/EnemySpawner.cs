using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer indicator;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject CollisionManager;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyCircle(5, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemyCircle(int radius, int count)
    {
        Spawner spawner = GetComponent<Spawner>();
        spawner.SpawnCircle(indicator.gameObject, radius, count, _ => { });
        spawner.SpawnCircle(indicator.gameObject, radius, count, _ => { });

        void callback(GameObject enemy)
        {
            enemy.GetComponent<EnemyMove>().player = player;
            CollisionManager.GetComponent<Collision>().Enemies.Add(enemy);
        }
        spawner.SpawnCircle(enemy, radius, count, callback);
    }
}
