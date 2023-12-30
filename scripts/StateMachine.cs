using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class StateMachine : Node {
	[Export]
	public State InitialState { get; private set; }

	public State PreviousState { get; private set; }	
	public State CurrentState { get; private set; }
	public Godot.Collections.Dictionary<string, State> States { get; private set; } = new();


	public override void _Ready() {
		foreach (var child in GetChildren()) {
			if (child is State s) {
				States[s.Name.ToString().ToLower()] = s;
				s.Connect(State.SignalName.Transitioned, new Callable(this, MethodName.OnChildTransition));
			}
		}

		if (InitialState != null) {
			InitialState.Enter();
			CurrentState = InitialState;
		}

		PreviousState = CurrentState;
	}

	public override void _Process(double delta) {
		CurrentState?.StateProcess((float)delta);
	}

	public override void _PhysicsProcess(double delta) 	{
		CurrentState?.StatePhysicsProcess((float)delta);
	}

	public void OnChildTransition(State fromState, string newStateName) {
		if (fromState != CurrentState) return;

		State newState = States.GetValueOrDefault(newStateName.ToLower());
		if (newState == null) return;

		CurrentState?.Exit();
		PreviousState = CurrentState;
		
		newState.Enter();
		CurrentState = newState;
	}
}
