var title = document.getElementById("title")
var categoryId = title.innerText;
var element = document.getElementById(categoryId);
element.className += " " + "active";

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

var avatar = getCookie("Avatar");

if (avatar != undefined) {
    if (localStorage.getItem("Avatar") === undefined || localStorage.getItem("Avatar") !== avatar)
        localStorage.setItem("Avatar", avatar);

    localAvatar = avatar;

    var avatarImage = document.getElementById("Avatar");
    avatarImage.src = avatar;
}