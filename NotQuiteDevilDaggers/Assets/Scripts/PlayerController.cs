using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject m_loseCam;
    [SerializeField] private GameObject m_winCam;
    public Transform CamRef;
    private bool m_died = false;
    public void Die()
    {
        GameManager.Instance.EndGame(true);
    }

    public void SpawnPostCam(bool _hasDied)
    {
        if(_hasDied)
        {
            GameManager.Instance.ObjectsToDestroy.Add(Instantiate(m_loseCam, CamRef.position, CamRef.rotation));
        }
        else
        {
            GameManager.Instance.ObjectsToDestroy.Add(Instantiate(m_winCam, CamRef.position, CamRef.rotation));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Col");
        if(collision.gameObject.tag == "Fatal")
        {
            if(!m_died)
            {
                m_died = true;
                Die();
            }
            
        }
    }
}
