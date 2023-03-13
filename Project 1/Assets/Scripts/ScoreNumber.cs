using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreNumber : MonoBehaviour
{
    public List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDigit(int digit)
    {
        GetComponent<Image>().sprite = sprites[digit];
        GetComponent<Image>().color = Color.white;
    }

    public void Clear()
    {
        SetDigit(0);
        //GetComponent<Image>().color = Color.clear;
    }
}
