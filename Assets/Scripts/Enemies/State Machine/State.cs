using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
	protected FiniteStateMachine stateMachine;
	protected Entity entity;

	protected float startTime;

	protected string animBoolName;

	protected bool isAnimationFinished;

	public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
	{
		this.entity = entity;
		this.stateMachine = stateMachine;
		this.animBoolName = animBoolName;
	}

	public virtual void Enter()
	{
		startTime = Time.time;
		entity.Anim.SetBool(animBoolName, true);
		DoChecks();

	}

	public virtual void Exit()
	{
		entity.Anim.SetBool(animBoolName, false);
	}

	public virtual void LogicUpdate()
	{

	}

	public virtual void PhysicsUpdate()
	{
		DoChecks();
	}

	public virtual void DoChecks()
	{

	}

	public virtual void AnimationTrigger() { } //Am animationspunkt wo etwas passieren soll 
	public virtual void AnimationFinishTrigger() => isAnimationFinished = true;  //Am ende der Attack
}
