using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public float speed = 10f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * direction * Time.deltaTime;
    }
}
