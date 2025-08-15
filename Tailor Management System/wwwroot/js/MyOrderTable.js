$(document).ready(function () {
    $('#MyOrdersTable').DataTable({
        ajax: {
            url: '/CustomerArea/Order/MyOrder', 
            type: 'GET',
            dataSrc: ''
        },
        columns: [
            { data: 'dressType' },
            { data: 'measurements' },
            { data: 'timeDuration' },
            {
                data: 'status',
                render: function (data, type, row) {
                    if (data === "Pending") {
                        return '<span class="badge bg-secondary p-2">Pending</span>';
                    } else if (data === "Rejected") {
                        return '<span class="badge bg-danger p-2">Rejected</span>';
                    } else if (data === "Confirmed" && !row.paymentDone) {
                        return `<a href="/CustomerArea/Order/Pay/${row.orderId}" class="btn btn-success btn-sm">
                          <i class="bi bi-credit-card"></i> Pay Now
                           </a>`;
                    }
                    else if (row.paymentDone) {
                        return `<span class="badge bg-primary p-2"><i class="bi bi-check-circle-fill"></i> Paid</span>`;
                    }
                    return data;
                }
            },
            {
                data: 'createdAt',
                render: function (data) {
                    const date = new Date(data);
                    return date.toISOString().split('T')[0];
                }
            },
            {
                data: 'orderId',
                render: function (data, type, row) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/CustomerArea/Order/Details/${data}" class="btn btn-info btn-sm m-1">
                                    <i class="bi bi-eye"></i> Details
                                </a>
                            </div>`;
                }
            }
        ],
        responsive: true,
        ordering: true,
        searching: true,
        paging: true
    });
});
