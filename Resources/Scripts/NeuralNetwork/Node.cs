using System;

class NeuralNetworkNode {
    public float[] inputWeights = null;
    public float activationBias = 0f;
    public float value = 0f;
    public NeuralNetworkNode(float[] inputWeights, float activationBias) {
        this.inputWeights = inputWeights;
        this.activationBias = activationBias;
    }

    public NeuralNetworkNode(NeuralNetworkLayer previousLayer){
		if(previousLayer == null){
			return;
		}
		
		inputWeights = new float[previousLayer.nodes.Length];
		
		Random random = new Random();
		
		for(int i = 0; i < inputWeights.Length; i++){
			inputWeights[i] = ((float)random.NextDouble() * 2f) - 1f;
		}
		
		RandomBias(random);
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
    public float[] GetAllWeights() {
        if(inputWeights == null) {
            return new float[]{activationBias};
        }
        float[] allWeights = new float[inputWeights.Length + 1];
        for(int i = 0; i < inputWeights.Length; i++) {
            allWeights[i] = inputWeights[i];
        }
        allWeights[inputWeights.Length] = activationBias;
        return allWeights;
    }
    public void SetAllWeights(float[] weights) {
        if(inputWeights == null) {
            activationBias = weights[0];
        }
        for(int i = 0; i < weights.Length - 1; i++) {
            inputWeights[i] = weights[i];   
        }
        activationBias = weights[weights.Length - 1];
    }
    private double LogSigmoid(double x)
	{
		return (1.0 / (1.0 + Math.Exp(-x)));
	}

    public void RandomBias(Random random){
		activationBias = ((float)random.NextDouble() * inputWeights.Length * 2f) - inputWeights.Length;
	}

    public void Mutate(Random rand) {
        if(inputWeights == null) return;
        for(int i = 0; i < inputWeights.Length; i++){
			if(rand.NextDouble() < 0.01f){
				inputWeights[i] = ((float)rand.NextDouble() * 2f) - 1f;
			}
		}
				
		if(rand.NextDouble() < 0.01f){
			RandomBias(rand);
		}
    }
}