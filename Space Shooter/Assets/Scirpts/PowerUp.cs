using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp: MonoBehaviour
{
    [SerializeField]
    private float _collectableSpeed = 4;

    [SerializeField]
    private int _powerUpID;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _collectableSpeed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if (other.tag == "Player" && _powerUpID == 0) {
            player.powerShoot = true;
        }
    }
    /*IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        //If the colliding object's tag is "PowerUp"
        if (other.tag == "Player" && _powerUpID == 1)
        {
            //If Power_Up is false, it means the powerup routine is not currently running
            Destroy(this.gameObject);
            if (!other.gameObject.GetComponent<Player>().powerShoot)
            {
                other.gameObject.GetComponent<Player>().powerShoot = true;
                float duration = 5f;

                //Takes a timeStamp
                float timeStamp = Time.time;

                //While the current time is less than the timeStamp + the duration of the power up
                while (Time.time < timeStamp + duration)
                {
                    //If the flag to reset the powerup is active
                    if (other.gameObject.GetComponent<Player>().resetPowerShoot)
                    {
                        //Toggle it off
                        other.gameObject.GetComponent<Player>().resetPowerShoot = false;
                        //reset the powerup so that it ends in (current time + 5 seconds);
                        timeStamp = Time.time;
                    }
                    //Wait for next frame. Do that until the duration is over.

                    yield return null;
                }
                //The current time is now greater or equal to the timeStamp + the duration
                //This means the power up should be over
                other.gameObject.GetComponent<Player>().powerShoot = false;
            }
            //Otherwise, it means that a different powerup routine is already running
            //We're just going to let that other routine know that it should reset the timer
            else
            {
                other.gameObject.GetComponent<Player>().resetPowerShoot = true;
            }
        }
    }*/
}




