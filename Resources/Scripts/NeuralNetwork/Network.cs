using System;

class NeuralNetwork {
    public NeuralNetworkLayer[] layers = null;
    public NeuralNetwork(int[] layerCounts) {
        layers = new NeuralNetworkLayer[layerCounts.Length];
        layers[0] = new NeuralNetworkLayer(layerCounts[0],null);
        for(int i = 1; i < layerCounts.Length; i++) {
            layers[i] = new NeuralNetworkLayer(layerCounts[i], layers[i--]);
        }
    }
    public void SetInput(float[] inputs) {
        if(inputs.Length != layers[0].nodes.Length) return;
        for(int i = 0; i < layers[0].nodes.Length; i++) {
            layers[0].nodes[0].value = inputs[i];
        }
    }
    public float[] getOutput() {
        float[] output = new float[layers[layers.Length--].nodes.Length];
        for(int i = 0; i < layers[layers.Length--].nodes.Length; i++) {
            output[i] = layers[layers.Length--].nodes[i].value;
        }
        return output;
    }
    public void Process() {
        for(int i = 0; i < layers.Length; i++) {
            layers[i].Process();
        }
    }
    public float[][][] GeAllWeights() {
        float[][][] allWeights = new float[layers.Length][][];
        for(int i = 0; i < layers.Length; i++) {
            allWeights[i] = layers[i].GetAllWeights();
        }
        return allWeights;
    }
    public void SetAllWeights(float[][][] weights) {
        for(int i = 0; i < layers.Length; i++) {
            layers[i].SetAllWeights(weights[i]);
        }
    }
    public void Mutate() {
        if(layers == null) return;
        Random rand = new Random();
        for(int i = 0; i < layers.Length; i++) {
            layers[i].Mutate(rand);
        }        
    }
}