using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public float speed;

    public Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
        //Debug.Log("Grip!");
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
        //Debug.Log("Trigger!");
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            //gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.unscaledDeltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent );   ////////////Trigger poke animation
        }

        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.unscaledDeltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }
}
