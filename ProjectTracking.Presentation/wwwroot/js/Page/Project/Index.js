$(document).ready(function () {
    $('#projectDataTable').DataTable();
});

$("#btnProjectAdd").on('click',
    function () {
        $("#projectAddModal").modal('toggle');
    }); 