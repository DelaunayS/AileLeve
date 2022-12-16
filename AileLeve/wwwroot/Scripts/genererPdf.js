// !! nécessite que le script jsPdf soit chargé sur la page
window.jsPDF = window.jspdf.jsPDF;

//Genere le pdf en fonction de l'id de la div
function genererPdf(id) {

    //création d'un canvas
    html2canvas(document.querySelector("#doc")).then(canvas => {
        document.body.appendChild(canvas);
        canvas.download = "filename.jpg";
        canvas.href = canvas.toDataURL();

        //creation du doc pdf
        var doc = new jsPDF();
        //ajout du canvas au doc pdf
        doc.addImage(canvas.href, 'PNG', 20, 20,175,100);        
        doc.save('test.pdf');
    });
}