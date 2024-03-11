using Godot;
using System;

public partial class Player : CharacterBody3D {
	[ExportGroup("Node References")]
	[Export]
	public Node3D Model { get; private set; }
	[Export]
	public MovementComponent MovementComponent { get; private set; }
	[Export]
	public CameraController CameraController { get; private set; }
	[Export]
	public StateMachine MovementStateMachine { get; private set; }
	[Export]
	public AnimationTree AnimationTree { get; private set; }
	[Export]
	public AnimationPlayer AnimationPlayer { get; private set; }

	public override void _Process(double delta) {
		var InputDirection = Vector3.Zero;
		InputDirection.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		InputDirection.Z = Input.GetActionStrength("move_backward") - Input.GetActionStrength("move_forward");

		MovementComponent.Direction = InputDirection;
		MovementComponent.MoveDirection = InputDirection.Rotated(Vector3.Up, CameraController.GetHRot()).Normalized();
	}

	public bool WantsToStandStill() => MovementComponent.Direction == Vector3.Zero;
	public bool WantsToWalk() => MovementComponent.Direction != Vector3.Zero && !Input.IsActionPressed("sprint");
	public bool WantsToRun() => MovementComponent.Direction != Vector3.Zero && Input.IsActionPressed("sprint");
	public bool WantsToJump() => (MovementStateMachine.PreviousState is Airborne j && j.JumpQueued) || (IsOnFloor() && Input.IsActionJustPressed("jump"));
	public bool IsFalling() => !IsOnFloor() && Velocity.Y <= 0;
}
