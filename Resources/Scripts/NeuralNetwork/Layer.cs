using System;
using System.Linq;

class NeuralNetworkLayer {
    public NeuralNetworkNode[] nodes;
    public NeuralNetworkLayer(int length, NeuralNetworkLayer previousLayer) {
        nodes = new NeuralNetworkNode[length];
        for(int i = 0; i < nodes.Length; i++) {
            nodes[i] = new NeuralNetworkNode(previousLayer);
        }
    }
}