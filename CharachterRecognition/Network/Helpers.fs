namespace Network

open MathNet.Numerics.LinearAlgebra
open System

module Helpers =

    let sigmoid (z : Vector<double>) = z |> Vector.map(fun x -> 1.0 / (1.0 + exp x))

    let swap (a: _ array) i j = 
        let t = a.[i]
        a.[i] <- a.[j]
        a.[j] <- t

    let shuffle a = 
        let rnd = new Random()
        let n = Array.length a
        for i = 0 to n-1 do
            swap a i (rnd.Next(n))

    let chunks chunkSize (arr : _ array) = 
        query {
            for i in 0..(arr.Length - 1) do
            groupBy (i / chunkSize) into g
            select (g |> Seq.map (fun i -> arr.[i]) |> Seq.toArray)
        } |> Seq.toArray
        