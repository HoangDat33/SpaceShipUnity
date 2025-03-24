using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    GameObject scoreUITextGO;
    public GameObject ExplosionGO; 
    public GameObject SmallExplosionGO; 
    GameObject nextLevelCanvas; 
    float speed = 3f;
    float horizontalRange = 5f; 
    float direction = 1f; 
    Vector2 startPosition;
    int hitCount = 0;
    [SerializeField] int requiredHits = 50;

    void Start()
    {
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
        startPosition = transform.position; 
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += speed * direction * Time.deltaTime;

        float rightLimit = startPosition.x + horizontalRange;
        float leftLimit = startPosition.x - horizontalRange;

        if (position.x > rightLimit)
        {
            position.x = rightLimit;
            direction = -1f;
        }
        else if (position.x < leftLimit)
        {
            position.x = leftLimit;
            direction = 1f;
        }

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShipTag" || collision.tag == "PlayeBulletTag")
        {
            Vector2 collisionPoint = collision.transform.position;
            PlaySmallExplosion(collisionPoint);

            hitCount++;
            if (hitCount >= requiredHits)
            {
                PlayBossDestruction();
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
                ShowNextLevelMessage();
                Destroy(gameObject);
            }
        }
    }

    void PlaySmallExplosion(Vector2 position)
    {
        if (SmallExplosionGO != null)
        {
            GameObject smallExplosion = Instantiate(SmallExplosionGO);
            smallExplosion.transform.position = position;
        }
    }

    void PlayBossDestruction()
    {
        if (ExplosionGO != null)
        {
            // Tạo 3 vị trí nổ trên boss (tùy chỉnh theo kích thước boss)
            Vector2 center = transform.position;
            Vector2 offset1 = new Vector2(-0.5f, 0.5f); // Ví dụ: góc trên trái
            Vector2 offset2 = new Vector2(0.5f, 0.5f);  // Ví dụ: góc trên phải
            Vector2 offset3 = new Vector2(0f, -0.5f);   // Ví dụ: dưới giữa

            // Tạo 3 vụ nổ lớn
            GameObject explosion1 = Instantiate(ExplosionGO);
            explosion1.transform.position = center + offset1;

            GameObject explosion2 = Instantiate(ExplosionGO);
            explosion2.transform.position = center + offset2;

            GameObject explosion3 = Instantiate(ExplosionGO);
            explosion3.transform.position = center + offset3;
        }
    }
    public void SetRequiredHits(int hits)
    {
        requiredHits = hits;
    }
    void ShowNextLevelMessage()
    {
        nextLevelCanvas = GameObject.FindGameObjectWithTag("NextLevelCanvas");

        // Nếu không tìm thấy, tìm trong tất cả đối tượng (kể cả Inactive)
        if (nextLevelCanvas == null)
        {
            nextLevelCanvas = Resources.FindObjectsOfTypeAll<Canvas>()
                .FirstOrDefault(c => c.CompareTag("NextLevelCanvas"))?.gameObject;
        }

        if (nextLevelCanvas == null)
        {
            Debug.LogError("❌ Không tìm thấy NextLevelCanvas!");
            return;
        }

        nextLevelCanvas.SetActive(true); // Bật canvas
        Time.timeScale = 0f; // Tạm dừng game
    }

}