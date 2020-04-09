using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreValue = default;

    private int m_score = 0;

    private void Awake()
    {
        SetScore(); //Initial set
    }

    public void ScoreHit(int increment)
    {
        m_score += increment;
        SetScore();
    }

    private void SetScore()
    {
        m_scoreValue?.SetText(m_score.ToString());
    }
}
