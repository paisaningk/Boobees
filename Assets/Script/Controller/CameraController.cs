using System;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class CameraController : MonoBehaviour
    {
        private Func<Vector3> getCameraPoitionFunc;
        [SerializeField] private Transform Player;
        
        // Start is called before the first frame update
        void Start()
        {
            getCameraPoitionFunc = () => Player.position;
        }

        // Update is called once per frame
        void Update()
        {
            var cameraFollwPoition = getCameraPoitionFunc();
            cameraFollwPoition.z = transform.position.z;

            Vector3 cameraMoveDir = (cameraFollwPoition - transform.position).normalized;
            float disance = Vector3.Distance(cameraFollwPoition,transform.position);
            float camraMoveSpped = 1;

            if (disance > 0 )
            {
                Vector3 newCameraPointon = transform.position + cameraMoveDir * disance * camraMoveSpped * Time.deltaTime;

                float distanceAfterMoving = Vector3.Distance(newCameraPointon,cameraFollwPoition);

                if (distanceAfterMoving > disance)
                {
                    //Overshot the target 
                    newCameraPointon = cameraFollwPoition;
                }

                transform.position = newCameraPointon;
            }
        }

        private void CreateDashEffect(Vector3 position, Vector3 dir, float dashSize)
        {
            
        }
    }
}
