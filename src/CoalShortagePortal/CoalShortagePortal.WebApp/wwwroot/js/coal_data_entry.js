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

function onPasteAreaChange(evt, pasteAreaType) {
    // https://stackoverflow.com/a/4099587/2746323
    var blnCameFromPaste = ((evt.target.getAttribute("pasted") || "") == "1");
    if (blnCameFromPaste) {
        var pastedData = evt.target.value;
        console.log(pastedData);
        // check for non empty strings
        if (pastedData != null && pastedData != "") {
            // create pastedDataArray
            var pastedDataArray = []
            var pastedRows = pastedData.split("\n");
            for (var i = 0; i < pastedRows.length; i++) {
                var rowString = pastedRows[i];
                var rowCells = rowString.split('\t');
                pastedDataArray.push(rowCells);
            }
            if (pasteAreaType == 'coal_shortage') {
                pasteCoalShortageData(pastedDataArray);
            } else if (pasteAreaType == 'other_reasons') {
                pasteOtherReasonsData(pastedDataArray);
            } else if (pasteAreaType == 'critical_coal') {
                pasteCriticalData(pastedDataArray);
            }
        }
    }
    evt.target.value = "";
    evt.target.setAttribute("pasted", "0")
}

function onPasteAreaPaste(sndr) {
    sndr.setAttribute('pasted', '1');
}

function pasteCoalShortageData(dataArray) {
    // sort the data table by serial number
    var tabl = $('#coal_entry_table').DataTable()
    tabl.order([0, 'asc']).draw();

    // get the coal_shortage_prev text areas
    var prevAvgInps = document.getElementsByClassName('coal_shortage_prev');
    var lossInps = document.getElementsByClassName('coal_shortage_loss');
    var remarksInps = document.getElementsByClassName('coal_shortage_remarks');
    for (var rowIter = 0; rowIter < dataArray.length; rowIter++) {
        // paste the avg gen value in the appropriate cell
        if ((rowIter < prevAvgInps.length) && (dataArray[rowIter].length > 0) && !isNaN(dataArray[rowIter][0])) {
            prevAvgInps[rowIter].value = dataArray[rowIter][0];
        }

        // paste the gen loss value in the appropriate cell
        if ((rowIter < lossInps.length) && (dataArray[rowIter].length > 1) && !isNaN(dataArray[rowIter][1])) {
            lossInps[rowIter].value = dataArray[rowIter][1];
        }

        // paste the remarks value in the appropriate cell
        if ((rowIter < remarksInps.length) && (dataArray[rowIter].length > 2)) {
            remarksInps[rowIter].value = dataArray[rowIter][2];
        }
    }
    alert('Coal shortage excel cells paste done, please check in table...');
}

function pasteOtherReasonsData(dataArray) {
    // sort the data table by serial number
    var tabl = $('#other_entry_table').DataTable()
    tabl.order([0, 'asc']).draw();

    // get the coal_shortage_prev text areas
    var prevAvgInps = document.getElementsByClassName('other_reasons_prev');
    var lossInps = document.getElementsByClassName('other_reasons_loss');
    var remarksInps = document.getElementsByClassName('other_reasons_remarks');
    for (var rowIter = 0; rowIter < dataArray.length; rowIter++) {
        // paste the avg gen value in the appropriate cell
        if ((rowIter < prevAvgInps.length) && (dataArray[rowIter].length > 0) && !isNaN(dataArray[rowIter][0])) {
            prevAvgInps[rowIter].value = dataArray[rowIter][0];
        }

        // paste the gen loss value in the appropriate cell
        if ((rowIter < lossInps.length) && (dataArray[rowIter].length > 1) && !isNaN(dataArray[rowIter][1])) {
            lossInps[rowIter].value = dataArray[rowIter][1];
        }

        // paste the remarks value in the appropriate cell
        if ((rowIter < remarksInps.length) && (dataArray[rowIter].length > 2)) {
            remarksInps[rowIter].value = dataArray[rowIter][2];
        }
    }
    alert('Gen loss due to other reasons excel cells paste done, please check in table...');
}

function pasteCriticalData(dataArray) {
    // sort the data table by serial number
    var tabl = $('#critical_entry_table').DataTable()
    tabl.order([0, 'asc']).draw();

    // get the coal_shortage_prev text areas
    var presentGenInps = document.getElementsByClassName('critical_coal_present');
    var lossInps = document.getElementsByClassName('critical_coal_loss');
    var daysInps = document.getElementsByClassName('critical_coal_days');
    var remarksInps = document.getElementsByClassName('critical_coal_remarks');
    for (var rowIter = 0; rowIter < dataArray.length; rowIter++) {
        // paste the present gen value in the appropriate cell
        if ((rowIter < presentGenInps.length) && (dataArray[rowIter].length > 0) && !isNaN(dataArray[rowIter][0])) {
            presentGenInps[rowIter].value = dataArray[rowIter][0];
        }

        // paste the gen loss value in the appropriate cell
        if ((rowIter < lossInps.length) && (dataArray[rowIter].length > 1) && !isNaN(dataArray[rowIter][1])) {
            lossInps[rowIter].value = dataArray[rowIter][1];
        }

        // paste the days value in the appropriate cell
        if ((rowIter < daysInps.length) && (dataArray[rowIter].length > 2) && !isNaN(dataArray[rowIter][2])) {
            daysInps[rowIter].value = dataArray[rowIter][2];
        }

        // paste the remarks value in the appropriate cell
        if ((rowIter < remarksInps.length) && (dataArray[rowIter].length > 3)) {
            remarksInps[rowIter].value = dataArray[rowIter][3];
        }
    }
    alert('Critical coal excel cells paste done, please check in table...');
}