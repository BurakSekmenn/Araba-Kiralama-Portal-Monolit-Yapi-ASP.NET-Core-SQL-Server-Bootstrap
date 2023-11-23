$(document).ready(function () {
    GetRole();
    $('.delete-button').on('click', function () {
        var id = $(this).data('id');
        var deleteButton = $(this);

        // Silme işlemi için AJAX isteği gönder
        $.ajax({
            url: '/Vehicle/VehiclYakıtDelete/' + id,
            type: 'POST',
            success: function (data) {
                if (data.success) {
                    // Silme işlemi başarılı ise SweetAlert kullanarak bildirim göster
                    Swal.fire({
                        icon: 'success',
                        title: 'Başarılı!',
                        text: 'Silme işlemi başarıyla gerçekleştirildi.'
                    });

                    // Silinen satırı tablodan kaldır
                    deleteButton.closest('tr').remove();
                } else {
                    // Silme işlemi başarısız ise SweetAlert ile hata bildirimi göster
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: data.error
                    });
                }
            },
            error: function () {
                // Hata durumunda gerekli işlemleri yapabilirsiniz
                console.error('Silme işlemi sırasında bir hata oluştu.');
            }
        });
    });
})
function openUpdateModal(id, aracYakıtTuru) {
    $('#updateId').val(id);
    $('#updateAracYakıtTuru').val(aracYakıtTuru);
    $('#updateModal').modal('show');
}
function updateVehicle() {

    var formData = $('#updateForm').serialize();
    $.ajax({
        url: '/Vehicle/VehiclYakıtUpdate',
        type: 'POST',
        data: formData,
        success: function (data) {
            if (data.success) {
                location.reload();
            } else {
                alert('Güncelleme işlemi başarısız!');
            }
        },
        error: function () {

            console.error('Güncelleme işlemi sırasında bir hata oluştu.');
        }
    });


    return false;
}

$(document).ready(function () {
    $('.delete-button').on('click', function () {
        var id = $(this).data('id');
        var deleteButton = $(this);

        // Silme işlemi için AJAX isteği gönder
        $.ajax({
            url: '/Vehicle/VehiclYakıtDelete/' + id,
            type: 'POST',
            success: function (data) {
                if (data.success) {
                    // Silme işlemi başarılı ise SweetAlert kullanarak bildirim göster
                    Swal.fire({
                        icon: 'success',
                        title: 'Başarılı!',
                        text: 'Silme işlemi başarıyla gerçekleştirildi.'
                    });

                    // Silinen satırı tablodan kaldır
                    deleteButton.closest('tr').remove();
                } else {
                    // Silme işlemi başarısız ise SweetAlert ile hata bildirimi göster
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: data.error
                    });
                }
            },
            error: function () {
                // Hata durumunda gerekli işlemleri yapabilirsiniz
                console.error('Silme işlemi sırasında bir hata oluştu.');
            }
        });
    });
});
