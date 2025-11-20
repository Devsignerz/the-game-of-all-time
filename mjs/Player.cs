using System;
using Godot;

public partial class Player : Node3D
{
	private Camera3D cam_;
	
	public override void _Ready()
	{
		FindCam();
	}

	public override void _Process(double delta)
	{
		var vel = Input.GetLastMouseVelocity();
		vel *= (float)delta * .01f;
		RotateY(vel.X * -1);
		cam_.RotateX(vel.Y * -1);
		var newCamRot = cam_.Rotation;
		newCamRot.X = float.Clamp(newCamRot.X, float.DegreesToRadians(-90.0f), float.DegreesToRadians(90.0f));
		cam_.Rotation = newCamRot;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
	}

	private void FindCam()
	{
		foreach (var node in GetTree().GetNodesInGroup("cam"))
		{
			if (node is not Camera3D cam) continue;
			cam_ = cam;
			return;
		}
		throw new Exception("Failed to find camera for player.");
	}
}
