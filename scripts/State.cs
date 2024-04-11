using Godot;
using Godot.Collections;
using System;
using System.Linq;

[GlobalClass]
public partial class State : Node {
	// [Signal]
	// public delegate void TransitionedEventHandler();
	[Export]
	private Array<State> _states;
	public StateMachine StateMachine { get; set; }

	protected private State GetState<State>() => _states.OfType<State>().FirstOrDefault();

	public virtual void Enter() {}
	public virtual void Exit() {}
	public virtual State StateProcess(float delta) => null;
	public virtual State StatePhysicsProcess(float delta) => null;
	public virtual State StateInput(InputEvent @event) => null;
	public virtual State StateUnhandledInput(InputEvent @event) => null;
	public virtual State StateUnhandledKeyInput(InputEvent @event) => null;
}
