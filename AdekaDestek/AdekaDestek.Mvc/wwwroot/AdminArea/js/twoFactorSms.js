$(document).ready(function () {

    $(function () {
        const urlSms = '/Admin/User/TwoFactorWithSms/';
        const urlAuthenticator = '/Admin/User/TwoFactorWithAuthenticator/';
        const placeHolderDiv = $('#modalPlaceHolder');
        var optionValue = $("#TwoFactorTypeSelect").val();
        if (optionValue === "1") {
            $("#btn-save").hide();
            $("#btn-saveSms").show();
        }
        else if (optionValue === "2") {
            $("#btn-saveSms").hide();
            $("#btn-save").show();
        }
        $("#TwoFactorTypeSelect").on('change', function () {

            switch ($(this).val()) {
                case "1":
                    $("#btn-save").hide();
                    $("#btn-saveSms").show();
                    break;
                case "2":
                    $("#btn-saveSms").hide();
                    $("#btn-save").show();
                    break;
                default:
            }
        });



        $("#btn-saveSms").click(function () {

            $.get(urlSms).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
                $("#timer").hide();
                $("#btnSave").hide();
            });


        });


        //Verification Code send SMS
        $(document).on('click',
            '#btnSendCode',
            function (event) {
                event.preventDefault();

                $("#timer").show();
                $("#btnSave").show();
                $("#btnSendCode").hide();

                var timeLeft = parseInt(120);

                function makeTimer() {
                    var minutes = Math.floor(timeLeft / 60);
                    var seconds = Math.floor(timeLeft - (minutes * 60));

                    $("#minutes").html(minutes);
                    $("#seconds").html(seconds);

                    if (minutes < 10) { minutes = "0" + minutes };
                    if (seconds < 10) { seconds = "0" + seconds };

                    timeLeft--
                    console.log(timeLeft);

                    if (timeLeft == 0) {
                        window.location.href = "/Admin/Auth/Logout";

                    }

                }

                makeTimer();
                setInterval(() => makeTimer(), 1000);
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    url: '/Admin/User/TwoFactorWithSmsVerification/',
                    success: function (data) {
                        const userDto = jQuery.parseJSON(data);
                        if (userDto.ResultStatus === 0) {
                            toastr.success("Doğrulama kodu cep telefonunuza gönderilmiştir.");
                        } else {

                        }
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(`${err.responseText}`, "Hata!");
                    }
                });


            });

        //In case the verification code is incorrect
        function sendCodePage() {
            $.get(urlSms).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
                $("#btnSendCode").hide();
            });
        }

        //Verification Code save.
        placeHolderDiv.on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-user-sms');
                const actionUrl = form.attr('action');
                const dataToSend = new FormData(form.get(0));
                $.ajax({
                    url: actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        const confirmTwoFactorSmsModel = jQuery.parseJSON(data);
                        console.log(confirmTwoFactorSmsModel);
                        const oldFormBody = $('.model-body').html();
                        const newFormBody = $('.modal-body', confirmTwoFactorSmsModel.TwoFactorSmsConfirmPartial);
                        placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                        const isValid = form.find('[name="IsValid"]').val() === 'True';
                        if (confirmTwoFactorSmsModel.twoFactorSmsDto.ResultStatus === 1) {
                            toastr.error("Doğrulama kodunu hatalı girdiniz. Lütfen tekrar deneyin.", 'Hatalı İşlem!');

                            sendCodePage();

                            //setTimeout(function () {
                            //    window.location.assign("/Admin/User/TwoFactorAuth");
                            //    //1 saniye sonra yönlenecek
                            //}, 2000);
                        }
                        else if (confirmTwoFactorSmsModel.twoFactorSmsDto.ResultStatus === 0) {

                            Swal.fire(
                                'Başarılı İşlem!',
                                "İki adımlı kimlik doğrulama tipiniz 'Cep Telefonu' olarak belirlenmiştir. Giriş sayfasına yönlendiriliyorsunuz...",
                                'success'
                            );
                            setTimeout(function () {
                                window.location.assign("/Admin/Auth/Logout");
                                //after 4 seconds.
                            }, 4000);
                        }
                    },


                });


            });

        //Close modal-form
        placeHolderDiv.on('click',
            '#closeWindow',
            function (event) {
                event.preventDefault();
                window.location.assign("/Admin/User/TwoFactorAuth");


            });



        //Close modal-from anohter type
        placeHolderDiv.on('click',
            '#btnCancel',
            function (event) {
                event.preventDefault();
                window.location.assign("/Admin/User/TwoFactorAuth");


            });

    });


});
