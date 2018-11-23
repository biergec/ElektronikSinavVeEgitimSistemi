function TestSinavKayit() {

    var testSinavId = $("#testSinavId").val();
    var soruSayisi = $("#soruSayisi").val();
    var testSinavCevaplariList = [];

    for (var i = 0; i < soruSayisi; i++) {
        var testSinavSorularId = $("#testSinavSorularId" + (i + 1)).val();
        var testSinavCevapSikki = $("#inputSoruCevabi" + (i + 1)).val();

        testSinavCevaplariList[i] = {
            TestSinavSorularId: testSinavSorularId,
            SoruCevapSikki: testSinavCevapSikki
        };
    }

    var testSinavSinaviKayitEtViewModel = {
        SinavId: testSinavId,
        TestSinavCevaplariList: testSinavCevaplariList
    };


    $.ajax({
        url: '/Home/GirilenTestSinaviniKayitEt',
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            testSinavSinaviKayitEtViewModel: testSinavSinaviKayitEtViewModel
        },
        success: function (result) {
            if (result.isSuccess) {
                showNotification("top", "right", "success", result.message);

                setTimeout(function () {
                    // İşlem başarılı ise sınavlarım menüsüne yönlendirsin!
                    window.location.href = "/";
                }, 1500);

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