using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Score")]
    public TMP_Text scoreText;
    public int score;

    [Header("Movement")]
    public float flySpeed = 5f;
    public float yawAmount = 120f;

    [Header("Audio")]
    public AudioSource waypointAudio;



    private float yaw;


    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {


        // Move forward
        transform.position += transform.forward * flySpeed * Time.deltaTime;

        // Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotation calculations
        yaw += horizontalInput * yawAmount * Time.deltaTime;
        float pitch = Mathf.Lerp(0f, 90f, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0f, 20f, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        // Apply rotation
        transform.localRotation = Quaternion.Euler(
            Vector3.up * yaw +
            Vector3.right * pitch +
            Vector3.forward * roll
        );
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.layer == LayerMask.NameToLayer("Waypoint"))
    {
        Destroy(other.gameObject, 0.1f);

        if (waypointAudio != null)
            waypointAudio.Play();

        score++;
        UpdateScoreUI();
    }
    else if (other.gameObject.layer == LayerMask.NameToLayer("Danger"))
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
        else
            Debug.LogWarning("ScoreText is not assigned in the Inspector.");
    }
}
