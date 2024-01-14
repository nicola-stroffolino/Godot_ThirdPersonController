using Godot;
using System;

public partial class Skeleton : Skeleton3D {
	[Export]
	public Player Actor { get; set; }
	[Export]
	public CollisionShape3D CollisionBox { get; set; }
	[Export]
	public MeshInstance3D Mesh { get; set; }
	[Export]
	public MeshInstance3D Mesh2 { get; set; }

	public override void _Process(double delta) {
		var lf = GetBoneGlobalPose(59); // 59 - left foot
		var rf = GetBoneGlobalPose(64); // 64 - right foot
		var root = GetBoneGlobalPose(0);
		
		// Mesh.Position = GetBoneGlobalPose(59).Origin.Rotated(Vector3.Up, Actor.Model.Rotation.Y);
		// Mesh2.Position = GetBoneGlobalPose(64).Origin.Rotated(Vector3.Up, Actor.Model.Rotation.Y);

		Mesh.Position = GetBoneGlobalPose(59).Origin.Rotated(Vector3.Up, Actor.Model.Rotation.Y);
		Mesh.Position = new() {
			X = root.Origin.X,
			Y = Math.Min(lf.Origin.Y, rf.Origin.Y),
			Z = root.Origin.Z
		};

		CollisionBox.Position = new() {
			X = CollisionBox.Position.X,
			Y = 10,
			Z = CollisionBox.Position.Z
		};
	}
}
