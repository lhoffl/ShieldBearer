using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    private int _last_score = 0;
    private int score;
    public Text _score_text;

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

    public int GetLastScore() {
        _last_score = score;
        return _last_score;
    }

}
