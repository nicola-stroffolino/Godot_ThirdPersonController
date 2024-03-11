using Godot;
using System;

public partial class Walk : State {
	public override void Enter() {
		GD.Print("walk");

		Actor.MovementComponent.ActualSpeed = Actor.MovementComponent.WalkingSpeed;
	}

	public override State StatePhysicsProcess(float delta) {
		Actor.MovementComponent.ApplyVelocity();
		return null;
    }

	public override State StateProcess(float delta) {	
		if (Actor.WantsToStandStill()) return GetState<Idle>();

		if (Actor.WantsToRun()) return GetState<Run>();

		if (Actor.WantsToJump()) return GetState<Jump>();

		if (Actor.IsFalling()) return GetState<Airborne>();

		return null;
	}
}
