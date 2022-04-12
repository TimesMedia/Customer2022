var table = document.getElementById("myTable");
var rows = document.getElementsByTagName("tr");
for (i = 1; i < rows.length; i++) {
    var currentRow = table.rows[i];
    currentRow.onclick = function () {
        Array.from(this.parentElement.children).forEach(function (el) {
            el.classList.remove('selected-row');
        });
        this.classList.add('selected-row');

    }
}
//const searchInput = document.getElementById('myTable');

//searchInput.addEventListener('input', () => {
//console.log('Event handler fired');
//});

function tablesearch() {
    let input, filter, td, tr, table, i, txtvalue;
    //console.log("is this working?");
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    for (let i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtvalue = td.textContent || td.innerText;
            if (txtvalue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            }
            else {
                tr[i].style.display = "none";
            }
        }
    }
}


