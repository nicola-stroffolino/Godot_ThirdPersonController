using Godot;
using System;

public partial class Idle : State {	
	public override void Enter() {
		GD.Print("idle " + GetTree().Root.GetChild(0).Name);
		
		Actor.Movement.ActualSpeed = 0;
	}

	public override State StatePhysicsProcess(float delta) {
		Actor.Movement.ApplyVelocity();
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
