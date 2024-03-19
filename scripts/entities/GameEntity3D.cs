using Godot;
using System;

[GlobalClass]
public partial class GameEntity3D : CharacterBody3D {
	[ExportGroup("Node References")]
	[Export]
	public MovementController Movement { get; private set; }
	[Export]
	public StateMachine MovementStateMachine { get; private set; }
	[Export]
	public AnimationTree AnimationTree { get; private set; }
	[Export]
	public AnimationPlayer AnimationPlayer { get; private set; }
	[ExportGroup("Game References")]
	[Export]
	public GameEntity3D LockedTarget { get; set; }

	public bool IsLockedOn { get; set; } = false;
	public float FacingAngle { get; set; }

	public virtual bool WantsToStandStill() => false;
	public virtual bool WantsToWalk() => false;
	public virtual bool WantsToRun() => false;
	public virtual bool WantsToJump() => false;
	public virtual bool IsFalling() => false;
}
