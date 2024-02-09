using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtController : MonoBehaviour
{
    public Transform objectToLookAt;
    public float headweight;
    public float bodyWeight;
    [SerializeField]private Animator animator;


    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(objectToLookAt.position);
        animator.SetLookAtWeight(1, bodyWeight, headweight);
    }
}
