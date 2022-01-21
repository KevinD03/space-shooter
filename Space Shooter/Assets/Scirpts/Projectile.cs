using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _projectileSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireUp();
        destoryProjectile();
    }

    void fireUp() {
        transform.Translate(Vector3.up * _projectileSpeed * Time.deltaTime);
    }

    void destoryProjectile() {
        if (transform.position.y >= 6.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
