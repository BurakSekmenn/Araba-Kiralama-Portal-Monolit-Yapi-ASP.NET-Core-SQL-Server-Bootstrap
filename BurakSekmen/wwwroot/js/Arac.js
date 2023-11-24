$(document).ready(function () {
    $('.delete-button').on('click', function () {
        var Id = $(this).data('id');
        var deleteButton = $(this);

        $.ajax({
            url: '/Vehicle/VehicleDelete/' + Id,
            type: 'POST',
            success: function (data) {
                if (data.success) {

                    Swal.fire({
                        icon: 'success',
                        title: 'Başarılı!',
                        text: 'Silme işlemi başarıyla gerçekleştirildi.'
                    });
                    deleteButton.closest('tr').remove();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: data.error
                    });
                }
            },
            error: function () {

                console.error('Silme işlemi sırasında bir hata oluştu.');
            }
        });
    });
})

    new DataTable('#deneme', {
        responsive: true,
        "language": {
            "search": "Arama Yap:",
            "lengthMenu": "Her Sayfada _MENU_  tane veri göster",
            "zeroRecords": "Kayıt Bulunamadı",
            "info": "_MAX_ kayıttan   _PAGE_ - _MAX_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt Bulunamadı",
            "infoFiltered": "(Toplam Veri Sayısı _MAX_ )",
            "paginate": {
                "previous": "Geri",
                "next": "İleri"
            }
             
        }
    });
