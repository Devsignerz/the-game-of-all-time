using Godot;
using System;

public partial class MjsTestScene : Node
{
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventKey key && key.IsReleased() && key.Keycode == Key.Escape)
		{
			GetTree().Quit();
		}
	}
}
