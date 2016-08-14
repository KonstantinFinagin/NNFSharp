namespace Network

open MathNet.Numerics.LinearAlgebra
open Helpers

type Network(sizes : int[]) = 

    member this.num_layers = sizes.Length
    member this.sizes = sizes
    member this.biases = [| for i in 1 .. sizes.Length-1
                                do yield Vector<double>.Build.Random(sizes.[i], 1)|]
    
    member this.weights = [| for i in 0 .. sizes.Length-2 do
                                let x = sizes.[i]
                                let y = sizes.[i+1]
                                yield Matrix<double>.Build.Random(y, x) |]

    member this.feedforward (a : Vector<double>) = 
        let mutable result = a
        for i in 0 .. this.num_layers-2 do
            result <- sigmoid(this.weights.[i] * result + this.biases.[i])
        result    


    // update the network's weights and biases with a mini batch
    member this.update_mini_batch(
        (mini_batch : (Vector<double> * Vector<double>)[]),
        (eta : double)) =
        // TODO 
        1
                

    // Stochastic gradient descent
    member this.SGD(
        (training_data : (Vector<double> * Vector<double>)[]), 
        (epochs : int), 
        (mini_batch_size : int), 
        (eta : double), 
        (?test_data : (Vector<double> * Vector<double>)[])) =

        let hasTestData = match test_data with 
                          | Some(test_data) -> true
                          | None -> false


        let shuffledData = shuffle training_data

        let mini_batches = training_data |> chunks mini_batch_size 

        for i in 0 .. mini_batches.Length-1 do
            this.update_mini_batch(mini_batches.[i], eta) |> ignore


