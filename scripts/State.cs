using Godot;
using Godot.Collections;
using System;
using System.Linq;

[GlobalClass]
public partial class State : Node {
	// [Signal]
	// public delegate void TransitionedEventHandler();
	[Export]
	public GameEntity3D Actor { get; set; }
	[Export]
	private Array<State> _states;
	public Tween Tween { get; set; } = null;
	protected private State GetState<State>() => _states.OfType<State>().FirstOrDefault();

	public virtual void Enter() {}
	public virtual void Exit() {}
	public virtual State StateProcess(float delta) => null;
	public virtual State StatePhysicsProcess(float delta) => null;
}
