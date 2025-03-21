using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteriod : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private GameObject _explosionprefab;

    private SpawnManager _spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_speed * Time.deltaTime * Vector3.forward);   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            Instantiate(_explosionprefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
        }
    }
}
