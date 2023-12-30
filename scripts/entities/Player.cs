using Godot;
using System;

public partial class Player : CharacterBody3D {
	[ExportGroup("Movement Variables")]
	[Export]
	public int WalkingSpeed { get; set; } = 2; //km/h
	[Export]
	public int RunningSpeed { get; set; } = 6; //km/h
	[Export]
	public float TimeToJumpPeak { get; set; } = .4f; //second
	[Export]
	public int JumpHeight { get; set; } = 2; //meter

	[ExportGroup("Node References")]
	[Export]
	public Node3D Model { get; private set; }
	[Export]
	public MovementComponent MovementComponent { get; private set; }
	[Export]
	public CameraComponent CameraComponent { get; private set; }
	[Export]
	public StateMachine StateMachine { get; private set; }

	public override void _Process(double delta) {
		var InputDirection = Vector3.Zero;
		InputDirection.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		InputDirection.Z = Input.GetActionStrength("move_backward") - Input.GetActionStrength("move_forward");

		MovementComponent.SetDirection(InputDirection);
	}
}
