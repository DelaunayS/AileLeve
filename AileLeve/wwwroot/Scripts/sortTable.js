/* Permet de trier les colonnes d'un tableau
dans le sens croissant ou décroissant*/

function sortTable(table, n,iconeTri) {
  var rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
  switching = true;
  dir = "asc";
  


  while (switching) {

    switching = false;
    rows = table.rows;
    /* Boucle sur toutes les lignes du tableau (sauf la
    premiere, qui contient les en-têtes de tableau): */
    for (i = 1; i < (rows.length - 1); i++) {
      // Il ne peut pas y avoir de changement:
      shouldSwitch = false;
      /* Recupere la ligne actuelle et la ligne suivante: */
      x = rows[i].getElementsByTagName("TD")[n];
      y = rows[i + 1].getElementsByTagName("TD")[n];
      /* Vérifie si les deux rangées doivent changer de place,
      en fonction de la direction, asc ou desc : */
      if (dir == "asc") {
        if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
          shouldSwitch = true;
          break;
        }
      } else if (dir == "desc") {
        if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
          shouldSwitch = true;
          break;
        }
      }
    }
    if (shouldSwitch) {
      rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
      switching = true;

      switchcount++;
    } else {
      if (switchcount == 0 && dir == "asc") {
        dir = "desc";
        switching = true;

      }
    }
  }

  // Permet d'échanger les icônes ascendant et descendant
  if (dir == "asc") {
    iconeTri.classList.add("fa-sort-down");
    iconeTri.classList.remove("fa-sort-up");
  }
  if (dir == "desc") {
    iconeTri.classList.remove("fa-sort-down");
    iconeTri.classList.add("fa-sort-up");
  }
}