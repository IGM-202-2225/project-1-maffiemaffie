using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    private float duration;
    private float sinceStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (0, 0, -1);
        FindObjectOfType<GameManager>().GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (sinceStart > duration) return;
        
        sinceStart += Time.deltaTime;
        Color color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, sinceStart / duration);
    }
}
