using Godot;
using System;

public partial class EntityStateMachine : StateMachine {
	public GameEntity3D Actor { get; set; }

	public override void _Ready() {
		Actor = GetParent() as GameEntity3D;
		
		foreach (var child in GetChildren()) {
			if (child is EntityState s) {
				s.Actor = Actor;
				s.StateMachine = this;
			}
		}

		base._Ready();
	}
}
