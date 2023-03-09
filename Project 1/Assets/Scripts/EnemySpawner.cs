using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer indicator;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject bitchShooter;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject CollisionManager;

    private int spawning = 0;

    private enum State
    {
        Idle,
        Spawning
    }
    private State state = State.Idle;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyCircle(5, 8);
        GameObject bitch = Instantiate(bitchShooter, new Vector3(0, 0), Quaternion.identity);
        bitch.GetComponent<BitchShooter>().player = player;
        bitch.GetComponent<BitchShooter>().collisionManager = CollisionManager;
    }

    // Update is called once per frame
    void Update()
    {
        state = State.Idle;
        if (spawning > 0) state = State.Spawning;

        if (state == State.Idle && CollisionManager.GetComponent<Collision>().Enemies.Count == 0)
        {
            SpawnEnemyCircle(4, 8);
        }
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
            spawning--;

        }
        spawner.SpawnCircle(enemy, radius, count, callback);
        spawning += count;
    }
}
