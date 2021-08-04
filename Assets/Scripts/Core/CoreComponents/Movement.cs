using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
	public Rigidbody2D RB { get; private set; }

	public int FacingDirection { get; private set; }

	public Vector2 CurrentVelocity { get; private set; }

	private Vector2 workSpace;

	protected override void Awake()
	{
		base.Awake();

		RB = GetComponentInParent<Rigidbody2D>();

		FacingDirection = 1;
	}

	public void LogicUpdate()
	{
		CurrentVelocity = RB.velocity;
	}

	#region Set Functions
	public void SetVelocityX(float velocity)
	{
		workSpace.Set(velocity, CurrentVelocity.y);
		RB.velocity = workSpace;
		CurrentVelocity = workSpace;
	}

	public void SetVelocityY(float velocity)
	{
		workSpace.Set(CurrentVelocity.x, velocity);
		RB.velocity = workSpace;
		CurrentVelocity = workSpace;
	}
	#endregion

	#region Check Functions
	public void CheckIfShouldFlip(int xInput)
	{
		if (xInput != 0 && xInput != FacingDirection)
		{
			Flip();
		}
	}
	#endregion

	#region Other Functions
	private void Flip()
	{
		FacingDirection *= -1;

		RB.transform.Rotate(0.0f, 180.0f, 0.0f);
	}
	#endregion
}
