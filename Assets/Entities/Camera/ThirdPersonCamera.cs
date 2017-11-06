// Author: Mathias Dam Hedelund
// Contributors: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
    public class ThirdPersonCamera : MonoBehaviour 
    {
        public GameObject target;
        public float damping = 1;
        
        private Vector3 offset;
        private bool active = false;
    
        void Start()
        {
            offset = transform.position - target.transform.position;
        }
        
        void LateUpdate()
        {
            if (!active)
            {
                return;
            }

            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
            transform.position = position;
    
            transform.LookAt(target.transform.position);
        }

        public void SetActive(bool value)
        {
            active = value;
        }
    }
}