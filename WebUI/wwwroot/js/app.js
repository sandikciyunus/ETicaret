$(".btn-delete").click(function () {
    var id = $(this).data("id");
    var text = $(this).data("text");
    var url = $(this).data("url");
    swal({
        title: "Emin misiniz?",
        text: text,
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "Get",
                    url: url + id,
                    dataType: "json",
                    success: function (data) {
                        if (!data.isDeleted) {
                            swal('Hata!', data.message, 'error');
                            return;
                        } else {
                            window.location.reload();
                        }
                    }
                })
            }
        });
});

