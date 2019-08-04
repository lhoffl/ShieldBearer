using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private int score = 0;    
    public Text _score_text;

    void Start() {
        _score_text.text = "Score | " + score;
    }

    public void IncrementScore(int p) {
        score += p;
        _score_text.text = "Score | " + score;
    }
}
