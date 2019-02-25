// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// To add empty text box field in the form
var i = 1;
function Dynamicforminput() {
    var division = document.createElement('DIV');
    division.innerHTML = DynamicTextBox("");
    document.getElementById("subcategorydiv").appendChild(division.firstChild);
    ++i;
    console.log(i);
}

// The style of the textbox field method passed set in the document innerHTML
function DynamicTextBox(value) {
    return '<div class="form-group">  ' +
        '<label class="control-label">Sub Boat Class['+ i +']</label>  ' +
        '<input name="Sub_Boat_Types['+ i + '].Sub_boat_class_type" class="form-control" aria-describedby="button-addon2" />  ' +
        '<div class="input-group-append">  ' +
        '<input class="btn btn-outline-secondary" type="button" id="button-addon2" onclick="RemoveDynamicTextBox(this)" value="Remove boat sub class"/>  ' +
        '</div>  ' +
        '<span asp-validation-for="Sub_Boat_Types" class="text-danger"></span>  ' +
        '</div>  ';
}

// To remove the added textbox from the form
function RemoveDynamicTextBox(div) {
    --i;
    document.getElementById("subcategorydiv").removeChild(div.parentNode.parentNode);
}