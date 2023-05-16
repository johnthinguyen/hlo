
//Button Back to top
window.onscroll = () => {
    toggleTopButton();
}
function scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
}

function toggleTopButton() {
    if (document.body.scrollTop > 500 ||
        document.documentElement.scrollTop > 500) {
        document.getElementById('back-to-up').classList.remove('d-none');
    } else {
        document.getElementById('back-to-up').classList.add('d-none');
    }
}
//============================
$(document).ready(function () {
    
    $(".up").click(function () {
        $('html, body').animate({
            scrollTop: 0
        }, 2000);
     })
    
    //Button coppy
    $(".btn_coppy").on("click", function (e) {
        e.preventDefault();
        var key = $(this).data("key");
        var key_name = $(this).data("name");
        var f_name = $(this).data("content")
        var row = ".key_" + key;

        var dataJson = {}

        $.ajax({
            type: 'POST',
            url: '/Home/SearchAll',
            data: {
                searchString: key_name,
                folderID: folder_Id,
                f_name: f_name
            },
            success: function (query) {
                dataJson['key'] = key_name
                var value = "";
                for (var i = 0; i < Langs.length; i++) {
                    valueLang = query[i]["Detail"][0]["Name"];
                    dataJson[Langs[i]] = valueLang;

                }

                document.execCommand(JSON.stringify(dataJson));
                Swal.fire({
                    icon: 'success',
                    title: 'Bạn đã coppy ' + key_name + "\n" + value,
                    showConfirmButton: true,
                    timer: 1000
                })
            }
        })
    })

    //Điền thông tin vào từ Json
    $("#btn_Convert").on('click', function () {
        var textJson = $("#jsonInput").val()
        try {
            var dataJson = JSON.parse(textJson)
            var check_exampleInputKey = document.getElementById("exampleInputKey").readOnly
            if (textJson != null) {
                if (!check_exampleInputKey)
                    document.getElementById("exampleInputKey").value = dataJson['key'];
                for (var i = 0; i < Langs.length; i++) {
                    $("#exampleInput" + Langs[i]).val(dataJson[Langs[i]]);
                }
            }

        }
        catch {

        }
    })

    //clear form add 
    $("#btn_Clear").on('click', function () {
        $("#jsonInput").val("")
        try {
            var check_exampleInputKey = document.getElementById("exampleInputKey").readOnly
            if (!check_exampleInputKey)
                document.getElementById("exampleInputKey").value = "";
            for (var i = 0; i < Langs.length; i++) {
                $("#exampleInput" + Langs[i]).val("");
            }
        }
        catch {

        }
    })
});