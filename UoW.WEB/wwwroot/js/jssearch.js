
const searchWrapper = document.querySelector(".search-input");
const inputBox = searchWrapper.querySelector("input");
const suggBox = searchWrapper.querySelector(".autocom-box");
const icon = searchWrapper.querySelector(".icon");
let linkTag = searchWrapper.querySelector("a");
let webLink;
inputBox.focus();
let suggestions = data;
inputBox.onkeyup = (e) => {
    let productData = e.target.value; 
    let emptyArray = [];
    if (productData) {
        //icon.onclick = () => {
        //    webLink = "/BayiUrun/DetailProductSearch/?productData=" + productData;
        //    linkTag.setAttribute("href", webLink);
        //    console.log(webLink);
        //    linkTag.click();
        //}
        emptyArray = suggestions.filter((data) => {
            return data.UrunAdi.toLocaleLowerCase().includes(productData.toLocaleLowerCase());
        });
        emptyArray = emptyArray.map((data) => {
            return data = '<li id="'+ data.ID +'">' + data.UrunAdi + '</li>';
        });
        searchWrapper.classList.add("active");
        showSuggestions(emptyArray);
        let allList = suggBox.querySelectorAll("li");
        for (let i = 0; i < allList.length; i++) {
            allList[i].setAttribute("onclick", "select(this)");
        }
    } else {
        searchWrapper.classList.remove("active"); 
    }
}

function select(element) {
    let selectData = element.textContent;
    let productId = element.id;
    inputBox.value = selectData;
    inputBox.onkeydown = (e) => {
        if (e.keyCode == "13") {
            webLink = "/BayiUrun/DetailProductSearch/?productId=" + productId;
            linkTag.setAttribute("href", webLink);
            linkTag.click();
        }
    }
    icon.onclick = () => {
        webLink = "/BayiUrun/DetailProductSearch/?productId=" + productId;
        linkTag.setAttribute("href", webLink);
        linkTag.click();
    }
    searchWrapper.classList.remove("active");
    inputBox.focus();
}

function showSuggestions(list) {
    let listData;
    if (!list.length) {
        userValue = inputBox.value;
        listData = '<li>' + userValue + '</li>';
    } else {
        listData = list.join('');
    }
    suggBox.innerHTML = listData;
}