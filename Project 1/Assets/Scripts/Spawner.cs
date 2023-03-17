using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private struct InstantiateCall
    {
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public Action<GameObject> OnCall;
        
        public InstantiateCall(GameObject prefab, Vector3 position, Quaternion rotation, Action<GameObject> onCall)
        {
            this.prefab = prefab;
            this.position = position;
            this.rotation = rotation;
            OnCall = onCall;
        }
    }

    private (float Right, float Top, float Left, float Bottom) bounds;

    [SerializeField]
    private float spawnIntervalSeconds;

    [SerializeField]
    private GameObject indicatorPrefab;

    private Queue<InstantiateCall> instantiationQueue = new();
    private int updateIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        #region Initialize Bounds
        bounds.Top = Camera.main.orthographicSize;
        bounds.Bottom = -Camera.main.orthographicSize;
        bounds.Left = -Camera.main.orthographicSize * Camera.main.aspect;
        bounds.Right = Camera.main.orthographicSize * Camera.main.aspect;
        #endregion Initialize Bounds
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        int spawnUpdatesSinceStart = Mathf.FloorToInt(Time.time / spawnIntervalSeconds);
        int spawnUpdatesSinceLastUpdate = spawnUpdatesSinceStart - updateIndex;

        for (int i = 0; i < spawnUpdatesSinceLastUpdate; i++)
        {
            if (instantiationQueue.Count == 0) break;
            InstantiateCall thisCall = instantiationQueue.Dequeue();
            thisCall.OnCall(Instantiate(thisCall.prefab, thisCall.position, thisCall.rotation));
        }
        updateIndex = spawnUpdatesSinceStart;
    }

    public void SpawnCircle(GameObject prefab, Vector3 center, float radius, int count, Action<GameObject> onCall)
    {
        float dAngle = Mathf.PI * 2 / count;
        for (int i = 0; i < count; i++)
        {
            Vector3 position = center + new Vector3(
                radius * Mathf.Cos(dAngle * i),
                radius * Mathf.Sin(dAngle * i),
                0);
            InstantiateCall thisCall = new(prefab, position, Quaternion.identity, onCall);
            instantiationQueue.Enqueue(thisCall);
        }
    }

    public enum Side
    {
        Top,
        Bottom,
        Left,
        Right
    }
    public void SpawnArc(GameObject prefab, Side side, int count, Action<GameObject> onCall, bool directionPositiveToNegative = false)
    {
        switch (side)
        {
            case Side.Top:
                {
                    List<Vector3> points = GetArcPoints(count);
                    if (directionPositiveToNegative) points.Reverse();
                    foreach (Vector3 point in points)
                    {
                        InstantiateCall thisCall = new(prefab, point, Quaternion.identity, onCall);
                        instantiationQueue.Enqueue(thisCall);
                    }
                    break;
                }
            case Side.Right:
                {
                    List<Vector3> points = GetArcPoints(count, true);
                    if (directionPositiveToNegative) points.Reverse();
                    foreach (Vector3 point in points)
                    {
                        InstantiateCall thisCall = new(prefab, point, Quaternion.identity, onCall);
                        instantiationQueue.Enqueue(thisCall);
                    }
                    break;
                }
            case Side.Bottom:
                {
                    List<Vector3> points = GetArcPoints(count);
                    if (directionPositiveToNegative) points.Reverse();
                    foreach (Vector3 point in points)
                    {
                        InstantiateCall thisCall = new(prefab, new Vector3(point.x, -point.y), Quaternion.identity, onCall);
                        instantiationQueue.Enqueue(thisCall);
                    }
                    break;
                }
            case Side.Left:
                {
                    List<Vector3> points = GetArcPoints(count, true);
                    if (directionPositiveToNegative) points.Reverse();
                    foreach (Vector3 point in GetArcPoints(count, true))
                    {
                        InstantiateCall thisCall = new(prefab, new Vector3(-point.x, point.y), Quaternion.identity, onCall);
                        instantiationQueue.Enqueue(thisCall);
                    }
                    break;
                }
        }
    }

    private List<Vector3> GetArcPoints(int count, bool vertical = false)
    {
        List<Vector3> points = new();
        if (vertical)
        {
            float yStep = (bounds.Top - bounds.Bottom) / count;
            for (float y = bounds.Left; y <= bounds.Right; y += yStep)
            {
                // 0.5R( (x/U)^2 + 1 )
                float x = 0.5f * bounds.Right * (Mathf.Pow((y / bounds.Top), 2) + 1);
                points.Add(new Vector3(x,y));
            }
        }
        else
        {
            float xStep = (bounds.Right - bounds.Left) / count;
            for (float x = bounds.Left; x <= bounds.Right; x += xStep)
            {
                // 0.5U( (x/R)^2 + 1 )
                float y = 0.5f * bounds.Top * (Mathf.Pow((x / bounds.Right), 2) + 1);
                points.Add(new Vector3(x, y));
            }
        }

        return points;
    }
}
