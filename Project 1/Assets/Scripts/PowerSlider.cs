using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
    public int Power;
    [SerializeField]
    private int maxPower = 100;
    [SerializeField]
    private GameObject pressSpace;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().value = 0;
    }

    public void increasePower(int amount)
    {
        Power += amount;
        Power = Mathf.Clamp(Power, 0, maxPower);
        GetComponent<Slider>().value = (float)Power / maxPower;

        if  (Power == maxPower)
        {
            pressSpace.GetComponent<Image>().color = Color.white;
        }
    }

    public void resetPower()
    {
        Power = 0;
        GetComponent<Slider>().value = (float)Power / maxPower;
        pressSpace.GetComponent<Image>().color = Color.clear;
    }
}
