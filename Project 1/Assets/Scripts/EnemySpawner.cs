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

    public float BitchCooldown;
    private float bitchCooldown;
    private float timeSinceLastBitchSpawned;

    private int spawning = 0;

    private enum State
    {
        Idle,
        Spawning
    }
    private State state = State.Idle;
    
    private (float Right, float Top, float Left, float Bottom) bounds;


    // Start is called before the first frame update
    void Start()
    {
        #region Initialize Bounds
        bounds.Top = Camera.main.orthographicSize;
        bounds.Bottom = -Camera.main.orthographicSize;
        bounds.Left = -Camera.main.orthographicSize * Camera.main.aspect;
        bounds.Right = Camera.main.orthographicSize * Camera.main.aspect;
        #endregion Initialize Bounds

        SpawnEnemyCircle(player.transform.position, 1, 8);

        bitchCooldown = Gaussian(1f, 0.5f) * BitchCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        state = State.Idle;
        if (spawning > 0) state = State.Spawning;

        if (state == State.Idle && CollisionManager.GetComponent<Collision>().Enemies.Count == 0)
        {
            // SpawnEnemyCircle(player.transform.position, 3f, 16);
            SpawnEnemyArc(10);
        }

        timeSinceLastBitchSpawned += Time.deltaTime;
        if (timeSinceLastBitchSpawned > bitchCooldown)
        {
            timeSinceLastBitchSpawned %= bitchCooldown;
            bitchCooldown = Gaussian(1f, 0.5f) * BitchCooldown;
            SpawnBitchEnemy();
        }
    }

    public void SpawnEnemyCircle(Vector3 center, float radius, int count)
    {
        Spawner spawner = GetComponent<Spawner>();
        spawner.SpawnCircle(indicator.gameObject, center, radius, count, _ => { });
        spawner.SpawnCircle(indicator.gameObject, center, radius, count, _ => { });

        void callback(GameObject enemy)
        {
            enemy.GetComponent<EnemyMove>().player = player;
            CollisionManager.GetComponent<Collision>().Enemies.Add(enemy);
            spawning--;

        }
        spawner.SpawnCircle(enemy, center, radius, count, callback);
        spawning += count;
    }

    public void SpawnEnemyArc(int count)
    {
        Spawner.Side side;
        bool directionPositiveToNegative;

        #region get side and direction
        float xNorm = player.transform.position.x / bounds.Right;
        float yNorm = player.transform.position.y / bounds.Top;

        if (xNorm < yNorm)
        {
            if (xNorm < -yNorm) side = Spawner.Side.Left;
            else side = Spawner.Side.Top;
        }
        else
        {
            if (xNorm < -yNorm) side = Spawner.Side.Bottom;
            else side = Spawner.Side.Right;
        }

        if (side == Spawner.Side.Top || side == Spawner.Side.Bottom)
        {
            directionPositiveToNegative = player.transform.position.x > 0;
        }
        else
        {
            directionPositiveToNegative = player.transform.position.y > 0;
        }
        #endregion get side and direction

        Spawner spawner = GetComponent<Spawner>();
        spawner.SpawnArc(indicator.gameObject, side, count, _ => { }, directionPositiveToNegative);

        void callback(GameObject enemy)
        {
            enemy.GetComponent<EnemyMove>().player = player;
            CollisionManager.GetComponent<Collision>().Enemies.Add(enemy);
            spawning--;

        }
        spawner.SpawnArc(enemy, side, count, callback, directionPositiveToNegative);
        spawning += count;
    }

    private void SpawnBitchEnemy()
    {
        GameObject thisBitch = Instantiate(bitchShooter, GaussianVector(), Quaternion.identity);
        thisBitch.GetComponent<BitchShooter>().player = player;
        thisBitch.GetComponent<BitchShooter>().collisionManager = CollisionManager;
        CollisionManager.GetComponent<Collision>().Bitches.Add(thisBitch);
    }

    private Vector3 GaussianVector()
    {
        float x = Gaussian(0, 2);
        float y = Gaussian(0, 2);
        
        if (x < 0)
        {
            x += bounds.Right;
        } else
        {
            x += bounds.Left;
        }
        if (y < 0)
        {
            y += bounds.Top;
        } else
        {
            y += bounds.Bottom;
        }

        return new Vector3(x, y, 0);
    }

    private float Gaussian(float mean, float stdDev)
    {
        float val1 = UnityEngine.Random.Range(0f, 1f);
        float val2 = UnityEngine.Random.Range(0f, 1f);

        float gaussValue =
                 Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
                 Mathf.Sin(2.0f * Mathf.PI * val2);

        return mean + stdDev * gaussValue;
    }

}
