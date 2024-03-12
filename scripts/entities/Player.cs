using Godot;
using System;
using System.Reflection.Metadata;

public partial class Player : GameEntity3D {
	[Export]
	public CameraController CameraController { get; private set; }

	public float LookingRotation { get; set; }
	public bool IsLockedOn { get; set; } = false;

	public override void _Ready(){
		LookingRotation = Rotation.Y;
	}

	public override void _Process(double delta) {
		
	}
	
	int peppe = 0;

	public override void _PhysicsProcess(double delta) {
		if (Input.IsActionJustPressed("lock_to_target")) IsLockedOn = !IsLockedOn;

		var InputDirection = Vector3.Zero;
		InputDirection.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		InputDirection.Z = Input.GetActionStrength("move_backward") - Input.GetActionStrength("move_forward");

		Movement.Direction = InputDirection;
		if (IsLockedOn) {
			Movement.MoveDirection = InputDirection.Rotated(Vector3.Up, 
				Mathf.Atan2(
					GlobalPosition.X - LockedTarget.GlobalPosition.X, GlobalPosition.Z - LockedTarget.GlobalPosition.Z
				)
			).Normalized();
		} else {
			Movement.MoveDirection = InputDirection.Rotated(Vector3.Up, CameraController.GetHRot()).Normalized();
		}

		if (Movement.Direction != Vector3.Zero) {
			var n = Mathf.RadToDeg(Mathf.Atan2(Movement.MoveDirection.X, Movement.MoveDirection.Z));
			var o = Mathf.RadToDeg(LookingRotation);

			var angleDifference = Mathf.Abs(n - o);
			if (angleDifference > 180) angleDifference = 360 - angleDifference;
			
			// if (n != o) GD.Print($"New: {n} - Old: {o} - Result: {angleDifference}");

			LookingRotation = Mathf.Atan2(Movement.MoveDirection.X, Movement.MoveDirection.Z); 
		}

		Rotation = new() {
			Y = (float) Mathf.Wrap(
				Mathf.LerpAngle(
					Rotation.Y,
					LookingRotation,
					delta * 10 // Actor.StateMachine.CurrentState is Airborne ? 0f : 1f 
				),
				-Math.PI,
				Math.PI
			)
		};

		GetNode<Label>("Control/Label").Text = $@"Input Direction: {Movement.Direction}
			Move Direction: {Movement.MoveDirection}
			Velocity: {Movement.Velocity}

			Target Look: {LookingRotation}
			Locked On Target: {IsLockedOn}
		";
	}

	public override bool WantsToStandStill() => Movement.Direction == Vector3.Zero;
	public override bool WantsToWalk() => Movement.Direction != Vector3.Zero && !Input.IsActionPressed("sprint");
	public override bool WantsToRun() => Movement.Direction != Vector3.Zero && Input.IsActionPressed("sprint");
	public override bool WantsToJump() => (MovementStateMachine.PreviousState is Airborne j && j.JumpQueued) || (IsOnFloor() && Input.IsActionJustPressed("jump"));
	public override bool IsFalling() => !IsOnFloor() && Velocity.Y <= 0;
}
