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

function Edit(lnk) {
    var Id = lnk.getAttribute("value");
    var urlParam = lnk.getAttribute("value1");

    $.ajax({
        url: '/AdminOperation/Edit'+urlParam,
        type: 'GET',
        datatype: "JSON",
        data: { id: Id },
        success: function(data) {
            $('#card-type-body-'+urlParam).html(data);
        }
    });
}

function Delete(lnk) {
    var Id = lnk.getAttribute("value");
    var urlParam = lnk.getAttribute("value1");

    if (confirm("Are you sure?(this may cause other data connected to the "+urlParam+" to be lost)")) {
        $.ajax({
            url: '/AdminOperation/Delete' + urlParam,
            type: 'POST',
            data: { id: Id },
            success: function (data) {
                $('#card-type-body-'+urlParam).html(data);
                OnSuccess(urlParam+" Delete");
            },
            fail: function(data) {
                OnFail(urlParam+" Delete");
            } 
        });
    }
    

}



function OnSuccess(data) {
    $.notify(data + " is success","success");
}

function OnFail(data) {
    $.notify(data + " is fail", "error");
}

function AddSuccess(data) {
    $("#Add"+data).removeClass('active');
    $("#"+data+"List").addClass('active')
    OnSuccess(data+" Add");
}

function AddFail(data) {
    $("#Add"+data).removeClass('active');
    $("#"+data+"List").addClass('active')
    OnFail(data+" Add");
}

function List(lnk) {
    var urlParam = lnk.getAttribute("id");
    var res = urlParam.replace("List", "");
    $.ajax({
        url: '/AdminOperation/Get' + urlParam,
        type: 'GET',
        data: null,
        success: function(data) {
            $('#card-type-body-'+res).html(data);
            OnSuccess("Get "+urlParam);
            $("#Add"+res).removeClass('active');
            $("#"+res+"List").addClass('active');
        },
        fail: function(data) {
            OnFail("Get " + urlParam);
        } 
    });
}

function Add(lnk) {
    var urlParam = lnk.getAttribute("id");
    var res = urlParam.replace("Add", "");


    $.ajax({
        url: '/AdminOperation/Get' + urlParam,
        type: 'GET',
        data: null,
        success: function(data) {
            $('#card-type-body-'+res).html(data);
            OnSuccess("Get "+urlParam);
            $("#"+res+"List").removeClass('active');
            $("#Add"+res).addClass('active');
        },
        fail: function(data) {
            OnFail("Get " + urlParam);
        } 
    });
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
    
        reader.onload = function(e) {
            $('#imgPrv').attr('src', e.target.result);
        }
    
        reader.readAsDataURL(input.files[0]);
    }
}

$("#imgInp").change(function() {
    readURL(this);
});


$('#TypeModal').on('hidden.bs.modal',
    function() {
        document.location.reload();
    });


$('#BrandModal').on('hidden.bs.modal',
    function() {
        document.location.reload();
    });

$('#ModelModal').on('hidden.bs.modal',
    function() {
        document.location.reload();
    });






