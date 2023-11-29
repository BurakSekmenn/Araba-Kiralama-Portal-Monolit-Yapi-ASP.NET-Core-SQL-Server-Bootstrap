$(document).ready(function () {
    GetRole()
});

/*read data*/
function GetRole() {
    $.ajax({
        url: '/Admin/GetRole',
        type: 'GET',
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            console.log(response)
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td class="colspan="5">' + 'Role not availabe' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object);
            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.id + '</td>';
                    object += '<td>' + item.fullName + '</td>';
                    object += '<td>' + item.userName + '</td>';
                    object += '<td>' + item.role + '</td>';
                    //object += '<td> <a href = "#" id="editModal class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a>  <a href = "#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Delete</a> </td> ';
                    object += `<td> <a href="/Admin/Edit/${item.id}" class="btn btn-primary " data-id="${item.id}">Edit</a>  </td> `;
                    object += '</tr>';

                });

                $('#tblBody').html(object)
            }
        },
        Error: function () {
            alert('Unable to read the data.');
        }
    });
}





