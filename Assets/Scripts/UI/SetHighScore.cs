using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetHighScore : MonoBehaviour
{
    public delegate void _onSetHighScore();
    public static _onSetHighScore OnSetHighScore;

    private void Start()
    {
        SetTxtHighScore();
    }

    void SetTxtHighScore()
    {
        this.GetComponent<TMP_Text>().text = NavigationData.GetHighScore().ToString();
    }

    private void OnEnable()
    {
        OnSetHighScore += SetTxtHighScore;
    }
    private void OnDisable()
    {
        OnSetHighScore -= SetTxtHighScore;
    }
}
