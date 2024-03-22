using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class LerpMovemen : MonoBehaviour
    {
        public ActionBasedContinuousMoveProvider movement;

        Vector2 input;
        public float currentSpeed;
        public float maxSpeed;
        public float startSpeed;
        public float stopSpeed;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

           var inputLeft = movement.leftHandMoveAction.action.ReadValue<Vector2>();
           var inputRight = movement.rightHandMoveAction.action.ReadValue<Vector2>();
            input = inputLeft + inputRight;

            if(input != Vector2.zero)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * startSpeed);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * stopSpeed);
            }


            movement.moveSpeed = currentSpeed;
            //Debug.Log(movement.moveSpeed);
        }
    }
}
