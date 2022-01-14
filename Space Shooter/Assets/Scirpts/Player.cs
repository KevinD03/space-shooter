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

    private float _nextFire = 0f;

    [SerializeField]
    private bool _powerShoot = false;

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

        if (_enemySpawnManager == null) {
            Debug.LogError("Spawn manager is null");
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
        Debug.Log(_playerSpeed);

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

    public void changePowerShoot() {
        _powerShoot = true;
    }
}
 