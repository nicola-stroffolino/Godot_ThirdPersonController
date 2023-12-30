using Godot;
using System;

public partial class AnimationComponent : Node {
	[Export]
	private AnimationTree TargetAnimationTree;

	public override void _Ready(){
		
	}

	private void MotionStatusChanged(double value, double delta) {
		const string IWR = "parameters/animation_state_machine/movement/iwr_blend/blend_amount";
		TargetAnimationTree.Set(IWR, Mathf.Lerp((float)TargetAnimationTree.Get(IWR), value, delta * 5));
	}

	private void JumpStateChanged(bool value) {
		TargetAnimationTree.Set("parameters/animation_state_machine/conditions/airborne", value);
		TargetAnimationTree.Set("parameters/animation_state_machine/conditions/landed", !value);
	}

	private void StrafeStateChanged(Vector2 strafe) {
		TargetAnimationTree.Set("parameters/animation_state_machine/movement/walk_blend/blend_position", strafe);
		TargetAnimationTree.Set("parameters/animation_state_machine/movement/run_blend/blend_position", strafe);
	}
}








