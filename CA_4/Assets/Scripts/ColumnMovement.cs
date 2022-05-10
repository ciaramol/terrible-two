using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnMovement : MonoBehaviour
{
    enum State { Active, Closing, Inactive, Opening }
    // active (idle) -> closing -> inactive (idle) -> opening -> active(idle) -> etc

    public float columnLength = 100f;
    public float keepPartiallyOpenBy = 5;
    public float minInactiveDuration = 10, maxInactiveDuration = 20;
    public float minActiveDuration = 5, maxActiveDuration = 10;
    public float minMoveDuration = 5, maxMoveDuration = 10;

    private State state = State.Active;
    private float moveSpeed;
    private bool hidesLeft; // NOTE: This is the world's Left, but it looks like the right from the ship's POV
    private float inactiveDuration;
    private float activeDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        hidesLeft = Random.Range(0, 2) == 0 ? true : false; // 2 since max is not inclusive
        inactiveDuration = Random.Range(minInactiveDuration, maxInactiveDuration);
        activeDuration = Random.Range(minActiveDuration, maxActiveDuration);
        float movementDuration = Random.Range(minMoveDuration, maxMoveDuration);
        moveSpeed = columnLength / movementDuration;

        Invoke("Close", Random.Range(0.1f, 10f)); // so they don't move kind of at the same time
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Active || state == State.Inactive) return;

        if (state == State.Closing)
        {
            if (Mathf.Abs(transform.position.x) >= columnLength - keepPartiallyOpenBy)
            {
                state = State.Inactive;
                //Debug.Log("State changed to Inactive");
                Invoke("Open", inactiveDuration);
            }
            transform.Translate((hidesLeft? 1 : -1) * Vector3.left * Time.deltaTime * moveSpeed);
        }
        else if (state == State.Opening)
        {
            if( (hidesLeft && transform.position.x > 0) || (!hidesLeft && transform.position.x < 0) )
            {
                state = State.Active;
                //Debug.Log("State changed to Active");
                Invoke("Close", activeDuration);
            }
            transform.Translate((hidesLeft ? -1 : 1) * Vector3.left * Time.deltaTime * moveSpeed);
        }
    }

    void Close()
    {
        state = State.Closing;
        //Debug.Log("State changed to Closing");
    }

    void Open()
    {
        state = State.Opening;
        //Debug.Log("State changed to Opening");
    }
}
