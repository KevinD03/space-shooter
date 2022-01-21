using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _enemySpeed = 2;
    [SerializeField]
    private int _enemyHealth = 8;

    private Player _player;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-7f, 7f), 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // enemy collide with player
        if (other.tag == "Player") {
            // get player script component
            
            if (_player != null) {
                _player.damaged();
            }
            Destroy(this.gameObject);
        }

        // enemy collide with projectile from player
        if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            this._enemyHealth--;
            if (this._enemyHealth < 1) {
                Destroy(this.gameObject);
                
                Debug.Log("score");
                _player.addScore(10);
                
            }
            
        }
    }
}
