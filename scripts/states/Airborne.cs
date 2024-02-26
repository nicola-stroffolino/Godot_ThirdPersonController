using Godot;
using System;

[GlobalClass]
public partial class Airborne : State {
	public bool JumpQueued { get; set; } = false;
	public float JumpBufferCounter { get; set; } = 0;

	public override void Enter() {
		GD.Print("airborne");
		if (JumpQueued) {
			JumpBufferCounter = 0;
			JumpQueued = false;
		}
		if (Actor.StateMachine.PreviousState is not Jump) {
			Actor.AnimationTree.Set("parameters/falling_idle/blend_amount", 1);
		}
	}

	public override void Exit() {
		var v = Actor.MovementComponent.Velocity;
		Actor.MovementComponent.Velocity = new Vector3(v.X, 0, v.Z);

		Actor.AnimationTree.Set("parameters/falling_idle/blend_amount", 0);
	}

	public override void StatePhysicsProcess(float delta) {
		var v = Actor.MovementComponent.Velocity;
		var g = Actor.MovementComponent.Gravity;
		Actor.MovementComponent.Velocity = new Vector3(v.X, v.Y - g * delta, v.Z);
	}

	public override void StateProcess(float delta) {
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

		if (Actor.IsOnFloor() && Actor.MovementComponent.Velocity.Y < 0) {
			if (Actor.MovementComponent.Direction == Vector3.Zero) {
				EmitSignal(SignalName.Transitioned, this, "idle");
				return;
			}

			if (Actor.MovementComponent.Direction != Vector3.Zero && !Input.IsActionPressed("sprint")) {
				EmitSignal(SignalName.Transitioned, this, "walk");
				return;
			}

			if (Actor.MovementComponent.Direction != Vector3.Zero && Input.IsActionPressed("sprint")) {
				EmitSignal(SignalName.Transitioned, this, "run");
				return;
			}
		}
	}
}
