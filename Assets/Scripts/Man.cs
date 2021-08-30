using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

public class Man : MonoBehaviour
{
    public Body fab_head, fab_leg, fab_arm, fab_torso;
    private Body leg_high_l, leg_high_r, leg_low_r, leg_low_l, foot_r, foot_l, torso,head,arm_low_l, arm_low_r, arm_high_l, arm_high_r,palm_l,palm_r;
    private void Start()
    {
        leg_high_l = Instantiate(this.fab_leg, this.transform.position+new Vector3(0.02f, -0.2f, 0), this.transform.rotation);
        leg_high_r = Instantiate(this.fab_leg, this.transform.position + new Vector3(-0.02f, -0.2f, 0), this.transform.rotation);
        leg_low_r = Instantiate(this.fab_leg, this.transform.position + new Vector3(-0.02f, -0.3f, 0), this.transform.rotation);
        leg_low_l = Instantiate(this.fab_leg, this.transform.position + new Vector3(0.02f, -0.3f, 0), this.transform.rotation);
        foot_r = Instantiate(this.fab_leg, this.transform.position + new Vector3(0.02f, -0.4f, 0), this.transform.rotation);
        foot_l = Instantiate(this.fab_leg, this.transform.position + new Vector3(0.02f, -0.4f, 0), this.transform.rotation);
        torso = Instantiate(this.fab_torso, this.transform.position, this.transform.rotation);
        head = Instantiate(this.fab_head, this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation);
        arm_low_l = Instantiate(this.fab_arm, this.transform.position + new Vector3(0.2f, 0.1f, 0), this.transform.rotation);
        arm_low_r = Instantiate(this.fab_arm, this.transform.position + new Vector3(-0.2f, 0.1f, 0), this.transform.rotation);
        arm_high_l = Instantiate(this.fab_arm, this.transform.position + new Vector3(0.2f, -0.1f, 0), this.transform.rotation);
        arm_high_r = Instantiate(this.fab_arm, this.transform.position + new Vector3(-0.2f, -0.1f, 0), this.transform.rotation);
        palm_l = Instantiate(this.fab_arm, this.transform.position + new Vector3(0.2f, -0.3f, 0), this.transform.rotation);
        palm_r = Instantiate(this.fab_arm, this.transform.position + new Vector3(-0.2f, -0.3f, 0), this.transform.rotation);
        leg_high_l.joint.connectedBody = torso.rigid;
        leg_high_r.joint.connectedBody = torso.rigid;
        arm_high_l.joint.connectedBody = torso.rigid;
        arm_high_r.joint.connectedBody = torso.rigid;
        head.joint.connectedBody = torso.rigid;
        arm_low_r.joint.connectedBody = arm_high_r.rigid;
        arm_low_l.joint.connectedBody = arm_high_l.rigid;
        palm_r.joint.connectedBody = arm_low_r.rigid;
        palm_l.joint.connectedBody = arm_low_l.rigid;
        leg_high_r.joint.connectedBody = torso.rigid;
        leg_low_l.joint.connectedBody= leg_high_l.rigid;
        leg_low_r.joint.connectedBody= leg_high_r.rigid;
        foot_l.joint.connectedBody= leg_low_l.rigid;
        foot_r.joint.connectedBody= leg_low_r.rigid;
        leg_high_l.joint.distance = 0.02f;
        leg_high_r.joint.distance = 0.02f;
        arm_high_l.joint.distance = 0.02f;
        arm_high_r.joint.distance = 0.02f;
        head.joint.distance = 0.02f;
    }
    private void Update()
    {
        torso.rigid.AddTorque(2.0f);
    }
}