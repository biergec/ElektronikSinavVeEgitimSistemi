function YeniDersKayit() {
    var dersId = $("#dersListesi").val();
    var dersKayitAnahtari = $("#inputDersKayitAnahtari").val();

    if (dersId.length <= 0 || dersKayitAnahtari.length <= 0) {
        showNotification("top", "right", "warning", "Lütfen Kayıt Anahtarını giriniz!");
        return;
    }

    $.ajax({
        url: '/Home/OgrenciYeniDersKaydi',
        type: 'POST',
        dataType: 'json',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            dersId: dersId,
            dersKayitAnahtari: dersKayitAnahtari
        },
        success: function (result) {
            if (result.isSuccess) {

                showNotification("top", "right", "success", result.message + " Lütfen bekleyiniz.");

                setTimeout(function (parameters) {
                    // İşlem başarılı ise sınavlarım menüsüne yönlendirsin!
                    window.location.href = "/";
                }, 3000);

            } else {
                showNotification("top", "right", "warning", result.message);
            }
        },
        error: function () {
            showNotification("top", "right", "warning", "İşlem gerçekleştirilemedi!");
        }
    });

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