using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private GameObject _LaserPrefab;

    private Player _player;
    private Animator _Anim;
    private AudioSource _AudioSource;
    private float _FireRate = 3.0f;
    private float _CanFire = -1;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _AudioSource = GetComponent<AudioSource>();
        if(_player == null)
        {
            Debug.LogError("The PLAYER is null");
        }
        _Anim = GetComponent<Animator>();
            if(_Anim == null)
        {
            Debug.LogError("The ANÝMATOR is null");
        }
    }

    // Update is called once per frame
    void Update() 
    {
        CalculateMovement();
        if(Time.time > _CanFire)
        {
            _FireRate = Random.Range(3f, 7f);
            _CanFire = Time.time + _FireRate;
            GameObject _enemyLaser = Instantiate(_LaserPrefab, transform.position, Quaternion.identity);
            lazer[] lasers = _enemyLaser.GetComponentsInChildren<lazer>();

            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }   

            _Anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            _AudioSource.Play();
            Destroy(this.gameObject, 2.3f);
        }
        if(other.tag == "lazer")
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);
            }

            _Anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            _AudioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.3f);
        }
    }
}
