 //Proje ekleme Js

$("#btnKaydet").click(function () {
    if ($('#ProjectName').val() == "" || $('#ProjectDesc').val() == "") {
        alert("Proje adı ve açıklaması boş bırakılamaz!")
    }
    else {
        let add = {
            ProjectName: $("#ProjectName").val(),
            ProjectDescription: $("#ProjectDesc").val(),
        };
        $.ajax({
            type: "post",
            url: ("/Project/Add"),
            data: add,
            success: function (func) {
                url("/Project/Index")
            }
        });
    }
});
//Proje not ekleme Js
$("#noteAddBtn").click(function () {
    if ($('#noteTitle').val() == "" || $('#noteDesc').val() == "") {
        alert("Proje adı ve açıklaması boş bırakılamaz!")
    }
    else {
        let add = {
            ProjectNoteTitle: $("#noteTitle").val(),
            ProjectNoteDescription: $("#noteDesc").val(),
            ProjectId: $('#ProjectId').val(),
        };
        $.ajax({
            type: "post",
            url: ("/ProjectNote/Add"),
            data: add,
            success: function (func) {
                alert("Başarı");
            }
        });
    }
});
function drawDataTable() {
    $('#projectNotes').DataTable({
        ajax: '/ProjectNote/GetProjectNote/2',
        columns: [
            { data: 'name' },
            { data: 'desc' },
        ]
    });
}