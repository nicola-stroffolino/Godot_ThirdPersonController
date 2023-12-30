using Godot;
using System;

[GlobalClass]
public partial class Jump : State {
	public bool JumpQueued { get; set; } = false;
	public float JumpBufferCounter { get; set; } = 0;

	public override void Enter() {	
		GD.Print("jump");	
		Actor.MovementComponent.SetVelocity('y', Actor.MovementComponent.JumpSpeed);
		if (Actor.MovementComponent.GetVelocity().Y == 0) GD.Print("che cazzo sta succedendo");
		if (JumpQueued) {
			JumpBufferCounter = 0;
			JumpQueued = false;
		}
	}

    public override void Exit() {
        Actor.MovementComponent.SetVelocity('y', 0);
    }

    public override void StateProcess(float delta) {
		if (Input.IsActionJustPressed("jump") && Actor.Velocity.Y <= 0) { // if descending
			GD.Print("next jump buffered");
			JumpQueued = true;
		}

		if (JumpQueued) {
			JumpBufferCounter += delta;
			// GD.Print(JumpBufferCounter);

			if (JumpBufferCounter > 0.3) { // Next jump will NOT be buffered
				JumpQueued = false;
				JumpBufferCounter = 0;
			}
		}

		if (Actor.IsOnFloor()) {
			if (Actor.MovementComponent.Direction == Vector3.Zero) {
				EmitSignal(SignalName.Transitioned, this, "idle");
				return;
			}

			if (Actor.MovementComponent.Direction != Vector3.Zero && !Input.IsActionPressed("sprint")) {
				EmitSignal(SignalName.Transitioned, this, "walk");
				return;
			}

			if (Actor.MovementComponent.Direction != Vector3.Zero && Input.IsActionPressed("sprint")) {
				EmitSignal(SignalName.Transitioned, this, "run");
				return;
			}
		}
	}
}
