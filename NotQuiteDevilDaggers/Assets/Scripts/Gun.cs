using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_gunpoint;
    [SerializeField] float spread;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < 15; i++)
            {
                GameObject bullet = Instantiate(m_bullet, m_gunpoint.position, m_gunpoint.rotation);
                var randomNumberX = Random.Range(-spread, spread);
                var randomNumberY = Random.Range(-spread, spread);
                var randomNumberZ = Random.Range(-spread, spread);
                bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            }
            
        }
    }
}
