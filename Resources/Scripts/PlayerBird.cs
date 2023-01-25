using Godot;
using System;


class PlayerBird : Bird
{
    private bool dead = false;
    public override void _Process(float delta)
    {
        base._Process(delta);
        HandleInputs();
        if(dead) LinearVelocity = Vector2.Zero;
    }

    private protected override void Score(int pipeID)
    {
        base.Score(pipeID);
        var label = GetNode<Label>("/root/Level/UI/Score");
        label.Text = "Best Score: " + score;
    }
    private void HandleInputs()
    {
        var level = GetNode<Level>("/root/Level");
        if(Input.IsActionJustPressed("Jump") && level.simGo) this.Flap();
    }

    private protected override void Kill()
    {
        var level = GetNode<Level>("/root/Level");
        level.simGo = false;
        dead = true;
    }
}