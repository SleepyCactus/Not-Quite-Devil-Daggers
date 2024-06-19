using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFlyingMotor : MotorBase
{
    public GameObject t_target;
    [SerializeField] private float m_lookEasing = 0.5f;
    [SerializeField] private float m_maxEasing = 0.5f;
    [SerializeField] LayerMask m_avoidanceLayer;
    [SerializeField] float m_distScalar;
    [SerializeField] float m_maxDist;
    [SerializeField] float m_wavePower;
    [SerializeField] float m_waveSpeed;
    Vector3 avoidanceVector = Vector3.zero;
    

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

        m_distScalar = Mathf.Clamp(Mathf.Pow(Mathf.Clamp(Vector3.Distance(t_target.transform.position, transform.position), 0, m_maxDist) / m_maxDist,2),0,0.8f);

        Collider[] collisions = Physics.OverlapSphere(transform.position,4f,m_avoidanceLayer);
        avoidanceVector = Vector3.zero;
        foreach (Collider c in collisions)
        {
            Vector3 v = (transform.position - c.transform.position);
            avoidanceVector += v * Mathf.Abs(v.magnitude - 4f)/4f;
        }
        
        Vector3 offset = new Vector3(0, (Mathf.Sin(Time.fixedTime * m_waveSpeed)* m_distScalar * m_wavePower), 0);
        Vector3 directTrajectory = Vector3.Lerp((t_target.transform.position - transform.position).normalized* Speed, offset,m_distScalar) + (avoidanceVector);
        trajectoryVector = Vector3.Lerp(trajectoryVector, directTrajectory, Mathf.Lerp(m_lookEasing,m_maxEasing,m_distScalar));
        
    }

    private void OnDrawGizmos()
    {
        if(m_debugLines)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, trajectoryVector);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, avoidanceVector);

        }
    }
}
