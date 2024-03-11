using Godot;
using System;

public partial class Run : State {
	public override void Enter() {
		GD.Print("run");
		
		Actor.MovementComponent.ActualSpeed = Actor.MovementComponent.RunningSpeed;
	}
	
	public override State StatePhysicsProcess(float delta) {
		Actor.MovementComponent.ApplyVelocity();
		return null;
    }

	public override State StateProcess(float delta) {
		if (Actor.WantsToStandStill()) return GetState<Idle>();
		
		if (Actor.WantsToWalk()) return GetState<Walk>();

		if (Actor.WantsToJump()) return GetState<Jump>();

		if (Actor.IsFalling()) return GetState<Airborne>();
		
		return null;
	}
}
