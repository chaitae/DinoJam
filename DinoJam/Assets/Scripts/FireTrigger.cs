using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    public BasicDino basicDino;
    public StrawHouse strawHouse;

    public void SetOnFire(bool givePoints = false)
    {
        if (basicDino != null)
            basicDino.SetOnFire(givePoints);
        if (strawHouse != null)
            strawHouse.SetOnFire(givePoints);
    }
}
