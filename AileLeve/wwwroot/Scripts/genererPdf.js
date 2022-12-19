// !! nécessite que le script jsPdf soit chargé sur la page
window.jsPDF = window.jspdf.jsPDF;

//Genere le pdf en fonction de l'id de la div
function genererPdf() {

    //création d'un canvas
    html2canvas(document.querySelector("#main")).then(canvas => {
        document.body.appendChild(canvas);
        canvas.download = "filename.jpg";
        canvas.href = canvas.toDataURL();

        
        //creation du doc pdf
        var doc = new jsPDF("p","mm", "a4");
        var width = doc.internal.pageSize.getWidth();
        var height = doc.internal.pageSize.getHeight();
        //ajout du canvas au doc pdf
        doc.addImage(canvas.href, 'PNG', 0, 0, width, height);
        doc.save('admin.pdf');
        window.location.reload();
    });
}