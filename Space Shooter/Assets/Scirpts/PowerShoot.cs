using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShoot : MonoBehaviour
{
    [SerializeField]
    private float _collectableSpeed = 4;
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
        // powerShoot collide with player
        if (other.tag == "Player")
        {
            // get player script component
            Player player_behaviour = other.transform.GetComponent<Player>();
            if (player_behaviour != null)
            {
                player_behaviour.changePowerShoot();
            }
            Destroy(this.gameObject);
        }
    }
}
