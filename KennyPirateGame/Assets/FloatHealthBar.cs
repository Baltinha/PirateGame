using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatHealthBar : MonoBehaviour
{
    [SerializeField] private Slider m_slider;
    [SerializeField] private Transform m_targetTransform;
    [SerializeField] private Vector3 m_offset;

    private Camera m_camera;


    public void UpdateHealthBar(float CurrentHealth, float MaxHealth) 
    {
        m_slider.value = CurrentHealth / MaxHealth;
    }
    // Start is called before the first frame update

    private void Awake()
    {
        m_camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = m_camera.transform.rotation;
        transform.position = m_targetTransform.position + m_offset;
    }
}
