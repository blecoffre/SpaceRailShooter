using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionsHandler : MonoBehaviour
{
    [SerializeField] private float m_levelLoadDelay = 3.0f;

    [SerializeField] private GameObject m_explosionSystem = default;
    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        DeathEffect();
        Invoke("ReloadLevel", m_levelLoadDelay);
    }

    private void DeathEffect()
    {
        m_explosionSystem?.SetActive(true);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
