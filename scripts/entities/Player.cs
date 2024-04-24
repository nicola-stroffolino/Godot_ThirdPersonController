using Godot;
using System;
using System.Reflection.Metadata;

public partial class Player : GameEntity3D {
	public CameraController CameraController { get; set; }

	public override void _Ready(){
		FacingAngle = Rotation.Y;
	}

	public override void _Input(InputEvent @event) {
		var InputDirection = Vector3.Zero;
		InputDirection.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		InputDirection.Z = Input.GetActionStrength("move_backward") - Input.GetActionStrength("move_forward");
		Movement.Direction = InputDirection;
	}

	public override void _PhysicsProcess(double delta) {
		if (LockedTarget is not null) {
			var targetDirection = GlobalPosition - LockedTarget.GlobalPosition;
			Movement.MoveDirection = Movement.Direction.Rotated(Vector3.Up, Mathf.Atan2(targetDirection.X, targetDirection.Z)).Normalized();
			FacingAngle = Mathf.Atan2(-targetDirection.X, -targetDirection.Z);
		} else {
			var cameraDirection = CameraController.HCamRotation;
			Movement.MoveDirection = Movement.Direction.Rotated(Vector3.Up, cameraDirection).Normalized();

			if (Movement.Direction != Vector3.Zero) FacingAngle = Mathf.Atan2(Movement.MoveDirection.X, Movement.MoveDirection.Z);
		}

		if (Movement.Direction != Vector3.Zero) {
			var n = Mathf.RadToDeg(Mathf.Atan2(Movement.MoveDirection.X, Movement.MoveDirection.Z));
			var o = Mathf.RadToDeg(FacingAngle);

			var angleDifference = Mathf.Abs(n - o);
			if (angleDifference > 180) angleDifference = 360 - angleDifference;	
			
			// if (n != o) GD.Print($"New: {n} - Old: {o} - Result: {angleDifference}");

			// FacingAngle = Mathf.Atan2(Movement.MoveDirection.X, Movement.MoveDirection.Z); 
		}

		Rotation = new() {
			Y = (float) Mathf.Wrap(
				Mathf.LerpAngle(
					Rotation.Y,
					// IsLockedOn ? lockedFacingAngle + Mathf.Pi : FacingAngle,
					FacingAngle + Mathf.Pi,
					delta * 10
				),
				-Math.PI,
				Math.PI
			)
		};
	}

	public override void _Process(double delta) {
		GetNode<Label>("Control/Label").Text = $@"Input Direction: {Movement.Direction}
			Move Direction: {Movement.MoveDirection}
			Velocity: {Movement.Velocity}

			Target Look: {FacingAngle}
			Locked On Target: {LockedTarget is not null}
		";

		AnimationTree.Set("parameters/state_machine/strafe_state_machine/walk_blend/blend_position", new Vector2(-Movement.Direction.X, -Movement.Direction.Z)); 
		AnimationTree.Set("parameters/state_machine/strafe_state_machine/run_blend/blend_position", new Vector2(-Movement.Direction.X, -Movement.Direction.Z));
	}

	public void OnTargetLocked(LockMarker target) {
		LockedTarget = target;
	} 

	public override bool WantsToStandStill() => Movement.Direction == Vector3.Zero;
	public override bool WantsToWalk() => Movement.Direction != Vector3.Zero && !Input.IsActionPressed("sprint");
	public override bool WantsToRun() => Movement.Direction != Vector3.Zero && Input.IsActionPressed("sprint");
	public override bool WantsToJump() => (MovementStateMachine.PreviousState is Airborne j && j.JumpQueued) || (IsOnFloor() && Input.IsActionJustPressed("jump"));
	public override bool IsFalling() => !IsOnFloor() && Velocity.Y <= 0;
}
