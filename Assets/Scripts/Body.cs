using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Body : MonoBehaviour
{
    public Rigidbody rigid;
    private SpriteRenderer rend;
    public HingeJoint joint;
    public float force, targetRotation;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rend = GetComponent<SpriteRenderer>();
        joint = GetComponent<HingeJoint>();
        rigid.WakeUp();
    }
    private void Update()
    {
        Vector3 eulerRotation = new Vector3(0, 0, targetRotation);
        Quaternion deltaRotation = Quaternion.Euler(eulerRotation);
        rigid.MoveRotation(deltaRotation);
        //rigid.MoveRotation(Mathf.LerpAngle(rigid.rotation, targetRotation, force * Time.fixedDeltaTime)); FOR 2D
    }
}
