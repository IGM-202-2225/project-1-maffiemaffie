using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : HealthUWU
{
    [SerializeField]
    private GameObject slider;
    public override void Hurt(int amount)
    {
        base.Hurt(amount);
        slider.GetComponent<Slider>().value = (float)health / maxHealth;
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        slider.GetComponent<Slider>().value = (float)health / maxHealth;
    }
}
