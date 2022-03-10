// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelector("#copynotification").addEventListener("animationend", e => {
    e.target.classList.remove("fade");
})

function copyLink(event){
    event.preventDefault();
    navigator.clipboard.writeText(event.target.innerHTML.trim());
    document.querySelector("#copynotification").classList.remove("fade");
    setTimeout(() =>
        document.querySelector("#copynotification").classList.add("fade"),
        1
    );
}

function makeShortLink(event){

    let url = document.querySelector("#urlfield").value;

    fetch(`/ShortLink?url=${encodeURIComponent(url)}`, {
        method: 'POST'
    })
    .then(data => data.json())
    .then(data => {
        let location = window.location;
        
        document.querySelector("#outputbody").innerHTML = `${location.protocol}//${location.host}/${data.path}`;
        document.querySelector("#outputbody").setAttribute("href", `${location.protocol}//${location.host}/${data.path}`);
    })
}