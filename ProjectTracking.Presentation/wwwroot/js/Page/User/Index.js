
$(document).ready(function () {
    $('#userDataTable').DataTable();
});

$("#btnAddUser").on('click',
    function () {
        $("#userAddModal").modal('toggle');
    });


$("#btnSave").on('click',
    function () {
        var model = {};
        model.Name = $('#txtName').val();
        model.Surname = $('#txtSurname').val();
        model.Email = $('#txtMail').val();
        model.Password = $('#txtPassword').val();
        model.PasswordAgain = $('#txtPasswordAgain').val();

        AjaxPostJsonModel("/User/Add", model).then((response) => {

            if (response.status == 1) {
                $("#userAddModal").modal('toggle');
                $('#txtName').val('');
                $('#txtSurname').val('');
                $('#txtMail').val('');
                $('#txtPassword').val('');
                $('#txtPasswordAgain').val('');

                $.ajax({
                    url: '/User/UserGet',
                    type: "GET",
                    processData: false,
                    cache: false,
                    beforeSend: function () {

                    },
                    success: function (data) {
                        $("#userDataTable").html('');
                        $("#userDataTable").html(data);

                    },
                    complete: function () {

                    }
                });

            }
        });

    });


$("btnUpdate").on('click',
    function () {
        $("#userUpdateModal").modal('toggle');
        $.ajax({
            url: '/User/UserGet',
            type: "GET",
            processData: false,
            cache: false,
            beforeSend: function () {

            },
            success: function (data) {
                $("#empoloyees-tblwrapper").html('');
                $("#empoloyees-tblwrapper").html(data);

            },
            complete: function () { }
        });
    }
)


function AjaxPostJsonModel(url, data) {
    return $.ajax({
        url: url,
        data: JSON.stringify(data),
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        processData: false,
        cache: false,
        beforeSend: function () {

        },
        complete: function () {

        }
    });
}