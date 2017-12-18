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