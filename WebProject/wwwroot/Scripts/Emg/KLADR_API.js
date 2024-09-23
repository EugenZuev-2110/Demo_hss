var apiURL = "http://kladr-api.ru/api.php";
var answer = "";

function kladrCityQuery(cityName) {
    var contentType = "city";
    $.ajax({
        url: apiURL,
        data: {
            "query": cityName,
            "contentType": contentType
        }
    }).done(function (data) {
        debugger;
        var jsonAnswer = JSON.parse(data);

    });
}
