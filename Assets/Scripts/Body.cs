using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Body : MonoBehaviour
{
    public Rigidbody2D rigid;
    private SpriteRenderer rend;
    public SpringJoint2D joint;
    public float force, targetRotation;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        joint = GetComponent<SpringJoint2D>();
    }
    private void Update()
    {
        rigid.MoveRotation(Mathf.LerpAngle(rigid.rotation, targetRotation,force * Time.fixedDeltaTime));
    }
}
