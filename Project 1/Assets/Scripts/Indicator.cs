using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField]
    private float lifetimeSeconds;

    private float timeAtSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeAtSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeAtSpawn < lifetimeSeconds) return;
        Destroy(gameObject);
    }
}
