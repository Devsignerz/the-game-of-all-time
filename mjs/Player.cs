using System;
using Godot;

public partial class Player : Node3D
{
	[Export]
	public Camera3D Camera = null;

	[Export] public float MoveSpeed = 8.0f;
	
	public override void _Ready()
	{
		if (Camera == null)
		{
			FindCam();
		}
	}

	public override void _Process(double delta)
	{
		ProcessMouseMotion(delta);
		ProcessKeyPresses(delta);
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
			Camera = cam;
			return;
		}
		throw new Exception("Failed to find camera for player.");
	}
	
	private void ProcessMouseMotion(double delta)
	{
		var vel = Input.GetLastMouseVelocity();
		vel *= (float)delta * .01f;
		RotateY(vel.X * -1);
		Camera.RotateX(vel.Y * -1);
		var newCamRot = Camera.Rotation;
		newCamRot.X = float.Clamp(newCamRot.X, float.DegreesToRadians(-90.0f), float.DegreesToRadians(90.0f));
		Camera.Rotation = newCamRot;
	}
	
	private void ProcessKeyPresses(double delta)
	{
		var vel = Vector3.Zero;
		if (Input.IsPhysicalKeyPressed(Key.W))
		{
			vel.Z = -1;
		}
		if (Input.IsPhysicalKeyPressed(Key.S))
		{
			vel.Z = 1;
		}
		if (Input.IsPhysicalKeyPressed(Key.A))
		{
			vel.X = -1;
		}
		if (Input.IsPhysicalKeyPressed(Key.D))
		{
			vel.X = 1;
		}

		if (vel.LengthSquared() > 1.0f)
		{
			vel = vel.Normalized();
		}

		vel *= (float)delta * MoveSpeed;
		vel = vel.Rotated(Vector3.Up, Rotation.Y);
		
		Position += vel;
	}
	
}
