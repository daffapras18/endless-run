using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed = 5f;
    private PlayerMovement playerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GameObject.Find("Gordo").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementScript.gameOver == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x < -19)
            {
                transform.position = new Vector2(0, 0);
            }
        }
    }
}
