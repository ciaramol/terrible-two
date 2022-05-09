using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip crashSound;
    public AudioClip dragSound;

    private bool isDragging = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter - not crashable");

        if (other.gameObject.tag != "Crashable") return;

        // Play sound
        audioSource.PlayOneShot(crashSound);
        Invoke("DragStarts", 1.8f);

        // Subtract HP

        // I tried adding an impulse in the opposite direction, but it doesn't seem to work, perhaps because of the character controller?
        //Vector3 bounceDirecton = other.gameObject.transform.position - gameObject.transform.position;
        //shipRigidbody.AddForce(bounceDirecton.normalized * 1000);
        //Debug.Log(bounceDirecton.normalized * 1000);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Crashable") return;

        if (!audioSource.isPlaying) audioSource.PlayOneShot(dragSound);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Crashable") return;

        if(isDragging)
        {
            isDragging = false;
            audioSource.Stop(); // only drag sound should be interrupted
            // TODO: find a way of fading out the audio rather than cutting it out
        }
    }

    void DragStarts()
    {
        isDragging = true;

        // Stop audio so drag sfx is activated in OnTriggerStay
        audioSource.Stop();

        // Start subtracting HP slowly
    }

    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag != "Crashable") return;

    //    Debug.Log("Collision enter");
    //}
}
