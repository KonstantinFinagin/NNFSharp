namespace CharachterRecognition.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Network
    {
        private List<int> layerSizes;
        private int layerNumber;

        private double biases;
        private double weights;
        
        public Network(List<int> layerSizes)
        {
            this.layerNumber = layerSizes.Count();
            this.layerSizes = layerSizes;

            this.biases = GetBiases(layerSizes);
            this.weights = GetWeights(layerSizes);
        }

        private double GetWeights(IEnumerable<int> sizes)
        {
            foreach (var layerSize in sizes.Skip(1))
            {

            }
        }

        private double GetBiases(IEnumerable<int> sizes)
        {
            var temp = sizes.Take()
            



        }
    }
}
