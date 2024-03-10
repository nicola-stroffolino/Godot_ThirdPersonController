using Godot;
using System;


public partial class Walk : State {
	public override void Enter() {
		GD.Print("walk");
		Actor.MovementComponent.ActualSpeed = Actor.MovementComponent.WalkingSpeed;

		// Actor.AnimationTree.Set("parameters/free_iwr_blend/blend_amount", 0);
	}

	public override State StateProcess(float delta) {	
		if (Actor.WantsToStandStill()) return GetState<Idle>();

		if (Actor.WantsToRun()) return GetState<Run>();

		return null;
	}
}
