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
	public RayCast3D LandingRay { get; set; }


	// Functional Variables
	public int ActualSpeed { get; set; }
	public float Gravity { get; set; }
	public float JumpSpeed { get; set; }
	public Vector3 Direction { get; set; } = Vector3.Zero;
	private Vector3 MoveDirection = Vector3.Zero;
	private Vector3 Velocity = Vector3.Zero;
	private float LookingRotation;

	public override void _Ready() {
		Gravity =	2 * Actor.JumpHeight / (Actor.TimeToJumpPeak * Actor.TimeToJumpPeak); //m/s^2;
		JumpSpeed = Gravity * Actor.TimeToJumpPeak; //m/s

		LookingRotation = Actor.CameraComponent.HCamRotation;
		Actor.AnimationPlayer.Play("player_animations_root/front_walking");
		Actor.AnimationPlayer.SpeedScale = 0.5f;
	}

	public override void _Process(double delta) {
		GetNode<Label>("../Control/Label").Text = $@"Input Direction: {Direction}
			Move Direction: {MoveDirection}
			Velocity: {Velocity}

			Target Look: {LookingRotation}
		";
	}

	public override void _PhysicsProcess(double delta) {
		// Velocity.X = Mathf.Lerp(Velocity.X, MoveDirection.X * ActualSpeed, (float)delta * 5);
		// Velocity.Z = Mathf.Lerp(Velocity.Z, MoveDirection.Z * ActualSpeed, (float)delta * 5);

		Velocity = DivideVector3ByVelocity(Actor.AnimationPlayer.GetRootMotionPosition(), (float)(delta * 0.5)).Rotated(Vector3.Up, Actor.Model.Rotation.Y);
		Actor.Velocity = Velocity;
		// Actor.Velocity = v;
		Actor.MoveAndSlide();
	}

	public void SetDirection(Vector3 direction) {
		Direction = direction;
		MoveDirection = Direction.Rotated(Vector3.Up, Actor.CameraComponent.GetHRot()).Normalized();

		// Rotating the Model is not a duty of the movement component
		if (Direction != Vector3.Zero) LookingRotation = Mathf.Atan2(MoveDirection.X, MoveDirection.Z);
		Actor.Model.Rotation = new() {
			Y = (float)Mathf.DegToRad(Mathf.Wrap(Mathf.RadToDeg(Mathf.LerpAngle(Actor.Model.Rotation.Y, LookingRotation, 0.2)), -180, 180.0))
		};
	}

	public void SetVelocity(int axys, float value) {
		if (axys < 120 || axys > 122) return;

		Velocity[axys - 120] = value;
	}

	public Vector3 GetVelocity() => Velocity;

	public Vector3 DivideVector3ByVelocity(Vector3 v, float d) => new(v.X / d, Velocity.Y, v.Z / d);
}
