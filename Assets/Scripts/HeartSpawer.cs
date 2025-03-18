using UnityEngine;

public class HeartSpawer : MonoBehaviour
{
    public GameObject HeartGO;

    float maxSpawnRateInSeconds = 15f;

    void Start()
    {
    }
    void Update()
    {

    }

    void SpawnHeart()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject aHeart = (GameObject)Instantiate(HeartGO);
        aHeart.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextHeartSpawn();
    }

    void ScheduleNextHeartSpawn()
    {
        float spawnInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInSeconds = 1f;
        }

        Invoke("SpawnHeart", spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds++;
        }

        if (maxSpawnRateInSeconds == 50f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    public void ScheduleHeartSpawner()
    {
        maxSpawnRateInSeconds = 5f;
        Invoke("SpawnHeart", maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void UnscheduleHeartSpawner()
    {
        CancelInvoke("SpawnHeart");
        CancelInvoke("IncreaseSpawnRate");
    }
}
