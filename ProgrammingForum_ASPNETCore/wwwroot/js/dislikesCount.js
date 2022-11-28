var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/post").build();

connection.on("UpdateDislikesInPage", function (totalDislikes, postId, disliked, userId) {
    document.getElementById("dislikesCount-" + postId).innerText = `${totalDislikes}`;

    if (disliked) {
        var icon = document.getElementById("dislikeIcon-" + userId);
        icon.classList.remove("fa-regular");
        icon.classList.add("fa-solid");
    } else {
        var icon = document.getElementById("dislikeIcon-" + userId);
        icon.classList.remove("fa-solid");
        icon.classList.add("fa-regular");
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

function SubmitDislike(postId, userId) {
    connection.invoke("UpdateDislikes", postId, userId).catch(function (err) {
        return console.error(err.toString());
    });
}