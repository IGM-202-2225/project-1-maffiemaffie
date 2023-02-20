using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    private Vector3 mousePos;

    [SerializeField]
    private Camera mainCam;

    [SerializeField]
    private float MaxVelocity = 10f;

    [SerializeField]
    float strength = 10.0f;

    [SerializeField]
    float damping = 0.9f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        Vector3 mouseWorldSpace = mainCam.ScreenToWorldPoint(mousePos);
        Vector3 toCursor = mouseWorldSpace - transform.position;
        direction = (direction + 0.5f * Time.deltaTime * toCursor).normalized;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(toCursor.y, toCursor.x));
        acceleration = strength * toCursor.magnitude * direction;

        velocity += acceleration * Time.deltaTime;
        if (velocity.sqrMagnitude > MaxVelocity * MaxVelocity) velocity = velocity.normalized * MaxVelocity;

        velocity *= Mathf.Pow(damping, Time.deltaTime);

        position += velocity * Time.deltaTime;
        transform.position = new Vector3(position.x, position.y, 0);
    }

    void OnMove(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }
}
