function enableEdit() {
    document.getElementById("animalName").removeAttribute("readonly");
    document.getElementById("animalCategory").removeAttribute("readonly");
    document.getElementById("animalAge").removeAttribute("readonly");
    document.getElementById("animalDescription").removeAttribute("readonly");

    // החלפת כפתור ה-Edit בכפתור ה-Save
    document.getElementById("editButton").classList.add("d-none");
    document.getElementById("saveButton").classList.remove("d-none");
}