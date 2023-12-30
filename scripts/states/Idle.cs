using Godot;
using System;

[GlobalClass]
public partial class Idle : State {
	public override void Enter() {
		GD.Print("idle");
		Actor.MovementComponent.ActualSpeed = 0;
	}

	public override void StateProcess(float delta) {
		if (Actor.StateMachine.PreviousState is Jump j && j.JumpQueued) {
			// GD.Print("comencing new jump");
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
