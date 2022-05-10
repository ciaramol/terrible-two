using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip laserHitSound;
    public AudioClip crashSound;
    public AudioClip dragSound;
    public float laserHpLoss = 5;
    public float collisionHpLoss = 10;
    public float dragHpLoss = 5;
    public int maxHp = 100;
    public Slider hpSlider;
    public Image hpSliderFill;

    public float shipHp = 100f;
    private bool lowHpFlag = false;
    private bool isCrashing = false;
    private bool isDragging = false;

    void Start()
    {
        shipHp = maxHp;
    }

    void OnTriggerEnter(Collider other)
    {
        /// GOAL ///
        if (other.gameObject.tag == "Goal") GameWon();

        /// LASER ///
        else if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            audioSource.PlayOneShot(laserHitSound);
            ReduceHp(laserHpLoss);
        }

        /// CRASHING ///
        else if (other.gameObject.tag == "Crashable")
        {
            isCrashing = true;

            // Play sound
            audioSource.PlayOneShot(crashSound);
            Invoke("DragStarts", 1.8f);

            // Subtract HP
            ReduceHp(collisionHpLoss);

            // I tried adding an impulse in the opposite direction, but it doesn't seem to work, perhaps because of the character controller?
            //Vector3 bounceDirecton = other.gameObject.transform.position - gameObject.transform.position;
            //shipRigidbody.AddForce(bounceDirecton.normalized * 1000);
            //Debug.Log(bounceDirecton.normalized * 1000);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Crashable") return;

        // Subtract HP a bit
        if (isDragging) ReduceHp(dragHpLoss * Time.deltaTime);

        // Start/restart audio if needed
        if (!audioSource.isPlaying) audioSource.PlayOneShot(dragSound);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Crashable") return;
        isCrashing = false;

        if (isDragging)
        {
            isDragging = false;
            audioSource.Stop(); // only drag sound should be interrupted
            // TODO: find a way of fading out the audio rather than cutting it out
        }
    }

    void DragStarts()
    {
        if (!isCrashing) return; // in case it gets called but collision is over
        isDragging = true;

        // Stop audio so drag sfx is activated in OnTriggerStay
        audioSource.Stop();
    }

    void ReduceHp(float amount)
    {
        shipHp = Mathf.Max(shipHp - amount, 0);
        hpSlider.value = shipHp / maxHp;

        if (shipHp == 0)
        {
            hpSliderFill.enabled = false;
            GameOver();
        }
        else if (!lowHpFlag && shipHp <= maxHp/3)
        {
            lowHpFlag = true;
            hpSliderFill.color = Color.red;
        }
    }

    void GameWon()
    {
        SceneManager.LoadScene("Win");
        Cursor.lockState = CursorLockMode.None;
    }

    void GameOver()
    {
        SceneManager.LoadScene("Lose");
        Cursor.lockState = CursorLockMode.None;
    }

    //SceneManager.LoadScene("Indoor");

    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag != "Crashable") return;

    //    Debug.Log("Collision enter");
    //}
}
