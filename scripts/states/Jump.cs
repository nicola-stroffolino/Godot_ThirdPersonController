using Godot;
using System;

[GlobalClass]
public partial class Jump : State {

	// public bool Completed { get; set; } = false;

	public override void Enter() {	
		GD.Print("jump");
		// Actor.MovementComponent.SetVelocity('y', Actor.MovementComponent.JumpSpeed);
		// Completed = true;

		Actor.AnimationTree.Set("parameters/jump_shot/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public override void Exit() {
		// Completed = false;
	}

	public override void StateProcess(float delta) {
		if (!Actor.IsOnFloor()) {
			EmitSignal(SignalName.Transitioned, this, "airborne");
			return;
		}
	}
}
