using Godot;
using System;

public partial class AnimationHandler : AnimationTree {
	[Export]
	public Player Actor { get; set; }
	
	public override void _Ready() {
	}

	public override void _Process(double delta) {
		switch (Actor.StateMachine.CurrentState) {
			case Idle:
				break;
			default:
				break;
		}
	}
}
