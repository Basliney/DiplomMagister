var user;

function GetUserClient() {
    if (getCookie('accessToken') == undefined) {
        return;
    }

    var request = new XMLHttpRequest();
    request.open('GET', '/Profile/getUserClient', true);
    request.onload = () => {
        if (request.status == 200 || request.status == 202) {
            var data = request.response;
            user = data;
        }
        else {

        }
    };
    request.send();
}

function GetProfilePhoto() {
    if (getCookie('accessToken') == undefined) {
        return;
    }

    var request = new XMLHttpRequest();
    request.open('GET', '/Profile/getphoto', true);
    request.onload = () => {
        if (request.status == 200 || request.status == 202) {
            var data = request.response;
            document.getElementById('profile-image').src = data;
            console.log('Ok');
        }
        else {
            console.log('Задайте изображение профиля');

        }
    };
    request.send();
}

function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}
