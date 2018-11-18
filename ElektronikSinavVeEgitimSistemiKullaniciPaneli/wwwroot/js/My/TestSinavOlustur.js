function YeniSoruEkle(soruSikSayisi) {

    if (!soruKontrol(soruSikSayisi)) {
        showNotification("top", "right", "warning", "Yeni soru eklemeden önce lütfen şu anki soruyu tamamlayınız!");
        return false;
    }

    var eskiSoruSayisi = $("#soruSayisi").val();
    var yeniSoruSayisi = parseInt(eskiSoruSayisi) + 1;

    var sorularHtml = "";
    for (var i = 0; i < soruSikSayisi; i++) {
        sorularHtml += "<div class='row m-t-1'>" +
            "<div class='col-md-1'>" +
            "<a class='btn btn-info'>" + (i + 1) + "</a>" +
            "</div>" +
            "<div class='col-md-11'>" +
            "<input type='text' class='form-control' id='soru" + yeniSoruSayisi + "cevap" + (i + 1) + "' placeholder='Lütfen bir cevap giriniz'></div></div>";
    }

    var yeniSoruHtml = "<div class='card card-nav-tabs' id='soru" + yeniSoruSayisi + "Card'>" +
        "<div class='card-header card-header-success'>" +
        "<div class='row'><div class='col-md-1'>" +
        "<button class='form-control'>" + yeniSoruSayisi + "</button>" +
        "</div>" +
        "<div class='col-md-11'>" +
        "<input type='text' style='color: white' class='form-control' id='soru" + yeniSoruSayisi + "' placeholder='Lütfen soru metnini giriniz'>" +
        "</div></div> </div>" +
        "<div class='card-body'>" +
        "<div class='row'>" +
        "<div class='col-md-12'>" +
        "<input type='number' min='1' max='" + soruSikSayisi + "' class='form-control' id='soru" + yeniSoruSayisi + "DogruCevap' placeholder='Sorunun doğru cevabının numarasını giriniz'> " +
        "</div></div>" +
        "<br />" + sorularHtml + "</div></div></div ><br>";


    $("#soruSayisi").val(yeniSoruSayisi);
    $("#soruAlani").prepend(yeniSoruHtml);

    showNotification("top", "right", "success", yeniSoruSayisi + ". Soru Eklendi");

    return true;
}


// önceki soru tamamen girlmediyse yeni soru ekleme
function soruKontrol(soruSikSayisi) {

    var soruSayisi = $("#soruSayisi").val();
    var hataSayisi = 0;

    for (var i = 0; i < soruSayisi; i++) {

        var soruText = $("#soru" + (i + 1)).val();
        if (!soruText || soruText.length == 0) {
            hataSayisi += 1;
        }

        var soruDogruCevap = $("#soru" + (i + 1) + "DogruCevap").val();
        if (!soruDogruCevap || soruDogruCevap.length == 0) {
            hataSayisi += 1;
        }

        for (var j = 0; j < soruSikSayisi; j++) {
            var soruCevapKontrol = $("#soru" + soruSayisi + "cevap" + (j + 1)).val();
            if (!soruCevapKontrol || soruCevapKontrol.length == 0) {
                hataSayisi += 1;
            }
        }

        if (!SoruDogruCevapGirisKontrol(parseInt(i + 1), soruSikSayisi)) {
            showNotification("top", "right", "warning", parseInt(i + 1) + ". Soru Cevabınız Hatalıdır !");
            return false;
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
        showNotification("top", "right", "success", "Son soru başarılı bir şekilde silindi.");
    } else {
        showNotification("top", "right", "warning", "İlk soru silinemez. Sınavı iptal etmek için kayıt etmeden çıkabilirsiniz!");
    }
}


// soru doğru cevabı şıklardan birisi mi kontrol et! değlse hata ver
function SoruDogruCevapGirisKontrol(suAnkiSoru, soruSikSayisi) {

    var soruDogruCevap = $("#soru" + suAnkiSoru + "DogruCevap").val();
    if (soruDogruCevap > soruSikSayisi || soruDogruCevap <= 0) {
        return false;
    }

    return true;
}


function SinaviKayitEt(soruSikSayisi) {

    if (!soruKontrol(soruSikSayisi)) {
        showNotification("top", "right", "warning", "Lütfen hatalı sorularınızı düzeltiniz!");
        return false;
    }

    var soruSayisi = $("#soruSayisi").val();
    var dersKodu = $("#dersKodu").val();

    var testSinavSorulari = [];
    var soruSiklariText = [];
    var soruTemplate = [];

    for (var i = 0; i < soruSayisi; i++) {

        var soruText = $("#soru" + (i + 1)).val();

        var soruDogruCevap = $("#soru" + (i + 1) + "DogruCevap").val();

        for (var j = 0; j < soruSikSayisi; j++) {
            var soruCevap = $("#soru" + soruSayisi + "cevap" + (j + 1)).val();
            soruSiklariText[j] = soruCevap;
        }

        var soru = {
            SoruDogruSik: soruDogruCevap,
            SoruText: soruText,
            SoruSiklari: soruSiklariText
        };

        soruTemplate[i] = soru;
    }

    testSinavSorulari = {
        DersGuidId: dersKodu,
        SoruTemplate: soruTemplate
    };


    $.ajax({
        url: '/Sinav/TestSinavOlustur',
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            testSinavSorulari: testSinavSorulari
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