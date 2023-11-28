using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_rotatespeed;

    private float m_Horizontal;
    private float m_Vertical;
    private Transform m_Sprite;


    private const string k_Horizontal = "Horizontal";
    private const string k_Vertical = "Vertical";
    void Start()
    {
        m_Sprite = GetComponentInChildren<Transform>().GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        m_Horizontal = Input.GetAxisRaw(k_Horizontal);
        m_Vertical = Input.GetAxisRaw(k_Vertical);

        Vector2 movement = new Vector2(m_Horizontal, m_Vertical);
        float InputMagnitude = Mathf.Clamp01(movement.magnitude);
        movement.Normalize();

        transform.Translate(movement * m_speed * InputMagnitude * Time.deltaTime, Space.World);

        if (movement != Vector2.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movement);
            m_Sprite.transform.rotation = Quaternion.RotateTowards(m_Sprite.transform.rotation,rotation, m_rotatespeed * Time.deltaTime);
        }
    }

}
