'use strict';

let nextIndex = 1;

function updateItemCount() {
    let form = document.getElementById("todoList");
    let countValidator = document.getElementById("itemCountValidator");
    let countDisplay = document.getElementById("itemCountDisplay");

    let items = form.querySelectorAll(".itemContainer");
    countValidator.value = items.length;
    countDisplay.textContent = `(${items.length})`;
}

function addItem(itemContainer) {
    let template = document.getElementById("todo-item-template");

    let newItem = template.content.querySelector(".itemContainer").cloneNode(true);
    let idxName = "idx" + nextIndex;
    newItem.querySelector(".itemIdx").setAttribute("value", idxName);
    newItem.querySelector(".itemDesc").setAttribute("name", `items[${idxName}].description` );
    setOnClick(newItem);

    nextIndex++;
    itemContainer.after(newItem);

    updateItemCount();
}

function removeItem(itemContainer) {
    itemContainer.remove();

    updateItemCount();
}

function addItemEventHandler(event) {
    let itemContainer = event.target.closest(".itemContainer");

    if (null !== itemContainer) {
        addItem(itemContainer);
    }
}

function removeItemEventHandler(event) {
    let itemContainer = event.target.closest(".itemContainer");
    if (null !== itemContainer) {
        removeItem(itemContainer);
    }
}

function setOnClick(listItemElementContainer) {
    let add = Array.from(listItemElementContainer.querySelectorAll(".addItemButton"));
    let remove = Array.from(listItemElementContainer.querySelectorAll(".removeItemButton"));

    add.forEach(a => a.addEventListener("click", addItemEventHandler));
    remove.forEach(r => r.addEventListener("click", removeItemEventHandler));
}

function setupList() {
    let form = document.getElementById("todoList");
    setOnClick(form);

    updateItemCount();
    let count = document.getElementById("itemCountValidator");
    nextIndex = count.value;

    form.addEventListener(
        "submit",
        function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }

            form.classList.add("was-validated");
        },
        false
    );
}

if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", event => setupList());
}
else {
    setupList();
}
