function SinavKayit() {

    var sinavKayitOnay = confirm("Sinavı kayıt etmek istediğinize emin misiniz? Sinava tekrar geri dönemessiniz.");
    if (!sinavKayitOnay) {
        return;
    }


    var sinavId = $("#klasikSinavId").val();
    var soruSayisi = $("#soruSayisi").val();
    var sinavSoruCevap = [];

    for (var i = 0; i < soruSayisi; i++) {
        var soruMetinId = "soruMetni" + (i + 1);
        sinavSoruCevap[i] = {
            SoruText: document.getElementById(soruMetinId).innerText,
            SoruCevapText: $("#inputSoruCevap" + (i + 1)).val()
        };
    }

    var klasikSinavSinavKayit = {
        SinavSoruCevaplari: sinavSoruCevap,
        SinavId : sinavId
    };

    $.ajax({
        url: '/Home/KlasikSinavSinavKayit',
        type: 'POST',
        dataType: 'json',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            klasikSinavSinavKayit: klasikSinavSinavKayit
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