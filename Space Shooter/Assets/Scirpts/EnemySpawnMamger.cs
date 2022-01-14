using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnMamger : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _enemyPrefabs;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _playerDeath = false;

    void Start()
    {
        StartCoroutine(spawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnRoutine() {
        while (_playerDeath == false) {
            Vector3 spawnLocation = new Vector3(Random.Range(-7f, 7f), 7f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Length - 1)], spawnLocation, Quaternion.identity);
            //GameObject newEnemy = Instantiate(_enemyPrefabs, spawnLocation, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.0f);
        }
    }
 
    public void OnPlayerDeath() {
        _playerDeath = true;
    }
}
