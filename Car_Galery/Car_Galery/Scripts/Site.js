$(function() {
    var Accordion = function(el, multiple) {
        this.el = el || {};
        this.multiple = multiple || false;

        // Variables privadas
        var links = this.el.find('.link');
        // Evento
        links.on('click', {el: this.el, multiple: this.multiple}, this.dropdown)
    }

    Accordion.prototype.dropdown = function(e) {
        var $el = e.data.el;
        $this = $(this),
            $next = $this.next();

        $next.slideToggle();
        $this.parent().toggleClass('open');

        if (!e.data.multiple) {
            $el.find('.submenu').not($next).slideUp().parent().removeClass('open');
        };
    }

    var accordion = new Accordion($('#accordion'), false);
});


function FillBrands() {
    //Form post
    var TypeId = $('#TypeId').val();

    if (TypeId == 0) {
        $("#BrandId").attr("disabled", "disabled");
    } else {
        $("#BrandId").removeAttr("disabled");
    }

    $("#ModelId").attr("disabled", "disabled");

    $.ajax({
        url:'/Inventory/FillBrands',
        type:'GET',
        datatype:"JSON",
        data: {TypeId: TypeId},
        success: function(response) {
            $("#ModelId").html("");
            $("#ModelId").append(
                $('<option></option>').val(0).html("--Select Model--"));
            $("#BrandId").html("");
            $("#BrandId").append(
                $('<option></option>').val(0).html("--Select Brand--"));
            console.log(response);
            $.each(response,
                function(index, value) {
                    $("#BrandId").append(
                        $('<option></option>').val(value.Id).html(value.Name));
                });
        }
    });
}

function FillModels() {
    //Form post
    var BrandId = $('#BrandId').val();

    if (TypeId == 0) {
        $("#ModelId").attr("disabled", "disabled");
    } else {
        $("#ModelId").removeAttr("disabled");
    }

    $.ajax({
        url:'/Inventory/FillModels',
        type:'GET',
        datatype:"JSON",
        data: {BrandId: BrandId},
        success: function(response) {
            $("#ModelId").html("");
            $("#ModelId").append(
                $('<option></option>').val(0).html("--Select Model--"));
            console.log(response);
            $.each(response,
                function(index, value) {
                    $("#ModelId").append(
                        $('<option></option>').val(value.Id).html(value.Name));
                });
        }
    });
}

function GetModal(lnk) {
    var VehicleId = lnk.getAttribute("value");

    $.ajax({
        url:'/Inventory/GetVehicleModal',
        type:'POST',
        datatype:"JSON",
        data: {id: VehicleId},
        success: function(data) {
            $('#VehicleModal').html(data);
        }

    });
}







