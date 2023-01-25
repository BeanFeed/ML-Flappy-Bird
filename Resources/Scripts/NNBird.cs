using Godot;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

//using System.Linq;

public class NNBird : Bird
{
    public NeuralNetwork Brain = new NeuralNetwork(new int[] { 1, 5, 1 });
    private BirdSpawner spawner = null;
    public float startY = 0;
    public override void _Ready()
    {
        base._Ready();
        var nextPipeDetector = GetNode<NextPipeDetector>("NextPipeDetector");
        nextPipeDetector.Position = new Vector2(Position.x, -7);
        startY = this.Position.y;
    }
    public override void _Process(float delta)
    {
        base._Process(delta);
        GetChild<RayCast2D>(3).Position = new Vector2(startX, -7);
        if (!isDead)
        {
            var nextPipe = GetNode<Level>("/root/Level").GetNextPipe();
            if (nextPipe != null)
            {
                float dist = Position.y - nextPipe.Position.y;
                GD.Print(nextPipe.id);
                Brain.SetInput(new float[]{dist});
                Brain.Process();
                var outS = Brain.getOutput();
                //GD.Print(outS[0]);
                if(outS[0] > 0.5) { Flap(); }
            }
            if(GetNode<RayCast2D>("GroundDetector").GetCollider()!= null && ((Node)GetNode<RayCast2D>("GroundDetector").GetCollider()).Name == "BottomBorder") Flap();
        }

        
    }

    public void SetSpawner(BirdSpawner spawner)
    {
        this.spawner = spawner;
    }

    public override void Kill()
    {
        base.Kill();
        spawner.CheckAllDead();
        this.Position = new Vector2(-20, -20);
    }
    /*
    public void Respawn()
    {
        //this.Mode = ModeEnum.Rigid;
        this.isDead = false;
        this.LinearVelocity = Vector2.Zero;
        this.Position = new Vector2(startX, startY);
        GD.Print("Restart: " + Position);
        score = 0;
        pastPipeIDs = new List<int>();
    }
    */
}
