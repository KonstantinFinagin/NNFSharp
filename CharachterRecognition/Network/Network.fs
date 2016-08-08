namespace Network

open MathNet.Numerics.LinearAlgebra

type Network(sizes : int[]) = 

    let sigmoid (z : Vector<double>) = z |> Vector.map(fun x -> 1.0 / (1.0 + exp x))

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

    // Stochastic gradient descent
    member this.SGD 
        (training_data : (Vector<double> * Vector<double>)[]) 
        (epochs : int) 
        (mini_batch_size : int) 
        (eta : double) 
        (test_data : (Vector<double> * Vector<double>)[]) =



        1



