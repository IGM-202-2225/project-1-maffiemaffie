using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    private bool isFiring = false;

    [SerializeField]
    float fireRate = 50f;

    [SerializeField]
    GameObject projectile;

    float sinceLastFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void OnFire(InputValue fireValue)
    {
        isFiring = fireValue.isPressed;
    }

    void Fire()
    {
        sinceLastFire += Time.deltaTime;
        if (!isFiring) return;
        
        if (sinceLastFire > 1 / fireRate)
        {
            sinceLastFire %= 1 / fireRate;
            GameObject thisProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            thisProjectile.GetComponent<BulletMove>().direction = transform.right;
        }
    }
}
