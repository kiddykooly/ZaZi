function bidvalidate() {
    $('#bid-form').validate({

        rules: {
            Name: {
                required: true
            },
            Email: {
                required: true,
                email: true,
            },
            BidPrice: {
                required: true
            }
        },

        messages: {
            Name: {
                required: "Tên không được để trống.",
            },
            Email: {
                required: "Email không được để trống.",
                email: "Email không hợp lệ."
            },
            BidPrice: {
                required: "Giá đấu không được để trống."
            }
        },

        invalidHandler: function (event, validator) { //display error alert on form submit   

        },

        highlight: function (element) { // hightlight error inputs
            $(element)
                .closest('.input-field span').addClass('active');
        },

        success: function (label) {
            label.closest('.input-field span').removeClass('active');
            label.remove();
        },

        errorPlacement: function (error, element) {
            $(element)
                .closest("form")
                .find("span[data-for='" + element.attr("id") + "']")
                .html(error.text())
        },

        submitHandler: function (form) {
            var frm = $("#bid-form");
            $.ajax({
                url: frm.attr('action'),
                type: frm.attr('method'),
                data: frm.serialize(),
                dataType: 'json',
                success: function (data) {
                    window.location.reload();
                },
                complete: function () { }
            });
            event.preventDefault();
        }
    });
}