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

}
