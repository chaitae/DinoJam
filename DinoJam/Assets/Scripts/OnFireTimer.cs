using System.Collections;
using UnityEngine;

public class OnFireTimer : OnFireListener
{
    public BasicDino basicDino;
    public float timeOnFireSeconds;

    public override void SetOnFire(bool givePoints = true)
    {
        StartCoroutine(WaitBeforeDeath());
    }

    private IEnumerator WaitBeforeDeath()
    {
        yield return new WaitForSeconds(timeOnFireSeconds);
        basicDino.Kill();
    }
}
