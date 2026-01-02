using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {

  	    public float FlySpeed = 5;
   	    public float YawAmount = 120;
   	    public int score;
    public AudioSource Waypoint;
    public TMP_Text ScoreText; 

    private float Yaw;

    void Start()
    {
        global::System.Object value = ScoreText.GetComponent<TMP_Text>(); 
        ScoreText.text = "" + score;
    }



    // Update is called once per frame
    void Update() {

        // move forward
        transform.position += transform.forward * FlySpeed * Time.deltaTime;

        // inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // yaw, pitch, roll
        Yaw += horizontalInput * YawAmount * Time.deltaTime;
        float pitch = Mathf.Lerp(0, 90, Mathf.Abs(verticalInput)) * Mathf.Sign(verticalInput);
        float roll = Mathf.Lerp(0, 20, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

        // apply rotation
        transform.localRotation = Quaternion.Euler(Vector3.up * Yaw + Vector3.right * pitch + Vector3.forward * roll);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "waypoint")
        {
            Destroy(other.gameObject, 0.1f); //destroys the waypoint in 0.1 second
            Waypoint.Play(); //plays the sound effect
            score++; //updates the score system by adding 1
            ScoreText.text = "" + score; //updates the HUD

	if (score == 5)
	{
	   Application.LoadLevel("level2");
	}

        }

        if (other.gameObject.tag == "danger")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
