Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class VehicleMaintanance_LgstServiceDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/LgstServiceDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Dim objCOA As New clsChartOfAccounts
    Private Shared sCMDSave As String
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objvam As New clsVehicleAddtlnMaster

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try

            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LDB")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCMDSave = "NO" : imgbtnWaiting.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True

                        sCMDSave = "YES"
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                End If
                BindExistingVehicle()
                ddlExistingVehicleNo.SelectedValue = objGen.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                '  sAssetRefNo = objGen.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("MasterID"))))
                ddlExistingVehicleNo_SelectedIndexChanged(sender, e)
            End If
        Catch
        End Try
    End Sub
    Protected Sub BindExistingVehicle()
        Try
            ddlExistingVehicleNo.DataSource = objvam.LoadVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlExistingVehicleNo.DataTextField = "LVM_RegNo"
            ddlExistingVehicleNo.DataValueField = "LVM_ID"
            ddlExistingVehicleNo.DataBind()
            ddlExistingVehicleNo.Items.Insert(0, "Select Existing Vehicle")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExistingVehicleNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingVehicleNo.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistingVehicleNo.SelectedIndex > 0 Then
                BindDetails(ddlExistingVehicleNo.SelectedValue)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub BindDetails(ByVal iVehicleId As Integer)
        Dim dt As New DataTable
        Try
            dt = objvam.LoadVehicleDetails(sSession.AccessCode, sSession.AccessCodeID, iVehicleId)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("LVM_Id")
                If IsDBNull(dt.Rows(0)("LVM_RegNo")) = False Then
                    ' txtRegistrationNo.Text = dt.Rows(0)("LVM_RegNo")
                Else
                    '   txtRegistrationNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_ChassisNo")) = False Then
                    '    txtChassisNo.Text = dt.Rows(0)("LVM_ChassisNo")
                Else
                    '   txtChassisNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_EngineNo")) = False Then
                    '  txtEngineNo.Text = dt.Rows(0)("LVM_EngineNo")
                Else
                    '  txtEngineNo.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_VehicleType")) = False Then
                    '   ddlVehicleType.SelectedValue = dt.Rows(0)("LVM_VehicleType")
                Else
                    '  ddlVehicleType.SelectedIndex = 0
                End If
                If IsDBNull(dt.Rows(0)("LVM_OwnerName")) = False Then
                    '    txtOwnerName.Text = dt.Rows(0)("LVM_OwnerName")
                Else
                    '   txtOwnerName.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_ServiceCntrDtls")) = False Then
                    '  txtServiceCenter.Text = dt.Rows(0)("LVM_ServiceCntrDtls")
                Else
                    '  txtServiceCenter.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_VehicleDetails")) = False Then
                    '    txtDetails.Text = dt.Rows(0)("LVM_VehicleDetails")
                Else
                    '   txtDetails.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("LVM_VehicleDetails")) = False Then
                    '  txtDetails.Text = dt.Rows(0)("LVM_VehicleDetails")
                Else
                    ' txtDetails.Text = ""
                End If

                '     txtMtrRdng.Text = objvam.GetMeterReading(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlExistingVehicleNo.SelectedValue)

                If (dt.Rows(0)("LVM_DelFlag") = "W") Then
                    lblStatus.Text = "Waiting For Approval"
                    '  txtRegistrationNo.Enabled = False
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                    imgbtnWaiting.Visible = True
                End If
                If (dt.Rows(0)("LVM_Delflag") = "A") Then
                    lblStatus.Text = "Approved"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True
                    imgbtnWaiting.Visible = False
                End If
                If (dt.Rows(0)("LVM_Delflag") = "D") Then
                    lblStatus.Text = "Deactivated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                    imgbtnAttachment.Visible = False : lblBadgeCount.Visible = False
                    imgbtnWaiting.Visible = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
