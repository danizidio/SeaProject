using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreTxt, _highScoreTxt;
    [SerializeField] int _currentScore, _highScore;

    private void OnEnable()
    {
        _currentScore = GameBehaviour.GetScore();
        _highScore = NavigationData.GetHighScore();

        if(_currentScore > _highScore)
        {
            _highScore = _currentScore;
        }

        _scoreTxt.text = _currentScore.ToString();
        _highScoreTxt.text = _highScore.ToString();
    }

    private void OnDisable()
    {
        _currentScore = 0;
        _highScore = 0;
    }
}
