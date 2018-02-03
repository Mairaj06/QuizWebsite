$(function () {
    var obj = new Object();
    APICall("UsersList", "SuccessUsersList", "FailureUsersList", "POST", JSON.stringify({ User: obj }));
});

function CheckUser()
{
    var username = $.trim($("#txtUserName").val());
    if (username != "")
        APICall("CheckUser/" + username, "SuccessCheckUser", "FailureCheckUser", "GET");
}
function SuccessCheckUser(resp)
{
    if (resp)
    {
        ShowErrorToastMessage("Username already exists");
    }
}
function AddUser() {
    if (!ValidateForm("frmAddUser"))
        return;
    
    var User = new Object();
    User.UserId = 0;
    User.UserName = $("#txtUserName").val();
    User.FirstName = $("#txtFirstName").val();
    User.LastName = $("#txtLastName").val();
    User.Password = $("#txtPassword").val();
    User.Email = $("#txtEmail").val();
    User.Role = parseInt($("#ddlRole").val());
    User.IsActive = $("#chkActive").is(":checked");

    APICall("SaveUser", "SuccessSaveUser", "FailureSaveUser", "POST", JSON.stringify({ objUser: User }));
}
function SuccessSaveUser(resp) {
    
    if (resp) {
        if (resp.Success)
            ShowSuccessToastMessage(resp.ErrorMessage);
        else
            ShowErrorToastMessage(resp.ErrorMessage);
    }
}
function FailureSaveUser(err) {

}

function SuccessUsersList(data)
{
    
    var columns = [
        { "data": "UserName", "bSortable": true },
        { "data": "FirstName", "bSortable": true },
        { "data": "LastName", "bSortable": true },
        {
            mRender: function (data, type, row) {
                return (row.Role == 1 ? "Admin" : "User");
            }, "bSortable": false
        },
    {
        mRender: function (data, type, row) {
            return "<a href='Quiz/AddQuiz?Id=" + row.UserId + "'><span class='glyphicon glyphicon-edit icons' title='Edit' data-toggle='tooltip'></span></a>";
        }, "bSortable": false
    },
    {
        mRender: function (data, type, row) {
            return "<span class='glyphicon glyphicon-trash icons' title='Delete' data-toggle='tooltip' onclick='ConfirmDeleteQuiz(" + row.UserId + ")'></span>";
        }, "bSortable": false

    }];
    BindDataTable("tblUsersList", columns, data);
}