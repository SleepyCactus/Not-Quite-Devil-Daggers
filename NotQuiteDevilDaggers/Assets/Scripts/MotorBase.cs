using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotorBase : MonoBehaviour
{
    [SerializeField] protected bool m_debugLines;
    protected Coroutine m_tick;
    [HideInInspector] public Vector3 trajectoryVector;
    public float Speed;
    abstract public IEnumerator Tick();
    abstract public void UpdateMotor();
    abstract public void StopMotor();
    abstract public void StartMotor();

}