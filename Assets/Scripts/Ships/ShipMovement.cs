using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour 
{
	Rigidbody2D _rb;

	[SerializeField] float _accelerationPower = 5f;
	[SerializeField] float _steeringPower = 5f;
	float _steeringAmount, _speed, _direction;


	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () 
  {
		_steeringAmount = Input.GetAxis ("Horizontal");
		_speed = -Input.GetAxis ("Vertical") * _accelerationPower;
		//_speed = -1 * _accelerationPower;
		_direction = Mathf.Sign(Vector2.Dot (_rb.velocity, _rb.GetRelativeVector(Vector2.up)));
		_rb.rotation += _steeringAmount * _steeringPower * _rb.velocity.magnitude * _direction;

		_rb.AddRelativeForce (Vector2.up * _speed);

		_rb.AddRelativeForce (-Vector2.right * _rb.velocity.magnitude * _steeringAmount / 2);		
	}
}
