using Godot;
using System;

public partial class Run : State {
	public override void Enter() {
		GD.Print("run");
		
		Actor.Movement.ActualSpeed = Actor.Movement.RunningSpeed;
	}
	
	public override State StatePhysicsProcess(float delta) {
		Actor.Movement.ApplyVelocity();
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
