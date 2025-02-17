using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _RotateSpeed = 20f;
    [SerializeField]
    private GameObject _ExplosionPrefab;
    private SpawnManager _SpawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _SpawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _RotateSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.tag == "lazer")
        {
            Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(_other.gameObject);
            _SpawnManager.StartSpawning();
            Destroy(this.gameObject, 0.10f);
        }
    }
}
