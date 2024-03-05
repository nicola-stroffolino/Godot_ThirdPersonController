using System;
using Godot;

[GlobalClass]
public partial class MovementComponent : Node {
// 	[Export]
// 	private CharacterBody3D Actor;
// 	[Export]
// 	public int WalkingSpeed { get; set; } = 2; //km/h
// 	[Export]
// 	public int RunningSpeed { get; set; } = 6; //km/h
// 	[Export]
// 	public float TimeToJumpPeak { get; set; } = .4f; //second
// 	[Export]
// 	public int JumpHeight { get; set; } = 2; //meter
	
// 	[Signal]
// 	public delegate void MotionStateEventHandler(double value, double delta);
// 	[Signal]
// 	public delegate void JumpStateEventHandler(bool value);
// 	[Signal]
// 	public delegate void StrafeStateEventHandler(Vector2 strafe);
	
// 	private int ActualSpeed;
// 	private float Gravity;
// 	private float JumpSpeed;
// 	private float AngularAcceleration = 7;
// 	private AnimationTree AnimTree;
	
// 	public override void _Process(double delta) {
// 		Gravity = 2 * JumpHeight / (TimeToJumpPeak*TimeToJumpPeak); //m/s^2
// 		JumpSpeed = Gravity * TimeToJumpPeak; //m/s
// 	}
	
// 	private Vector3 Direction = Vector3.Zero;
// 	private Vector3 Velocity = Vector3.Zero;
// 	private Vector3 StrafeDirection = Vector3.Zero;
// 	private Vector3 Strafe = Vector3.Zero;
// 	private bool Ascending = false;
// 	private bool Airborne = false;
// 	private double DeltaTot = 0f;
// 	private float HCamRotation = 0;
// 	private double inertia = 0;
	
// 	public override void _PhysicsProcess(double delta) {
// 		inertia = delta * 5;
// 		if (!Actor.IsOnFloor()) Velocity.Y -= (float)(Gravity * delta);
// 		ChangeVelocity(delta);
// 		RotateActor(delta);
// 		HandleJump();
// 		Actor.Velocity = Velocity;
// 		Actor.MoveAndSlide();
// 	}
	
// 	private void ChangeVelocity(double delta) {
// 		if (Direction == Vector3.Zero) {
// 			Velocity.X = Mathf.Lerp(Velocity.X, 0f, (float)delta * 5);
// 			Velocity.Z = Mathf.Lerp(Velocity.Z, 0f, (float)delta * 5);
// 			// Animation
// 			EmitSignal(SignalName.MotionState, -1, delta);
// 			return;
// 		}
		
// 		Velocity.X = Mathf.Lerp(Velocity.X, Direction.X * ActualSpeed, (float)delta * 5);
// 		Velocity.Z = Mathf.Lerp(Velocity.Z, Direction.Z * ActualSpeed, (float)delta * 5);
// 		// Animation
// 		EmitSignal(SignalName.MotionState, ActualSpeed == RunningSpeed ? 1 : 0, delta);
// 	}
	
// 	private void RotateActor(double delta) {
// 		//if (Direction == Vector3.Zero) return;
		
// 		HCamRotation = GetNode<Node3D>("../CameraComponent/Horizontal").GlobalTransform.Basis.GetEuler().Y;
// 		var Armature = GetNode<Node3D>("../Armature");
// 		Armature.Rotation = new Vector3 {
// 			X = (float)Math.PI/2, // 90 deg = π/2
// 			Y = (float)Mathf.LerpAngle(Armature.Rotation.Y, HCamRotation + (float)Math.PI, delta * AngularAcceleration)
// 		};
// 	}
		
// 	private void HandleJump() {
// 		if (!Actor.IsOnFloor()) {
// 			Airborne = true;
// 			//Animation
// 			EmitSignal(SignalName.JumpState, true);
// 			return;
// 		}
		
// 		if (Airborne) {
// 			Airborne = false;
// 			// Animation
// 			EmitSignal(SignalName.JumpState, false);
// 		}
// 	}
	
// 	private void RotateDirection(Vector3 direction) {
// 		Direction = direction.Rotated(Vector3.Up, HCamRotation).Normalized();
// 	}
	
// 	private void HandleStrafe(Vector3 direction) {
// 		StrafeDirection = StrafeDirection.Lerp(direction, (float)inertia);
// 		// Animatiion
// 		EmitSignal(SignalName.StrafeState, new Vector2(-StrafeDirection.X, -StrafeDirection.Z));
// 	}
	
// 	private void ChangeSpeed(bool sprinting) {
// 		ActualSpeed = sprinting ? RunningSpeed : WalkingSpeed;
// 	}
	
// 	private void Jump() {
// 		if (Actor.IsOnFloor()) Velocity.Y = JumpSpeed;
// 	}

	[Export]
	public Player Actor { get; private set; }
	[Export]
	public int WalkingSpeed { get; set; } = 2; //km/h
	[Export]
	public int RunningSpeed { get; set; } = 6; //km/h
	[Export]
	public float TimeToJumpPeak { get; set; } = .4f; //second
	[Export]
	public int JumpHeight { get; set; } = 1; //meter

	// Functional Variables
	public int ActualSpeed { get; set; }
	public float Gravity { get; set; }
	public float JumpSpeed { get; set; }
	public Vector3 Direction { get; set; } = Vector3.Zero;
	public Vector3 MoveDirection { get; set; } = Vector3.Zero;
	public Vector3 Velocity { get; set; } = Vector3.Zero;

	public override void _Ready() {
		Gravity =	2 * JumpHeight / (TimeToJumpPeak * TimeToJumpPeak); //m/s^2;
		JumpSpeed = Gravity * TimeToJumpPeak; //m/s
	}

	public override void _Process(double delta) {
		GetNode<Label>("../Control/Label").Text = $@"Input Direction: {Direction}
			Move Direction: {MoveDirection}
			Velocity: {Velocity}

			Target Look: 
		";
	}

	public override void _PhysicsProcess(double delta) {
		// if (Actor.StateMachine.CurrentState is Airborne && Direction == Vector3.Zero) {
		// 	// Velocity = new Vector3(0, Velocity.Y, 0);
		// 	// Velocity = new Vector3(Mathf.Lerp(Velocity.X, 0f, (float)delta * 5), Velocity.Y, Velocity.Z);
		// 	Velocity = new Vector3(Mathf.Lerp(Velocity.X, 0f, 1f), Velocity.Y, Mathf.Lerp(Velocity.Z, 0f, 1f));
		// } else if (Actor.StateMachine.CurrentState is not Airborne) {
		// 	// Velocity = new Vector3(MoveDirection.X * ActualSpeed, Velocity.Y, MoveDirection.Z * ActualSpeed);
		// }

		Velocity = new Vector3(MoveDirection.X * ActualSpeed, Velocity.Y, MoveDirection.Z * ActualSpeed);

		Actor.Velocity = Velocity;
		Actor.MoveAndSlide();
	}

	public void ActuallyJump() {
		Velocity = new Vector3(Velocity.X, JumpSpeed, Velocity.Z);
	}

	public void CheckIfAirborne() {
		if (Actor.VerticalStateMachine.CurrentState is not Airborne) return;

		Actor.AnimationTree.Set("parameters/jump_shot/request", (int)AnimationNodeOneShot.OneShotRequest.FadeOut);
		Actor.AnimationTree.Set("parameters/falling_idle/blend_amount", 1);
	}
}
