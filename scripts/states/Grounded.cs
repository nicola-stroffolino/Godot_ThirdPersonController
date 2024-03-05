using Godot;
using System;

public partial class Grounded : State {
	public override void Enter() {
		
	}

	public override State StateProcess(float delta) {
		if (Actor.VerticalStateMachine.PreviousState is Airborne j && j.JumpQueued) return GetState<Jump>();

		if (Actor.IsOnFloor() && Input.IsActionJustPressed("jump")) return GetState<Jump>();

		if (!Actor.IsOnFloor()) return GetState<Airborne>();

		return null;
	}
}
