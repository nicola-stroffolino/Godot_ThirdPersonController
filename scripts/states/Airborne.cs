using Godot;
using System;

public partial class Airborne : State {
	public bool JumpQueued { get; set; } = false;
	public float JumpBufferCounter { get; set; } = 0;

	public override void Enter() {
		GD.Print("airborne");
		if (JumpQueued) {
			JumpBufferCounter = 0;
			JumpQueued = false;
		}
		if (Actor.VerticalStateMachine.PreviousState is not Jump) {
			Actor.AnimationTree.Set("parameters/falling_idle/blend_amount", 1);
		}
	}

	public override void Exit() {
		var v = Actor.MovementComponent.Velocity;
		Actor.MovementComponent.Velocity = new Vector3(v.X, 0, v.Z);

		// Actor.AnimationTree.Set("parameters/falling_idle/blend_amount", 0);
		Actor.AnimationTree.Set("parameters/falling_idle/request", (int)AnimationNodeOneShot.OneShotRequest.FadeOut);
	}

	public override State StatePhysicsProcess(float delta) {
		var v = Actor.MovementComponent.Velocity;
		var g = Actor.MovementComponent.Gravity;
		Actor.MovementComponent.Velocity = new Vector3(v.X, v.Y - g * delta, v.Z);

		return null;
	}

	public override State StateProcess(float delta) {
		if (Input.IsActionJustPressed("jump") && Actor.Velocity.Y <= 0) { // if descending
			GD.Print("next jump buffered");
			JumpQueued = true;
		}

		if (JumpQueued) {
			JumpBufferCounter += delta;

			if (JumpBufferCounter > 0.3) { // Next jump will NOT be buffered
				JumpQueued = false;
				JumpBufferCounter = 0;
				GD.Print("Next jump will NOT be buffered");
			}
		}

		if (Actor.IsOnFloor()) return GetState<Grounded>();

		return null;
	}
}
