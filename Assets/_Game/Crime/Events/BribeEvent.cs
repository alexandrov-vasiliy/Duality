using _Game;
using UnityEngine;

public class BribeEvent : MonoBehaviour
{
    [SerializeField] private int bribeAmount = 50;
    [SerializeField] private Transform bribeSpawnPoint;
    [SerializeField] private GameObject bribePrefab;

    private GameObject spawnedBribe;

    public void ApplyBribe()
    {
        G.RunState.rewardMercy += bribeAmount;

        spawnedBribe = Instantiate(bribePrefab, bribeSpawnPoint);

    }

    public void RemoveBribe()
    {
        G.RunState.reward -= bribeAmount;
        if (spawnedBribe != null)
        {
            Destroy(spawnedBribe);
        }
    }
}
