using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSense : CoreComponent
{
	public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }

	[SerializeField] private Transform groundCheck;

	[SerializeField] private float groundCheckRadius;

	[SerializeField] private LayerMask whatIsGround;

	public bool Ground => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
}
