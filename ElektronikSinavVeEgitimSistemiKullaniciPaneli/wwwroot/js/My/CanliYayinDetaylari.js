function CanliYayinAktifYap(canliYayinGuid) {
    $.ajax({
        url: '/CanliYayin/CanliYayinBaslat',
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            canliYayinGuid: canliYayinGuid
        },
        success: function (result) {
            window.location.href = "/CanliYayin/CanliYayinDetaylari?canliYayinGuid="+result.data.toString();
        },
        error: function () {
            alert("Yayın Aktif Etme Başarısız!");
        }
    });
}

function CanliYayiniSil(canliYayinGuid) {
    $.ajax({
        url: '/CanliYayin/CanliYayiniSil',
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            canliYayinGuid: canliYayinGuid
        },
        success: function (result) {
            if (result.Data.isSuccess) {
                window.location.href = "/CanliYayin/Index";
            } else {
                window.location.href = "/CanliYayin/CanliYayinDetaylari?canliYayinGuid="+result.data.canliYayinGuid.toString();
            }
        },
        error: function () {
            alert("Yayın Pasif Yapma Başarısız!");
        }
    });
}

function CanliYayinPasifYap(canliYayinGuid) {
    $.ajax({
        url: '/CanliYayin/CanliYayinSonlandir',
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            canliYayinGuid: canliYayinGuid
        },
        success: function (result) {
            window.location.href = "/CanliYayin/CanliYayinDetaylari?canliYayinGuid="+result.data.toString();
        },
        error: function () {
            alert("Yayın Pasif Yapma Başarısız!");
        }
    });
}
