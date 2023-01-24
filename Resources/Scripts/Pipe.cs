using Godot;
using System;

public class Pipe : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public bool hasPast = false;
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
        var rand = new Random();
        //sets position to 5 pixels right of viewport and gives new random Y value
        if(Position.x < -40) this.Position = new Vector2(GetViewport().Size.x + 5,  rand.Next(100,(int)GetViewport().Size.y - 20));
    }
}
