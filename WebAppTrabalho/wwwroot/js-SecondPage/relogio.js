export function Relogio() {
    const horaElement = document.getElementById('horas');
    const minutoElement = document.getElementById('minutos');
    const segundoElement = document.getElementById('segundos');

    const data = new Date();
    const hora = data.getHours();
    const minuto = data.getMinutes();
    const segundo = data.getSeconds();

    if (hora < 10) {
        hora = "0" + hora;
    } else if (minuto < 10) {
        minuto = "0" + minuto;
    } else if (segundo < 10) {
        segundo = "0" + segundo;
    }

    horaElement.innerHTML = hora;
    minutoElement.innerHTML = minuto;
    segundoElement.innerHTML = segundo;

    setTimeout(Relogio, 1000);
}