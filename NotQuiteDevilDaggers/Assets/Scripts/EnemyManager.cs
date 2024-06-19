using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    [SerializeField] private GameObject m_enemyPrefab;

    public void SpawnEnemy()
    {
        EnemyList.Add(Instantiate(m_enemyPrefab,transform.position,Quaternion.identity));
    }

    public void ClearEnemies()
    {
        foreach (GameObject obj in EnemyList)
        {
            Destroy(obj);
        }
        EnemyList.Clear();
    }

    public void PauseEnemies()
    {
        foreach (GameObject obj in EnemyList)
        {
            obj.GetComponent<MotorBase>().StopMotor();
        }
    }
}
