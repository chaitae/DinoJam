using UnityEngine;

public class OnDeathParticles : OnDeathListener
{
    public ParticleSystem particleSystem;

    public override void Killed()
    {
        particleSystem.Stop();
    }
}
