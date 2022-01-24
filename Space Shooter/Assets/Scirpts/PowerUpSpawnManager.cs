using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _powerUpPrefabs;
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
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-7f, 7f), 7f, 0);
            //GameObject newPowerShoot = Instantiate(_powerUpPrefabs[UnityEngine.Random.Range(0, _powerUpPrefabs.Length - 1)], spawnLocation, Quaternion.identity);
            GameObject newPowerShoot = Instantiate(_powerUpPrefabs, spawnLocation, Quaternion.identity);
            newPowerShoot.transform.parent = _powerUpContainer.transform;
            //yield return new WaitForSeconds(Random.Range(2,5));
            yield return new WaitForSeconds(2f);
        }
    }
}
