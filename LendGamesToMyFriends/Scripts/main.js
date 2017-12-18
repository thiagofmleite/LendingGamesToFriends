function readImage() {
    // verifica se há um arquivo
    if (this.files && this.files[0]) {
        // invoca o fileReader
        var reader = new FileReader();
        // Joga a imagem na tela
        reader.addEventListener("load", function (e) {
            document.querySelector("#imgCover").src = e.target.result;
            document.querySelector("#Cover").value = e.target.result;
        });

        reader.readAsDataURL(this.files[0]);
    }

}

function returnGame(id) {
    $.ajax({
        type: 'GET',
        url: '../Lending/Return/' + id
    })
        .done(function (response, status, xhr) {
            swal({ title: 'Sucesso', text: 'O jogo foi devolvido com sucesso', type: 'success' }, function () {
                location.reload();
            });
        }).fail(function (response, xhd) {
            console.error(response);
            swal({ title: 'Erro', text: 'Não foi possível realizar a devolução', type: 'error' });
        });
}