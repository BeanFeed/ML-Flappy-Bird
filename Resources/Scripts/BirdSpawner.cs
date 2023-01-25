using Godot;
using System;
using System.Collections.Generic;

public class BirdSpawner : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int bestScore = 0;
    private int genCount = 1;
    [Export]
    private int birdCount = 50;
    public float[][][] bestModel = null;
    private bool spawnNextFrame = false;

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
            bird.startX = 40;
            bird.startY = 151;
            bird.Position = new Vector2(bird.startX, bird.startY);
        }
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //GD.Print("test");
        if(spawnNextFrame) SpawnNew();

    }
    private void SpawnNew()
    {
        for (int i = 0; i < birdCount; i++)
        {
            var bird = GD.Load<PackedScene>("res://Resources/Objects/NNBird.tscn").Instance<NNBird>();
            bird.SetSpawner(this);
            birds.Add(bird);
            GetNode<Node2D>("/root/Level/Birds").AddChild(bird);
            bird.startX = 40;
            bird.startY = 151;
            bird.Position = new Vector2(bird.startX, bird.startY);
        }
        foreach(var bird in birds)
        {
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
        if(bestModel != null) birds[0].Brain.SetAllWeights(bestModel);
        spawnNextFrame = false;
    }
    public void Respawn()
    {
        foreach (var bird in birds)
        {
            var birdScore = bird.GetScore();
            var birdBrainWeights = bird.Brain.GeAllWeights();
            if (bestScore < birdScore)
            {
                bestScore = birdScore;
                bestModel = birdBrainWeights;
            }
            
            bird.QueueFree();

        }
        var genLabel = GetNode<Label>("/root/Level/UI/Generation");
        genCount++;
        genLabel.Text = "Generation Count: " + genCount;
        var scoreLabel = GetNode<Label>("/root/Level/UI/Score");
        scoreLabel.Text = "Best Score: " + bestScore;
        birds.RemoveRange(0, birds.Count);
        spawnNextFrame = true;
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

    public void SaveModel()
    {
        foreach (var bird in birds)
        {
            var birdScore = bird.GetScore();
            var birdBrainWeights = bird.Brain.GeAllWeights();
            if (bestScore < birdScore)
            {
                bestScore = birdScore;
                bestModel = birdBrainWeights;
            }

        }
    }
}
