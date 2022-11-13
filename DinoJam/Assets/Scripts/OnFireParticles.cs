using UnityEngine;

public class OnFireParticles: OnFireListener
{
    public GameObject particleSystem;

    public override void SetOnFire()
    {
        particleSystem.SetActive(true);
    }
}
