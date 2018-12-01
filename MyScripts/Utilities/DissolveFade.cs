using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolveFade : MonoBehaviour
{

    Image m_img;

    float m_currentThreshold = 1;
    float m_targetThreshold = 0;

    [Range(0, 1)]
    [SerializeField] float m_dissolveSpeedMultiplier = 0.5f;

    void Start()
    {
        m_img = GetComponentInChildren<Image>();
        m_img.material.SetFloat("_DissolveScale", m_currentThreshold);

    }

    void Update()
    {
        if (m_currentThreshold < 0.0001f) Destroy(gameObject);
        m_currentThreshold = Mathf.Lerp(m_currentThreshold, m_targetThreshold, 0.05f * m_dissolveSpeedMultiplier);
        m_img.material.SetFloat("_DissolveScale", m_currentThreshold);
    }
}
