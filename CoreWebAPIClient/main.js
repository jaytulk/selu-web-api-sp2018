function setUp() {
  const testButton = document.getElementById("testButton");
  testButton.addEventListener("click", getItems);

  const itemForm = document.getElementById("itemForm");
  itemForm.addEventListener("submit", function(e) {
    e.preventDefault();
    return addItem();
  });

  getItems();
}

const toJSON = response => response.json();

const fetchJSON = (url, options = {}) =>
  fetch(url, {
    ...options
  }).then(toJSON);

const setItems = response => {
  const itemList = document.getElementById("itemList");
  itemList.innerHTML = "";
  response.forEach(x => {
    const item = document.createElement("li");
    item.innerText = x.name;
    itemList.appendChild(item);
  });
};

const showError = error => {
  console.log(error);
  const errorContainer = document.getElementById('errorContainer');
  errorContainer.style.visibility = 'visible';
  
  const errorField = document.getElementById("errorField");
  errorField.innerText = error.message;
};

function getItems() {
    const activeOnlyCheckbox = document.getElementById('activeOnlyCheckbox');
    console.log(activeOnlyCheckbox)
    
    let url = "http://localhost:50925/api/users";
    
    if(activeOnlyCheckbox.checked){
        url = url + '?activeOnly=true';
    }

  fetchJSON(url)
    .then(setItems)
    .catch(showError);
}

const clearErrors = () => {
    const errorContainer = document.getElementById('errorContainer');
    errorContainer.style.visibility = 'hidden';
    
  const errorField = document.getElementById("errorField");
  errorField.innerHTML = "";
};

function addItem() {
  clearErrors();

  const url = "http://localhost:50925/api/users";
  const itemInput = document.getElementById("itemInput");
  
  if (!itemInput.value) {
    showError({ message: "A name is required" });
    return;
  }

  fetch(url, {
    method: "POST",
    body: JSON.stringify({ name: itemInput.value }),
    headers: {
      "Content-Type": "application/json"
    }
  })
    .then((response = {}) => {
      getItems();
      itemInput.value = "";
    })
    .catch(showError);
}

setUp();
