
// Pour faire disparaitre le champ date si un enseignant veut s'inscrire
function blocDate_hide() {

    var element = document.getElementById("blocDate");
    element.style.display = "none";
    var valueDate = document.getElementById("DateNaissanceForm");
    valueDate.value="2000-01-01";
}

// Pour faire apparaitre le champ date si un élève veut s'inscrire
function blocDate_show() {

    var element = document.getElementById("blocDate");
    element.style.display = "block";
    element.removeAttribute("style");
    var valueDate = document.getElementById("DateNaissanceForm");
    valueDate.value="";
}