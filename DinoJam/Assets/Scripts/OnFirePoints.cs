public class OnFirePoints : OnFireListener
{
    public string firePointsKey;
    public int firePoints;

    public override void SetOnFire()
    {
        LevelManager.Instance.RegisterPoints(transform.position, firePointsKey, firePoints);
    }
}
