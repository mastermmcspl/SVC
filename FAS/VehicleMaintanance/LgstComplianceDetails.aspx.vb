Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports Microsoft.Office.Interop
Partial Class VehicleMaintanance_LgstComplianceDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Logistics/LgstAddtnlMaster"
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
        imgbtnTyre.ImageUrl = "~/Images/Add24.png"
        imgbtnBattery.ImageUrl = "~/Images/Add24.png"
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
                lblID.Text = "0"
                '   BindExistingVehicle()
                ' BindExistingVehicle()
                loadComplianceType()
                Dim sAssetRefNo As String = ""
                sAssetRefNo = Request.QueryString("MasterID")
                If sAssetRefNo <> "" Then
                    sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "LGSTVM")
                    imgbtnAdd.Visible = False : imgbtnSave.Visible = False : sCMDSave = "NO"
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
                    Dim sPID As String = ""
                    sPID = Request.QueryString("PID")
                    If sPID <> "" Then
                        Dim iInvID As Integer = objGen.DecryptQueryString(Request.QueryString("PID"))
                    End If
                End If
            End If
        Catch
        End Try
    End Sub
    'Protected Sub BindExistingVehicle()
    '    Try
    '        ddlVehicleNo.DataSource = objvam.LoadVehicle(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
    '        ddlVehicleNo.DataTextField = "LVM_RegNo"
    '        ddlVehicleNo.DataValueField = "LVM_ID"
    '        ddlVehicleNo.DataBind()
    '        ddlVehicleNo.Items.Insert(0, "Select Vehicle No")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Protected Sub bindComplianceType()
    '    Try
    '        ddlCompliance.DataSource = objvam.LoadComplianceType(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlCompliance.DataTextField = "Mas_Desc"
    '        ddlCompliance.DataValueField = "Mas_Id"
    '        ddlCompliance.DataBind()
    '        ddlCompliance.Items.Insert(0, "Select Compliance Type")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub loadComplianceType()
        Dim dt As New DataTable
        Try
            ddlCompliancetype.Items.Insert(0, "---Select Compliance Type ---")
            ddlCompliancetype.Items.Insert(1, "Battery")
            ddlCompliancetype.Items.Insert(2, "Tyres")
            ddlCompliancetype.Items.Insert(3, "Other Compliance")
            ddlCompliancetype.Items.Insert(4, "Loan Due")
            ddlCompliancetype.Items.Insert(5, "Insurance Due")
            ddlCompliancetype.Items.Insert(6, "All Compliances")
            ddlCompliancetype.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadComplianceType")
        End Try
    End Sub
End Class
