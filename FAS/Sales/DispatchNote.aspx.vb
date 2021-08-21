Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Sales_DispatchNote
    Inherits System.Web.UI.Page
    Private sFormName As String = "Reports_Viewer_SalesInvoiceItemWiseRpt"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objItemWise As New ClsItemWiseSalesInvoice
    Private Shared sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsFASPermission As New clsFASPermission
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            lblError.Text = ""
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "SINV")

                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                End If
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasSIn", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SalesPermission.aspx", False) 'Permissions/SalesPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then
                '        
                '    End If
                'End If

                LoadInvoice()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadInvoice()
        Try
            ddlInvoice.DataSource = objItemWise.Invoice(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlInvoice.DataTextField = "SPO_OrderCode"
            ddlInvoice.DataValueField = "SPO_ID"
            ddlInvoice.DataBind()
            ddlInvoice.Items.Insert(0, New ListItem("--- Select Order No. ---", "0"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadDispatchNo(ByVal iOrderID As Integer)
        Try
            ddlDispatch.DataSource = objItemWise.BindDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            ddlDispatch.DataTextField = "SDM_Code"
            ddlDispatch.DataValueField = "SDM_ID"
            ddlDispatch.DataBind()
            ddlDispatch.Items.Insert(0, New ListItem("--- Select Invoice No. ---", "0"))
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDispatchNo")
        End Try
    End Sub
    Private Sub ddlInvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlInvoice.SelectedIndexChanged
        Try
            If ddlInvoice.SelectedIndex > 0 Then
                LoadDispatchNo(ddlInvoice.SelectedValue)
            Else
                ddlDispatch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlInvoice_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlDispatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDispatch.SelectedIndexChanged
        Dim iReportType As Integer
        Dim objProForma As New clsPROFormaSalesOrder
        Try
            iReportType = objProForma.GetReportTypeFromPrintSettings(sSession.AccessCode, sSession.AccessCodeID)
            If iReportType > 0 Then
                Response.Redirect("~/Reports/Viewer/SalesInvoiceRdlc.aspx?ExistingOrder=" & ddlInvoice.SelectedValue & " &ExistingDispatch=" & ddlDispatch.SelectedValue & "")
            Else
                Response.Redirect("~/Reports/Viewer/SalesInvoiceItemWiseRpt.aspx?ExistingOrder=" & ddlInvoice.SelectedValue & " &ExistingDispatch=" & ddlDispatch.SelectedValue & "")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDispatch_SelectedIndexChanged")
        End Try
    End Sub
End Class
