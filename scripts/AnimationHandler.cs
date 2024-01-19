using Godot;
using System;

public partial class AnimationHandler : AnimationTree {
	[Export]
	public Player Actor { get; set; }
	
	public override void _Ready() {
	}

	public override void _Process(double delta) {
		switch (Actor.StateMachine.CurrentState) {
			case Jump:
				// float animationLength = 0f;
				// switch (Actor.StateMachine.PreviousState) {
				// 	case Idle:
				// 		animationLength = Actor.AnimationTree.GetAnimation("jump").Length;
				// 		Actor.AnimationTree.Set("parameters/jump_anim_transition/transition_request", "idle");
				// 		break;
				// 	case Walk:
				// 	case Run:
				// 		animationLength = Actor.AnimationTree.GetAnimation("running_jump").Length;
				// 		Actor.AnimationTree.Set("parameters/jump_anim_transition/transition_request", "moving");
				// 		break;
				// 	default:
				// 		break;
				// }

				// Actor.AnimationTree.Set("parameters/jump_time_scale/scale", animationLength / (Actor.TimeToJumpPeak * 2));
				Set("parameters/jump_shot/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
				break;
			case Idle:
				Set("parameters/free_walk_or_run/blend_amount", -1);
				break;
			case Walk:
				Set("parameters/free_walk_or_run/blend_amount", 0);
				break;
			case Run:
				Set("parameters/free_walk_or_run/blend_amount", 1);
				break;
			default:
				break;
		}
	}
}
