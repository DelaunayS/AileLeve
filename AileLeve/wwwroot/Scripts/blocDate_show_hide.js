
// Pour faire disparaitre le champ date et le champ représentant légal si un enseignant veut s'inscrire 
function blocDate_hide() {

    var element = document.getElementById("blocDate");
    element.style.display = "none";
    var valueDate = document.getElementById("DateNaissanceForm");
    valueDate.value = "2000-01-01";
    var element = document.getElementById("blocRLPrenom");
    element.style.display = "none";
    var element = document.getElementById("blocRLNom");
    element.style.display = "none";
}

// Pour faire apparaitre le champ date et le champ représentant légal si un élève veut s'inscrire
function blocDate_show() {

    var element = document.getElementById("blocDate");
    element.style.display = "block";
    element.removeAttribute("style");
    var valueDate = document.getElementById("DateNaissanceForm");
    valueDate.value = "";
    var element = document.getElementById("blocRLPrenom");
    element.style.display = "block";
    element.removeAttribute("style");
    var element = document.getElementById("blocRLNom");
    element.style.display = "block";
    element.removeAttribute("style");
}




   
   

