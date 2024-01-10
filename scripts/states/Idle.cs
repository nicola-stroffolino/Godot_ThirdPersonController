using Godot;
using System;

[GlobalClass]
public partial class Idle : State {
	public override void Enter() {
		GD.Print("idle");
		Actor.MovementComponent.ActualSpeed = 0;
		
		Actor.AnimationTree.Set("parameters/free_walk_or_run/blend_amount", -1);
	}

	public override void StateProcess(float delta) {
		if (!Actor.IsOnFloor() && Actor.StateMachine.PreviousState is not Jump) {
			EmitSignal(SignalName.Transitioned, this, "airborne");
			return;
		}

		if (Actor.StateMachine.PreviousState is Airborne j && j.JumpQueued) {
			EmitSignal(SignalName.Transitioned, this, "jump");
			return;
		}

		if (Actor.IsOnFloor() && Input.IsActionJustPressed("jump")) {
			EmitSignal(SignalName.Transitioned, this, "jump");
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
