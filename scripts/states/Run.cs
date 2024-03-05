using Godot;
using System;

public partial class Run : State {
	public override void Enter() {
		GD.Print("run");
		Actor.MovementComponent.ActualSpeed = Actor.MovementComponent.RunningSpeed;

		Actor.AnimationTree.Set("parameters/free_iwr_blend/blend_amount", 1);
	}
	
	public override State StateProcess(float delta) {
		if (Actor.MovementComponent.Direction == Vector3.Zero) {
			// EmitSignal(SignalName.Transitioned, this, "idle");
			return GetState<Idle>();
		}

		if (!Input.IsActionPressed("sprint")) {
			// EmitSignal(SignalName.Transitioned, this, "walk");
			return GetState<Walk>();
		}

		return null;
	}
}
