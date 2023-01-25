using Godot;
using System;
using System.Collections.Generic;

public class BirdSpawner : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int bestScore = 0;
    [Export]
    private int birdCount = 1;
    public float[][][] bestModel = null;

    private List<NNBird> birds = new List<NNBird>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        for (int i = 0; i < birdCount; i++)
        {
            var bird = GD.Load<PackedScene>("res://Resources/Objects/NNBird.tscn").Instance<NNBird>();
            bird.SetSpawner(this);
            bird.startPos = this.Position.x;
            birds.Add(bird);
            GetNode<Node2D>("/root/Level/Birds").AddChild(bird);
        }
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //GD.Print("test");
        var allDead = true;
        if (birds.Count == 0) allDead = false;
        foreach (var bird in birds)
        {
            if (!bird.dead) allDead = false;
            break;
        }

        if (allDead)
        { 
            GetNode<Level>("/root/Level").Restart();
            GetNode<Level>("/root/Level").simGo = false;
        }
    }

    public void Respawn()
    {
        foreach (var bird in birds)
        {
            bird.ReturnStats();
            bird.dead = false;
            bird.Position = this.Position;
            if (bestModel != null)
            {
                bird.Brain.SetAllWeights(bestModel);
                bird.Brain.Mutate();
            }
            else
            {
                bird.Brain.Mutate();
            }
        }
    }

    public void CheckAllDead()
    {
        
    }
}
