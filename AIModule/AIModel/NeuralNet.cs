using Tensorflow;
using Tensorflow.Keras.Engine;
using static Tensorflow.Binding;
using static Tensorflow.KerasApi;

namespace AIModule.AIModel
{
    public class NeuralNet : Model
    {
        Layer fc1;
        Layer fc2;
        Layer output;

        public NeuralNet(NeuralNetArgs args) :
            base(args)
        {
            var layers = keras.layers;

            // First fully-connected hidden layer.
            fc1 = layers.Dense(args.NeuronOfHidden1, activation: args.Activation1);

            // Second fully-connected hidden layer.
            fc2 = layers.Dense(args.NeuronOfHidden2, activation: args.Activation2);

            output = layers.Dense(args.NumClasses, activation: args.ActivationOutput); 

            StackLayers(fc1, fc2, output);
        }

        // Set forward pass.
        protected override Tensors Call(Tensors inputs, Tensor state = null, bool? training = null)
        {
            inputs = fc1.Apply(inputs);
            inputs = fc2.Apply(inputs);
            inputs = output.Apply(inputs);
            return inputs;
        }
    }
}
