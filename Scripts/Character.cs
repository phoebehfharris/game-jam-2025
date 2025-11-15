using Godot;
using System;

public partial class Character : CharacterBody2D
{

	TileMapLayer tile_map;
	NavigationAgent2D nav;
	

	public override void _Ready()
	{
		tile_map = GetNode<TileMapLayer>("../TileMapLayer");
		nav = GetNode<NavigationAgent2D>("NavigationAgent2D");
		nav.VelocityComputed += OnNavigationAgent2DVelocityComputed;
	}
	
	public override void _PhysicsProcess(double delta) {
		if (nav.IsNavigationFinished()) {return;}

		GD.Print("Meow");
		Vector2 next_position = nav.GetNextPathPosition();
		
		GD.Print(nav.TargetPosition);
		GD.Print(next_position);
		GD.Print(this.GlobalPosition);
		
		Vector2 velocity = this.GlobalPosition.DirectionTo(next_position) * 250.0f;
		
		nav.Velocity = velocity;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left) 
		{
			Vector2 global_space = GetGlobalMousePosition();
			Vector2 local_space = tile_map.ToLocal(global_space);
			Vector2I	 map_coords = tile_map.LocalToMap(local_space);
			Vector2 map_position = tile_map.MapToLocal(map_coords);
			nav.TargetPosition = map_position; 
		}
	}
	
	public void OnNavigationAgent2DVelocityComputed(Vector2 safe_velocity) {
		GD.Print("Buhhh");
		this.Velocity = safe_velocity;
		MoveAndSlide();
	}
}
