using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
    NULL,
    CHASER,
    SHOOTER
}

public class EnemyShip : MonoBehaviour
{
    [SerializeField] ShipType _shipType;

    [SerializeField] int _hullHitDamage;

    [SerializeField] GameObject _explosion, _cannonball;

    [SerializeField] float _distanceToAct;
    [SerializeField] Vector2 _playerDistance;
    [SerializeField] Vector3 localPosition;
    GameObject _target;

    bool _canShoot = true;

    Rigidbody2D _rb;

    [SerializeField] float _accelerationPower;
    float _plusAcceleration;
    [SerializeField] float _steeringPower;
    float _steeringAmount, _speed, _direction;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");

        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_shipType == ShipType.CHASER)
        {
            ChasePlayer();
        }

        _playerDistance = _target.transform.position - transform.position;

        OnDistanceToAct();
    }

    void ChasePlayer()
    {
        localPosition = _target.transform.position - transform.position;
        localPosition = localPosition.normalized;

        _steeringAmount = localPosition.x * localPosition.y;

        _direction = Mathf.Sign(Vector2.Dot(_rb.velocity, _rb.GetRelativeVector(Vector2.up)));

        _speed = -1 *   (_accelerationPower + _plusAcceleration);

        _rb.rotation += _steeringAmount * _steeringPower * _rb.velocity.magnitude * _direction;

        _rb.AddRelativeForce(Vector2.up * _speed);

        _rb.AddRelativeForce(Vector2.right * _rb.velocity.magnitude * _steeringAmount / 2);
    }

    public void ShipWrecked()
    {
        GetComponent<Animator>().SetTrigger("Sinking");

        _accelerationPower = 0;
        _steeringPower = 0;

        GameBehaviour.OnEarningPoints?.Invoke();

        StartCoroutine(DestroyingGameObject());
    }

    void OnDistanceToAct()
    {
        if (_playerDistance.magnitude <= _distanceToAct)
        {
            switch (_shipType)
            {
                case ShipType.CHASER:
                    {
                        _plusAcceleration = 5;

                        return;
                    }
                case ShipType.SHOOTER:
                    {
                        if(_canShoot)
                        {
                            StartCoroutine(Shots());
                        }
                       
                        _plusAcceleration = -3;

                        return;
                    }
            }
        }
        else
        {
            switch (_shipType)
            {
                case ShipType.CHASER:
                    {
                        _plusAcceleration = 0;

                        return;
                    }
                case ShipType.SHOOTER:
                    {
                        StopCoroutine(Shots());

                        ChasePlayer();

                        _plusAcceleration = 0;

                        return;
                    }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (_shipType == ShipType.CHASER)
            {
                ICanBeDamaged _iCanBeDamaged = collision.collider.GetComponentInParent<ICanBeDamaged>();

                _iCanBeDamaged.TakingDamage(_hullHitDamage);

                GameObject temp = Instantiate(_explosion, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
                temp.transform.localScale = new Vector2(3, 3);

                GetComponent<ShipBehaviour>().FullDamage();

                StartCoroutine(DestroyingGameObject());
            }
        }
    }

    public IEnumerator Shots()
    {
        _canShoot = false;

        yield return new WaitForSeconds(3);


        Vector3 difference = new Vector3(_target.transform.position.x,
                                         _target.transform.position.y,
                                         _target.transform.position.z) - transform.position;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        //transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ + 180);
        
        GameObject temp = Instantiate(_cannonball, new Vector2(transform.position.x, transform.position.y), Quaternion.Euler(transform.position.x, transform.position.y, transform.rotation.z));

        temp.GetComponent<Cannonball>().ShotDirection(difference);

        _canShoot = true;
    }

    IEnumerator DestroyingGameObject()
    {
        yield return new WaitForSeconds(1);

        GetComponent<ShipBehaviour>().DestroyCanvas();
        Destroy(gameObject);

    }
}
