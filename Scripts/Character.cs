using Godot;
using System;

public partial class Character : CharacterBody2D
{

	TileMapLayer tile_map;
	Camera2D camera;
	

	public override void _Ready()
	{
		tile_map = GetNode<TileMapLayer>("../TileMapLayer");
		camera = GetNode<Camera2D>("../Camera2D");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left) 
		{
			Vector2 global_space = GetGlobalMousePosition();
			Vector2I	 map_coords = tile_map.LocalToMap(global_space);
			GD.Print(map_coords);
		}
	}
}
