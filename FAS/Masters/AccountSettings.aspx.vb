Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Masters_AccountSettings
    Inherits System.Web.UI.Page

    Private sFormName As String = "Account_Settings"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Private Shared sSession As AllSession
    Dim objAccSetting As New clsAccountSetting
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        'imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub pageload(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadZone()
                ChkbxSave.Enabled = False
                Getdetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub Getdetails()
        Dim dt As New DataTable
        Try
            dt = objAccSetting.GetAccDetails(sSession.AccessCode, sSession.AccessCodeID)
            If dt.Rows.Count > 0 Then
                LoadZone()
                ddlAccZone.SelectedValue = dt.Rows(0)("AS_ZoneID")
                LoadRegion(ddlAccZone.SelectedValue)
                ddlAccRgn.SelectedValue = dt.Rows(0)("AS_RegionID")
                LoadArea(ddlAccRgn.SelectedValue)
                ddlAccArea.SelectedValue = dt.Rows(0)("AS_AreaID")
                LoadAccBrnch(ddlAccArea.SelectedValue)
                ddlAccBrnch.SelectedValue = dt.Rows(0)("AS_BranchID")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Getdetails")
        End Try
    End Sub
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "--- Select Zone ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccZone.SelectedIndexChanged
        Try
            ChkbxSave.Enabled = False : ChkbxSave.Checked = False
            If ddlAccZone.SelectedIndex > 0 Then
                LoadRegion(ddlAccZone.SelectedValue)
            Else
                ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "--- Select Region ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAccRgn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccRgn.SelectedIndexChanged
        Try
            ChkbxSave.Enabled = False : ChkbxSave.Checked = False
            If ddlAccRgn.SelectedIndex > 0 Then
                LoadArea(ddlAccRgn.SelectedValue)
            Else
                ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "--- Select Area ---")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadArea")
        End Try
    End Sub
    Private Sub ddlAccArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccArea.SelectedIndexChanged
        Try
            ChkbxSave.Enabled = False : ChkbxSave.Checked = False
            If ddlAccArea.SelectedIndex > 0 Then
                LoadAccBrnch(ddlAccArea.SelectedValue)
            Else
                ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objAccSetting.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "--- Select Branch ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ChkbxSave_CheckedChanged(sender As Object, e As EventArgs) Handles ChkbxSave.CheckedChanged
        Dim objAccSetting As New clsAccountSetting
        Dim Arr As String
        Try
            Arr = objAccSetting.DeleteChartofACC(sSession.AccessCode, sSession.AccessCodeID)
            lblError.Text = "Deleted successfully"

            objAccSetting.AS_ID = 0
            objAccSetting.AS_CompanyIID = sSession.AccessCodeID
            objAccSetting.AS_ZoneID = ddlAccZone.SelectedValue
            objAccSetting.AS_RegionID = ddlAccRgn.SelectedValue
            objAccSetting.AS_AreaID = ddlAccArea.SelectedValue
            objAccSetting.AS_BranchID = ddlAccBrnch.SelectedValue
            objAccSetting.AS_CreatedBy = sSession.UserID
            objAccSetting.AS_UpdatedBy = sSession.UserID
            objAccSetting.AS_DelFlag = "A"
            objAccSetting.AS_Status = "C"
            objAccSetting.AS_YearID = sSession.YearID
            objAccSetting.AS_CompID = sSession.AccessCodeID
            objAccSetting.AS_Opeartion = "C"
            objAccSetting.AS_IPAddress = sSession.IPAddress
            Arr = objAccSetting.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objAccSetting)
            lblError.Text = "Saved Successfully"
            Getdetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ChkbxSave_CheckedChanged")
            Throw
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Try
            ChkbxSave.Enabled = False : ChkbxSave.Checked = False
            If ddlAccBrnch.SelectedIndex > 0 Then
                ChkbxSave.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccBrnch_SelectedIndexChanged")
        End Try
    End Sub
End Class
