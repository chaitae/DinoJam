using UnityEngine;

public class StrawHouse : MonoBehaviour
{
    private static readonly int FULL_FIRE = 3;
    public BasicDino dinoPrefab;
    public int dinosInside;
    public Transform spawnPoint;

    public ParticleSystem[] fireLevels;

    private int fireCount = 0;

    public void SetOnFire()
    {
        Debug.Log($"House fire! {fireCount}");
        if(fireCount >= FULL_FIRE)
            return;
        fireLevels[fireCount].gameObject.SetActive(true);
        fireCount += 1;

        if(fireCount >= FULL_FIRE)
        {
            SpawnFireDinos();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        FireTrigger fireTrigger = other.GetComponent<FireTrigger>();
        if (fireTrigger == null || fireCount < FULL_FIRE)
            return;
        fireTrigger.SetOnFire();
    }

    private void SpawnFireDinos()
    {
        for(int i = 0; i < dinosInside; i++)
        {
            BasicDino dino = Instantiate(dinoPrefab);
            dino.transform.parent = spawnPoint;
            dino.transform.localPosition = Vector3.zero;
            dino.SetOnFire();
        }
    }
}
