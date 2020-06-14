import {
    Relogio
} from "./relogio.js";

function Carregar() {
    const img = document.getElementById('img');
    const texto = document.getElementById('box-mensagem');

    const data = new Date();
    const hora = data.getHours();

    if (hora >= 0 && hora < 12) {
        // Bom Dia!
        img.src = '../img-SecondPage/manha.jpg';
        document.body.style.backgroundColor = '#fffcf7';
        texto.style.color = '#aa9c7e';
        texto.innerText = 'Bom Dia!';
    } else if (hora >= 12 && hora < 18) {
        // Boa Tarde!
        img.src = '../img-SecondPage/tarde.jpg';
        document.body.style.backgroundColor = '#b9846f';
        texto.style.color = '#b9846f';
        texto.innerText = 'Boa Tarde!';
    } else {
        //Boa Noite!
        img.src = '../img-SecondPage/noite.jpg';
        document.body.style.backgroundColor = '#515154';
        texto.style.color = '#515154';
        texto.innerText = 'Boa Noite!';
    }
    Relogio();

    setTimeout(Carregar, 1000);
}

Carregar();