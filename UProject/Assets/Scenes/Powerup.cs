using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private int PowerupID;
    [SerializeField]
    private AudioClip _AudioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_AudioClip, transform.position);

            switch (PowerupID)
            {
                case 0:
                   player.TripleShotActive();
                    break;
                case 1:
                    player.SpeedPowerupActive();
                    break;
                case 2:
                    player.ShieldPowerupActive();
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
