using Godot;
using System;

public partial class Jump : State {
	public override void Enter() {	
		GD.Print("jump");

		Actor.MovementComponent.Velocity = new Vector3(Actor.Velocity.X, Actor.MovementComponent.JumpSpeed, Actor.Velocity.Z);
		// Actor.AnimationTree.Set("parameters/jump_shot/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public override State StateProcess(float delta) {
		if (!Actor.IsOnFloor()) return GetState<Airborne>();

		return null;
	}
}
