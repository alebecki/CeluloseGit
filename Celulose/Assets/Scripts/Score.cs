using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int scoreValue;
    TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI endScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        score = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = scoreValue.ToString();
        endScore.text = scoreValue.ToString();
    }
}
