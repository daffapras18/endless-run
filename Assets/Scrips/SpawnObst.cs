using UnityEngine;

public class SpawnObs : MonoBehaviour
{
    private PlayerMovement playerMovementScript;

    public GameObject[] enemy; // Array untuk berbagai tipe musuh
    public float spawnTime = 1; // Waktu awal spawn
    private float repeatRate;   // Interval spawn

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GameObject.Find("Gordo").GetComponent<PlayerMovement>();

        // Menentukan interval spawn secara acak
        repeatRate = Random.Range(spawnTime, 4f);

        // Memulai proses spawn berulang
        InvokeRepeating("SpawnEnemy", spawnTime, repeatRate);
    }

    // Spawn musuh
    void SpawnEnemy()
    {
        if (playerMovementScript.gameOver == false)
        {
            // Memilih musuh secara acak dari array
            int randomNumber = Random.Range(0, enemy.Length);

            // Spawn musuh di posisi tertentu
            Instantiate(enemy[randomNumber], new Vector2(20, -1), Quaternion.identity);
        }
    }
}
