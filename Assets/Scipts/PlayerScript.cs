using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Score")]
    public TMP_Text scoreText;
    public int score;

    [Header("Movement")]
    public float flySpeed = 5f;
    public float yawAmount = 120f;

    [Header("Audio")]
    public AudioSource waypointAudio;

    [Header("Win UI")]
    public GameObject levelCompleteUI;
    public string level1SceneName = "level1";

    private float yaw;
    private bool levelFinished;

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        if (levelFinished)
            return;

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
    if (levelFinished)
        return;

    if (other.gameObject.layer == LayerMask.NameToLayer("Waypoint"))
    {
        Destroy(other.gameObject, 0.1f);

        if (waypointAudio != null)
            waypointAudio.Play();

        score++;
        UpdateScoreUI();

        if (score == 7)
        {
            levelFinished = true;
            if (levelCompleteUI != null)
                levelCompleteUI.SetActive(true);
        }
    }
    else if (other.gameObject.layer == LayerMask.NameToLayer("Danger"))
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

    public void ResetToLevel1()
    {
        SceneManager.LoadScene(level1SceneName);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
        else
            Debug.LogWarning("ScoreText is not assigned in the Inspector.");
    }
}
