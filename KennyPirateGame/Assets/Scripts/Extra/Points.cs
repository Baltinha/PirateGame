using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    [Header("Points Text")]
    [SerializeField] private TMPro.TMP_Text m_TextPoints;

    void Update()
    {
        m_TextPoints.text = GameManager.Instance.PointsInGame.ToString("0");
    }
}
