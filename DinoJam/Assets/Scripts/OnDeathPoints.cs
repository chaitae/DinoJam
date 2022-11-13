public class OnDeathPoints : OnDeathListener
{
    public string killPointsKey;
    public int killPoints;

    public override void Killed()
    {
        LevelManager.Instance.RegisterPoints(transform.position, killPointsKey, killPoints);
    }
}
