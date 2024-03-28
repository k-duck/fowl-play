using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DsyncScript : MonoBehaviour
{
    private Transform target;
    private Rigidbody rb;

    private GameController controller;
    private GameObject settings;
    public GameObject left;
    public GameObject right;

    public bool hitWall = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");
        controller = objs[0].GetComponent<GameController>();

        settings = GameObject.Find("settings_Button");

        rb = GetComponent<Rigidbody>();

        if (controller.GetHand() == 0)
        {
            target = right.transform;
        }
        else
        {
            target = left.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetHand() == 0)
        {
            target = left.transform;
        }
        else
        {
            target = right.transform;
        }

        if (hitWall)
        {
            controller.SetMoveBool(false);
        }
        else
        {
            var prev_move = Convert.ToBoolean(settings.GetComponent<SettingsScript>().movType_prev);
            controller.SetMoveBool(prev_move);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = ((target.position - transform.position) / Time.fixedUnscaledDeltaTime) / 2;

        Quaternion rotationDiff = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);


        Vector3 rotationDiffInDeg = angleInDegree * rotationAxis;
        rb.angularVelocity = (rotationDiffInDeg * Mathf.Deg2Rad / Time.fixedUnscaledDeltaTime);

        var centerPoint_handL_rig = rb.transform.InverseTransformPoint(target.transform.position);

        if (centerPoint_handL_rig.x <= -0.5 || centerPoint_handL_rig.x >= 0.5 || centerPoint_handL_rig.z <= -0.5 || centerPoint_handL_rig.z >= 0.5 || centerPoint_handL_rig.y <= -0.5 || centerPoint_handL_rig.y >= 0.5)
        {
            hitWall = true;
        }
        else
        {
            hitWall = false;
        }

        //Debug.Log("hitWall: " + centerPoint_handL_rig);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            hitWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            hitWall = false;
        }
    }
}
