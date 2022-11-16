using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    public BasicDino basicDino;
    public StrawHouse strawHouse;

    public void SetOnFire()
    {
        if (basicDino != null)
            basicDino.SetOnFire();
        if (strawHouse != null)
            strawHouse.SetOnFire();
    }
}
