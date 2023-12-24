$(document).ready(function () {
       $("button.btn-secondary").click(function () {
      
        $("button.btn-info").hide();
    });

    loadmarka();

    function loadmarka() {
        $("#tbaraba tbody").empty();
        $.ajax({
            url: "/Vehicle/Arababul",
            type: "Get",
            data: {},
            success: function (data) {
                var i = 1;
                $.each(data, function (index, item) {
                    var tr = $('<tr id="' + item.id + '"></tr>');
                    tr.append('<td>' + i + '</td>');
                    tr.append('<td>' + item.aracmarka + '</td>');

                    var btnGuncelle = $('<button type="button" id="duzenleButton" class="btn btn-secondary">Düzenle</button>');
                    btnGuncelle.click(function () {
                        btnGuncelleClick(item.id, item.aracmarka);
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

    function btnGuncelleClick(id, aracmarka) {
        $("#id").val(id);
        $("#aracmarka").val(aracmarka);
    }

 



    $("button.btn-success").click(function () {
        event.preventDefault(); 

        var id = $("#id").val();
        var aracmarka = $("#aracmarka").val();

        if (aracmarka.trim() === "") {
           
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Araç marka Lütfen Düzenle deyip güncelleme yapınız.',
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
            url: "/Vehicle/ArabaUpdate",
            type: "POST",
            data: { id: id, aracmarka: aracmarka },
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Veri güncelleme işlemi başarıyla tamamlandı.',
                });
                $("#id").val("");
                $("#aracmarka").val("");
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
        var aracmarka = $("#aracmarka").val();
        if (aracmarka.trim() === "") {
            // SweetAlert ile hata bildirimi
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Araç marka Lütfen Düzenle deyip silme işlemi yapınız.',
            });
            return; 
        }
    
        $.ajax({
            url: "/Vehicle/AracDelete",
            type: "POST",
            data: { id: id, aracmarka: aracmarka },
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Veri Silme işlemi başarıyla tamamlandı.',
                });
                $("#id").val("");
                $("#aracmarka").val("");
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
        var aracmarka = $("#aracmarka").val();

       
        if (aracmarka.trim() === "") {
       
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: 'Araç marka alanı boş olamaz.',
            });
            return; // İşlemi sonlandır
        }


      


        // AJAX kullanarak sunucuya verileri gönder
        $.ajax({
            url: "/Vehicle/AracInsert",
            type: "POST",
            data: { id: id, aracmarka: aracmarka },
            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Başarılı!',
                    text: 'Veri Ekleme işlemi başarıyla tamamlandı.',
                });
                $("#id").val("");
                $("#aracmarka").val("");
                loadmarka();
            },
            error: function () {
                console.error("Veri kaydetme işleminde hata oluştu.");
            }
        });
    });
});
