using Godot;
using System;

[GlobalClass]
public partial class Walk : State {
	public override void Enter() {
		GD.Print("walk");
		Actor.MovementComponent.ActualSpeed = Actor.WalkingSpeed;
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

		if (Actor.MovementComponent.Direction == Vector3.Zero) {
			EmitSignal(SignalName.Transitioned, this, "idle");
			return;
		}

		if (Input.IsActionPressed("sprint")) {
			EmitSignal(SignalName.Transitioned, this, "run");
			return;
		}
	}
}
