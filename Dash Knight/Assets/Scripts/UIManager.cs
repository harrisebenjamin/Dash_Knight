using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _winText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _winText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Updates the user score on the HUD
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    //Displays the congratulations text when the player wins
    public void ShowWinText()
    {
        _winText.enabled = true;
    }
}
