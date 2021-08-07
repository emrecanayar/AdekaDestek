
var uri = "@Model.AuthenticatorUri";
new QRCode(document.getElementById("qrcode"), {

    text: uri,
    width: 150,
    height: 150,
    correctLevel: QRCode.CorrectLevel.H
});
