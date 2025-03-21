using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private int _speed = 3;
    [SerializeField]
    private int PowerUpID;
    private UIManager _uimanager;
    // Start is called before the first frame update
    void Start()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uimanager == null)
        {
            Debug.LogError("UIManager does not exist");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);
        
        if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

        if (_uimanager.playerScore > 50 && _uimanager.playerScore <= 80)
        {
            _speed = 5;
        }
        else if (_uimanager.playerScore > 80 && _uimanager.playerScore <= 150)
        {
            _speed = 8;
        }
        else if (_uimanager.playerScore > 150)
        {
            _speed = 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                switch (PowerUpID)
                {
                    case 0:
                        player.ActivateTrippleShoot();
                        break;
                    case 1:
                        player.ActivateSpeedBoost();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                    default:
                        Debug.Log("shield");
                        break;
                }
               
            }
            Destroy(this.gameObject);
        }
    }
}
