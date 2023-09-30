document.getElementById("load-friends-button").addEventListener("click",
    async function () {

        var response = await fetch("workers-list", { method: "GET" })
        var responseBody = await response.text();
        document.getElementById("list").innerHTML = responseBody;
    })