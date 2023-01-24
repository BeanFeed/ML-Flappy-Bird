using Godot;
using System;

public class PipeSpawner : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool canSpawn = true;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var timer = GetNode<Timer>("SpawnTimer");
        timer.Connect("timeout", this, "Spawn");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//
//  }
    private void Spawn() {
        var pipe = GD.Load<PackedScene>("res://Resources/Objects/Pipe.tscn");
    }
}
