var quizActionsUrl = "Quiz/";
$(function () {
    $("#divAddCategory").on("shown.bs.modal", function () {
        $("#txtCategory").val("");
    });;
    LoadQuizCategory();
    
});
function LoadQuizCategory()
{
    APICall(quizActionsUrl + "LoadAllCategories", "SuccessLoadQuizCategory", "FailureLoadQuizCategory", "GET");
}
function SuccessLoadQuizCategory(resp)
{
    if (resp && resp.length > 0)
    {
        RemoveDataTable("tblQuizCategories");
        $("#tblQuizCategories tbody").html("");
        for (var i = 0; i < resp.length; i++)
        {
            var tr = "<tr><td>" + (i + 1) + "</td><td>" + resp[i].Category + "</td><td>"+resp[i].Count+"</td>";
            tr += "<td><span onclick='EditCategory(" + resp[i].Id + "," + resp[i].IsDeleted + ",\"" + resp[i].Category + "\")' class='glyphicon glyphicon-edit icons' title='Edit' data-toggle='tooltip'></span></td>" +
            "<td><span class='glyphicon glyphicon-trash icons' title='Delete' data-toggle='tooltip' onclick='ConfirmDeleteCategory(" + resp[i].Id + "," + resp[i].IsDeleted + ",\"" + resp[i].Category + "\")'></span></td></tr>";
            $("#tblQuizCategories tbody").append(tr);
        }
        
    }
    var columns = [{ "bSortable": true }, { "bSortable": true }, { "bSortable": true }, { "bSortable": false }, { "bSortable": false }];
    BindStaticDataTable("tblQuizCategories", columns);
}
function FailureLoadQuizCategory(err)
{

}
function ShowAddCategoryPopup()
{
    $("#divAddCategory").modal("show");
}
function CreateCategoryObject(categoryID) {
    var category;
    var categoryID;
    var isDeleted = false;
    if ($("#btnAddCategory").attr("data-catId")) {
        categoryID = $("#btnAddCategory").attr("data-catId");
        isDeleted = $("#btnAddCategory").attr("data-deleted");
        $("#btnAddCategory").removeAttr("data-catId");
        $("#btnAddCategory").removeAttr("data-deleted");
    }
    else
    {
        isDeleted = false;
        categoryID = 0;
    }
    category = $("#txtCategory").val();
    SaveQuizCategory(categoryID, category, isDeleted);
    
}
function SaveQuizCategory(categoryID,category,isDeleted)
{
    var obj = new Object();
    obj.Id = categoryID;
    obj.Category = category;
    obj.IsDeleted = isDeleted;
    APICall(quizActionsUrl+"SaveQuizCategory", "SuccessSaveQuizCategory", "FailureSaveQuizCategory", "POST", JSON.stringify({ Obj: obj }));
}
function SuccessSaveQuizCategory(resp)
{
    if (resp && resp.length > 0)
    {
        if ($("#divDeleteCategory").css("display") == "block")
            ShowSuccessToastMessage("Quiz category deleted successfully.");
        else
            ShowSuccessToastMessage("Quiz category saved successfully.");
        $("#divAddCategory").modal("hide");
        $("#divDeleteCategory").modal("hide");
        SuccessLoadQuizCategory(resp);
    }
}
function FailureSaveQuizCategory(err)
{
    
}
function EditCategory(Id, Isdeleted, Category)
{
    $("#divAddCategory").modal("show");
    $("#txtCategory").val(Category);
    $("#btnAddCategory").attr({ "data-catId": Id, "data-deleted": Isdeleted });

}
function ConfirmDeleteCategory(Id, Isdeleted, Category)
{
    $("#divDeleteCategory").modal("show");
    $("#txtCategory").val(Category);
    $("#btnAddCategory").attr({ "data-catId": Id, "data-deleted": true });
}