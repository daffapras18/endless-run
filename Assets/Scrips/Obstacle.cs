using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5f;
    private float bound = -12f;
    private PlayerMovement playerMovementScript;
    private bool hasScored = false;

    void Start()
    {
        playerMovementScript = GameObject.Find("Gordo").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovementScript.gameOver == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Hapus obstacle jika melewati batas
            if (transform.position.x < bound)
            {
                Destroy(gameObject);
            }

            // Tambahkan skor jika obstacle melewati Gordo
            if (transform.position.x < playerMovementScript.transform.position.x && !hasScored)
            {
                FindFirstObjectByType<UImanager>().AddScore(10);
                hasScored = true; // Mencegah penambahan skor ganda
            }
        }
    }
}
