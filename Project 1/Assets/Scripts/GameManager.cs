using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Score;
    [SerializeField]
    private List<GameObject> digitLabels;
    [SerializeField]
    private GameObject playAgainButton;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementScore(int amount)
    {
        Score += amount;
        foreach(GameObject digit in digitLabels)
        {
            digit.GetComponent<ScoreNumber>().Clear();
        }

        for (int i = digitLabels.Count - 1, digits = Score; digits > 0 && i >= 0; digits /= 10, i--)
        {
            int thisDigit = digits % 10;
            digitLabels[i].GetComponent<ScoreNumber>().SetDigit(thisDigit);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        playAgainButton.GetComponent<Image>().color = Color.white;
        playAgainButton.GetComponent<Button>().interactable = true;
    }
}
