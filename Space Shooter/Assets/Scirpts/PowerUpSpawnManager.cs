using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _powerShootPrefabs;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _powerUpContainer;

    void Start()
    {
        StartCoroutine(spawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    IEnumerator spawnRoutine()
    {
        while (true)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-7f, 7f), 7f, 0);
            GameObject newPowerShoot = Instantiate(_powerShootPrefabs, spawnLocation, Quaternion.identity);
            //GameObject newEnemy = Instantiate(_enemyPrefabs, spawnLocation, Quaternion.identity);
            newPowerShoot.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(3,9));
        }
    }
}
