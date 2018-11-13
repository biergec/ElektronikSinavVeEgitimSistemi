function SinaviSil(sinavId) {
    var alertOnay = confirm("İlgili sınavı silmek istediğinize emin misiniz?");
    if (!alertOnay) {
        return;
    }

    $.ajax({
        url: '/Sinav/SinavSil',
        type: 'POST',
        dataType: 'json',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            sinavId: sinavId
        },
        success: function (result) {
            if (result.isSuccess) {

                document.getElementById("sinavListesi").disable = true;
                showNotification("top", "right", "success", result.message + " Liste birkaç saniye içerisinde yenilenecektir.");

                setTimeout(function (parameters) {
                    // İşlem başarılı ise sınavlarım menüsüne yönlendirsin!
                    window.location.href = "/Sinav/Sinavlarim";
                }, 3000);

            } else {
                showNotification("top", "right", "warning", result.message);
            }
        },
        error: function () {
            showNotification("top", "right", "warning", "İşlem gerçekleştirilemedi!");
        }
    });

    return;
}

function showNotification(from = "top", align = "right", type = "success", message) {
    $.notify({
        icon: "add_alert",
        message: message
    }, {
            type: type,
            timer: 4000,
            placement: {
                from: from,
                align: align
            }
        });
}