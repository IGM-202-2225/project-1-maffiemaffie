using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRedButThenDontAnymore : MonoBehaviour
{
    [SerializeField]
    private float duration;

    [SerializeField]
    private Color color = Color.red;

    private float timeSinceStart;
    private bool isFlashing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFlashing) return;
        timeSinceStart += Time.deltaTime;

        if (timeSinceStart >= duration)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            isFlashing = false;
        }
    }

    public void Flash()
    {
        GetComponent<SpriteRenderer>().color = color;
        timeSinceStart = 0;
        isFlashing = true;
    }
}
