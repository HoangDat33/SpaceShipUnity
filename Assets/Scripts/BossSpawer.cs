using UnityEngine;

public class BossSpawer : MonoBehaviour
{
    public GameObject BossGO;

    void Start()
    {
    }

    void Update()
    {
    }

    public GameObject SpawnBoss()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject aBoss = Instantiate(BossGO);
        aBoss.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        return aBoss;
    }
}