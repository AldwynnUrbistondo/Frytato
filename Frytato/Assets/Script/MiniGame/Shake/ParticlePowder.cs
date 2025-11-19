using UnityEngine;

public class ParticlePowder : MonoBehaviour
{
    public ParticleSystem powder;

    private void Start()
    {
        powder.Stop();
    }

    public void PlayPowderEffect()
    {
        powder.Play();
    }

    public void StopPowderEffect()
    {
        powder.Stop();
    }
}
