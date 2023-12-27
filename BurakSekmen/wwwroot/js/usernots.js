$(document).ready(function () {
    $("button.btn-secondary").click(function () {

        $("button.btn-info").hide();
    });

    loadmarka();

    function loadmarka() {
        $("#tbaraba tbody").empty();
        $.ajax({
            url: "/Home/NotBul",
            type: "Get",
            data: {},
            success: function (data) {
                var i = 1;
                $.each(data, function (index, item) {
                    var tr = $('<tr id="' + item.id + '"></tr>');
                    tr.append('<td>' + i + '</td>');
                    tr.append('<td>' + item.not + '</td>');

                    var btnGuncelle = $('<button type="button" id="duzenleButton" class="btn btn-secondary">Düzenle</button>');
                    btnGuncelle.click(function () {
                        btnGuncelleClick(item.id, item.not);
                    });
                    var td = $('<td></td>');
                    td.append(btnGuncelle);
                    tr.append(td);

                    $("#tbaraba tbody").append(tr);
                    i++;
                });
            }
        });
    }

    function btnGuncelleClick(id, not) {
        $("#id").val(id);
        $("#not").val(not);
    }





    $("button.btn-success").click(function () {
        event.preventDefault();

        var id = $("#id").val();
        var not = $("#not").val();

        if (not.trim() === "") {

            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Lütfen Düzenle deyip güncelleme yapınız.',
            });
            return;
        }
        if (id.trim() === "") {

            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Yanlış Güncelleme Yap Buttonuna Bastınız.',
            });
            return;
        }




        $.ajax({
            url: "/Home/NotUpdate",
            type: "POST",
            data: { id: id, not: not },
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Veri güncelleme işlemi başarıyla tamamlandı.',
                });
                $("#id").val("");
                $("#not").val("");
                loadmarka();
            },
            error: function () {
                console.error("Veri güncelleme işleminde hata oluştu.");
            }
        });
    });

    $("button.btn-danger").click(function () {
        event.preventDefault();

        var id = $("#id").val();
        var not = $("#not").val();
        if (not.trim() === "") {
            // SweetAlert ile hata bildirimi
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Lütfen Düzenle deyip silme işlemi yapınız.',
            });
            return;
        }

        $.ajax({
            url: "/Home/NotDelete",
            type: "POST",
            data: { id: id, not: not },
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Veri Silme işlemi başarıyla tamamlandı.',
                });
                $("#id").val("");
                $("#not").val("");
                loadmarka();
            },
            error: function () {
                console.error("Veri Silme işleminde hata oluştu.");
            }
        });
    });
    $("button.btn-info").click(function () {
        event.preventDefault();

        var id = $("#id").val();
        var not = $("#not").val();


        if (not.trim() === "") {

            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Araç marka alanı boş olamaz.',
            });
            return; // İşlemi sonlandır
        }





        // AJAX kullanarak sunucuya verileri gönder
        $.ajax({
            url: "/Home/NotInsert",
            type: "POST",
            data: { not: not },
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Veri Ekleme işlemi başarıyla tamamlandı.',
                });
                $("#id").val("");
                $("#not").val("");
                loadmarka();
            },
            error: function () {
                console.error("Veri kaydetme işleminde hata oluştu.");
            }
        });
    });
});
