using System;
using System.Collections.Generic;
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

    [SerializeField]
    private float spawnIntervalSeconds;

    [SerializeField]
    private GameObject indicatorPrefab;

    private Queue<InstantiateCall> instantiationQueue = new();
    private int updateIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

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

    public void SpawnCircle(GameObject prefab, float radius, int count, Action<GameObject> onCall)
    {
        float dAngle = Mathf.PI * 2 / count;
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(
                radius * Mathf.Cos(dAngle * i),
                radius * Mathf.Sin(dAngle * i),
                0);
            InstantiateCall thisCall = new(prefab, position, Quaternion.identity, onCall);
            instantiationQueue.Enqueue(thisCall);
        }
    }
}
