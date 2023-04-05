function change() {
    const checkbox = document.getElementById("check_khalid");

    if (checkbox.checked) {
        document.getElementById("prix").style.display = 'none';
    } else {
        document.getElementById("prix").style.display = 'block';
    }
}
