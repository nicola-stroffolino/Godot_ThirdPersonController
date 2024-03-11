using Godot;
using System;

[GlobalClass]
public partial class GameEntity3D : CharacterBody3D {
    [ExportGroup("Node References")]
	[Export]
	public Node3D Model { get; private set; }
	[Export]
	public MovementComponent MovementComponent { get; private set; }
	[Export]
	public StateMachine MovementStateMachine { get; private set; }
	[Export]
	public AnimationTree AnimationTree { get; private set; }
	[Export]
	public AnimationPlayer AnimationPlayer { get; private set; }

    public virtual bool WantsToStandStill() => false;
    public virtual bool WantsToWalk() => false;
	public virtual bool WantsToRun() => false;
	public virtual bool WantsToJump() => false;
	public virtual bool IsFalling() => false;
}