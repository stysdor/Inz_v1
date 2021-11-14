using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeutralNetwork
{
    public class Manager
    {
        public int populationSize;//creates population size
        public int[] layers = new int[3] { 5, 3, 2 };//initializing network to the right size
        public List<NeuralNetwork> networks;

         public float MutationChance = 0.01f;

        public float MutationStrength = 0.5f;

        public void InitNetworks()
        {
            networks = new List<NeuralNetwork>();
            for (int i = 0; i < populationSize; i++)
            {
                NeuralNetwork net = new NeuralNetwork(layers);
                net.Load("ModelSave.txt");//on start load the network save
                networks.Add(net);
            }
        }

        public void SortNetworks()
        {

            networks.Sort();
            networks[populationSize - 1].Save("ModelSave.txt");//saves networks weights and biases to file, to preserve network performance
            for (int i = 0; i < populationSize / 2; i++)
            {
                networks[i] = networks[i + populationSize / 2].Copy(new NeuralNetwork(layers));
                networks[i].Mutate((int)(1 / MutationChance), MutationStrength);
            }
        }
    }
}
