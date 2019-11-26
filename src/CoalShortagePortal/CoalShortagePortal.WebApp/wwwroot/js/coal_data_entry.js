$(document).ready(function () {
    // ids of data entry tables
    var entryTableIds = ['coal_entry_table', 'other_entry_table', 'critical_entry_table']
    for (var tableIter = 0; tableIter < entryTableIds.length; tableIter++) {
        // convert each data entry table to jquery data table
        var table = $('#' + entryTableIds[tableIter]).DataTable({
            fixedColumns: true,
            fixedHeader: true,
            "lengthMenu": [[10, 20, -1], [10, 20, "All"]],
            "pageLength": -1,
            dom: 'Bfrtip',
            buttons: ['pageLength', 'copy', 'csv', 'excel', 'pdf', 'print']
        });
    }
});