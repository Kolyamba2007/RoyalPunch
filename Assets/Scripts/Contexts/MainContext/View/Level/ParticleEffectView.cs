using strange.extensions.mediation.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class ParticleEffectView : View
    {
        [SerializeField] private ParticleSystem particleSystem;

        private void OnValidate()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        public void SetActive(bool enabled)
        {
            if (enabled)
                particleSystem.Play();
            else
                particleSystem.Stop();
        }
    }
}
