$(document).ready(function () {
    function loadvehicle() {
        $.ajax({
            url: "Vehicle/vehiclegetir",
            type: "GET",
            data: {},
            success: function (data) {
                var i = 1;
                $.each(data, function (index, item) {
                    var vehicle = '<tr id="' + item.id + '"><td>' + i + ' </td> <td>' + item.aracKategoriAdi + '</td><td><button class="btn btn-warning edit">Düzenle</button></td></tr>';
                    $("#tbl1 tbody").append(vehicle);
                    i++;
                });
            }
        });
    }
    loadvehicle();
});