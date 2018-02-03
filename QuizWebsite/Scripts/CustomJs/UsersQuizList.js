var categoryId = 0;
$(function () {
    
    APICall("LoadAllQuizByCategory", "SuccessLoadAllQuizByCategory", "FailureLoadAllQuizByCategory", "POST");
});
function SuccessLoadAllQuizByCategory(resp)
{
    if (resp.length > 0)
    {
        var html = "";
        var count = 0;
        for (var i = 0; i < resp.length; i++)
        {
            if (count == 0)
                html += "<div class='row paddingBottom10'>";
            if (count == 3)
                html += "</div><div class='row'>";
            html += "<div class='col-lg-4 paddingTop10'><div class='col-lg-12 lstQuiz paddingBottom10'>" +
                "<div class='col-lg-12 paddingTop10'><strong> Quiz Name : </strong>" + resp[i].Name + "</div>" +
                "<div class='col-lg-12 paddingTop10'><strong> Category : </strong>" + resp[i].Category + "</div>" +
                "<div class='col-lg-12 paddingTop10'><div class='row'><input type='button' class='btn btn-primary pull-right' value='Take Quiz' onclick='GoToQuiz(" + resp[i].ID + ")' /></div></div>" +
                "</div></div>";
            if (count == 3) {
                count = 0;
            }
            count++;
        }
        $("#divQiuzList").html(html);
        
    }
}

function GoToQuiz(id)
{
    window.location = "AttemptQuiz?Id=" + btoa(id);
}