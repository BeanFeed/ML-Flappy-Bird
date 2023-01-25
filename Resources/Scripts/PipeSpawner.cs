using Godot;
using System;

public class PipeSpawner : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool canSpawn = true;
    [Export]
    private int pipeSpeed = 10;
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
    private void Spawn()
    {
        if (!GetNode<Level>("/root/Level").simGo) return;
        Pipe pipe = GD.Load<PackedScene>("res://Resources/Objects/Pipe.tscn").Instance<Pipe>();
        pipe.Position = new Vector2(this.Position.x,pipe.Position.y);
        GetNode<Node>("/root/Level/Pipes").AddChild(pipe);
        
        pipe.SetSpeed(pipeSpeed);
    }
}
