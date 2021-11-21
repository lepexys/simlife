using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Man : MonoBehaviour
{
    enum State { Idle,Walk, Turn,Run, Sit, Crawl};
    bool is_left = true;
    State state;
    float walk_cicle = 0.0f,posture = 7.0f;
    public Body fab_head, fab_leg_low, fab_leg_high, fab_foot, fab_arm_high, fab_arm_low, fab_palm, fab_torso;
    [HideInInspector]
    public Body leg_high_l, leg_high_r, leg_low_r, leg_low_l, foot_r, foot_l, torso,head,arm_low_l, arm_low_r, arm_high_l, arm_high_r,palm_l,palm_r;
    private List<Body> parts;
    private void Start()
    {
        state = 0;
        leg_high_l = Instantiate(this.fab_leg_high, this.transform.position+new Vector3(0, -0.2f, 0.1f), this.transform.rotation);
        leg_high_r = Instantiate(this.fab_leg_high, this.transform.position + new Vector3(0, -0.2f, -0.1f), this.transform.rotation);
        leg_low_r = Instantiate(this.fab_leg_low, this.transform.position + new Vector3(0, -0.3f, -0.1f), this.transform.rotation);
        leg_low_l = Instantiate(this.fab_leg_low, this.transform.position + new Vector3(0, -0.3f, 0.1f), this.transform.rotation);
        foot_r = Instantiate(this.fab_foot, this.transform.position + new Vector3(0, -0.4f, -0.1f), this.transform.rotation);
        foot_l = Instantiate(this.fab_foot, this.transform.position + new Vector3(0, -0.4f, 0.1f), this.transform.rotation);
        torso = Instantiate(this.fab_torso, this.transform.position, this.transform.rotation);
        head = Instantiate(this.fab_head, this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation);
        arm_low_l = Instantiate(this.fab_arm_low, this.transform.position + new Vector3(0, -0.1f, 0.1f), this.transform.rotation);
        arm_low_r = Instantiate(this.fab_arm_low, this.transform.position + new Vector3(0, -0.1f, -0.1f), this.transform.rotation);
        arm_high_l = Instantiate(this.fab_arm_high, this.transform.position + new Vector3(0, 0.1f, 0.1f), this.transform.rotation);
        arm_high_r = Instantiate(this.fab_arm_high, this.transform.position + new Vector3(0, 0.1f, -0.1f), this.transform.rotation);
        palm_l = Instantiate(this.fab_palm, this.transform.position + new Vector3(0, -0.2f, 0.1f), this.transform.rotation);
        palm_r = Instantiate(this.fab_palm, this.transform.position + new Vector3(0, -0.2f, -0.1f), this.transform.rotation);
        leg_high_l.gameObject.gameObject.layer = 6;
        leg_low_l.gameObject.layer = 6;
        foot_l.gameObject.layer = 6;
        arm_low_l.gameObject.layer = 6;
        arm_high_l.gameObject.layer = 6;
        palm_l.gameObject.layer = 6;
        torso.gameObject.layer = 7;
        head.gameObject.layer = 7;
        leg_high_r.gameObject.layer = 8;
        leg_low_r.gameObject.layer = 8;
        foot_r.gameObject.layer = 8;
        arm_low_r.gameObject.layer = 8;
        arm_high_r.gameObject.layer = 8;
        palm_r.gameObject.layer = 8;
        arm_high_l.joint.connectedAnchor += new Vector2(0, 0.1f);
        arm_high_r.joint.connectedAnchor += new Vector2(0, 0.1f);
        leg_high_l.joint.connectedAnchor += new Vector2(0, -0.01f);
        leg_high_r.joint.connectedAnchor += new Vector2(0, -0.01f);
        leg_high_l.joint.connectedBody = torso.rigid;
        leg_high_r.joint.connectedBody = torso.rigid;
        arm_high_l.joint.connectedBody = torso.rigid;
        arm_high_r.joint.connectedBody = torso.rigid;
        head.joint.connectedBody = torso.rigid;
        arm_low_r.joint.connectedBody = arm_high_r.rigid;
        arm_low_l.joint.connectedBody = arm_high_l.rigid;
        palm_r.joint.connectedBody = arm_low_r.rigid;
        palm_l.joint.connectedBody = arm_low_l.rigid;
        leg_low_l.joint.connectedBody= leg_high_l.rigid;
        leg_low_r.joint.connectedBody= leg_high_r.rigid;
        foot_l.joint.connectedBody= leg_low_l.rigid;
        foot_r.joint.connectedBody= leg_low_r.rigid;
        leg_high_l.joint.distance = 0.02f;
        leg_high_r.joint.distance = 0.02f;
        arm_high_l.joint.distance = 0.02f;
        arm_high_r.joint.distance = 0.02f;
        head.joint.distance = 0.02f;
        torso.targetRotation = posture;
        parts = new List<Body> { leg_high_l, leg_high_r, leg_low_r, leg_low_l, foot_r, foot_l, torso,head,arm_low_l, arm_low_r, arm_high_l, arm_high_r,palm_l,palm_r};
    }
    public void Move(float dist)
    {
        for(int i = 0;i< parts.Count; i++)
        {
            parts[i].transform.position += new Vector3(0,0,dist);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!is_left)
                state = State.Turn;
            else
                state = State.Walk;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (is_left)
                state = State.Turn;
            else
                state = State.Walk;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            walk_cicle = 0.0f;
            leg_high_l.targetRotation = 0.0f;
            leg_high_r.targetRotation = 0.0f;
            leg_low_r.targetRotation = 0.0f;
            leg_low_l.targetRotation = 0.0f;
            if (state == State.Walk)
                state = State.Idle;
        }
    }
    private void FixedUpdate()
    {
        float max_angle = 30.0f,add_angle = 50.0f,speed = 6.0f,force = 30.0f;
        if (state == State.Walk)
        {
            walk_cicle += Time.fixedDeltaTime* speed;
            walk_cicle = (walk_cicle > Mathf.PI * 2) ? 0.0f : walk_cicle;
            int sign = is_left ? 1 : -1;
            if (Mathf.Sin(walk_cicle) * max_angle * sign < sign * leg_high_l.targetRotation)
            {
                leg_low_l.targetRotation = (sign * Mathf.Cos(Mathf.Sin(walk_cicle) * Mathf.PI/2) * add_angle) + Mathf.Sin(walk_cicle) * max_angle;
                leg_low_r.targetRotation = -Mathf.Sin(walk_cicle) * max_angle;
            }
            else
            {
                leg_low_r.targetRotation = (sign * Mathf.Cos(Mathf.Sin(walk_cicle) * Mathf.PI/2) * add_angle) - Mathf.Sin(walk_cicle) * max_angle;
                leg_low_l.targetRotation = Mathf.Sin(walk_cicle) * max_angle;
            }
            leg_high_l.targetRotation = Mathf.Sin(walk_cicle) * max_angle;
            leg_high_r.targetRotation = -leg_high_l.targetRotation;
            torso.rigid.AddForce(is_left? Vector2.left * force : Vector2.right* force);
        }
        if (state == State.Turn)
        {
            posture = -posture;
            state = State.Walk;
            is_left = !is_left;
            torso.targetRotation = posture;
            walk_cicle = 0.0f;
            leg_high_l.targetRotation = 0.0f;
            leg_high_r.targetRotation = 0.0f;
            foot_r.targetRotation = -foot_r.targetRotation;
            foot_l.targetRotation = -foot_l.targetRotation;
        }
    }
}