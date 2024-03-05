using Godot;
using System;

public partial class Jump : State {
	public override void Enter() {	
		GD.Print("jump");

		Actor.AnimationTree.Set("parameters/jump_shot/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public override State StateProcess(float delta) {
		if (!Actor.IsOnFloor()) {
			// EmitSignal(SignalName.Transitioned, this, "airborne");
			return GetState<Airborne>();
		}

		return null;
	}
}
