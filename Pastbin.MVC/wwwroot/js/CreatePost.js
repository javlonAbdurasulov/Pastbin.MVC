// Получаем элементы
var modal = document.getElementById("myModal");
var btn = document.getElementById("openModal");
var span = document.getElementsByClassName("close")[0];

// Открываем модальное окно при нажатии на ссылку
btn.onclick = function () {
    modal.style.display = "block";
}

// Закрываем модальное окно при нажатии на <span> (x)
span.onclick = function () {
    modal.style.display = "none";
}

// Закрываем модальное окно при клике вне окна
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

// Обработка отправки формы
//document.getElementById("dataForm").onsubmit = function (event) {
//    event.preventDefault();
//    alert("Форма отправлена!");
//    modal.style.display = "none";
//}