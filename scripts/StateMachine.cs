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
	// public Godot.Collections.Dictionary<string, State> States { get; private set; } = new();

	[Signal]
	public delegate void StateTransitionedEventHandler(State fromState, State toState);

	public override void _Ready() {
		// foreach (var child in GetChildren()) {
		// 	if (child is State s) {
		// 		States[s.Name.ToString().ToLower()] = s;
		// 		s.Connect(State.SignalName.Transitioned, new Callable(this, MethodName.OnChildTransition));
		// 	}
		// }

		// if (InitialState != null) {
		// 	InitialState.Enter();
		// 	CurrentState = InitialState;
		// }

		// PreviousState = CurrentState;
		ChangeState(InitialState);
	}
	
	public override void _Process(double delta) {
		var newState = CurrentState.StateProcess((float)delta);
		if (newState is not null) ChangeState(newState);
		// CurrentState?.StateProcess((float)delta);
	}

	public override void _PhysicsProcess(double delta) {
		var newState = CurrentState.StatePhysicsProcess((float)delta);
		if (newState is not null) ChangeState(newState);
		// CurrentState?.StatePhysicsProcess((float)delta);
	}

	public override void _Input(InputEvent @event) {
		var newState = CurrentState.StateInput(@event);
		if (newState is not null) ChangeState(newState);
	}

	public override void _UnhandledInput(InputEvent @event) {
		var newState = CurrentState.StateUnhandledInput(@event);
		if (newState is not null) ChangeState(newState);
	}

	public override void _UnhandledKeyInput(InputEvent @event) {
		var newState = CurrentState.StateUnhandledKeyInput(@event);
		if (newState is not null) ChangeState(newState);
	}

	public void ChangeState(State newState) {
		CurrentState?.Exit();
		PreviousState = CurrentState;

		CurrentState = newState;
		CurrentState.Enter();

		EmitSignal(SignalName.StateTransitioned, PreviousState, CurrentState);
	}

	// public void OnChildTransition(State fromState, string newStateName) {
	// 	if (fromState != CurrentState) return;

	// 	State newState = States.GetValueOrDefault(newStateName.ToLower());
	// 	if (newState == null) return;

	// 	CurrentState?.Exit();
	// 	PreviousState = CurrentState;
		
	// 	newState.Enter();
	// 	CurrentState = newState;
	// }
}
