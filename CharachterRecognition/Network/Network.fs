namespace Network

open MathNet.Numerics.LinearAlgebra

type Network(sizes : int[]) = 

    let sigmoid z = 1.0 / (1.0 + exp z)



    member this.num_layers = sizes.Length
    member this.sizes = sizes
    member this.biases = [| for i in 1 .. sizes.Length-1
                                do yield Matrix<double>.Build.Random(sizes.[i], 1)|]
    
    member this.weights = [| for i in 0 .. sizes.Length-2 do
                                let x = sizes.[i]
                                let y = sizes.[i+1]
                                yield Matrix<double>.Build.Random(y, x) |]










