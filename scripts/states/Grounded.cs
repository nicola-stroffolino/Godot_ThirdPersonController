using Godot;
using System;

public partial class Grounded : State {
	public override void Enter() {
		GD.Print("grounded");
	}

	public override State StateProcess(float delta) {
		if (Actor.WantsToJump()) return GetState<Jump>();

		if (!Actor.IsOnFloor()) return GetState<Airborne>();

		return null;
	}
}
