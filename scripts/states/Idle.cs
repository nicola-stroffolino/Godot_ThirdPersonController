using Godot;
using System;

public partial class Idle : State {	
	public override void Enter() {
		// base.Enter();
		// Tween = Actor.CreateTween();
		GD.Print("idle");
		Actor.MovementComponent.ActualSpeed = 0;

		// Actor.AnimationTree.Set("parameters/free_iwr_blend/blend_amount", -1);
		// Tween.TweenProperty(Actor.AnimationTree, "parameters/free_iwr_blend/blend_amount", -1, 0.25);
		Tween?.Kill();
		Tween = Actor.CreateTween();
		Tween.TweenProperty(Actor.AnimationTree, "parameters/free_iwr_blend/blend_amount", -1, 0.25);
	}

	public override void Exit()
	{	
		// Tween.Kill();
	}

	public override State StateProcess(float delta) {
		

		if (Actor.MovementComponent.Direction != Vector3.Zero && !Input.IsActionPressed("sprint")) {
			return GetState<Walk>();
		}

		if (Actor.MovementComponent.Direction != Vector3.Zero && Input.IsActionPressed("sprint")) {
			return GetState<Run>();
		}

		return null;
	}
}
