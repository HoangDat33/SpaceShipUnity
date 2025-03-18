using UnityEngine;

public class HeartControl : MonoBehaviour
{
    float speed;
    void Start()
    {
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "PlayerShipTag"))
        {
            Destroy(gameObject);
        }
    }
}
