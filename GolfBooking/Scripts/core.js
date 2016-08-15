function getListCountry(id){
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/Country/listCountry?id=' + id);
    xhr.send();
    var content = "";
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            //alert(xhr.responseText);
            $("#country_id").html(xhr.responseText);
        }
    }
}
function getListProvin(country_id,id) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/Province/getListProvin?country_id=' + country_id+"&id="+id);
    xhr.send();
    var content = "";
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            //alert(xhr.responseText);
            $("#province_id").html(xhr.responseText);
        }
    }
}
function getCountryName(id,idcountry) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/Country/getCountryName?id=' + idcountry);
    xhr.send();
    var content = "";
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            //alert(xhr.responseText);
            $("#country_"+id).html(xhr.responseText);
        }
    }
}
function showLoadingImage() {
    $("#loadingImage").show();
}
function hideLoadingImage() {
    $("#loadingImage").hide();
}
function convertFromDateIdToDateString(sDate) {
    //sDate = sDate.replace(/\//g, "");
    //alert(sDate);
    sDate = sDate.substring(0, 4) + "-" + sDate.substring(4, 6) + "-" + sDate.substring(6, 8);
    return sDate;
}
function removeSpecialCharater(input) {
    if (input == null) return "";
    input = input.trim();
    input = input.replace(/\./g, "");
    input = input.replace(/\&/g, "");
    input = input.replace(/\'/g, "");
    input = input.replace(/\"/g, "");
    input = input.replace(/\“/g, "");
    input = input.replace(/\”/g, "");
    input = input.replace(/\;/g, "");
    input = input.replace(/\?/g, "");
    input = input.replace(/\!/g, "");
    input = input.replace(/\~/g, "");
    input = input.replace(/\*/g, "");
    input = input.replace(/\:/g, "");
    input = input.replace(/\"/g, "");
    input = input.replace("/", "");
    input = input.replace("%", "");
    input = input.replace("‘", "");
    input = input.replace("’", "");
    input = input.replace(/\"/g, "");
    input = input.replace("+", "");
    input = input.replace("“", "");
    input = input.replace("-", "_");
    input = input.replace("”", "");
    input = input.replace(/\,/g, "");

    return input;
  
}