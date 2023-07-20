
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
                $("#userDataTable").html('');
                $("#userDataTable").html(data);

            },
            complete: function () { }
        });
    }
)

function Delete(id) {
    Swal.fire({
        title: 'Dikkat !',
        text: "Kayıt silinecek onaylıyor musunuz ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Onay',
        cancelButtonText: 'Vazgeç'
    }).then((result) => {
        if (result.isConfirmed) {

            AjaxPostJsonModel("/User/Delete/" + id).then((response) => {

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

            });
        }
    });
}
function ChangeStatusWithPartialView(id) {

    AjaxPostJsonModel("/User/ChangeStatus/" + id).then((response) => {

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

    });

}


//function Update(id) {

//    $("#userUpdateModal").modal('toggle');

//    $.ajax({
//        url: '/User/Update/' + id,
//        type: "GET",
//        processData: false,
//        cache: false,
//        beforeSend: function () {

//        },
//        success: function (data) {
//            $("#userUpdateModal").html('');
//            $("#userUpdateModal").html(data);

//        },
//        complete: function () {

//        }
//    });

//    //$('#txtName').val(id.Name);
//    //$('#txtSurname').val(id.Surname);
//    //$('#txtMail').val(id.Email);
//    //$('#txtPassword').val(id.Password);
//    //$('#txtPasswordAgain').val(id.PasswordAgain);

//    //AjaxPostJsonModel("/User/Update/" + id).then((response) => {

//    //    if (response.status == 1) {
//    //        $("#userAddModal").modal('toggle');
//    //        $('#txtName').val('');
//    //        $('#txtSurname').val('');
//    //        $('#txtMail').val('');
//    //        $('#txtPassword').val('');
//    //        $('#txtPasswordAgain').val('');

//    //        $.ajax({
//    //            url: '/User/UserGet',
//    //            type: "GET",
//    //            processData: false,
//    //            cache: false,
//    //            beforeSend: function () {

//    //            },
//    //            success: function (data) {
//    //                $("#userDataTable").html('');
//    //                $("#userDataTable").html(data);

//    //            },
//    //            complete: function () {

//    //            }
//    //        });

//    //    }
//    //});

//};


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