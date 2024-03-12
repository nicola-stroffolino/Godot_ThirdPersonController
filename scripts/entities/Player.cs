using Godot;
using System;

public partial class Player : GameEntity3D {
	[Export]
	public CameraController CameraController { get; private set; }
	
	public override void _Process(double delta) {
		var InputDirection = Vector3.Zero;
		InputDirection.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		InputDirection.Z = Input.GetActionStrength("move_backward") - Input.GetActionStrength("move_forward");

		Movement.Direction = InputDirection;
		Movement.MoveDirection = InputDirection.Rotated(Vector3.Up, CameraController.GetHRot()).Normalized();
	
		GetNode<Label>("Control/Label").Text = $@"Input Direction: {Movement.Direction}
			Move Direction: {Movement.MoveDirection}
			Velocity: {Movement.Velocity}

			Target Look: 
		";

		LookAt(LockedTarget.GlobalPosition);
	}

	public override bool WantsToStandStill() => Movement.Direction == Vector3.Zero;
	public override bool WantsToWalk() => Movement.Direction != Vector3.Zero && !Input.IsActionPressed("sprint");
	public override bool WantsToRun() => Movement.Direction != Vector3.Zero && Input.IsActionPressed("sprint");
	public override bool WantsToJump() => (MovementStateMachine.PreviousState is Airborne j && j.JumpQueued) || (IsOnFloor() && Input.IsActionJustPressed("jump"));
	public override bool IsFalling() => !IsOnFloor() && Velocity.Y <= 0;
}
