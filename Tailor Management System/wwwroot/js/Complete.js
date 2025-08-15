$(document).ready(function () {
    $('#CompletedPaymentsTable').DataTable({
        ajax: {
            url: '/CustomerArea/Order/GetCompletedPayments',
            type: 'GET',
            dataSrc: 'data'
        },
        columns: [
            { data: 'paymentId', render: function (data) { return 'INV' + new Date().getFullYear() + data; } },
            { data: 'userName' },
            { data: 'phoneNumber' },
            { data: 'amount' },
            { data: 'dressType' },
            { data: 'orderStatus' },
            {
                data: 'createdAt',
                render: function (data) {
                    const date = new Date(data);
                    return date.toISOString().split('T')[0];
                }
            },
            {
                data: 'paymentId',
                render: function (data, type, row) {
                    return `<a href="/CustomerArea/Order/Details/${data}" class="btn btn-primary btn-sm">
                                        <i class="bi bi-file-earmark-text"></i> Print Invoice
                                    </a>`;
                }
            }
        ],
        responsive: true,
        ordering: true,
        searching: true,
        paging: true
    });
});