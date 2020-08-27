$(document).ready(function () {
    var oTable = $('#Subscriptions_Table').DataTable({
        "sDom": "<t><'row'<p i>>",
        "destroy": true,
        "scrollCollapse": true,
        "oLanguage": {
            "sLengthMenu": "_MENU_ ",
            "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
        },
        "iDisplayLength": 5,
        columns: [
            { title: "Transaction ID", width: "25%" },
            { title: "Transaction Date", width: "25%" },
            { title: "Amount", width: "25%" },
            { title: "Payment Method", width: "25%" }
        ],
        initComplete: function () {
            $("#Subscriptions_Table_filter").hide();
            $("#Subscriptions_Table_length").hide();
        }
        //data: [],
        //filter: true,
        //lengthChange: false,
        //paging: true,
        //info: false,
        //autoWidth: false,
        //pageLength: 5,
        //aaSorting: [[1, 'desc']],
        //language: {
        //    paginate: {
        //        next: "<i class='fa fa-caret-right'></i>",
        //        previous: "<i class='fa fa-caret-left'></i>"
        //    },
        //},
    });
    var paymentsTable = $('#PaymentMethods_Table').DataTable({
        "sDom": "<t><'row'<p i>>",
        "destroy": true,
        "scrollCollapse": true,
        "oLanguage": {
            "sLengthMenu": "_MENU_ ",
            "sInfo": "Showing <b>_START_ to _END_</b> of _TOTAL_ entries"
        },
        "iDisplayLength": 5,
        //data: [],
        //filter: true,
        //lengthChange: false,
        //paging: true,
        //info: false,
        //autoWidth: false,
        //pageLength: 5,
        //aaSorting: [[1, 'desc']],
        //language: {
        //    paginate: {
        //        next: "<i class='fa fa-caret-right'></i>",
        //        previous: "<i class='fa fa-caret-left'></i>"
        //    },
        //},
        columns: [
            { title: "Name", width: "25%" },
            { title: "Card Number", width: "25%" },
            { title: "Expiration Date", width: "25%" },
            { title: "Actions", width: "25%" }
        ],
        initComplete: function () {
            $("#PaymentMethods_Table_filter").hide();
            $("#PaymentMethods_Table_length").hide();
        }
    });

    $('#subscriptionTableSearch').on('keyup', function () {
        oTable.search($(this).val()).draw();
    });
    $('#paymentMethodTableSearch').on('keyup', function () {
        paymentsTable.search($(this).val()).draw();
    });

    GetSubscriptions();
    GetPaymentMethods();


    function GetSubscriptions() {
        AddSubscriptions(model.subscriptions);
    }
    function AddSubscriptions(subscriptions) {
        for (var i = 0; i < subscriptions.length; i++) {
            var item = subscriptions[i];
            var transactionID = "";
            var status = "";
            var valid = "";
            if (item.transactionId != 0) {
                transactionID = item.transactionId;
            }
            else {
                transactionID = "<span class='label label-info'>None</span>";
            }
            var rowHtml = '<tr id=' + item.subscriptionId + '>' +
                '<td v-align-middle semi-bold>' + transactionID + '</td>' +
                '<td v-align-middle>' + item.transactionDate + '</td>' +
                '<td v-align-middle>$' + item.amount + '</td>' +
                '<td v-align-middle>' + transactionID + '</td>' +//change to payment method
                '</tr>';

            $('#Subscriptions_Table').DataTable().row.add($(rowHtml)).draw();
            $('[data-toggle="tooltip"]').tooltip();
        }
    }

    function GetPaymentMethods() {
        AddPaymentMethods(model.paymentMethods);
    }
    function AddPaymentMethods(paymentMethods) {
        for (var i = 0; i < paymentMethods.length; i++) {
            var item = paymentMethods[i];
            var Name = item.firstName + " " + item.lastName;
            var rowHtml = '<tr id=' + item.paymentMethodID + '>' +
                '<td v-align-middle semi-bold>' + Name + '</td>' +
                '<td v-align-middle>' + item.cardNumber + '</td>' +
                '<td v-align-middle>' + item.timestamp + '</td>' +
                '<td v-align-middle>' + "<button type='button' class='edit-Btns btn-default' edit-id='" + item.paymentMethodID + "' data-toggle='modal' data-target='#addBalanceModal' title='Add Balance'><span data-toggle='tooltip' data-placement='top' title='Add Balance'><i class='fa fa-plus'></i></span></button> <button type='button' class='del-Btns btn-default' del-id='" + item.paymentMethodID + "' data-toggle='tooltip' data-placement='top' title='Delete Payment Method'><i class='fa fa-trash'></i></button>" + '</td>' +//change to payment method
                '</tr>';

            $('#PaymentMethods_Table').DataTable().row.add($(rowHtml)).draw();
            $('[data-toggle="tooltip"]').tooltip();
        }
    }
});