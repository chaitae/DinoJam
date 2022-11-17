using UnityEngine;

public class StrawHouse : MonoBehaviour
{
    private static readonly int FULL_FIRE = 3;
    public BasicDino dinoPrefab;
    public int dinosInside;
    public Transform spawnPoint;

    public ParticleSystem[] fireLevels;

    private int fireCount = 0;

    public void SetOnFire(bool givePoints = true)
    {
        if(fireCount >= FULL_FIRE)
            return;
        fireLevels[fireCount].gameObject.SetActive(true);
        fireCount += 1;

        if(fireCount >= FULL_FIRE)
        {
            HouseOnFire();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        FireTrigger fireTrigger = other.GetComponent<FireTrigger>();
        if (fireTrigger == null || fireCount < FULL_FIRE)
            return;
        fireTrigger.SetOnFire(false);
    }

    private void HouseOnFire()
    {
        LevelManager.Instance.RegisterPoints(transform.position, "Home", 75);
        SpawnFireDinos();
    }

    private void SpawnFireDinos()
    {
        for(int i = 0; i < dinosInside; i++)
        {
            BasicDino dino = Instantiate(dinoPrefab);
            dino.transform.parent = spawnPoint;
            dino.transform.localPosition = Vector3.zero;
            dino.SetOnFire(false);
        }
    }
}
