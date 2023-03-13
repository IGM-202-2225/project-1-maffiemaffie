using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    private bool isFiring = false;

    [SerializeField]
    private float fireRate = 50f;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private GameObject collisionManager;

    private float sinceLastFire = 0;

    private Queue<BigFireRound> bigFireQueue = new();

    private struct BigFireRound
    {
        public int count;
        public float offset;

        public BigFireRound(int count, float offset)
        {
            this.count = count;
            this.offset = offset;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AdvanceTime();
    }

    void OnFire(InputValue fireValue)
    {
        isFiring = fireValue.isPressed;
    }

    void OnBigFire(InputValue bigFireValue)
    {
        PowerSlider slider = FindObjectOfType<PowerSlider>();
        if (slider.GetComponent<Slider>().value == 1)
        {
            slider.resetPower();
            QueueBigFire();
            GetComponent<HealthUWU>().Heal(20);
        }
    }

    void AdvanceTime()
    {
        sinceLastFire += Time.deltaTime;
        if (sinceLastFire > 1 / fireRate)
        {
            sinceLastFire = Time.deltaTime % fireRate;
            Fire();
            DoBigFire();
        }
    }

    void Fire()
    {
        if (!isFiring) return;
        
        GameObject thisProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        thisProjectile.GetComponent<BulletMove>().direction = transform.right;
        collisionManager.GetComponent<Collision>().PlayerProjectiles.Add(thisProjectile);
    }

    void DoBigFire()
    {
        if (bigFireQueue.Count > 0)
        {
            BigFire(bigFireQueue.Dequeue());
        }
    }

    void BigFire(BigFireRound round)
    {
        for (float i = round.offset; i < round.count + round.offset; i++)
        {
            float rotation = Mathf.PI * 2 / round.count * i;
            GameObject thisProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            thisProjectile.GetComponent<BulletMove>().direction = new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0);
            thisProjectile.GetComponent<BulletMove>().speed = 25;
            collisionManager.GetComponent<Collision>().PlayerProjectiles.Add(thisProjectile);
        }
    }

    void QueueBigFire()
    {
        for(int i = 0; i < 12; i++)
        {
            bigFireQueue.Enqueue(new BigFireRound(16, 0.5f * (i % 2)));
        }
    }
}
