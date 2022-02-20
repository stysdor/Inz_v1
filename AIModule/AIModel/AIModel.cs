using System;
using System.Linq;
using Tensorflow;
using Tensorflow.NumPy;
using static Tensorflow.Binding;
using static Tensorflow.KerasApi;

namespace AIModule.AIModel
{
    public class AIModel
    {

        //dataset parameters.
        int num_classes = 1; // 1 output
        int num_features = 15; // parameters

        // Training parameters.
        float learning_rate = 0.1f;
        int display_step = 100;
        int batch_size = 256;
        int training_steps = 1000;

        float accuracy;
        IDatasetV2 train_data;
        NDArray x_test, y_test, x_train, y_train;

        public void PrepareData()
        {
            // Prepare data.
            ((x_train, y_train), (x_test, y_test)) = keras.datasets.mnist.load_data();

            // Use tf.data API to shuffle and batch data.
            train_data = tf.data.Dataset.from_tensor_slices(x_train, y_train);
            train_data = train_data.repeat()
                .shuffle(5000)
                .batch(batch_size)
                .prefetch(1)
                .take(training_steps);
        }

        public bool Run()
        {
            tf.enable_eager_execution();

            PrepareData();

            // Build neural network model.
            var neural_net = new NeuralNet(new NeuralNetArgs
            {
                NumClasses = num_classes,
                NeuronOfHidden1 = 128,
                Activation1 = keras.activations.Relu,
                NeuronOfHidden2 = 256,
                Activation2 = keras.activations.Relu
            });

            // Cross-Entropy Loss.
            // Note that this will apply 'softmax' to the logits.
            Func<Tensor, Tensor, Tensor> cross_entropy_loss = (x, y) =>
            {
                // Convert labels to int 64 for tf cross-entropy function.
                y = tf.cast(y, tf.int64);
                // Apply softmax to logits and compute cross-entropy.
                var loss = tf.nn.sparse_softmax_cross_entropy_with_logits(labels: y, logits: x);
                // Average loss across the batch.
                return tf.reduce_mean(loss);
            };

            // Accuracy metric.
            Func<Tensor, Tensor, Tensor> accuracy = (y_pred, y_true) =>
            {
                // Predicted class is the index of highest score in prediction vector (i.e. argmax).
                var correct_prediction = tf.equal(tf.math.argmax(y_pred, 1), tf.cast(y_true, tf.int64));
                return tf.reduce_mean(tf.cast(correct_prediction, tf.float32), axis: -1);
            };

            // Stochastic gradient descent optimizer.
            var optimizer = keras.optimizers.SGD(learning_rate);

            // Optimization process.
            Action<Tensor, Tensor> run_optimization = (x, y) =>
            {
                // Wrap computation inside a GradientTape for automatic differentiation.
                using var g = tf.GradientTape();
                // Forward pass.
                var pred = neural_net.Apply(x, training: true);
                var loss = cross_entropy_loss(pred, y);

                // Compute gradients.
                var gradients = g.gradient(loss, neural_net.trainable_variables);

                // Update W and b following gradients.
                optimizer.apply_gradients(zip(gradients, neural_net.trainable_variables.Select(x => x as ResourceVariable)));
            };


            // Run training for the given number of steps.
            foreach (var (step, (batch_x, batch_y)) in enumerate(train_data, 1))
            {
                // Run the optimization to update W and b values.
                run_optimization(batch_x, batch_y);

                if (step % display_step == 0)
                {
                    var pred = neural_net.Apply(batch_x, training: true);
                    var loss = cross_entropy_loss(pred, batch_y);
                    var acc = accuracy(pred, batch_y);
                    print($"step: {step}, loss: {(float)loss}, accuracy: {(float)acc}");
                }
            }

            // Test model on validation set.
            {
                var pred = neural_net.Apply(x_test, training: false);
                this.accuracy = (float)accuracy(pred, y_test);
                print($"Test Accuracy: {this.accuracy}");
            }

            return this.accuracy > 0.92f;
        }
    }
}