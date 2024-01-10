using Godot;
using System;

[GlobalClass]
public partial class Jump : State {

	public bool Completed { get; set; } = false;

	public override void Enter() {	
		GD.Print("jump");	
		Actor.MovementComponent.SetVelocity('y', Actor.MovementComponent.JumpSpeed);
		Completed = true;

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
		Actor.AnimationTree.Set("parameters/jump_Shot/request", (int)AnimationNodeOneShot.OneShotRequest.Fire);
	}

    public override void StateProcess(float delta) {
		if (Completed) {
			EmitSignal(SignalName.Transitioned, this, "airborne");
			return;
		}
	}
}
