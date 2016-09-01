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

    member this.backprop(x : Vector<double>, y: Vector<double>) =
        
        let mutable nabla_b = [| for i in 0 .. this.biases.Length-1 do
                                    let count = this.biases.[i].Count
                                    yield Vector<double>.Build.Dense(count)|]

        let mutable nabla_w = [| for i in 0 .. this.weights.Length-1 do
                                    let columns = this.weights.[i].ColumnCount
                                    let rows = this.weights.[i].RowCount
                                    yield Matrix<double>.Build.Dense(rows,columns) |]
        (nabla_b, nabla_w)

    // update the network's weights and biases with a mini batch
    member this.update_mini_batch((mini_batch : (Vector<double> * Vector<double>)[]),
                                  (eta : double)) =

        let mutable nabla_b = [| for i in 0 .. this.biases.Length-1 do
                                    let count = this.biases.[i].Count
                                    yield Vector<double>.Build.Dense(count)|]

        let mutable nabla_w = [| for i in 0 .. this.weights.Length-1 do
                                    let columns = this.weights.[i].ColumnCount
                                    let rows = this.weights.[i].RowCount
                                    yield Matrix<double>.Build.Dense(rows,columns) |]
        
        for x,y in mini_batch do
            let mutable (delta_nabla_b, delta_nabla_w) = 
                this.backprop(x,y)
               
            for i in 0 .. nabla_b.Length-1 do
                nabla_b.[i] <- nabla_b.[i] + delta_nabla_b.[i]

            for i in 0 .. nabla_b.Length-1 do
                nabla_w.[i] <- nabla_w.[i] + delta_nabla_w.[i]

        for i in 0 .. this.weights.Length-1 do
            let w = this.weights.[i]
            let nw = nabla_w.[i]

            this.weights.[i] <- w - (eta/double mini_batch.Length) * nw

        for i in 0 .. this.biases.Length-1 do
            let b = this.biases.[i]
            let nb = nabla_b.[i]

            this.biases.[i] <- b - (eta/double mini_batch.Length) * nb

    // Stochastic gradient descent
    member this.SGD((training_data : (Vector<double> * Vector<double>)[]), 
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


