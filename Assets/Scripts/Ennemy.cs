using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField] private GameObject m_deathEffect = default;
    [SerializeField] private Transform m_effectParent = default;
    [SerializeField] private int m_scoreValue = 100;

    private BoxCollider m_collider;

    private void Awake()
    {
        AddNonTriggerBoxCollider();
    }

    private void AddNonTriggerBoxCollider()
    {
        m_collider = GetComponent<BoxCollider>();
        if(m_collider == null)
            m_collider = gameObject.AddComponent<BoxCollider>();

        m_collider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        OnDeathActions();
    }

    private void OnDeathActions()
    {
        OnDeathEffect();
        AddScore();
        Destroy(gameObject);
    }

    private void OnDeathEffect()
    {
        GameObject go = Instantiate(m_deathEffect, transform.position, Quaternion.identity);
        go.transform.SetParent(m_effectParent);
    }

    private void AddScore()
    {
        FindObjectOfType<ScoreBoard>().ScoreHit(m_scoreValue);
    }
}
