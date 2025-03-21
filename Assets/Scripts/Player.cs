using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public int _score;

    private bool _isTrippleShootActive = false;
    public bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private int _speed = 5;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TrippleShootPrefab;
    [SerializeField]
    private GameObject _visualizer;
    [SerializeField]
    private GameObject[] _playerHurt;
    [SerializeField]
    private GameObject _gameOverVisualizer;
    [SerializeField]
    private GameObject _levelpanelVisualizer;
    [SerializeField]
    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;
    public Animator _levelAnim;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _levelAnim = GameObject.Find("Level_panel").GetComponent<Animator>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn manager is not found");
        }

        if(_uiManager == null)
        {
            Debug.Log("UI Manager is NULL");
        }

        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
         float HorizontalInput = Input.GetAxis("Horizontal");
         float VerticalInput = Input.GetAxis("Vertical");

         transform.Translate(_speed * Time.deltaTime * HorizontalInput * Vector3.right);
         transform.Translate(_speed * Time.deltaTime * VerticalInput * Vector3.up);
         
        PlayerMovement();
        Fire();   
    }

    private void PlayerMovement()
    {
        //Bounds on the y-axis
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.62f)
        {
            transform.position = new Vector3(transform.position.x, -3.62f, 0);
        }

        //Bounds on the x-axis
        if (transform.position.x <= -9.2f)
        {
            transform.position = new Vector3(-9.2f, transform.position.y, 0);
        }
        else if (transform.position.x >= 9.2f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, 0);
        }
    }

    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_isTrippleShootActive == true)
            {
                Instantiate(_TrippleShootPrefab, transform.position, Quaternion.identity);

            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.96f, 0), Quaternion.identity);
            }
            _audioSource.Play();
        }
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
        
            StartCoroutine(ShieldPowerDown());
            return;
        }
        _lives--;

        if(_lives == 2)
        {
            _playerHurt[0].SetActive(true);
        }
        else if(_lives == 1)
        {
            _playerHurt[1].SetActive(true);
        }
        _uiManager.UpdateLives(_lives);
        if (_lives == 0)
        {
             
            _spawnManager.OnPlayerDeath();
            _gameOverVisualizer.SetActive(true);
            _uiManager.UpdateBestScore();
            Destroy(this.gameObject);
        }  
    }

    public void ActivateTrippleShoot()
    {
        _isTrippleShootActive = true;
        StartCoroutine(TrippleShootPowerDown());     
    }

    public void ActivateSpeedBoost()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        _visualizer.SetActive(true);
    }
    IEnumerator TrippleShootPowerDown()
    {
        yield return new WaitForSeconds(10.0f);
        _isTrippleShootActive = false;
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(10.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(10.0f);
        _visualizer.SetActive(false);
        _isShieldActive = false;
    }

    public void AddScore(int points)
    {
       _score += points;
        _uiManager.UpdateScore();
    }
}
