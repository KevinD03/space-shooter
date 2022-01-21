using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Input playerInput;

    [SerializeField]
    private float _playerSpeed = 15f;

    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _fireSpeed = 0.1f;

    [SerializeField]
    private int _playerHealh = 3;

    [SerializeField]
    private EnemySpawnMamger _enemySpawnManager;

    private bool _resetPowerUp;

    private float _powerShootTimer;

    private float _nextFire = 0f;

    [SerializeField]
    private bool _powerShoot = false;

    [SerializeField]
    private int _score;

    private UI_Manager _uiManager;

    private void Awake()
    {
        playerInput = new Input();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput.Player.Shoot.performed += _ => Shoot();

        transform.position = new Vector3(0, 0, 0);
        _enemySpawnManager = GameObject.Find("EnemySpawnMamger").GetComponent<EnemySpawnMamger>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();

        if (_enemySpawnManager == null) {
            Debug.LogError("Spawn manager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
        Shoot();
    }

    void Move()
    {
        Vector2 move = playerInput.Player.Move.ReadValue<Vector2>();

        /*float xAxis_input = Input.GetAxis("Horizontal");
        float yAxis_input = Input.GetAxis("Vertical");*/
        // New Vector3(1,0,0)
        //transform.Translate(Vector3.right * xAxis_input * Time.deltaTime);

        Vector3 directional_control = new Vector3(move.x, move.y, 0);
        transform.Translate(directional_control * _playerSpeed * Time.deltaTime);

        // player bound

        /*if (transform.position.y >= 5)
        {
            transform.position = new Vector3(transform.position.x, 5, 0);
        }
        else if (transform.position.y <= -5)
        {
            transform.position = new Vector3(transform.position.x, -5, 0);
        }*/

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5f, 5f), 0);

        if (transform.position.x >= 7f)
        {
            transform.position = new Vector3(7f, transform.position.y, 0);
        }
        else if (transform.position.x <= -7f)
        {
            transform.position = new Vector3(-7f, transform.position.y, 0);
        }
    }


    void Shoot() {

        //Input.GetButtonDown("Fire1")
        if (playerInput.Player.Shoot.IsPressed() && Time.time > _nextFire) {
            _nextFire = Time.time + _fireSpeed;
            Instantiate(_projectilePrefab, transform.position + new Vector3(-0.3f, 0.8f, 0), Quaternion.identity);
            Instantiate(_projectilePrefab, transform.position + new Vector3(0.3f, 0.8f, 0), Quaternion.identity);

            if (_powerShoot == true) {
                Instantiate(_projectilePrefab, transform.position + new Vector3(-0.78f, -0.44f, 0), Quaternion.identity);
                Instantiate(_projectilePrefab, transform.position + new Vector3(0.78f, -0.44f, 0), Quaternion.identity);
            }
            
        }
    }

    public void damaged() {
        _playerHealh--;
        
        if (_playerHealh < 1) {
            _enemySpawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    /*public void changePowerShoot() {
        _powerShoot = true;
        StartCoroutine(powerShootOff());
    }

    IEnumerator powerShootOff() {
        yield return new WaitForSeconds(_powerShootTimer);
        _powerShoot = false;
    }*/

    public void addScore(int score) {
        _score += score;
        Debug.Log(_score);
        _uiManager.updateScore(_score);
    }


    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        //If the colliding object's tag is "PowerUp"
        if (other.tag == "PowerShoot")
        {
            //If Power_Up is false, it means the powerup routine is not currently running
            Destroy(other.gameObject);
            if (!_powerShoot)
            {
                _powerShoot = true;
                float duration = 5f;

                //Takes a timeStamp
                float timeStamp = Time.time;

                //While the current time is less than the timeStamp + the duration of the power up
                while (Time.time < timeStamp + duration)
                {
                    //If the flag to reset the powerup is active
                    if (_resetPowerUp)
                    {
                        //Toggle it off
                        _resetPowerUp = false;
                        //reset the powerup so that it ends in (current time + 5 seconds);
                        timeStamp = Time.time;
                    }
                    //Wait for next frame. Do that until the duration is over.

                    yield return null;
                }
                //The current time is now greater or equal to the timeStamp + the duration
                //This means the power up should be over
                _powerShoot = false;
            }
            //Otherwise, it means that a different powerup routine is already running
            //We're just going to let that other routine know that it should reset the timer
            else
            {
                _resetPowerUp = true;
            }
        }
    }

}



 