using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] GameObject _explosion;

    [SerializeField] int _damage;
    [SerializeField] float _speed;
    public float Speed { get { return _speed; } }

    [SerializeField] AudioClip _sfxShoot;
    [SerializeField] AudioClip _sfxExplode;

    private void Start()
    {
        SoundsBehaviour.OnPlayingSound?.Invoke(_sfxShoot);
    }

    private void LateUpdate()
    {
        CannonballOnScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICanBeDamaged _iCanBeDamaged = collision.GetComponentInParent<ICanBeDamaged>();

        if (_iCanBeDamaged != null)
        {
            _iCanBeDamaged.TakingDamage(_damage);

            SoundsBehaviour.OnPlayingSound?.Invoke(_sfxExplode);

            Instantiate(_explosion, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }

        if(collision.CompareTag("STRUCTURE"))
        {
            Instantiate(_explosion, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void CannonballOnScreen()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if ((0f <= viewPos.x && viewPos.x <= 1F) &&
            (0f <= viewPos.y && viewPos.y <= 1F) &&
            (Camera.main.transform.position.z < this.transform.position.z))
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShotDirection(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * Speed;
    }
}
