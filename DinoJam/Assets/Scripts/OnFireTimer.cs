using System.Collections;
using UnityEngine;

public class OnFireTimer : OnFireListener
{
    public float timeOnFireSeconds;

    public override void SetOnFire()
    {
        StartCoroutine(WaitBeforeDeath());
    }

    private IEnumerator WaitBeforeDeath()
    {
        yield return new WaitForSeconds(timeOnFireSeconds);
        Destroy(gameObject);
    }
}
