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
            birds.Add(bird);
            GetNode<Node2D>("/root/Level/Birds").AddChild(bird);
            bird.startX = Position.x;
            bird.Position = new Vector2(bird.startX, bird.startY);
        }
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //GD.Print("test");

    }

    public void Respawn()
    {
        foreach (var bird in birds)
        {
            var birdScore = bird.GetScore();
            var birdBrainWeights = bird.Brain.GeAllWeights();
            if (bestScore < birdScore || bestScore == 0)
            {
                bestScore = birdScore;
                bestModel = birdBrainWeights;
            }
            if (bestModel != null)
            {
                bird.Brain.SetAllWeights(bestModel);
                bird.Brain.Mutate();
            }
            else
            {
                bird.Brain.Mutate();
            }
            bird.Respawn();
        }
    }

    public void CheckAllDead()
    {
        var allDead = birds.Count != 0 ? true : false;
        foreach (var bird in birds)
        {
            if (!bird.IsDead()) allDead = false;
        }
        if(allDead) GetNode<Level>("/root/Level").Restart();
    }
}
