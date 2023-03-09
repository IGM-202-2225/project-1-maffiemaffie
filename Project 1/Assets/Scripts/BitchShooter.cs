using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitchShooter : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    public GameObject collisionManager;

    [SerializeField]
    private float fireRate = 1f;

    private float sinceLastFire = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        sinceLastFire += Time.deltaTime;
        if (sinceLastFire > 1 / fireRate)
        {
            sinceLastFire %= (1 / fireRate);
            GameObject thisProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            thisProjectile.GetComponent<BulletMove>().direction = (player.transform.position - thisProjectile.transform.position).normalized;
            collisionManager.GetComponent<Collision>().EnemyProjectiles.Add(thisProjectile);
        }
    }
}
