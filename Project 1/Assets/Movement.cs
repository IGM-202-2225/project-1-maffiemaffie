using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 direction = Vector3.right;

    private Vector3 Position
    {
        get
        {
            float _x = (transform.position.x + 10.0f) / 20.0f;
            float _y = (transform.position.y + 5.0f) / 10.0f;

            float scaledX = Mathf.LerpUnclamped(0, Screen.width, _x);
            float scaledY = Mathf.LerpUnclamped(0, Screen.height, _y);

            return new Vector3(scaledX, scaledY, transform.position.z);
        }

        set
        {
            float _x = value.x / Screen.width;
            float _y = value.y / Screen.height;

            float scaledX = Mathf.LerpUnclamped(-10.0f, 10.0f, _x);
            float scaledY = Mathf.LerpUnclamped(-5.0f, 5.0f, _y);

            transform.position = new Vector3(scaledX, scaledY, value.z);
        }
    }

    [SerializeField]
    float strength = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 toCursor = mousePos - Position;
        direction = Vector3.Normalize(toCursor);

        acceleration = strength * toCursor.sqrMagnitude * direction;

        velocity += acceleration * Time.deltaTime;
        Position += velocity * Time.deltaTime;
    }
}
