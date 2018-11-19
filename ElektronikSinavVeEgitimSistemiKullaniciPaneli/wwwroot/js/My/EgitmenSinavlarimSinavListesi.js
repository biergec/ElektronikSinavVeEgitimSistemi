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


function SinavSuresiGuncelleModalAcmadanOnce(sinavSuresi, sinavId) {
    $("#hiddenSinavIdSinavSuresiDegistir").val(sinavId);
    document.getElementById("sinavSuresiGosterimlani").innerText = sinavSuresi;
}

function SinavSuresiGuncelle() {
    var sinavSuresi = $("#inputModalSinavSuresi").val();
    if (sinavSuresi === null || sinavSuresi == "" || parseInt(sinavSuresi) === 0 ||  parseInt(sinavSuresi) < 0) {
        showNotification("top", "right", "warning", "Sinav süresi 1 dakikadan fazla olmalıdır.");
    }

    var hiddenSinavId = $("#hiddenSinavIdSinavSuresiDegistir").val();
    if (hiddenSinavId == null || hiddenSinavId == "") {
        showNotification("top", "right", "warning", "İşleminiz gerçekleştirilemedi!, Lütfen daha  sonra deneyiniz.");
    }


    $.ajax({
        url: '/Sinav/SinavSuresiniDeğiştir',
        type: 'POST',
        dataType: 'json',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            sinavId: hiddenSinavId,
            sinavSuresi : sinavSuresi
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


}

function SinavAktiflikDurumuDegistir(sinavId, sinavAktifMiPasifMiYapilacak) {
    var alertOnay = confirm("İlgili sınavı " + sinavAktifMiPasifMiYapilacak + " hale getirmek istediğinize emin misiniz?");
    if (!alertOnay) {
        return;
    }

    $.ajax({
        url: '/Sinav/SinavAktiflikDurumuDeğiştir',
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