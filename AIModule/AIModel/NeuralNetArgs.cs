using Tensorflow.Keras;
using Tensorflow.Keras.ArgsDefinition;

namespace AIModule.AIModel
{
    /// <summary>
    /// Network parameters.
    /// </summary>
    public class NeuralNetArgs : ModelArgs
    {
        /// <summary>
        /// 1st layer number of neurons.
        /// </summary>
        public int NeuronOfHidden1 { get; set; }
        public Activation Activation1 { get; set; }

        /// <summary>
        /// 2nd layer number of neurons.
        /// </summary>
        public int NeuronOfHidden2 { get; set; }
        public Activation Activation2 { get; set; }

        public int NumClasses { get; set; }
        public Activation ActivationOutput { get; set; }
    }
}
