function YeniSoruEkle() {

    if (!soruKontrol()) {
        showNotification("top", "right", "warning", "Yeni soru eklemeden önce lütfen şu anki soruyu tamamlayınız!");
        return false;
    }

    var eskiSoruSayisi = $("#soruSayisi").val();
    var yeniSoruSayisi = parseInt(eskiSoruSayisi) + 1;

    var yeniSoruHtml = "<div class='card card-nav-tabs' id='soru" + yeniSoruSayisi + "Card'>" +
        "<div class='card-header card-header-success'>" +
        "Soru " + yeniSoruSayisi + "</div>" +
        "<div class='card-body'>" +
        "<br /><input type='text' class='form-control' id='soru" + yeniSoruSayisi + "' placeholder='Lütfen soru metnini giriniz...'> <br /></div></div></div ><br>";


    $("#soruSayisi").val(yeniSoruSayisi);
    $("#soruAlani").prepend(yeniSoruHtml);

    showNotification("top", "right", "success", yeniSoruSayisi + ". Soru Eklendi");

    return true;
}

function soruKontrol() {

    var soruSayisi = $("#soruSayisi").val();
    var hataSayisi = 0;

    for (var i = 0; i < soruSayisi; i++) {
        var soruText = $("#soru" + (i + 1)).val();
        if (!soruText || soruText.length == 0) {
            hataSayisi += 1;
        }

        if (hataSayisi > 0) {
            return false;
        }
    }

    return true;
}

function SonSoruyuSil() {
    var soruSayisi = $("#soruSayisi").val();

    if (soruSayisi != 1) {
        $("#soruSayisi").val(soruSayisi - 1);
        $("#soru" + soruSayisi + "Card").remove();
        showNotification("top", "right", "success", "Son Soru başarılı bir şekilde silindi.");
    } else {
        showNotification("top", "right", "warning", "İlk Soru silinemez. Sınavı iptal etmek için kayıt etmeden çıkabilirsiniz!");
    }
}

function SinaviKayitEt(soruSikSayisi) {

    if (!soruKontrol()) {
        showNotification("top", "right", "warning", "Lütfen hatalı sorularınızı düzeltiniz!");
        return false;
    }

    var soruSayisi = $("#soruSayisi").val();
    var dersKodu = $("#dersKodu").val();
    var sorular = [];
    var klasikSinavSorulari = [];

    for (var i = 0; i < soruSayisi; i++) {
        var soruText = $("#soru" + (i + 1)).val();
        sorular[i] = soruText;
    }
    
    klasikSinavSorulari = {
        Sorular: sorular,
        DersGuidId: dersKodu
    };

    $.ajax({
        url: '/Sinav/KlasikSinavOlustur',
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            klasikSinavSorulari: klasikSinavSorulari
        },
        success: function (result) {
            if (result.isSuccess) {
                showNotification("top", "right", "success", "Sınav oluşturma başarılı. Sınavlarım menüsüne yönlendiriliyorsunuz. Lütfen bekleyin...");

                setTimeout(function() {
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

    return null;
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
