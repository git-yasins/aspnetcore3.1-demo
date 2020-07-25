let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder().withUrl("/counthub"
        //, signalR.HttpTransPortType.LongPolling
    ).build();

    connection.on("ReceiveUpdate", (update) => {
        const resultDiv = document.getElementById("result");
        resultDiv.innerHTML = update;
    })

    connection.on("someFunc", function(obj) {
        const resultDiv = document.getElementById("result");
        resultDiv.innerHTML = "Someon called,parameters:" + obj.random;
    })

    connection.on("Finished", function() {
        connection.stop();
        const resultDiv = document.getElementById("result")
        resultDiv.innerHTML = "Finished";
    })

    connection.start().catch(err => console.err(err.toString()));
}

setupConnection();

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    fetch("/api/count", {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => connection.invoke("GetLatestCount", id))
})