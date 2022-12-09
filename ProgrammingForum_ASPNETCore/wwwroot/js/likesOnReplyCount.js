var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/post").build();

connection.on("UpdateLikesOnReplyInPage", function (totalLikes, postId, postreplyId, liked, userId) {

    document.getElementById("likesCount-" + postId + "-" + postreplyId).innerText = `${totalLikes}`;

    if (liked) {
        var icon = document.getElementById("likeIconReply-" + postreplyId + "-" + userId);
        icon.classList.remove("fa-regular");
        icon.classList.add("fa-solid");
    } else {
        var icon = document.getElementById("likeIconReply-" + postreplyId + "-" + userId);
        icon.classList.remove("fa-solid");
        icon.classList.add("fa-regular");
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

function SubmitLikeOnReply(postId, postreplyId, userId) {
    connection.invoke("UpdateLikesOnReply", postId, postreplyId, userId).catch(function (err) {
        return console.error(err.toString());
    });
}