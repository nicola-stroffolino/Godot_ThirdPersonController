using Godot;
using System;

public partial class EntityState : State {
	[Export]
	public GameEntity3D Actor { get; set; }
}
