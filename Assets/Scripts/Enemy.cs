using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _speed = 3;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private UIManager _uimanager;
    private Animator _anim;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_uimanager == null)
        {
            Debug.LogError("UIManager is NULL");
        }
        if(_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("AudioSource is NULL");
        } 
         
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);
        if(transform.position.y <= -6.0f)
        {
            Destroy(this.gameObject);
        }

        if(_uimanager.playerScore > 50 && _uimanager.playerScore <= 80)
        {
            _speed =  5 ;
        }
        else if(_uimanager.playerScore > 80 && _uimanager.playerScore <= 150)
        {
            _speed = 8 ;
        }
        else if (_uimanager.playerScore > 150)
        {
            _speed = 10 ;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
            
        }

        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(1);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
           
        }
    }
}
