using Godot;
using System;

public partial class Idle : State {	
	public override void Enter() {
		GD.Print("idle");
		Actor.MovementComponent.ActualSpeed = 0;

		Actor.AnimationTree.Set("parameters/free_iwr_blend/blend_amount", -1);
	}

	public override State StateProcess(float delta) {
		if (Actor.WantsToWalk()) return GetState<Walk>();

		if (Actor.WantsToRun()) return GetState<Run>();

		return null;
	}
}
