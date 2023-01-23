using Godot;
using System;


class PlayerBird : Bird {

    public override void _Process(float delta)
    {
        HandleInputs();
    }

    private void HandleInputs() {
        if(Input.IsActionJustPressed("Jump")) this.Flap();
    }
}