// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//window.onload = function () {

//    runCode();

//};



$(document).ready(function () {

   

    //$('#btnAdd').click(function () {

    //});
    //$("#autocomplete").autocomplete({
    //    source: ["c++", "java", "php", "coldfusion", "javascript", "asp", "ruby"]
    //});
    //$("#autocomplete").on("autocompleteresponse",
    //    function (event, ui) {

    //        document.getElementById('autocomplete').value = (ui.content[0].value);
    //        $('.test').on('focus', function (event) {
    //            var inp = this;
    //            var t = self.setTimeout(function () { inp.select(); }, 1);
    //        });

    //    });

    //alert("pepe");
    //var data = {
    //    id: 1,
    //    text: 'Barn owl'
    //};

    //var newOption = new Option(data.text, data.id, false, false);
    //$('#state').append(newOption).trigger('change');



    //("#state").on("change", function () {

    $("body").on("change", "#state", function () {


        //var value = $(this).val();
        //alert("Value"); Funciona
    });


    //$("#state").select2({

    //    language: {
    //        noResults: function () {
    //            return '<button id="btn">Find</button>';
    //        }

    //});

    // Set up the Select2 control


    ;

    // Fetch the preselected item, and add to the control


    //$("#state").select2({
    //    ajax: {
    //        url: "/Orders/GetProducts",
    //        method: "get",
    //        data: function (params) {
    //            return {
    //                search: params.term
    //            };
    //        },
    //        processResults: function (data) {
    //            return {
    //                results: data
    //            };
    //        },
    //        cache: true
    //    },
    //    placeholder: "Enter a User ID or Name",
    //    templateResult: function (data) {
    //        return "(" + data.user + ") " + data.name;
    //    },
    //    templateSelection: function (data) {
    //        return "(" + data.user + ") " + data.name;
    //    }
    //}).ready(function () {
    //    $(".select2-selection__placeholder").text("Enter a User ID or Name")
    //});

    //$("body").on("click", "#btnAdd", function () {
    //    //Reference the Name and Country TextBoxes.
    //    var txtName = $("#txtName");

    //    var productos = document.getElementById('txtName');
    //    var opt = productos.options[productos.selectedIndex];

    //    alert(opt.val());
       
    //    ////Get the reference of the Table's TBODY element.
    //    //var tBody = $("#tblCustomers > TBODY")[0];

    //    ////Add Row.
    //    //var row = tBody.insertRow(-1);



    //    ////Add Name cell.
    //    //var cell = $(row.insertCell(-1));
    //    ////  cell.html(opt.value());
    //    //cell.html(txtName.val());

    //    ////Add Country cell.
    //    //cell = $(row.insertCell(-1));
    //    //cell.html(opt.text/*txtCountry.val()*/);


    //    ////Add Button cell.
    //    //cell = $(row.insertCell(-1));
    //    //var btnRemove = $("<input  />");
    //    //btnRemove.attr("type", "button");
    //    //btnRemove.attr("id", "btnRemove");
    //    //btnRemove.attr("class", "btn btn-default");
    //    //btnRemove.attr("onclick", "Remove(this);");
    //    //btnRemove.val("Remove");
    //    //cell.append(btnRemove);

    //    // Send the JSON array to Controller using AJAX.
    //    $.ajax({
    //        type: "POST",
    //        url: "/Orders/AddProduct",
    //        data: JSON.stringify(txtName.val()),
    //        dataType: 'json',
    //        contentType: 'application/json',
    //        success: function (r) {
    //            alert(r + " record(s) inserted.");
    //        }
    //    });


    //});

    var boton = document.getElementById('btnRemove');
    boton.onclick = function () {
        Remove();
    };


    function Remove() {


        var customers = new Array();
        var code = "peep";
        $("#tblCustomers TBODY TR").each(function () {
            var row = $(this);
            var customer = {};
            customer.ProductId = row.find("TD").eq(0).html();
            customer.Description = row.find("TD").eq(1).html();
            customers.push(customer);
            code = row.find("TD").eq(0).html();
        });

        // Send the JSON array to Controller using AJAX.
        $.ajax({
            type: "POST",
            url: "/Orders/RemoveProduct",
            data: JSON.stringify(customers),
            dataType: 'json',
            contentType: 'application/json',
            success: function (r) {
                alert(r + " record(s) inserted.");
            }
        });


    };




    //$("#tbListOfCountries").on('focusout', function () {

    //    $.ajax({
    //        url: "/Orders/ShowProducts",
    //        type: "GET",
    //        dataType: "json",
    //        data: { fetch: $("#tbListOfCountries").val() },
    //        success: function (query) {
    //            $("#tbListOfCountries").val(query[0]);
    //        },
    //    });



});


