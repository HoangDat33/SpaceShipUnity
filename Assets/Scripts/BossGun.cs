using UnityEngine;

public class BossGun : MonoBehaviour
{
    public GameObject EnemyBulletGO;
    public float fireRate = 4f; // Thời gian giữa các lần bắn
    public enum FirePattern { Straight, Spread, Spiral, Wave }
    public FirePattern currentPattern = FirePattern.Straight;
    public int bulletCount = 3; // Số đạn cho pattern Spread
    public float spreadAngle = 30f; // Góc tỏa cho pattern Spread
    public float spiralSpeed = 5f; // Tốc độ xoay cho pattern Spiral

    private float spiralAngle = 0f;

    void Start()
    {
        InvokeRepeating("FireEnemyBullet", 1f, fireRate);
    }

    void Update()
    {

    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");
        if (playerShip == null) return;

        switch (currentPattern)
        {
            case FirePattern.Straight:
                FireStraight(playerShip);
                break;
            case FirePattern.Spread:
                FireSpread(playerShip);
                break;
            case FirePattern.Spiral:
                FireSpiral(playerShip);
                break;
            case FirePattern.Wave:
                FireWave(playerShip);
                break;
        }
    }

    void FireStraight(GameObject player)
    {
        GameObject bullet = Instantiate(EnemyBulletGO, transform.position, Quaternion.identity);
        Vector2 direction = player.transform.position - bullet.transform.position;
        bullet.GetComponent<EnemyBullet>().setDirection(direction.normalized);
    }

    void FireSpread(GameObject player)
    {
        Vector2 baseDirection = (player.transform.position - transform.position).normalized;
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (spreadAngle * i / (bulletCount - 1));
            Vector2 direction = Quaternion.Euler(0, 0, angle) * baseDirection;

            GameObject bullet = Instantiate(EnemyBulletGO, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().setDirection(direction);
        }
    }

    void FireSpiral(GameObject player)
    {
        spiralAngle += spiralSpeed;
        if (spiralAngle >= 360f) spiralAngle -= 360f;

        Vector2 direction = new Vector2(
            Mathf.Cos(spiralAngle * Mathf.Deg2Rad),
            Mathf.Sin(spiralAngle * Mathf.Deg2Rad)
        );

        GameObject bullet = Instantiate(EnemyBulletGO, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().setDirection(direction);
    }

    void FireWave(GameObject player)
    {
        Vector2 baseDirection = (player.transform.position - transform.position).normalized;
        float waveAngle = Mathf.Sin(Time.time * 5f) * 30f; // Tạo hiệu ứng sóng

        Vector2 direction = Quaternion.Euler(0, 0, waveAngle) * baseDirection;
        GameObject bullet = Instantiate(EnemyBulletGO, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().setDirection(direction);
    }
}