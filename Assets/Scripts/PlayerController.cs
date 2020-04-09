using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField][Tooltip("In ms^-1")] private float m_xControlSpeed = 20.0f;
    [SerializeField] [Tooltip("In m")] private float m_xRange = 5.0f;

    [SerializeField][Tooltip("In ms^-1")] private float m_yControlSpeed = 20.0f;
    [SerializeField][Tooltip("In m")] private float m_yRange = 3.0f;

    [Header("Screen position based parameters")]
    [SerializeField] private float m_positionPitchFactor = -5.0f;
    [SerializeField] private float m_positionYawFactor = 5.0f;

    [Header("Control throw based parameters")]
    [SerializeField] private float m_controlPitchFactor = -20.0f;
    [SerializeField] private float m_controlRollFactor = -20.0f;

    private float m_xThrow;
    private float m_yThrow;

    private bool m_isControlEnabled = true;

    void Update()
    {
        if (m_isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void ProcessTranslation()
    {
        transform.localPosition = new Vector3(XClampedPos(), YClampedPos(), transform.localPosition.z);
    }

    private float XClampedPos()
    {
        m_xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = m_xThrow * m_xControlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        return Mathf.Clamp(rawXPos, -m_xRange, m_xRange);
    }

    private float YClampedPos()
    {
        m_yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = m_yThrow * m_yControlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        return Mathf.Clamp(rawYPos, -m_yRange, m_yRange);
    }

    private void ProcessRotation()
    {
        transform.localRotation = Quaternion.Euler(Pitch(), Yaw(), Roll());
    }

    private float Roll()
    {
        return m_xThrow * m_controlRollFactor;
    }

    private float Yaw()
    {
        return transform.localPosition.x * m_positionYawFactor;
    }

    private float Pitch()
    {
        float pitchDueToPosition = transform.localPosition.y * m_positionPitchFactor;
        float pitchDueToControlThrow = m_yThrow * m_controlPitchFactor;

        return pitchDueToPosition * pitchDueToControlThrow * Time.deltaTime;
    }

    private void OnPlayerDeath() //called by string ref in PlayerCollisionsHandler
    {
        m_isControlEnabled = false;
    }
}
