public class OnFirePoints : OnFireListener
{
    public string firePointsKey;
    public int firePoints;

    public override void SetOnFire(bool givePoints = true)
    {
        if(!givePoints)
            return;
        LevelManager.Instance.RegisterPoints(transform.position, firePointsKey, firePoints);
    }
}
