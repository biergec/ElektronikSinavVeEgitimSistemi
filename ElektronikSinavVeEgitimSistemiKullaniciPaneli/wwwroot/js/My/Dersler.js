$(document).ready(function () {

    $('#tableDersListesi').dataTable({
        "responsive": true,
        "dom": '<"html5buttons"B>lTfgitp',
        "buttons": [
            {
                extend: 'excel', title: 'Güncel Ders Listesi', footer: true,
                exportOptions: {
                    columns: [0, 1, 2]
                },
            },
            {
                extend: 'pdf', title: 'Güncel Ders Listesi', footer: true,
                exportOptions: {
                    columns: [0, 1, 2]
                },
                customize: function (doc) {
                    doc.defaultStyle.fontSize = 16
                }
            },
            {
                extend: 'print', footer: true,
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                },
                exportOptions: {
                    columns: [0, 1, 2],
                }
            },

        ],
        "language": {
            "emptyTable": "<span class='datatables-empty-message'><i class='fa fa-spinner fa-spin fa-3x fa-fw'></i><span class='sr-only'>Veriler Yükleniyor...</span></span>",
            "processing": "<i class='fa fa-spinner fa-spin fa-3x fa-fw'></i><span class='sr-only'>Veriler Yükleniyor...</span> ",
            "sDecimal": ".",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        },
        "order": [2, 'desc'],
        "columns": [
            { "data": "DersKodu" },
            { "data": "DersAdi" },
            {
                "data": null, "render": function (data) {
                    var tarihSplit = data.DersEklenmeTarihi.split('T');
                    return "<span>" + tarihSplit[0] + "</span>"
                }
            },
            {
                "data": null, "render": function (data) {

                    return "<center><div class='btn-group'><button data-toggle='dropdown' class='btn btn-success dropdown-toggle'>  <i class='fa fa-cog'></i> <span class='caret'></span></button><ul class='dropdown-menu'><li><a href='#' class='font-bold' value='" + data.DerslerId + "' id='" + data.DerslerId + "' onclick='SilmeOnay(this.id)'>Sil</a></li></ul></div ></center > "
                }
            }
        ]
    });


    $.ajax({
        url: "/Sinav/DersListesiGuncelle",
        type: "get",
        success: function(response) {
            if (response.isSuccess) {

                var Data = JSON.parse(response.data);

                // gelen bir veri yoksa spinnner kaldır ve veri bulunamadı yazdır.
                if (Data.length > 0) {
                    $('#tableDersListesi').dataTable().fnAddData(Data);
                } else {
                    var datatablesEmtyMessage = "İlgili kriterlerde veri bulunamadı.";
                    $('.datatables-empty-message').text(datatablesEmtyMessage);
                }
            } else {
                showNotification("top", "right", "warning", "İşlem gerçekleştirilemedi!");
            }
        },
        error: function() {
            showNotification("top", "right", "warning", "İşlem gerçekleştirilemedi!");
        }
    })
});


function DersKayit() {

    var dersKodu = $("#inputDersKodu").val();
    var dersAdi = $("#inputDersIsmi").val();

    if (!(dersKodu || dersAdi || dersAdi == "" || dersKodu == "")) {
        showNotification("top", "right", "warning", "Lütfen Ders Kodu ve Ders İsmini Giriniz!");
    }

    var dersEkleViewModel = {
        DersKodu : dersKodu,
        DersAdi : dersAdi
    };


    $.ajax({
        url: '/Sinav/DersEkle',
        type: 'POST',
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: {
            dersEkleViewModel: dersEkleViewModel
        },
        success: function (result) {
            if (result.isSuccess) {
                showNotification("top", "right", "success", "Ders Kaydı başarılı.");

                updateDatatables();
            } else {
                showNotification("top", "right", "warning", result.message);
            }
        },
        error: function () {
            showNotification("top", "right", "warning", "İşlem gerçekleştirilemedi!");
        }
    });

}


function updateDatatables() {
    $.ajax({
        url: "/Sinav/DersListesiGuncelle",
        type: "get",
        success: function(response) {
            if (response.isSuccess) {
                var Data = JSON.parse(response.data);

                // gelen bir veri yoksa spinnner kaldır ve veri bulunamadı yazdır.
                $('#tableDersListesi').dataTable().fnClearTable();

                if (Data.length > 0) {
                    $('#tableDersListesi').dataTable().fnAddData(Data);
                } else {
                    var datatablesEmtyMessage = "İlgili kriterlerde veri bulunamadı.";
                    $('.datatables-empty-message').text(datatablesEmtyMessage);
                }
            } else {
                showNotification("top", "right", "warning", "İşlem gerçekleştirilemedi!");
            }
        },
        error: function() {
            showNotification("top", "right", "warning", "İşlem gerçekleştirilemedi!");
        }
    })
}


function SilmeOnay(id) {

    swal({
            title: "Kaydı Silmek İstediğinize Emin Misiniz?",
            text: "Silinen kayıtlar geri alınamamaktadır.",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Kaydı Sil!",
            cancelButtonText: "İptal",
            closeOnConfirm: false,
            closeOnCancel: false
        },
        function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: "/Sinav/DersSil",
                    data: {
                        dersId: id
                    },
                    headers: {
                        RequestVerificationToken:
                            $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    type: "post",
                    success: function (response) {
                        if (response.isSuccess == true) {
                            
                            updateDatatables();
                            
                            swal("Kayıt Silme Başarılı", "", "success");
                        } else if (response.isSuccess == false) {
                            swal("Kayıt Silme Başarısız", "Sistemde oluşan bir hata nedeniyle kaydınız silinemedi.", "error");
                        }
                    },
                    error: function () {
                        swal("Kayıt Silme Başarısız", "Sistemde oluşan bir hata nedeniyle kaydınız silinemedi.", "error");
                    }
                })
            } else {
                swal("Silme İşlemi İptal Edildi", "", "error");
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