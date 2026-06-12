using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pontucao : MonoBehaviour
{
    float points = 0;
    public TextMeshProUGUI scoreText;


    void Start()
    {
        scoreText.text = "0";
    }
    public void AddPoints()
    {
        points++;
        scoreText.text = points.ToString();
    }
}
