using Godot;
using System;

[GlobalClass]
public partial class Jump : State {
	public override void Enter() {	
		GD.Print("jump");

		Actor.AnimationTree.Set("parameters/jump_shot/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

	public override void Exit() {
		
	}

	public override void StateProcess(float delta) {
		if (!Actor.IsOnFloor()) {
			EmitSignal(SignalName.Transitioned, this, "airborne");
			return;
		}
	}
}
