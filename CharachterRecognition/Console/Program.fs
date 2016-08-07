open Network

[<EntryPoint>]
let main argv =

    let network = new Network([| 5; 8; 2 |])


    printfn "%A" argv
    0 // return an integer exit code
