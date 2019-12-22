$(document).ready(function () {
    favorite();
    showAll();

    
    var en = '<li class="nav-item"><a class="nav-link text-dark" href="/Posts/Posts?culture=en">EN</a></li>';
    var tr = '<li class="nav-item"><a class="nav-link text-dark" href="/Posts/Posts?culture=tr">TR</a></li>';
    $(".mx-auto").append($(en));
    $(".mx-auto").append($(tr));
});

function favorite() {

    $(".favorite").on('click', function (e) {
        var fav = 0;
        var postId;
        postId = $(e.target).parent().parent().attr("id");

        if (e.target.style.color == "") {
            fav = 1;
            e.target.style.color = "red";
        }
        else{
            e.target.style.color = "";
            fav = 0;
        }
        $.ajax({
            url: '/Posts/Posts?handler=AddFavorite',
            type: 'GET',
            data: { "favorite": fav, "postId": postId },
            success: function (response) {
                if (response == true) {
                    console.log("İşlem yapıldı");
                } else {
                    alert('HATA VAR !!!');
                }
            }
        });
        
    });
}
function showAll() {
    //
    $(".showAll").on("click", function (e) {
        var postId = $(e.target).parent().parent().attr("id");
        $.ajax({
            type: "GET",
            url: "/Posts/Posts?handler=ShowAll",
            data: { "PostId": postId },
            success: function (response) {
                $(".ModalDiv").html(response);
                $("#showAllModal").modal('show');
            },
            error: function (error) {
                console.log("Error : ", error);
            }
        });
    });
}

function Comment(e,id) {
    var comment = $(e).siblings()[0].value;
    $("#showAllModal").modal('hide');

    $.ajax({
        url: '/Posts/Posts?handler=Comment',
        type: 'GET',
        data: { "comment": comment, "postId": id },
        success: function (response) {
            $(".ModalDiv").html(response);
            $("#showAllModal").modal('show');
        },
        error: function (error) {

        }
    });



}