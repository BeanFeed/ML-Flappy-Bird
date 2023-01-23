using Godot;
using System;

public class Pipe : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var windowHeight = GetViewport().Size.y;
        var rand = new Random();
        Position = new Vector2(Position.x, rand.Next(100,(int)windowHeight - 20));
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Position.x < -40) QueueFree();
    }
}
