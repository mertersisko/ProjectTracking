$(document).ready(function () {
    $('#projectDataTable').DataTable();
});

function ProjectAdd() {
    $("#projectAddModal").modal('toggle');

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
}
function NoteAdd(id) {
    $("#addProjectNote").modal('toggle');
    $("#ProjectId").val(id);

    $("#noteAddBtn").on('click',
        function () {
            var model = {};
            model.ProjectNoteTitle = $('#noteTitle').val();
            model.ProjectNoteDescription = $('#noteDesc').val();
            model.ProjectId = $('#ProjectId').val();

            AjaxPostJsonModel("/ProjectNote/Add", model).then((response) => {

                if (response.status == 1) {
                    $("#addProjectNote").modal('toggle');
                    $('#noteTitle').val('');
                    $("#ProjectId").val('')
                    $('#noteDesc').val('');

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
}
function View(id) {
    $("#viewprojectnote").modal('toggle');
    $.ajax({
        url: '/ProjectNote/GetProjectNote/' + id,
        type: 'GET',
        processData: false,
        cache: false,
        success: function (data) {
            $("#projectNotesTable").empty();
            var html = '';
            $.each(data,
            function (index, element) {
                html += '<tr>';
                html += '<td>' + element.id + '</td>';
                html += '<td>' + element.name + '</td>';
                html += '<td>' + element.desc + '</td>';
                html += '</tr>'
            });
            $("#projectNotesTable").append(html)
}
    });

}
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

