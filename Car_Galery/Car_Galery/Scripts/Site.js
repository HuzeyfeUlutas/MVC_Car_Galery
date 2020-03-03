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


function FillBrands(lnk) {
    //Form post
    var TypeId = $('#TypeId'+lnk).val();

    if (TypeId == 0) {
        $("#BrandId"+lnk).attr("disabled", "disabled");
    } else {
        $("#BrandId"+lnk).removeAttr("disabled");
    }

    $("#ModelId"+lnk).attr("disabled", "disabled");

    $.ajax({
        url:'/Inventory/FillBrands',
        type:'GET',
        datatype:"JSON",
        data: {TypeId: TypeId},
        success: function(response) {
            $("#ModelId"+lnk).html("");
            $("#ModelId"+lnk).append(
                $('<option></option>').val(0).html("--Select Model--"));
            $("#BrandId"+lnk).html("");
            $("#BrandId"+lnk).append(
                $('<option></option>').val(0).html("--Select Brand--"));
            $.each(response,
                function(index, value) {
                    $("#BrandId"+lnk).append(
                        $('<option></option>').val(value.Id).html(value.Name));
                });
        }
    });
}


function FillModels(lnk) {
    //Form post
    var BrandId = $('#BrandId'+lnk).val();

    if (TypeId == 0) {
        $("#ModelId"+lnk).attr("disabled", "disabled");
    } else {
        $("#ModelId"+lnk).removeAttr("disabled");
    }

    $.ajax({
        url:'/Inventory/FillModels',
        type:'GET',
        datatype:"JSON",
        data: {BrandId: BrandId},
        success: function(response) {
            $("#ModelId"+lnk).html("");
            $("#ModelId"+lnk).append(
                $('<option></option>').val(0).html("--Select Model--"));
            $.each(response,
                function(index, value) {
                    $("#ModelId"+lnk).append(
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

function InfoRequest(lnk) {
    var VehicleId = lnk.getAttribute("value");
    var RequestDateTime = lnk.getAttribute("value1");

    $.ajax({
        url:'/User/GetVehicleModal',
        type:'POST',
        datatype:"JSON",
        data: {id: VehicleId, dt: RequestDateTime},
        success: function(data) {
            $('#RequestVehicleModal').html(data);
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

function EditVehicle(lnk) {
    var Id = lnk.getAttribute("value");

    $.ajax({
        url: '/Inventory/VehicleEditModal',
        type: 'GET',
        datatype: "JSON",
        data: { id: Id },
        success: function(data) {
            $('#card-type-body-Vehicle').html(data);
            $("#DetailVehicle").removeClass('active');
            $("#EditVehicle").addClass('active');
        }
    });
}

function DetailVehicle(lnk) {
    var Id = lnk.getAttribute("value");

    $.ajax({
        url: '/Inventory/VehicleDetailModal',
        type: 'GET',
        datatype: "JSON",
        data: { id: Id, "__RequestVerificationToken": $('input[name=__RequestVerificationToken]').val()},
        success: function(data) {
            $('#card-type-body-Vehicle').html(data);
            
            $("#EditVehicle").removeClass('active');
            $("#DetailVehicle").addClass('active');
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
            data: { id: Id, "__RequestVerificationToken": $('input[name=__RequestVerificationToken]').val() },
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

function DeleteVehicle(lnk) {
    var Id = lnk.getAttribute("value");

    if (confirm("Are you sure?")) {
        $.ajax({
            url: '/Inventory/DeleteVehicle',
            type: 'POST',
            data: { id: Id },
            success: function (data) {
                $('#VehicleList').html(data);
                OnSuccess("Delete Vehicle");
            },
            fail: function(data) {
                OnFail("Delete Vehicle");
            } 
        });
    }
}

function DeleteUser(lnk) {
    var Id = lnk.getAttribute("value");

    if (confirm("Are you sure?")) {
        $.ajax({
            url: '/User/DeleteUser',
            type: 'POST',
            data: { id: Id },
            success: function (data) {
                $('#UserList').html(data);
                OnSuccess("Delete User");
            },
            fail: function(data) {
                OnFail("Delete User");
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

$('#VehicleModal').on('hidden.bs.modal',
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


function GetAddVehicle() {
    $.ajax({
        url: '/Inventory/VehicleAddModal',
        type: 'GET',
        data: null,
        success: function(data) {
            $('#VehicleAddModal').html(data);
            
        }
    });
}



