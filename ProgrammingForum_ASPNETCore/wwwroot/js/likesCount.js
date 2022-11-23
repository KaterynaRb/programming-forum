var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/post").build();

connection.on("UpdateLikesInPage", function (totalLikes, postId, liked, userId) {
    document.getElementById("likesCount-" + postId).innerText = `${totalLikes}`;

    if (liked) {

        var icon = document.getElementById("likeIcon-" + userId);
        icon.classList.remove("fa-regular");
        icon.classList.add("fa-solid");
    } else {

        var icon = document.getElementById("likeIcon-" + userId);
        icon.classList.remove("fa-solid");
        icon.classList.add("fa-regular");
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

function SubmitLike(postId, userId) {
    connection.invoke("UpdateLikes", postId , userId).catch(function (err) {
        return console.error(err.toString());
    });
}