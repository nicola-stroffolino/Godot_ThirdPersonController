using Godot;
using System;

[GlobalClass]
public partial class State : Node {
	[Signal]
	public delegate void TransitionedEventHandler();

	[Export]
	public Player Actor { get; set; }

	public virtual void Enter() {}

	public virtual void Exit() {}

	public virtual void StateProcess(float delta) {}

	public virtual void StatePhysicsProcess(float delta) {}
}
