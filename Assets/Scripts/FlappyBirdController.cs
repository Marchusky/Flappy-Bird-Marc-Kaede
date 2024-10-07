using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Required for TextMeshPro support

public class FlappyBirdController : MonoBehaviour
{
    // Variables for bird movement
    public float jumpForce = 5f;
    public Rigidbody2D rb;
    private bool isAlive = true;

    // Sound effects
    public AudioClip flapSound;
    public AudioClip scoreSound;
    public AudioClip deathSound;
    public AudioSource audioSource;

    // UI elements for score using TextMeshPro
    public TextMeshProUGUI scoreText;  // Changed to TextMeshProUGUI for TMP support
    private int score = 0;

    void Start()
    {
        // Initialize gravity for bird
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // Initialize score and update the UI
        UpdateScoreUI();
    }

    void Update()
    {
        if (isAlive)
        {
            // Bird flaps when space or left mouse button is pressed
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Flap();
            }

            // Debugging: Increase score when F key is pressed
            if (Input.GetKeyDown(KeyCode.F))
            {
                IncreaseScore();
            }
        }
    }

    void Flap()
    {
        // Apply upward force for jumping
        rb.velocity = Vector2.up * jumpForce;
        AudioManager.instance.PlayFlapSound();
        Debug.Log("Flap sound played!");  // Console output to confirm sound is triggered
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the bird collides with a pipe or the boundaries, it dies
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Limit"))
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // If the bird passes through the pipe gap, increase score
        if (isAlive && collider.gameObject.CompareTag("ScoreZone"))
        {
            IncreaseScore();
        }
    }

    void IncreaseScore()
    {
        score++;  // Increment score
        UpdateScoreUI();  // Update score display

        AudioManager.instance.PlayScoreSound();  // Play score sound

        // Log the current score to the console
        Debug.Log("Current Score: " + score);
    }

    void UpdateScoreUI()
    {
        // Update the score text in the UI
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    void Die()
    {
        if (!isAlive)
            return;  // Prevent multiple deaths

        isAlive = false;
        AudioManager.instance.PlayDeathSound();
        // Trigger game-over logic
        Invoke("RestartGame", 2f);  // Restart after 2 seconds
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the scene
    }
}
