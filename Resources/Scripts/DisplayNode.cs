using Godot;
using System;

public class DisplayNode : TextureButton
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private bool mouseOver = false;
    public float value = 0.0f;
    public float weight = 0.0f;
    public float bias = 0.0f;
    private static Label valueLabel;
    private static Label weightLabel;
    private static Label biasLabel;

    private static Panel popup;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        valueLabel = GetNode<Label>("Popup/DisplayValue");
        weightLabel = GetNode<Label>("Popup/DisplayWeight");
        biasLabel = GetNode<Label>("Popup/DisplayBias");

        popup = GetNode<Panel>("Popup");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (mouseOver == true)
        {
            valueLabel.Text = "Value: " + value.ToString();
            weightLabel.Text = "Weight: " + weight.ToString();
            biasLabel.Text = "Bias: " + bias.ToString();
        }
    }

    public void Mouse_Over() {
        mouseOver = true;
        this.RectScale = new Vector2(1.1f, 1.1f);
        var xSizeDif = (this.RectSize.x * 1.1) - this.RectSize.x;
        var ySizeDif = (this.RectSize.y * 1.1) - this.RectSize.y;
        this.RectPosition = new Vector2((float)(this.RectPosition.x - (xSizeDif / 2)), (float)(this.RectPosition.y - (ySizeDif / 2)));
        popup.Visible = true;
    }

    public void Mouse_Leave() {
        mouseOver = false;
        this.RectScale = new Vector2(1,1);
        var xSizeDif = (this.RectSize.x * 1.1) - this.RectSize.x;
        var ySizeDif = (this.RectSize.y * 1.1) - this.RectSize.y;
        this.RectPosition = new Vector2((float)(this.RectPosition.x + (xSizeDif / 2)), (float)(this.RectPosition.y + (ySizeDif / 2)));
        popup.Visible = false;
    }

}
