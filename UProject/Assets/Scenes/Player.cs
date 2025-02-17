using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    private float speedMultiplier = 2;
    [SerializeField]
    private GameObject lazerPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.5f;
    private float canFire = -1f;
    [SerializeField]
    private int lives = 3;
    private SpawnManager spawnManager;
    public bool isTripleShotActive = false;
    public bool isSpeedBoostPowerup = false;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    public bool isShieldBoostPowerup = false;
    [SerializeField]
    private GameObject ShieldVisualizer;

    [SerializeField]
    private GameObject _LeftEngine, _RightEngine;
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    public int Score;

    private UIManager uiManager;
    [SerializeField]
    private AudioClip _LazerSoundclip;
    private AudioSource _AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _AudioSource = GetComponent<AudioSource>();

        if (gameManager.isCoOpMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        if (spawnManager == null)
        {
            Debug.LogError("The spawn maanger is null");
        }
        if (uiManager == null)
        {
            Debug.LogError("The UI manager is null");
        }
        if(_AudioSource == null) 
        {
            Debug.LogError("AudioSource on the player is null");
        }
        else
        {
            _AudioSource.clip = _LazerSoundclip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne == true)
        {
            CalculateMovement();
            if ((Input.GetKeyDown(KeyCode.Space) && Time.time > canFire) && isPlayerOne == true)
            {
                FireLazer();
            }
        }
        if(isPlayerTwo == true)
        {
            PlayerTwoMovement();
            if (Input.GetKeyDown(KeyCode.Keypad0) && Time.time > canFire)
            {
                FireLazer();
            }
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

            transform.Translate(direction * Time.deltaTime * speed);

        if (transform.position.y >= 6f)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0);
        }
        else if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0);
        }
        if (transform.position.x >= 14f)
        {
            transform.position = new Vector3(14f, transform.position.y, 0);
        }
        else if (transform.position.x <= -14f)
        {
            transform.position = new Vector3(-14f, transform.position.y, 0);
        }
    }

    void PlayerTwoMovement()
    {
        if (isSpeedBoostPowerup == true)
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed * 1.5f);
            }

            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed * 1.5f);
            }

            if (Input.GetKey(KeyCode.Keypad2))
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed * 1.5f);
            }

            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 1.5f);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.Keypad2))
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
        if (transform.position.y >= 6f)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0);
        }
        else if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0);
        }
        if (transform.position.x >= 14f)
        {
            transform.position = new Vector3(14f, transform.position.y, 0);
        }
        else if (transform.position.x <= -14f)
        {
            transform.position = new Vector3(-14f, transform.position.y, 0);
        }
    }

    void FireLazer()
    {
        canFire = Time.time + fireRate;
        if(isTripleShotActive == true)
        {
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(lazerPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        }
        _AudioSource.Play();
    }
    public void Damage()
    {
        if(isShieldBoostPowerup == true)
        {
            isShieldBoostPowerup = false;
            ShieldVisualizer.SetActive(false);
            return;
        }
        lives--;

        if(lives == 2)
        {
            _LeftEngine.SetActive(true);
        }
        else if(lives == 1)
        {
            _RightEngine.SetActive(true);
        }

        uiManager.UpdateLives(lives);

        if(lives == 0)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedPowerupActive()
    {
        isSpeedBoostPowerup = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedPowerupRoutine());
    }

    public void ShieldPowerupActive()
    {
        isShieldBoostPowerup = true;
        ShieldVisualizer.SetActive(true);
    }  

    IEnumerator SpeedPowerupRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostPowerup = false;
        speed /= speedMultiplier;   
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    public void AddScore(int points)
    {
        Score += points;
        uiManager.UpdateScore(Score);
    }

}
