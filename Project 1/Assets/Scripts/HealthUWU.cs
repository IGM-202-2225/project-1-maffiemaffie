using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUWU : MonoBehaviour
{
    [SerializeField]
    protected int maxHealth;
    [SerializeField]
    private float cooldown;
    protected int health;

    [SerializeField]
    private int increaseScore;

    private float sinceLastHurt = 0;

    [SerializeField]
    private List<GameObject> spawnOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            foreach (GameObject gameObject in spawnOnDeath)
            {
                Instantiate(gameObject, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            FindObjectOfType<GameManager>().IncrementScore(increaseScore);
        }
    }

    public virtual void Hurt(int amount)
    {
        sinceLastHurt += Time.deltaTime;
        if (sinceLastHurt > cooldown)
        {
            health -= amount;
            sinceLastHurt %= cooldown;
        }

    }

    public virtual void Heal(int amount)
    {
        health += amount;
    }
}
