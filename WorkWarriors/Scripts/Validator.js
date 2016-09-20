﻿function ShowAddress() {
    $("#vac").hide();
    $("#valid").hide();
    $("#inactive").hide();
    $("#vacant").prop("checked", false);
    $("#validated").prop("checked", false);
    $("#inactive").prop("checked", false);
    event.preventDefault();
    $("#divProcessing").show();
    $.ajax({
        type: "GET",
        url: "http://localhost:14703/AddressValidator/getAddValStauts",
        contentType: "application/json; charset=utf-8",
        data: { street: '' + $('#Address').val() + '', City: '' + $('#locality').val() + '', state: '' + $('#administrative_area_level_1').val() + '', zip: '' + $('#postal_code').val() + '"' },
        dataType: "json",
        success: function (response, textStatus, jqXHR) {
            $("#divProcessing").hide();
            if (response.validated == true) {
                $("#valid").show();
                $("#validated").prop("checked", true);
                var titleMsg = "Your address has been validated!"
                var div = $('<div></div>');
                var outputMsg = "Are you ready to submit your service request?";
                div.html(outputMsg).dialog({
                    title: titleMsg,
                    height: 300,
                    width: 600,
                    autoOpen: true,
                    resizable: true,
                    modal: true,
                    buttons: {
                        "YES":
                        function () {


                            $(this).dialog('close');
                            $("#requestForm").submit();
                            if ($("#requestForm").valid()) {
                                $("#divProcessing").show();
                            }


                        },
                        "NO":
                        function () {
                            $(this).dialog("close");
                        }
                    }
                });
            }
            else {
                var outputMsg = "Do any of the following apply? <br> <br> 1) This property is a vacant house. <br><br> 2) This property is a vacant lot. <br><br> 3) This property does not receive door mail delivery, i.e., PO Box service only."
                var div = $('<div></div>');
                var titleMsg = "A few questions while we validate your address";
                div.html(outputMsg).dialog({
                    title: titleMsg,
                    height: 300,
                    width: 600,
                    resizable: true,
                    modal: true,
                    buttons: {
                        "YES": function () {
                            $("#divProcessing").show();
                            $("#vac").show();
                            $("#vacant").prop("checked", true);
                            $(this).dialog("close");
                            $.ajax({
                                type: "GET",
                                url: "http://localhost:14703/AddressValidator/RunStreetLevelValidation",
                                contentType: "application/json; charset=utf-8",
                                data: { street: '' + $('#Address').val() + '', City: '' + $('#locality').val() + '', state: '' + $('#administrative_area_level_1').val() + '', zip: '' + $('#postal_code').val() + '' },
                                dataType: "json",
                                success: function (response, textStatus, jqXHR) {
                                    $("#divProcessing").hide();
                                    if (response.validated == true) {
                                        var listOfAddresses = [];
                                        for (var i = 0; i < response.results.length; i++) {
                                            listOfAddresses.push([i + 1] + ")" + " " + response.results[i].AddressLine1 + "<br><br>")
                                        };
                                        var titleMsg = "Please confirm";
                                        var div = $('<div></div>');
                                        var outputMsg = "Is this address in the vicinity of one of the following address ranges?" + "<br><br>" + (listOfAddresses.join('\r\n'));
                                        div.html(outputMsg).dialog({
                                            title: titleMsg,
                                            height: 300,
                                            width: 600,
                                            autoOpen: true,
                                            resizable: true,
                                            modal: true,
                                            buttons: {
                                                "YES":
                                                function () {


                                                    $(this).dialog('close');
                                                    $("#validated").prop("checked", true);
                                                    var titleMsg = "Your address has been validated!"
                                                    var div = $('<div></div>');
                                                    var outputMsg = "Are you ready to submit your service request?";
                                                    div.html(outputMsg).dialog({
                                                        title: titleMsg,
                                                        height: 300,
                                                        width: 600,
                                                        autoOpen: true,
                                                        resizable: true,
                                                        modal: true,
                                                        buttons: {
                                                            "YES":
                                                            function () {


                                                                $(this).dialog('close');
                                                                $("#requestForm").submit();
                                                                if ($("#requestForm").valid()) {
                                                                    $("#divProcessing").show();
                                                                }


                                                            },
                                                            "NO":
                                                            function () {
                                                                $(this).dialog("close");
                                                            }
                                                        }
                                                    });

                                                },
                                                "NO":
                                                function () {
                                                    $(this).dialog("close");
                                                    var titleMsg = "We're sorry, but we were unable to validate your address.";
                                                    var div = $('<div></div>');
                                                    var outputMsg = "Would you like to request manual address validation?";
                                                    div.html(outputMsg).dialog({
                                                        title: titleMsg,
                                                        height: 300,
                                                        width: 600,
                                                        autoOpen: true,
                                                        resizable: true,
                                                        modal: true,
                                                        buttons: {
                                                            "YES":
                                                            function () {
                                                                $(this).dialog('close');
                                                                $("#inactive").show();
                                                                $("#inactive").prop("checked", true);
                                                                $.ajax({
                                                                    type: "GET",
                                                                    url: "http://localhost:14703/AddressValidator/ManualValidation",
                                                                    contentType: "application/json; charset=utf-8",
                                                                    data: { street: '' + $('#Address').val() + '', City: '' + $('#locality').val() + '', state: '' + $('#administrative_area_level_1').val() + '', zip: '' + $('#postal_code').val() + '', vacant: '' + $('#vacant').prop("checked") + '', validated: '' + $('#validated').prop("checked") + '' },
                                                                    dataType: "json",
                                                                    success: function (response, textStatus, jqXHR) {
                                                                        alert("Your request has been submitted,  We will get back to you shortly.");
                                                                    },
                                                                    error: function (jqXHR, textStatus, errorThrown) {
                                                                        alert('Error - ' + errorThrown);
                                                                    },
                                                                })
                                                                $("#requestForm").submit();
                                                                if ($("#requestForm").valid()) {
                                                                    $("#divProcessing").show();
                                                                }
                                                            },
                                                            "NO":
                                                            function () {
                                                                $(this).dialog("close");
                                                            }
                                                        }
                                                    });

                                                }
                                            }
                                        });
                                    }
                                    else {
                                        var titleMsg = "We're sorry, but we were unable to validate your address.";
                                        var div = $('<div></div>');
                                        var outputMsg = "Would you like to request manual address validation?";
                                        div.html(outputMsg).dialog({
                                            title: titleMsg,
                                            height: 300,
                                            width: 600,
                                            autoOpen: true,
                                            resizable: true,
                                            modal: true,
                                            buttons: {
                                                "YES":
                                                function () {
                                                    $(this).dialog('close');
                                                    $("#inactive").show();
                                                    $("#inactive").prop("checked", true);
                                                    $.ajax({
                                                        type: "GET",
                                                        url: "http://localhost:14703/AddressValidator/ManualValidation",
                                                        contentType: "application/json; charset=utf-8",
                                                        data: { street: '' + $('#Address').val() + '', City: '' + $('#locality').val() + '', state: '' + $('#administrative_area_level_1').val() + '', zip: '' + $('#postal_code').val() + '', vacant: '' + $('#vacant').prop("checked") + '', validated: '' + $('#validated').prop("checked") + '' },
                                                        dataType: "json",
                                                        success: function (response, textStatus, jqXHR) {
                                                            alert("Your request has been submitted,  We will get back to you shortly.");
                                                        },
                                                        error: function (jqXHR, textStatus, errorThrown) {
                                                            alert('Error - ' + errorThrown);
                                                        },
                                                    })
                                                    $("#requestForm").submit();
                                                    if ($("#requestForm").valid()) {
                                                        $("#divProcessing").show();
                                                    }
                                                },
                                                "NO":
                                                function () {
                                                    $(this).dialog("close");
                                                }
                                            }
                                        });

                                    }
                                },
                                failure: function (jqXHR, textStatus, errorThrown) {
                                    alert('Error - ' + errorThrown);
                                }
                            });
                        },
                        "NO": function () {
                            $(this).dialog("close");
                            var titleMsg = "We're sorry, but we were unable to validate your address.";
                            var div = $('<div></div>');
                            var listOfErrors = [];
                            for (var i = 0; i < response.errors.length; i++) {
                                listOfErrors.push([i + 1] + ")" + " " + response.errors[i].message + "<br><br>")

                            }
                            var outputMsg = "Address validation failed for the following reason(s):" + "<br><br>" + (listOfErrors.join('\r\n')) + "<br><br>" + "Would you like to request manual validation?";
                            div.html(outputMsg).dialog({
                                title: titleMsg,
                                height: 300,
                                width: 600,
                                autoOpen: true,
                                resizable: true,
                                modal: true,
                                buttons: {
                                    "YES":
                                    function () {
                                        $(this).dialog('close');
                                        $("#inactive").show();
                                        $("#inactive").prop("checked", true);
                                        $.ajax({
                                            type: "GET",
                                            url: "http://localhost:14703/AddressValidator/ManualValidation",
                                            contentType: "application/json; charset=utf-8",
                                            data: { street: '' + $('#Address').val() + '', City: '' + $('#locality').val() + '', state: '' + $('#administrative_area_level_1').val() + '', zip: '' + $('#postal_code').val() + '', vacant: '' + $('#vacant').prop("checked") + '', validated: '' + $('#validated').prop("checked") + '' },
                                            dataType: "json",
                                            success: function (response, textStatus, jqXHR) {
                                                alert("Your request has been submitted,  We will get back to you shortly.");
                                            },
                                            error: function (jqXHR, textStatus, errorThrown) {
                                                alert('Error - ' + errorThrown);
                                            },
                                        })
                                        $("#requestForm").submit();
                                        if ($("#requestForm").valid()) {
                                            $("#divProcessing").show();
                                        }
                                    },
                                    "NO":
                                    function () {
                                        $(this).dialog("close");
                                    }
                                }
                            });
                        },


                    }

                });

            }
        },
        //    failure: function(jqXHR, textStatus, errorThrown) {
        //        alert('Error - ' + errorThrown);
        //}
    });
}
function OnSuccess(response) {
    alert(response.d);
}