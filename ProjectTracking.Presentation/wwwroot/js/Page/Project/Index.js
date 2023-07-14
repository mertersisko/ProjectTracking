$(document).ready(function () {
    $('#projectDataTable').DataTable();
});

$("#btnProjectAdd").on('click',
    function () {
        $("#projectAddModal").modal('toggle');
    });

$("#btnProjectSave").on('click',
    function () {
        var model = {};
        model.ProjectName = $('#ProjectName').val();
        model.ProjectDescription = $('#ProjectDesc').val();

        AjaxPostJsonModel("/Project/Add", model).then((response) => {

            if (response.status == 1) {
                $("#projectAddModal").modal('toggle');
                $('#ProjectName').val('');
                $('#ProjectDesc').val('');


                $.ajax({
                    url: '/Project/ProjectGet',
                    type: "GET",
                    processData: false,
                    cache: false,
                    beforeSend: function () {

                    },
                    success: function (data) {
                        $("#projectDataTable").html('');
                        $("#projectDataTable").html(data);

                    },
                    complete: function () {

                    }
                });

            }
        });

    });


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

            AjaxPostJsonModel("/Project/Delete/" + id).then((response) => {

                $.ajax({
                    url: '/Project/ProjectGet',
                    type: "GET",
                    processData: false,
                    cache: false,
                    beforeSend: function () {

                    },
                    success: function (data) {
                        $("#projectDataTable").html('');
                        $("#projectDataTable").html(data);

                    },
                    complete: function () {

                    }
                });

            });
        }
    });
}


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