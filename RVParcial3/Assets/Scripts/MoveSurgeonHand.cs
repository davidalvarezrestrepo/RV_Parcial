using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSurgeonHand : MonoBehaviour
{
        public Transform objectToMoveAndRotate;
        public Transform objectToMoveToCamera;
        public float moveSpeed = 2f;
        public float rotateSpeed = 5f;

        private Vector3 originalLocalPos1;
        private Quaternion originalLocalRot1;
        private Vector3 originalPos2;

        private Vector3 targetLocalPosition = new Vector3(0.70599997f, 1.18400002f, -0.224999994f);
        private Quaternion targetLocalRotation = new Quaternion(-0.550710917f, -0.449380666f, -0.475682467f, 0.518170536f);

        private bool isToggled = false;

        void Start()
        {
            if (objectToMoveAndRotate != null)
            {
                originalLocalPos1 = objectToMoveAndRotate.localPosition;
                originalLocalRot1 = objectToMoveAndRotate.localRotation;
            }

            if (objectToMoveToCamera != null)
            {
                originalPos2 = objectToMoveToCamera.position;
            }
        }

        void Update()
        {
            if (isToggled)
            {
                if (objectToMoveAndRotate != null)
                {
                    objectToMoveAndRotate.localPosition = Vector3.MoveTowards(
                        objectToMoveAndRotate.localPosition,
                        targetLocalPosition,
                        Time.deltaTime * moveSpeed
                    );
                    objectToMoveAndRotate.localRotation = Quaternion.Slerp(
                        objectToMoveAndRotate.localRotation,
                        targetLocalRotation,
                        Time.deltaTime * rotateSpeed
                    );
                }

                if (objectToMoveToCamera != null)
                {
                    Vector3 camPos = Camera.main.transform.position + Camera.main.transform.forward * 2f;
                    objectToMoveToCamera.position = Vector3.MoveTowards(
                        objectToMoveToCamera.position,
                        camPos,
                        Time.deltaTime * moveSpeed
                    );
                }
            }
            else
            {
                if (objectToMoveAndRotate != null)
                {
                    objectToMoveAndRotate.localPosition = Vector3.MoveTowards(
                        objectToMoveAndRotate.localPosition,
                        originalLocalPos1,
                        Time.deltaTime * moveSpeed
                    );
                    objectToMoveAndRotate.localRotation = Quaternion.Slerp(
                        objectToMoveAndRotate.localRotation,
                        originalLocalRot1,
                        Time.deltaTime * rotateSpeed
                    );
                }

                if (objectToMoveToCamera != null)
                {
                    objectToMoveToCamera.position = Vector3.MoveTowards(
                        objectToMoveToCamera.position,
                        originalPos2,
                        Time.deltaTime * moveSpeed
                    );
                }
            }
        }

        public void ToggleTransition()
        {
            isToggled = !isToggled;
        }
}