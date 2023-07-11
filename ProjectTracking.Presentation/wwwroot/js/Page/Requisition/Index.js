$(document).ready(function () {
    $('#RequisitionDataTable').DataTable();
});

$("#btnAddRequisition").on('click',
    function () {
        $("#requisitionAddModal").modal('toggle');
    });
$("#btnReqSave").on('click',
    function () {
        var model = {};
        model.StartDate = $('#DtTarih').val();
        model.RequisitionName = $('#txtBaslik').val();
        model.RequisitionDescription = $('#txtTalepAciklama').val();

        AjaxPostJsonModel("/Requisition/Add", model).then((response) => {

            if (response.status == 1) {
                $("#requisitionAddModal").modal('toggle');
                $('#DtTarih').val('');
                $('#txtBaslik').val('');
                $('#txtTalepAciklama').val('');

                $.ajax({
                    url: '/Requisition/RequisitionGet',
                    type: "GET",
                    processData: false,
                    cache: false,
                    beforeSend: function () {

                    },
                    success: function (data) {
                        $("#empoloyees-tblwrapper").html('');
                        $("#empoloyees-tblwrapper").html(data);

                    },
                    complete: function () {

                    }
                });

            }
        });

    });
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