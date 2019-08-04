using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private int _high_score = 0;
    private int score;
    public Text _score_text;
    public Text _high_score_text;

    void Start() {
        score = 0;
        _score_text.text = "";
    }

    public void IncrementScore(int p) {
        score += p;
        _score_text.text = "Score | " + score;
    }

    public int GetCurrentScore() {
        return score;
    }

    public void ResetScore() {
        if(score > _high_score) {
            _high_score = score;
            _high_score_text.text = "High Score | " + _high_score;
        }
        score = 0; 
    }

    public int GetLastScore() {
        return _high_score;
    }

}
