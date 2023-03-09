using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUWU : MonoBehaviour
{
    public int MaxHealth;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Destroy(gameObject);
    }

    public void Hurt(int amount)
    {
        health -= amount;
    }
}
