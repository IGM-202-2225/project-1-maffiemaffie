using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatOut : MonoBehaviour
{
    private float velocity = 3f;
    private float damping = 3f;
    private float elapsed;

    [SerializeField]
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > time) Destroy(gameObject);

        transform.position += new Vector3(0, velocity) * Time.deltaTime;
        velocity -= damping * Time.deltaTime;

    }
}
