using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody m_rb;
    [SerializeField] GameObject m_body;
    [SerializeField] float speed;
    [SerializeField] float m_lifetime;
    // Start is called before the first frame update
    void Start()
    {
        m_body.transform.rotation = Random.rotation;
        m_rb.AddForce(transform.forward * speed,ForceMode.VelocityChange);
    }
    private void Update()
    {
        m_lifetime -= Time.deltaTime;
        if(m_lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void KillBullet()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.tag = "default";
        Invoke("KillBullet", 0.5f);
    }
}
