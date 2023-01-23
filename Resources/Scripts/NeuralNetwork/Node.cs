using System;

class NeuralNetworkNode {
    public float[] inputWeights = null;
    public float activationBias = 0f;
    public float value;
    public NeuralNetworkNode(float[] inputWeights, float activationBias) {
        this.inputWeights = inputWeights;
        this.activationBias = activationBias;
    }

    public NeuralNetworkLayer(NeuralNetworkLayer previousLayer) {
        if(previousLayer == null) return;


    }
    public void Process(NeuralNetworkLayer previousLayer) {
        //add all values from previous layer
        float preValues = 0f;
        for(int i = 0; i < previousLayer.nodes.Length; i++) {
            preValues += previousLayer.nodes[i].value * inputWeights[i];
        }
        //set value of node
        value = (float)LogSigmoid(preValues - activationBias);
    }

    private double LogSigmoid(double x)
	{
		return (1.0 / (1.0 + Math.Exp(-x)));
	}

    public void RandomBias(Random random){
		activiationBias = ((float)random.NextDouble() * inputWeights.Length * 2f) - inputWeights.Length;
	}
}