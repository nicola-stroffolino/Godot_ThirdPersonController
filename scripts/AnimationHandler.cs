using Godot;
using System;

public partial class AnimationHandler : AnimationTree {
	[Export]
	public StateMachine VerticalStateMachine { get; set; }
	[Export]
	public StateMachine MovementStateMachine { get; set; }
	[Export]
	public MovementComponent MovementComponent { get; set; }

	public override void _Ready() {
		
	}

	public override void _Process(double delta) {
		// switch (StateMachine.CurrentState) {
		// 	case Idle:
		// 		BlendFreeIwr(-1, delta * 10);
		// 		break;
		// 	case Walk:
		// 		BlendFreeIwr(0, delta * 10);
		// 		break;
		// 	case Run:
		// 		BlendFreeIwr(1, delta * 5);
		// 		break;
		// 	default:
		// 		break;
		// }
	}

	// private void BlendFreeIwr(double value, double weight) {
	// 	const string FREE_IWR = "parameters/free_iwr_blend/blend_amount";
	// 	Set(FREE_IWR, Mathf.Lerp((float)Get(FREE_IWR), value, weight));
	// }

	
}
