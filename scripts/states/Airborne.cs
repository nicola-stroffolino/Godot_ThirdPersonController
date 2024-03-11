using Godot;
using System;

public partial class Airborne : State {
	public bool JumpQueued { get; set; } = false;
	public float JumpBufferCounter { get; set; } = 0;

	public override void Enter() {
		GD.Print("airborne");
		if (JumpQueued) {
			JumpBufferCounter = 0;
			JumpQueued = false;
		}
	}

	public override void Exit() {
		var v = Actor.MovementComponent.Velocity;
		Actor.MovementComponent.Velocity = new Vector3(v.X, 0, v.Z);

		Actor.AnimationTree.Set("parameters/jump_shot/request", (int)AnimationNodeOneShot.OneShotRequest.FadeOut);
	}

	public override State StatePhysicsProcess(float delta) {
		Actor.MovementComponent.ApplyVelocity();
		Actor.MovementComponent.ApplyGravity(delta);

		return null;
	}

	public override State StateProcess(float delta) {
		if (Input.IsActionJustPressed("jump") && Actor.IsFalling()) { // if descending
			GD.Print("next jump buffered");
			JumpQueued = true;
		}

		if (JumpQueued) {
			JumpBufferCounter += delta;

			if (JumpBufferCounter > 0.3) { // Next jump will NOT be buffered
				JumpQueued = false;
				JumpBufferCounter = 0;
				GD.Print("Next jump will NOT be buffered");
			}
		}

		if (Actor.IsOnFloor()) {
			if (Actor.WantsToStandStill()) return GetState<Idle>();
			if (Actor.WantsToWalk()) return GetState<Walk>();
			if (Actor.WantsToRun()) return GetState<Run>();
		}

		return null;
	}
}
