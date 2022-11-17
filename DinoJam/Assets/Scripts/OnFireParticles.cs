using UnityEngine;

public class OnFireParticles: OnFireListener
{
    public GameObject particleSystem;

    public override void SetOnFire(bool givePoints = true)
    {
        particleSystem.SetActive(true);
    }
}
