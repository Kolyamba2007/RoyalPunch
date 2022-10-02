using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class CameraView : View
    {
        public Signal ReachedFinalPositionSignal { get; } = new Signal();
    
        [SerializeField] private Transform pos0;
        [SerializeField] private Transform pos1;
        [SerializeField] private Transform pos2;
        [SerializeField] private Transform pos3;

        protected override void Awake()
        {
            base.Awake();
        
            transform.localPosition = pos0.localPosition;
            transform.localRotation = pos0.localRotation;
        }

        public void StartMove()
        {
            StartCoroutine(Move());
        }
    
        private IEnumerator Move()
        {
            float step = 0;

            if (transform.localPosition !=
                Bezier.GetPoint(pos0.localPosition, pos1.localPosition, pos2.localPosition, pos3.localPosition, 0))
                step = 1;

            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime;

                transform.localPosition = Bezier.GetPoint(pos0.localPosition, pos1.localPosition, pos2.localPosition,
                    pos3.localPosition, Mathf.Abs(step - time));
                transform.localRotation = Quaternion.Slerp(pos0.localRotation, pos3.localRotation, Mathf.Abs(step - time));

                yield return null;
            }

            if (step == 0)
                ReachedFinalPositionSignal.Dispatch();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(pos0.position, pos1.position);
            Gizmos.DrawLine(pos1.position, pos2.position);
            Gizmos.DrawLine(pos2.position, pos3.position);

            Gizmos.color = new Color(0, 0, 255);
            int sigmentsNumber = 20;
            Vector3 preveousePoint = pos0.position;

            for (int i = 0; i < sigmentsNumber + 1; i++)
            {
                float paremeter = (float)i / sigmentsNumber;
                Vector3 point = Bezier.GetPoint(pos0.position, pos1.position, pos2.position, pos3.position, paremeter);
                Gizmos.DrawLine(preveousePoint, point);
                preveousePoint = point;
            }
        }
    }
}
