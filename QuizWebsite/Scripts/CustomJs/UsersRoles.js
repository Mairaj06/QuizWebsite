var categoryId = 0;
$(function () {

    APICall("UserRolesList", "SuccessUserRolesList", "FailureUserRolesList", "POST");
});
function SuccessUserRolesList(data) {
    var columns = [
        { "data": "RoleName", "bSortable": true },
        { "data": "RoleAbbrivation", "bSortable": true },
        {
            mRender: function (data, type, row) {
                return "<a href='Quiz/AddQuiz?Id=" + row.RoleID + "'><span class='glyphicon glyphicon-edit icons' title='Edit' data-toggle='tooltip'></span></a>";
            }, "bSortable": false
        },
        {
            mRender: function (data, type, row) {
                return "<span class='glyphicon glyphicon-trash icons' title='Delete' data-toggle='tooltip' onclick='ConfirmDeleteQuiz(" + row.UserId + ")'></span>";
            }, "bSortable": false

        }];
    BindDataTable("tblUsersRolesList", columns, data);
}

function FailureUserRolesList(error) {

}

function AddUserRole() {
    if (!ValidateForm("frmAddUserRole"))
        return;

    var UserRole = new Object();
    UserRole.RoleID = 0;
    UserRole.RoleName = $("#txtRoleName").val();
    UserRole.RoleAbbrivation = $("#txtRoleAbbrv").val();
    UserRole.IsActive = $("#chkActive").is(":checked");

    APICall("SaveUserRole", "SuccessSaveUserRole", "FailureSaveUserRole", "POST", JSON.stringify({ objUserRole: UserRole }));
}

function SuccessSaveUserRole(resp) {

    if (resp) {
        if (resp.Success)
            ShowSuccessToastMessage(resp.ErrorMessage);
        else
            ShowErrorToastMessage(resp.ErrorMessage);
    }
}
function FailureSaveUserRole(err) {

}