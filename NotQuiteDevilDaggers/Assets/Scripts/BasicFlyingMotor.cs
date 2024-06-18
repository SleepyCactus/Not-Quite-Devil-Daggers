using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFlyingMotor : MotorBase
{
    public GameObject t_target;
    [SerializeField] private float m_lookEasing = 0.5f;
    
    public override IEnumerator Tick()
    {
        while (true)
        {
            yield return null;
            UpdateMotor();
            
        }
        

    }
    public override void StartMotor()
    {
        if (m_tick == null)
        {
            m_tick = StartCoroutine("Tick");
        }
        else 
        {
            StartCoroutine(m_tick.ToString());
        }
        
    }
    public override void StopMotor()
    {
        StopCoroutine(m_tick);
    }
    public override void UpdateMotor()
    {
        Vector3 directTrajectory = (t_target.transform.position - transform.position).normalized;
        trajectoryVector = Vector3.Lerp(trajectoryVector, directTrajectory, m_lookEasing);
        
    }

    private void OnDrawGizmos()
    {
        if(m_debugLines)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, trajectoryVector);

        }
    }
}
