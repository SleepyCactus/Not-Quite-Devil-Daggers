using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    [SerializeField] private GameObject m_enemyPrefab;

    public void SpawnEnemy(Vector3 _position)
    {
        EnemyList.Add(Instantiate(m_enemyPrefab,_position,Quaternion.identity));
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

    public Vector3 RandomPointOnCircleEdge(float _radius)
    {
        Vector3 v = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
        return v * _radius;
    }
}
