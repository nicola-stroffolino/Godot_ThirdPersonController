using Godot;
using Godot.Collections;
using System;
using System.Linq;

[GlobalClass]
public partial class State : Node {
	// [Signal]
	// public delegate void TransitionedEventHandler();
	[Export]
	public Player Actor { get; set; }
	[Export]
	private Array<State> _states;
	public Tween Tween { get; set; }
	protected private State GetState<State>() => _states.OfType<State>().FirstOrDefault();

	public virtual void Enter() {
		// Tween = Actor.CreateTween();
	}
	public virtual void Exit() {
		// Tween.Stop();
		// Tween.Kill();
	}
	public virtual State StateProcess(float delta) => null;
	public virtual State StatePhysicsProcess(float delta) => null;
}
