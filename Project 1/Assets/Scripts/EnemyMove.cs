using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPlayer = (player.transform.position - transform.position).normalized;
        transform.position += toPlayer * speed * Time.deltaTime;
    }
}
