using System;
using System.Linq;

class NeuralNetworkLayer {
    public NeuralNetworkNode[] nodes;
    private NeuralNetworkLayer preL = null;
    public NeuralNetworkLayer(int length, NeuralNetworkLayer previousLayer) {
        nodes = new NeuralNetworkNode[length];
        for(int i = 0; i < nodes.Length; i++) {
            nodes[i] = new NeuralNetworkNode(previousLayer);
        }
        preL = previousLayer;
    }

    public void Process() {
        for(int i = 0; i < nodes.Length; i++) {
            nodes[i].Process(preL);
        }
    }
    public float[][] GetAllWeights(){
		float[][] allWeights = new float[nodes.Length][];
		
		for(int i = 0; i < nodes.Length; i++){
			allWeights[i] = nodes[i].GetAllWeights();
		}
		
		return allWeights;
	}
	
	public void SetAllWeights(float[][] weights){
		for(int i = 0; i < nodes.Length; i++){
			nodes[i].SetAllWeights(weights[i]);
		}
	}

    public void Mutate(Random rand) {
        for(int i = 0; i < nodes.Length; i++) {
            nodes[i].Mutate(rand);
        }
    }
}