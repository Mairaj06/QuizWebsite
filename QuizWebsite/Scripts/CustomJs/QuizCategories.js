$(function () {
    
    LoadQuizCategory();
    
});
function LoadQuizCategory()
{
    APICall("LoadAllCategories", "SuccessLoadQuizCategory", "FailureLoadQuizCategory", "GET");
}
function SuccessLoadQuizCategory(resp)
{
    var count = 0;
    if (resp && resp.length > 0) {
        $("#divQuizCategories").html("");
        var html = "";
        for (var i = 0; i < resp.length; i++)
        {
            if (count == 0)
                html += "<div class='row rowQuizCategory'>";
            if (count == 4)
                html += "</div><div class='row rowQuizCategory'>";
            html += "<div class='col-lg-3'><div class='row colQuizCategory'><div class='row'><div class='col-lg-12'><label>" + resp[i].Category + "</label></div></div><div class='row'><div class='col-lg-12'>Total Quiz : " + resp[i].Count + "</div></div><div class='row'><div class='col-lg-12'>View Quiz(es)</div></div></div>";
            if (count == 4) {
                count = 0;
            }
            count++;
            html += "</div>";

        }
        $("#divQuizCategories").html(html);
    }

}
function FailureLoadQuizCategory(err)
{

}
