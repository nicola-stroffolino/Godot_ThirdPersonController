using Godot;
using System;

public partial class Idle : State {	
	public override void Enter() {
		GD.Print("idle");
		
		Actor.MovementComponent.ActualSpeed = 0;
	}

	public override State StatePhysicsProcess(float delta) {
		Actor.MovementComponent.ApplyVelocity();
		return null;
	}

	public override State StateProcess(float delta) {
		if (Actor.WantsToWalk()) return GetState<Walk>();

		if (Actor.WantsToRun()) return GetState<Run>();

		if (Actor.WantsToJump()) return GetState<Jump>();

		if (Actor.IsFalling()) return GetState<Airborne>();

		return null;
	}
}
