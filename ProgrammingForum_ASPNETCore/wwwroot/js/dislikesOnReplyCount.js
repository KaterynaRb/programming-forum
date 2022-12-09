var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/post").build();

connection.on("UpdateDislikesOnReplyInPage", function (totalDislikes, postId, postreplyId, disliked, userId) {

    document.getElementById("dislikesCount-" + postId + "-" + postreplyId).innerText = `${totalDislikes}`;

    if (disliked) {
        var icon = document.getElementById("dislikeIconReply-" + postreplyId + "-" + userId);
        icon.classList.remove("fa-regular");
        icon.classList.add("fa-solid");
    } else {
        var icon = document.getElementById("dislikeIconReply-" + postreplyId + "-" + userId);
        icon.classList.remove("fa-solid");
        icon.classList.add("fa-regular");
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

function SubmitDislikeOnReply(postId, postreplyId, userId) {
    connection.invoke("UpdateDislikesOnReply", postId, postreplyId, userId).catch(function (err) {
        return console.error(err.toString());
    });
}