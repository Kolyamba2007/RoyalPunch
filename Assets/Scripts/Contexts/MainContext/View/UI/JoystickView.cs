using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Contexts.MainContext
{
    public class JoystickView : View
    {
        [SerializeField] private Image ring;
        [SerializeField] private Image circle;

        private Controls _controls;

        protected override void Awake()
        {
            base.Awake();

            SetActive(false);
        }

        public void SetData(Controls controls)
        {
            _controls = controls;
        }
    
        public void SetActive(bool enabled)
        {
            ring.enabled = enabled;
            circle.enabled = enabled;
        }

        public void MoveToPosition()
        {
            transform.position = _controls.User.TouchPosition.ReadValue<Vector2>();
        }
    }
}
