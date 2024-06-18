using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public MotorBase Motor;
    [SerializeField] Rigidbody m_rb;
    // Start is called before the first frame update
    void Start()
    {
        Motor.StartMotor();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Motor.trajectoryVector);
        m_rb.AddForce(transform.forward * Motor.Speed, ForceMode.Acceleration);
    }
}
