using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUWU : MonoBehaviour
{
    public int MaxHealth;
    private int health;

    [SerializeField]
    private List<GameObject> spawnOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
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
        }
    }

    public void Hurt(int amount)
    {
        health -= amount;
    }
}
