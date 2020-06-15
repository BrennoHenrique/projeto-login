export function Relogio() {
    const horaElement = document.getElementById('horas');
    const minutoElement = document.getElementById('minutos');
    const segundoElement = document.getElementById('segundos');

    const data = new Date();
    let hora = data.getHours();
    let minuto = data.getMinutes();
    let segundo = data.getSeconds();

    if (hora < 10) {
        hora = "0" + hora;
    }

    if (minuto < 10) {
        minuto = "0" + minuto;
    }

    if (segundo < 10) {
        segundo = "0" + segundo;
    }

    horaElement.innerHTML = hora;
    minutoElement.innerHTML = minuto;
    segundoElement.innerHTML = segundo;

    setTimeout(Relogio, 1000);
}