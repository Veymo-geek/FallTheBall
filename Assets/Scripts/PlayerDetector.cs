using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public float yMargin = 3f; //граница

    private Transform m_Player; 
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform; //поиск игрока
    }

    void Update()
    {
        if (m_Player.position.y+ yMargin < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, m_Player.position.y+ yMargin, transform.position.z);
        }
    }
}
