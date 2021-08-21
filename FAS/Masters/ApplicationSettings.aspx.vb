Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Masters_ApplicationSettings
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_ApplicationCOnfiguration"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objSettings As New clsSettings
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Private objclsGRACePermission As New clsFASPermission
    Private Shared sPSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        'imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnReport.Visible = True

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "APPS")
                imgbtnUpdate.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnUpdate.Visible = True
                    End If
                End If
                'imgbtnUpdate.Visible = False : imgbtnReport.Visible = False
                'sPSave = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasAS", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then
                '    End If
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnUpdate.Visible = True
                '        sPSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If

                LoadGroups()
                LoadCustomerDetails() : LoadCustomerVATDetails() : LoadCustomerCSTDetails() : LoadCustomerExciseDetails() : LoadCSalesDetails()
                LoadSupplierDetails() : LoadSuppliersVATDetails() : LoadSuppliersCSTDetails() : LoadSuppliersExciseDetails() : LoadPurchaseDetails()
                LoadCashDetails() : LoadBankDetails()
                LoadFEGDetails() : LoadFELDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadFEGDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "FE", "Gain")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlFEGGroup.SelectedValue = sArray(0)
                ddlFEGSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlFEGGroup.SelectedValue)
                ddlFEGSubGroup.DataTextField = "Description"
                ddlFEGSubGroup.DataValueField = "gl_id"
                ddlFEGSubGroup.DataBind()
                ddlFEGSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlFEGSubGroup.SelectedValue = sArray(1)
                ddlFEGGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlFEGSubGroup.SelectedValue)
                ddlFEGGL.DataTextField = "Description"
                ddlFEGGL.DataValueField = "gl_id"
                ddlFEGGL.DataBind()
                ddlFEGGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlFEGGL.SelectedValue = sArray(2)
                ddlFEGSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlFEGGL.SelectedValue)
                ddlFEGSubGL.DataTextField = "Description"
                ddlFEGSubGL.DataValueField = "gl_id"
                ddlFEGSubGL.DataBind()
                ddlFEGSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlFEGSubGL.SelectedValue = sArray(3)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSupplierDetails")
        End Try
    End Sub
    Private Sub LoadFELDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "FE", "Loss")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlFELGroup.SelectedValue = sArray(0)
                ddlFELSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlFELGroup.SelectedValue)
                ddlFELSubGroup.DataTextField = "Description"
                ddlFELSubGroup.DataValueField = "gl_id"
                ddlFELSubGroup.DataBind()
                ddlFELSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlFELSubGroup.SelectedValue = sArray(1)
                ddlFELGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlFELSubGroup.SelectedValue)
                ddlFELGL.DataTextField = "Description"
                ddlFELGL.DataValueField = "gl_id"
                ddlFELGL.DataBind()
                ddlFELGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlFELGL.SelectedValue = sArray(2)
                ddlFELSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlFELGL.SelectedValue)
                ddlFELSubGL.DataTextField = "Description"
                ddlFELSubGL.DataValueField = "gl_id"
                ddlFELSubGL.DataBind()
                ddlFELSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlFELSubGL.SelectedValue = sArray(3)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSupplierDetails")
        End Try
    End Sub
    Private Sub LoadBankDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Bank", "Bank")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlBankGroup.SelectedValue = sArray(0)
                ddlBankSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlBankGroup.SelectedValue)
                ddlBankSubGroup.DataTextField = "Description"
                ddlBankSubGroup.DataValueField = "gl_id"
                ddlBankSubGroup.DataBind()
                ddlBankSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlBankSubGroup.SelectedValue = sArray(1)
                ddlBankGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlBankSubGroup.SelectedValue)
                ddlBankGL.DataTextField = "Description"
                ddlBankGL.DataValueField = "gl_id"
                ddlBankGL.DataBind()
                ddlBankGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlBankGL.SelectedValue = sArray(2)
                ddlBankSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlBankGL.SelectedValue)
                ddlBankSubGL.DataTextField = "Description"
                ddlBankSubGL.DataValueField = "gl_id"
                ddlBankSubGL.DataBind()
                ddlBankSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlBankSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadBankDetails")
        End Try
    End Sub
    Private Sub LoadCashDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Cash", "Cash")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlcashGroup.SelectedValue = sArray(0)
                ddlCashSubgroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCashGroup.SelectedValue)
                ddlCashSubgroup.DataTextField = "Description"
                ddlCashSubgroup.DataValueField = "gl_id"
                ddlCashSubgroup.DataBind()
                ddlCashSubgroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlCashSubgroup.SelectedValue = sArray(1)
                ddlCashGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCashSubgroup.SelectedValue)
                ddlCashGL.DataTextField = "Description"
                ddlCashGL.DataValueField = "gl_id"
                ddlCashGL.DataBind()
                ddlCashGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlCashGL.SelectedValue = sArray(2)
                ddlCashSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCashGL.SelectedValue)
                ddlCashSubGL.DataTextField = "Description"
                ddlCashSubGL.DataValueField = "gl_id"
                ddlCashSubGL.DataBind()
                ddlCashSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlCashSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCashDetails")
        End Try
    End Sub
    Private Sub LoadPurchaseDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Purchase")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlSPurchaseGroup.SelectedValue = sArray(0)
                ddlSPurchaseSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSPurchaseGroup.SelectedValue)
                ddlSPurchaseSubGroup.DataTextField = "Description"
                ddlSPurchaseSubGroup.DataValueField = "gl_id"
                ddlSPurchaseSubGroup.DataBind()
                ddlSPurchaseSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlSPurchaseSubGroup.SelectedValue = sArray(1)
                ddlSPurchaseGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSPurchaseSubGroup.SelectedValue)
                ddlSPurchaseGL.DataTextField = "Description"
                ddlSPurchaseGL.DataValueField = "gl_id"
                ddlSPurchaseGL.DataBind()
                ddlSPurchaseGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlSPurchaseGL.SelectedValue = sArray(2)
                ddlSPurchaseSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlSPurchaseGL.SelectedValue)
                ddlSPurchaseSubGL.DataTextField = "Description"
                ddlSPurchaseSubGL.DataValueField = "gl_id"
                ddlSPurchaseSubGL.DataBind()
                ddlSPurchaseSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlSPurchaseSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPurchaseDetails")
        End Try
    End Sub
    Private Sub LoadSuppliersExciseDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "IGST")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlSExciseGroup.SelectedValue = sArray(0)
                ddlSExciseSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSExciseGroup.SelectedValue)
                ddlSExciseSubGroup.DataTextField = "Description"
                ddlSExciseSubGroup.DataValueField = "gl_id"
                ddlSExciseSubGroup.DataBind()
                ddlSExciseSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlSExciseSubGroup.SelectedValue = sArray(1)
                ddlSExciseGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSExciseSubGroup.SelectedValue)
                ddlSExciseGL.DataTextField = "Description"
                ddlSExciseGL.DataValueField = "gl_id"
                ddlSExciseGL.DataBind()
                ddlSExciseGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlSExciseGL.SelectedValue = sArray(2)
                ddlSExciseSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlSExciseGL.SelectedValue)
                ddlSExciseSubGL.DataTextField = "Description"
                ddlSExciseSubGL.DataValueField = "gl_id"
                ddlSExciseSubGL.DataBind()
                ddlSExciseSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlSExciseSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSuppliersExciseDetails")
        End Try
    End Sub
    Private Sub LoadSuppliersCSTDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "CGST")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlSCSTGroup.SelectedValue = sArray(0)
                ddlSCSTSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSCSTGroup.SelectedValue)
                ddlSCSTSubGroup.DataTextField = "Description"
                ddlSCSTSubGroup.DataValueField = "gl_id"
                ddlSCSTSubGroup.DataBind()
                ddlSCSTSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlSCSTSubGroup.SelectedValue = sArray(1)
                ddlSCSTGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSCSTSubGroup.SelectedValue)
                ddlSCSTGL.DataTextField = "Description"
                ddlSCSTGL.DataValueField = "gl_id"
                ddlSCSTGL.DataBind()
                ddlSCSTGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlSCSTGL.SelectedValue = sArray(2)
                ddlSCSTSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlSCSTGL.SelectedValue)
                ddlSCSTSubGL.DataTextField = "Description"
                ddlSCSTSubGL.DataValueField = "gl_id"
                ddlSCSTSubGL.DataBind()
                ddlSCSTSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlSCSTSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSuppliersCSTDetails")
        End Try
    End Sub
    Private Sub LoadSuppliersVATDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "SGST")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlSVATGroup.SelectedValue = sArray(0)
                ddlSVATSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSVATGroup.SelectedValue)
                ddlSVATSubGroup.DataTextField = "Description"
                ddlSVATSubGroup.DataValueField = "gl_id"
                ddlSVATSubGroup.DataBind()
                ddlSVATSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlSVATSubGroup.SelectedValue = sArray(1)
                ddlSVATGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSVATSubGroup.SelectedValue)
                ddlSVATGL.DataTextField = "Description"
                ddlSVATGL.DataValueField = "gl_id"
                ddlSVATGL.DataBind()
                ddlSVATGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlSVATGL.SelectedValue = sArray(2)
                ddlSVATSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlSVATGL.SelectedValue)
                ddlSVATSubGL.DataTextField = "Description"
                ddlSVATSubGL.DataValueField = "gl_id"
                ddlSVATSubGL.DataBind()
                ddlSVATSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlSVATSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSuppliersVATDetails")
        End Try
    End Sub
    Private Sub LoadSupplierDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Supplier", "Supplier")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlSGroup.SelectedValue = sArray(0)
                ddlSSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSGroup.SelectedValue)
                ddlSSubGroup.DataTextField = "Description"
                ddlSSubGroup.DataValueField = "gl_id"
                ddlSSubGroup.DataBind()
                ddlSSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlSSubGroup.SelectedValue = sArray(1)
                ddlSGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSSubGroup.SelectedValue)
                ddlSGL.DataTextField = "Description"
                ddlSGL.DataValueField = "gl_id"
                ddlSGL.DataBind()
                ddlSGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlSGL.SelectedValue = sArray(2)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSupplierDetails")
        End Try
    End Sub
    Private Sub LoadCustomerDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Customer")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlCGroup.SelectedValue = sArray(0)
                ddlCSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCGroup.SelectedValue)
                ddlCSubGroup.DataTextField = "Description"
                ddlCSubGroup.DataValueField = "gl_id"
                ddlCSubGroup.DataBind()
                ddlCSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlCSubGroup.SelectedValue = sArray(1)
                ddlCGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCSubGroup.SelectedValue)
                ddlCGL.DataTextField = "Description"
                ddlCGL.DataValueField = "gl_id"
                ddlCGL.DataBind()
                ddlCGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlCGL.SelectedValue = sArray(2)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomerDetails")
        End Try
    End Sub

    Private Sub LoadCustomerVATDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "SGST")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlCVatGroup.SelectedValue = sArray(0)
                ddlCVatSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCVatGroup.SelectedValue)
                ddlCVatSubGroup.DataTextField = "Description"
                ddlCVatSubGroup.DataValueField = "gl_id"
                ddlCVatSubGroup.DataBind()
                ddlCVatSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlCVatSubGroup.SelectedValue = sArray(1)
                ddlcVatGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCVatSubGroup.SelectedValue)
                ddlcVatGL.DataTextField = "Description"
                ddlcVatGL.DataValueField = "gl_id"
                ddlcVatGL.DataBind()
                ddlcVatGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlcVatGL.SelectedValue = sArray(2)
                ddlCVATSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlcVatGL.SelectedValue)
                ddlCVATSubGL.DataTextField = "Description"
                ddlCVATSubGL.DataValueField = "gl_id"
                ddlCVATSubGL.DataBind()
                ddlCVATSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlCVATSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomerVATDetails")
        End Try
    End Sub

    Private Sub LoadCustomerCSTDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "CGST")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlcCSTGroup.SelectedValue = sArray(0)
                ddlCCSTSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlcCSTGroup.SelectedValue)
                ddlCCSTSubGroup.DataTextField = "Description"
                ddlCCSTSubGroup.DataValueField = "gl_id"
                ddlCCSTSubGroup.DataBind()
                ddlCCSTSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlCCSTSubGroup.SelectedValue = sArray(1)
                ddlCCSTGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCCSTSubGroup.SelectedValue)
                ddlCCSTGL.DataTextField = "Description"
                ddlCCSTGL.DataValueField = "gl_id"
                ddlCCSTGL.DataBind()
                ddlCCSTGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlCCSTGL.SelectedValue = sArray(2)
                ddlcCSTSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCCSTGL.SelectedValue)
                ddlcCSTSubGL.DataTextField = "Description"
                ddlcCSTSubGL.DataValueField = "gl_id"
                ddlcCSTSubGL.DataBind()
                ddlcCSTSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlcCSTSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomerCSTDetails")
        End Try
    End Sub

    Private Sub LoadCustomerExciseDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "IGST")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlCExciseGroup.SelectedValue = sArray(0)
                ddlCExciseSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCExciseGroup.SelectedValue)
                ddlCExciseSubGroup.DataTextField = "Description"
                ddlCExciseSubGroup.DataValueField = "gl_id"
                ddlCExciseSubGroup.DataBind()
                ddlCExciseSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlCExciseSubGroup.SelectedValue = sArray(1)
                ddlCExciseGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCExciseSubGroup.SelectedValue)
                ddlCExciseGL.DataTextField = "Description"
                ddlCExciseGL.DataValueField = "gl_id"
                ddlCExciseGL.DataBind()
                ddlCExciseGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlCExciseGL.SelectedValue = sArray(2)
                ddlCExciseSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCExciseGL.SelectedValue)
                ddlCExciseSubGL.DataTextField = "Description"
                ddlCExciseSubGL.DataValueField = "gl_id"
                ddlCExciseSubGL.DataBind()
                ddlCExciseSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlCExciseSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomerExciseDetails")
        End Try
    End Sub

    Private Sub LoadCSalesDetails()
        Dim sPerm As String = ""
        Dim sArray As Array
        Dim i As Integer = 0
        Try
            sPerm = objSettings.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, "Customer", "Sales")
            sPerm = sPerm.Remove(0, 1)
            sArray = sPerm.Split(",")
            If sArray(0) <> 0 Then
                ddlCSalesGroup.SelectedValue = sArray(0)
                ddlCSalesSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCSalesGroup.SelectedValue)
                ddlCSalesSubGroup.DataTextField = "Description"
                ddlCSalesSubGroup.DataValueField = "gl_id"
                ddlCSalesSubGroup.DataBind()
                ddlCSalesSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If sArray(1) <> 0 Then
                ddlCSalesSubGroup.SelectedValue = sArray(1)
                ddlCSalesGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCSalesSubGroup.SelectedValue)
                ddlCSalesGL.DataTextField = "Description"
                ddlCSalesGL.DataValueField = "gl_id"
                ddlCSalesGL.DataBind()
                ddlCSalesGL.Items.Insert(0, "Select General Ledger")
            End If

            If sArray(2) <> 0 Then
                ddlCSalesGL.SelectedValue = sArray(2)
                ddlCSalesSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCSalesGL.SelectedValue)
                ddlCSalesSubGL.DataTextField = "Description"
                ddlCSalesSubGL.DataValueField = "gl_id"
                ddlCSalesSubGL.DataBind()
                ddlCSalesSubGL.Items.Insert(0, "Select Sub General Ledger")
            End If

            If sArray(3) <> 0 Then
                ddlCSalesSubGL.SelectedValue = sArray(3)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCSalesDetails")
        End Try
    End Sub

    Private Sub LoadGroups()
        Dim dt As New DataTable
        Try

            dt = objSettings.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, 0).Tables(0)
            ddlCGroup.DataSource = dt
            ddlCGroup.DataTextField = "Description"
            ddlCGroup.DataValueField = "gl_id"
            ddlCGroup.DataBind()
            ddlCGroup.Items.Insert(0, "Select Group")

            ddlCVatGroup.DataSource = dt
            ddlCVatGroup.DataTextField = "Description"
            ddlCVatGroup.DataValueField = "gl_id"
            ddlCVatGroup.DataBind()
            ddlCVatGroup.Items.Insert(0, "Select Group")

            ddlcCSTGroup.DataSource = dt
            ddlcCSTGroup.DataTextField = "Description"
            ddlcCSTGroup.DataValueField = "gl_id"
            ddlcCSTGroup.DataBind()
            ddlcCSTGroup.Items.Insert(0, "Select Group")

            ddlCExciseGroup.DataSource = dt
            ddlCExciseGroup.DataTextField = "Description"
            ddlCExciseGroup.DataValueField = "gl_id"
            ddlCExciseGroup.DataBind()
            ddlCExciseGroup.Items.Insert(0, "Select Group")

            ddlCSalesGroup.DataSource = dt
            ddlCSalesGroup.DataTextField = "Description"
            ddlCSalesGroup.DataValueField = "gl_id"
            ddlCSalesGroup.DataBind()
            ddlCSalesGroup.Items.Insert(0, "Select Group")

            ddlSGroup.DataSource = dt
            ddlSGroup.DataTextField = "Description"
            ddlSGroup.DataValueField = "gl_id"
            ddlSGroup.DataBind()
            ddlSGroup.Items.Insert(0, "Select Group")

            ddlSVATGroup.DataSource = dt
            ddlSVATGroup.DataTextField = "Description"
            ddlSVATGroup.DataValueField = "gl_id"
            ddlSVATGroup.DataBind()
            ddlSVATGroup.Items.Insert(0, "Select Group")

            ddlSCSTGroup.DataSource = dt
            ddlSCSTGroup.DataTextField = "Description"
            ddlSCSTGroup.DataValueField = "gl_id"
            ddlSCSTGroup.DataBind()
            ddlSCSTGroup.Items.Insert(0, "Select Group")

            ddlSExciseGroup.DataSource = dt
            ddlSExciseGroup.DataTextField = "Description"
            ddlSExciseGroup.DataValueField = "gl_id"
            ddlSExciseGroup.DataBind()
            ddlSExciseGroup.Items.Insert(0, "Select Group")

            ddlSPurchaseGroup.DataSource = dt
            ddlSPurchaseGroup.DataTextField = "Description"
            ddlSPurchaseGroup.DataValueField = "gl_id"
            ddlSPurchaseGroup.DataBind()
            ddlSPurchaseGroup.Items.Insert(0, "Select Group")

            ddlCashGroup.DataSource = dt
            ddlCashGroup.DataTextField = "Description"
            ddlCashGroup.DataValueField = "gl_id"
            ddlCashGroup.DataBind()
            ddlCashGroup.Items.Insert(0, "Select Group")

            ddlBankGroup.DataSource = dt
            ddlBankGroup.DataTextField = "Description"
            ddlBankGroup.DataValueField = "gl_id"
            ddlBankGroup.DataBind()
            ddlBankGroup.Items.Insert(0, "Select Group")

            ddlFEGGroup.DataSource = dt
            ddlFEGGroup.DataTextField = "Description"
            ddlFEGGroup.DataValueField = "gl_id"
            ddlFEGGroup.DataBind()
            ddlFEGGroup.Items.Insert(0, "Select Group")

            ddlFELGroup.DataSource = dt
            ddlFELGroup.DataTextField = "Description"
            ddlFELGroup.DataValueField = "gl_id"
            ddlFELGroup.DataBind()
            ddlFELGroup.Items.Insert(0, "Select Group")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlCashGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCashGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCashGroup.SelectedIndex > 0 Then
                ddlCashSubgroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCashGroup.SelectedValue)
                ddlCashSubgroup.DataTextField = "Description"
                ddlCashSubgroup.DataValueField = "gl_id"
                ddlCashSubgroup.DataBind()
                ddlCashSubgroup.Items.Insert(0, "Select Sub Group")
            Else
                ddlCashSubgroup.DataSource = dt
                ddlCashSubgroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCashGroup_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlBankGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlBankGroup.SelectedIndex > 0 Then
                ddlBankSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlBankGroup.SelectedValue)
                ddlBankSubGroup.DataTextField = "Description"
                ddlBankSubGroup.DataValueField = "gl_id"
                ddlBankSubGroup.DataBind()
                ddlBankSubGroup.Items.Insert(0, "Select Sub Group")
            Else
                ddlBankSubGroup.DataSource = dt
                ddlBankSubGroup.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBankGroup_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCGroup.SelectedIndex > 0 Then
                ddlCSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCGroup.SelectedValue)
                ddlCSubGroup.DataTextField = "Description"
                ddlCSubGroup.DataValueField = "gl_id"
                ddlCSubGroup.DataBind()
                ddlCSubGroup.Items.Insert(0, "Select Sub Group")

            Else
                ddlCSubGroup.DataSource = dt
                ddlCSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCGroup_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCashSubgroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCashSubgroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCashSubgroup.SelectedIndex > 0 Then
                ddlCashGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCashSubgroup.SelectedValue)
                ddlCashGL.DataTextField = "Description"
                ddlCashGL.DataValueField = "gl_id"
                ddlCashGL.DataBind()
                ddlCashGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlCashGL.DataSource = dt
                ddlCashGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCashSubgroup_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlBankSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlBankSubGroup.SelectedIndex > 0 Then
                ddlBankGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlBankSubGroup.SelectedValue)
                ddlBankGL.DataTextField = "Description"
                ddlBankGL.DataValueField = "gl_id"
                ddlBankGL.DataBind()
                ddlBankGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlBankGL.DataSource = dt
                ddlBankGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBankSubGroup_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCSubGroup.SelectedIndex > 0 Then
                ddlCGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCSubGroup.SelectedValue)
                ddlCGL.DataTextField = "Description"
                ddlCGL.DataValueField = "gl_id"
                ddlCGL.DataBind()
                ddlCGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlCGL.DataSource = dt
                ddlCGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCSubGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCVatGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCVatGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCVatGroup.SelectedIndex > 0 Then
                ddlCVatSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCVatGroup.SelectedValue)
                ddlCVatSubGroup.DataTextField = "Description"
                ddlCVatSubGroup.DataValueField = "gl_id"
                ddlCVatSubGroup.DataBind()
                ddlCVatSubGroup.Items.Insert(0, "Select Sub Group")

            Else
                ddlCVatSubGroup.DataSource = dt
                ddlCVatSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCVatGroup_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlCVatSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCVatSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCVatSubGroup.SelectedIndex > 0 Then
                ddlcVatGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCVatSubGroup.SelectedValue)
                ddlcVatGL.DataTextField = "Description"
                ddlcVatGL.DataValueField = "gl_id"
                ddlcVatGL.DataBind()
                ddlcVatGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlcVatGL.DataSource = dt
                ddlcVatGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCVatSubGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlcCSTGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcCSTGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlcCSTGroup.SelectedIndex > 0 Then
                ddlCCSTSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlcCSTGroup.SelectedValue)
                ddlCCSTSubGroup.DataTextField = "Description"
                ddlCCSTSubGroup.DataValueField = "gl_id"
                ddlCCSTSubGroup.DataBind()
                ddlCCSTSubGroup.Items.Insert(0, "Select Sub Group")

            Else
                ddlCCSTSubGroup.DataSource = dt
                ddlCCSTSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlcCSTGroup_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlCCSTSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCCSTSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCCSTSubGroup.SelectedIndex > 0 Then
                ddlCCSTGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCCSTSubGroup.SelectedValue)
                ddlCCSTGL.DataTextField = "Description"
                ddlCCSTGL.DataValueField = "gl_id"
                ddlCCSTGL.DataBind()
                ddlCCSTGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlCCSTGL.DataSource = dt
                ddlCCSTGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCCSTSubGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCExciseGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCExciseGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCExciseGroup.SelectedIndex > 0 Then
                ddlCExciseSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCExciseGroup.SelectedValue)
                ddlCExciseSubGroup.DataTextField = "Description"
                ddlCExciseSubGroup.DataValueField = "gl_id"
                ddlCExciseSubGroup.DataBind()
                ddlCExciseSubGroup.Items.Insert(0, "Select Sub Group")

            Else
                ddlCExciseSubGroup.DataSource = dt
                ddlCExciseSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCExciseGroup_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlCExciseSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCExciseSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCExciseSubGroup.SelectedIndex > 0 Then
                ddlCExciseGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCExciseSubGroup.SelectedValue)
                ddlCExciseGL.DataTextField = "Description"
                ddlCExciseGL.DataValueField = "gl_id"
                ddlCExciseGL.DataBind()
                ddlCExciseGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlCExciseGL.DataSource = dt
                ddlCExciseGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCExciseSubGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlCSalesGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCSalesGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCSalesGroup.SelectedIndex > 0 Then
                ddlCSalesSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlCSalesGroup.SelectedValue)
                ddlCSalesSubGroup.DataTextField = "Description"
                ddlCSalesSubGroup.DataValueField = "gl_id"
                ddlCSalesSubGroup.DataBind()
                ddlCSalesSubGroup.Items.Insert(0, "Select Sub Group")

            Else
                ddlCSalesSubGroup.DataSource = dt
                ddlCSalesSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCSalesGroup_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlCSalesSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCSalesSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCSalesSubGroup.SelectedIndex > 0 Then
                ddlCSalesGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlCSalesSubGroup.SelectedValue)
                ddlCSalesGL.DataTextField = "Description"
                ddlCSalesGL.DataValueField = "gl_id"
                ddlCSalesGL.DataBind()
                ddlCSalesGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlCSalesGL.DataSource = dt
                ddlCSalesGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCSalesSubGroup_SelectedIndexChanged")
        End Try
    End Sub


    Private Sub ddlSGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSGroup.SelectedIndex > 0 Then
                ddlSSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSGroup.SelectedValue)
                ddlSSubGroup.DataTextField = "Description"
                ddlSSubGroup.DataValueField = "gl_id"
                ddlSSubGroup.DataBind()
                ddlSSubGroup.Items.Insert(0, "Select Sub Group")
            Else
                ddlSSubGroup.DataSource = dt
                ddlSSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSSubGroup.SelectedIndex > 0 Then
                ddlSGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSSubGroup.SelectedValue)
                ddlSGL.DataTextField = "Description"
                ddlSGL.DataValueField = "gl_id"
                ddlSGL.DataBind()
                ddlSGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlSGL.DataSource = dt
                ddlSGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSSubGroup_SelectedIndexChanged")
        End Try
    End Sub



    Private Sub ddlSVATGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSVATGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSVATGroup.SelectedIndex > 0 Then
                ddlSVATSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSVATGroup.SelectedValue)
                ddlSVATSubGroup.DataTextField = "Description"
                ddlSVATSubGroup.DataValueField = "gl_id"
                ddlSVATSubGroup.DataBind()
                ddlSVATSubGroup.Items.Insert(0, "Select Sub Group")
            Else
                ddlSVATSubGroup.DataSource = dt
                ddlSVATSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSVATGroup_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlSVATSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSVATSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSVATSubGroup.SelectedIndex > 0 Then
                ddlSVATGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSVATSubGroup.SelectedValue)
                ddlSVATGL.DataTextField = "Description"
                ddlSVATGL.DataValueField = "gl_id"
                ddlSVATGL.DataBind()
                ddlSVATGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlSVATGL.DataSource = dt
                ddlSVATGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSVATSubGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSCSTGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSCSTGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSCSTGroup.SelectedIndex > 0 Then
                ddlSCSTSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSCSTGroup.SelectedValue)
                ddlSCSTSubGroup.DataTextField = "Description"
                ddlSCSTSubGroup.DataValueField = "gl_id"
                ddlSCSTSubGroup.DataBind()
                ddlSCSTSubGroup.Items.Insert(0, "Select Sub Group")
            Else
                ddlSCSTSubGroup.DataSource = dt
                ddlSCSTSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSCSTGroup_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlSCSTSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSCSTSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSCSTSubGroup.SelectedIndex > 0 Then
                ddlSCSTGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSCSTSubGroup.SelectedValue)
                ddlSCSTGL.DataTextField = "Description"
                ddlSCSTGL.DataValueField = "gl_id"
                ddlSCSTGL.DataBind()
                ddlSCSTGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlSCSTGL.DataSource = dt
                ddlSCSTGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSCSTSubGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSExciseGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSExciseGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSExciseGroup.SelectedIndex > 0 Then
                ddlSExciseSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSExciseGroup.SelectedValue)
                ddlSExciseSubGroup.DataTextField = "Description"
                ddlSExciseSubGroup.DataValueField = "gl_id"
                ddlSExciseSubGroup.DataBind()
                ddlSExciseSubGroup.Items.Insert(0, "Select Sub Group")
            Else
                ddlSExciseSubGroup.DataSource = dt
                ddlSExciseSubGroup.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSExciseGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSExciseSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSExciseSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSExciseSubGroup.SelectedIndex > 0 Then
                ddlSExciseGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSExciseSubGroup.SelectedValue)
                ddlSExciseGL.DataTextField = "Description"
                ddlSExciseGL.DataValueField = "gl_id"
                ddlSExciseGL.DataBind()
                ddlSExciseGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlSExciseGL.DataSource = dt
                ddlSExciseGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSExciseSubGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSPurchaseGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSPurchaseGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSPurchaseGroup.SelectedIndex > 0 Then
                ddlSPurchaseSubGroup.DataSource = objSettings.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlSPurchaseGroup.SelectedValue)
                ddlSPurchaseSubGroup.DataTextField = "Description"
                ddlSPurchaseSubGroup.DataValueField = "gl_id"
                ddlSPurchaseSubGroup.DataBind()
                ddlSPurchaseSubGroup.Items.Insert(0, "Select Sub Group")
            Else
                ddlSPurchaseSubGroup.DataSource = dt
                ddlSPurchaseSubGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSPurchaseGroup_SelectedIndexChanged")
        End Try

    End Sub

    Private Sub ddlSPurchaseSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSPurchaseSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSPurchaseSubGroup.SelectedIndex > 0 Then
                ddlSPurchaseGL.DataSource = objSettings.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSPurchaseSubGroup.SelectedValue)
                ddlSPurchaseGL.DataTextField = "Description"
                ddlSPurchaseGL.DataValueField = "gl_id"
                ddlSPurchaseGL.DataBind()
                ddlSPurchaseGL.Items.Insert(0, "Select General Ledger")
            Else
                ddlSPurchaseGL.DataSource = dt
                ddlSPurchaseGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSPurchaseSubGroup_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim iGroup As Integer = 0, iSubGroup As Integer = 0
        Dim iGL As Integer = 0, iSubGL As Integer = 0
        Try
            lblError.Text = ""
            'Customer Setting
            'Customer
            If ddlCGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select (Customer) Group." : lblError.Text = "Select (Customer) Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlCGroup.Focus()
                Exit Sub
            End If
            If ddlCSubGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select Sub Group." : lblError.Text = "Select Sub Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlCSubGroup.Focus()
                Exit Sub
            End If
            If ddlCGL.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select General Ledger." : lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
                ddlCGL.Focus()
            End If
            'VAT
            If ddlCVatGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select (SGST) Group." : lblError.Text = "Select (SGST) Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlCVatGroup.Focus()
                Exit Sub
            End If
            If ddlCVatSubGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select Sub Group." : lblError.Text = "Select Sub Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlCVatSubGroup.Focus()
                Exit Sub
            End If
            If ddlcVatGL.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select General Ledger." : lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
                ddlcVatGL.Focus()
            End If
            'Sales
            If ddlCSalesGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select (Sales) Group." : lblError.Text = "Select (Sales) Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlCSalesGroup.Focus()
                Exit Sub
            End If
            If ddlCSalesSubGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select Sub Group." : lblError.Text = "Select Sub Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlCSalesSubGroup.Focus()
                Exit Sub
            End If
            'Supplier Settings
            'Supplier
            If ddlSGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select (Supplier) Group." : lblError.Text = "Select (Supplier) Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlSGroup.Focus()
                Exit Sub
            End If
            If ddlSSubGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select Sub Group." : lblError.Text = "Select Sub Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlSSubGroup.Focus()
                Exit Sub
            End If
            If ddlSGL.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select General Ledger." : lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
                ddlSGL.Focus()
            End If
            'VAT
            If ddlSVATGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select (SGST) Group." : lblError.Text = "Select (SGST) Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlSVATGroup.Focus()
                Exit Sub
            End If
            If ddlSVATSubGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select Sub Group." : lblError.Text = "Select Sub Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlSVATSubGroup.Focus()
                Exit Sub
            End If
            If ddlSVATGL.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select General Ledger." : lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
                ddlSVATGL.Focus()
            End If
            'Sales
            If ddlSPurchaseGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select (Purchase) Group." : lblError.Text = "Select (Purchase) Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlSPurchaseGroup.Focus()
                Exit Sub
            End If
            If ddlSPurchaseSubGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select Sub Group." : lblError.Text = "Select Sub Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlSPurchaseSubGroup.Focus()
                Exit Sub
            End If
            'General Settings
            'Cash
            If ddlCashGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select (Cash) Group." : lblError.Text = "Select (Cash) Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlCashGroup.Focus()
                Exit Sub
            End If
            If ddlCashSubgroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select Sub Group." : lblError.Text = "Select Sub Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlCashSubgroup.Focus()
                Exit Sub
            End If
            If ddlCashGL.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select General Ledger." : lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
                ddlCashGL.Focus()
            End If
            'If ddlCashSubGL.SelectedIndex = 0 Then
            '    lblFASSettingsValidationMsg.Text = "Select Sub General Ledger." : lblError.Text = "Select Sub General Ledger."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
            '    Exit Sub
            '    ddlCashSubGL.Focus()
            'End If
            'Bank
            If ddlBankGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select (Bank) Group." : lblError.Text = "Select (Bank) Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlBankGroup.Focus()
                Exit Sub
            End If
            If ddlBankSubGroup.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select Sub Group." : lblError.Text = "Select Sub Group."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                ddlBankSubGroup.Focus()
                Exit Sub
            End If
            If ddlBankGL.SelectedIndex = 0 Then
                lblFASSettingsValidationMsg.Text = "Select General Ledger." : lblError.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
                ddlBankGL.Focus()
            End If
            'If ddlBankSubGL.SelectedIndex = 0 Then
            '    lblFASSettingsValidationMsg.Text = "Select Sub General Ledger." : lblError.Text = "Select Sub General Ledger."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASSettingValidation').modal('show');", True)
            '    Exit Sub
            '    ddlBankSubGL.Focus()
            'End If

            'Customer Settings

            'Customer
            If ddlCGroup.SelectedIndex > 0 Then
                iGroup = ddlCGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlCSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlCSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            If ddlCGL.SelectedIndex > 0 Then
                iGL = ddlCGL.SelectedValue
            Else
                iGL = 0
            End If

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, 0, "Customer", "Customer")


            'VAT

            If ddlCVatGroup.SelectedIndex > 0 Then
                iGroup = ddlCVatGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlCVatSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlCVatSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            If ddlcVatGL.SelectedIndex > 0 Then
                iGL = ddlcVatGL.SelectedValue
            Else
                iGL = 0
            End If

            'If ddlCVATSubGL.SelectedIndex > 0 Then
            '    iSubGL = ddlCVATSubGL.SelectedValue
            'Else
            'iSubGL = 0
            'End If

            iSubGL = 0

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Customer", "SGST")



            'CST

            'If ddlcCSTGroup.SelectedIndex > 0 Then
            '    iGroup = ddlcCSTGroup.SelectedValue
            'Else
            'iGroup = 0
            'End If

            'If ddlCCSTSubGroup.SelectedIndex > 0 Then
            '    iSubGroup = ddlCCSTSubGroup.SelectedValue
            'Else
            'iSubGroup = 0
            'End If

            'If ddlCCSTGL.SelectedIndex > 0 Then
            '    iGL = ddlCCSTGL.SelectedValue
            'Else
            'iGL = 0
            'End If

            'If ddlcCSTSubGL.SelectedIndex > 0 Then
            '    iSubGL = ddlcCSTSubGL.SelectedValue
            'Else
            'iSubGL = 0
            'End If

            iGroup = 0
            iSubGroup = 0
            iGL = 0
            iSubGL = 0

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Customer", "CGST")



            'Excise

            'If ddlCExciseGroup.SelectedIndex > 0 Then
            '    iGroup = ddlCExciseGroup.SelectedValue
            'Else
            'iGroup = 0
            'End If

            'If ddlCExciseSubGroup.SelectedIndex > 0 Then
            '    iSubGroup = ddlCExciseSubGroup.SelectedValue
            'Else
            'iSubGroup = 0
            'End If

            'If ddlCExciseGL.SelectedIndex > 0 Then
            '    iGL = ddlCExciseGL.SelectedValue
            'Else
            'iGL = 0
            'End If

            'If ddlCExciseSubGL.SelectedIndex > 0 Then
            '    iSubGL = ddlCExciseSubGL.SelectedValue
            'Else
            'iSubGL = 0
            'End If

            iGroup = 0
            iSubGroup = 0
            iGL = 0
            iSubGL = 0

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Customer", "IGST")



            'Sales
            If ddlCSalesGroup.SelectedIndex > 0 Then
                iGroup = ddlCSalesGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlCSalesSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlCSalesSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            'If ddlCSalesGL.SelectedIndex > 0 Then
            '    iGL = ddlCSalesGL.SelectedValue
            'Else
            'iGL = 0
            'End If

            'If ddlCSalesSubGL.SelectedIndex > 0 Then
            '    iSubGL = ddlCSalesSubGL.SelectedValue
            'Else
            'iSubGL = 0
            'End If

            iGL = 0
            iSubGL = 0

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Customer", "Sales")


            'Supplier Settings

            'Supplier
            If ddlSGroup.SelectedIndex > 0 Then
                iGroup = ddlSGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlSSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlSSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            If ddlSGL.SelectedIndex > 0 Then
                iGL = ddlSGL.SelectedValue
            Else
                iGL = 0
            End If

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, 0, "Supplier", "Supplier")



            'VAT
            If ddlSVATGroup.SelectedIndex > 0 Then
                iGroup = ddlSVATGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlSVATSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlSVATSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            If ddlSVATGL.SelectedIndex > 0 Then
                iGL = ddlSVATGL.SelectedValue
            Else
                iGL = 0
            End If

            'If ddlSVATSubGL.SelectedIndex > 0 Then
            '    iSubGL = ddlSVATSubGL.SelectedValue
            'Else
            iSubGL = 0
            'End If
            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Supplier", "SGST")



            'CST
            'If ddlSCSTGroup.SelectedIndex > 0 Then
            '    iGroup = ddlSCSTGroup.SelectedValue
            'Else
            '    iGroup = 0
            'End If

            'If ddlSCSTSubGroup.SelectedIndex > 0 Then
            '    iSubGroup = ddlSCSTSubGroup.SelectedValue
            'Else
            '    iSubGroup = 0
            'End If

            'If ddlSCSTGL.SelectedIndex > 0 Then
            '    iGL = ddlSCSTGL.SelectedValue
            'Else
            '    iGL = 0
            'End If

            'If ddlSCSTSubGL.SelectedIndex > 0 Then
            '    iSubGL = ddlSCSTSubGL.SelectedValue
            'Else
            '    iSubGL = 0
            'End If

            iGroup = 0
            iSubGroup = 0
            iGL = 0
            iSubGL = 0

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Supplier", "CGST")



            'Excise
            'If ddlSExciseGroup.SelectedIndex > 0 Then
            '    iGroup = ddlSExciseGroup.SelectedValue
            'Else
            '    iGroup = 0
            'End If

            'If ddlSExciseSubGroup.SelectedIndex > 0 Then
            '    iSubGroup = ddlSExciseSubGroup.SelectedValue
            'Else
            '    iSubGroup = 0
            'End If

            'If ddlSExciseGL.SelectedIndex > 0 Then
            '    iGL = ddlSExciseGL.SelectedValue
            'Else
            '    iGL = 0
            'End If

            'If ddlSExciseSubGL.SelectedIndex > 0 Then
            '    iSubGL = ddlSExciseSubGL.SelectedValue
            'Else
            '    iSubGL = 0
            'End If

            iGroup = 0
            iSubGroup = 0
            iGL = 0
            iSubGL = 0

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Supplier", "IGST")



            'Sales            
            If ddlSPurchaseGroup.SelectedIndex > 0 Then
                iGroup = ddlSPurchaseGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlSPurchaseSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlSPurchaseSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            'If ddlSPurchaseGL.SelectedIndex > 0 Then
            '    iGL = ddlSPurchaseGL.SelectedValue
            'Else
            '    iGL = 0
            'End If

            'If ddlSPurchaseSubGL.SelectedIndex > 0 Then
            '    iSubGL = ddlSPurchaseSubGL.SelectedValue
            'Else
            '    iSubGL = 0
            'End If

            iGL = 0
            iSubGL = 0

            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Supplier", "Purchase")


            'General Settings - Cash
            If ddlCashGroup.SelectedIndex > 0 Then
                iGroup = ddlCashGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlCashSubgroup.SelectedIndex > 0 Then
                iSubGroup = ddlCashSubgroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            If ddlCashGL.SelectedIndex > 0 Then
                iGL = ddlCashGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlCashSubGL.SelectedIndex > 0 Then
                iSubGL = ddlCashSubGL.SelectedValue
            Else
                iSubGL = 0
            End If
            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Cash", "Cash")

            'General Settings - Bank
            If ddlBankGroup.SelectedIndex > 0 Then
                iGroup = ddlBankGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlBankSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlBankSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            If ddlBankGL.SelectedIndex > 0 Then
                iGL = ddlBankGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlBankSubGL.SelectedIndex > 0 Then
                iSubGL = ddlBankSubGL.SelectedValue
            Else
                iSubGL = 0
            End If
            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "Bank", "Bank")
            'FE Gain 
            If ddlFEGGroup.SelectedIndex > 0 Then
                iGroup = ddlFEGGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlFEGSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlFEGSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            If ddlFEGGL.SelectedIndex > 0 Then
                iGL = ddlFEGGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlFEGSubGL.SelectedIndex > 0 Then
                iSubGL = ddlFEGSubGL.SelectedValue
            Else
                iSubGL = 0
            End If
            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "FEGain", "FEGain")

            'FELoss
            If ddlFELGroup.SelectedIndex > 0 Then
                iGroup = ddlFELGroup.SelectedValue
            Else
                iGroup = 0
            End If

            If ddlFELSubGroup.SelectedIndex > 0 Then
                iSubGroup = ddlFELSubGroup.SelectedValue
            Else
                iSubGroup = 0
            End If

            If ddlFELGL.SelectedIndex > 0 Then
                iGL = ddlFELGL.SelectedValue
            Else
                iGL = 0
            End If

            If ddlFELSubGL.SelectedIndex > 0 Then
                iSubGL = ddlFELSubGL.SelectedValue
            Else
                iSubGL = 0
            End If
            objSettings.UpdateSettings(sSession.AccessCode, sSession.AccessCodeID, iGroup, iSubGroup, iGL, iSubGL, "FELoss", "FELoss")
            lblFASSettingsValidationMsg.Text = "Successfully Upated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASSettingValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Private Sub ddlcVatGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcVatGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlcVatGL.SelectedIndex > 0 Then
                ddlCVATSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlcVatGL.SelectedValue)
                ddlCVATSubGL.DataTextField = "Description"
                ddlCVATSubGL.DataValueField = "gl_id"
                ddlCVATSubGL.DataBind()
                ddlCVATSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlCVATSubGL.DataSource = dt
                ddlCVATSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlcVatGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCCSTGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCCSTGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCCSTGL.SelectedIndex > 0 Then
                ddlcCSTSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCCSTGL.SelectedValue)
                ddlcCSTSubGL.DataTextField = "Description"
                ddlcCSTSubGL.DataValueField = "gl_id"
                ddlcCSTSubGL.DataBind()
                ddlcCSTSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlcCSTSubGL.DataSource = dt
                ddlcCSTSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCCSTGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCExciseGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCExciseGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCExciseGL.SelectedIndex > 0 Then
                ddlCExciseSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCExciseGL.SelectedValue)
                ddlCExciseSubGL.DataTextField = "Description"
                ddlCExciseSubGL.DataValueField = "gl_id"
                ddlCExciseSubGL.DataBind()
                ddlCExciseSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlCExciseSubGL.DataSource = dt
                ddlCExciseSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCExciseGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCSalesGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCSalesGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCSalesGL.SelectedIndex > 0 Then
                ddlCSalesSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCSalesGL.SelectedValue)
                ddlCSalesSubGL.DataTextField = "Description"
                ddlCSalesSubGL.DataValueField = "gl_id"
                ddlCSalesSubGL.DataBind()
                ddlCSalesSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlCSalesSubGL.DataSource = dt
                ddlCSalesSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCSalesGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlSVATGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSVATGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSVATGL.SelectedIndex > 0 Then
                ddlSVATSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlSVATGL.SelectedValue)
                ddlSVATSubGL.DataTextField = "Description"
                ddlSVATSubGL.DataValueField = "gl_id"
                ddlSVATSubGL.DataBind()
                ddlSVATSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlSVATSubGL.DataSource = dt
                ddlSVATSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSVATGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlSCSTGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSCSTGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSCSTGL.SelectedIndex > 0 Then
                ddlSCSTSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlSCSTGL.SelectedValue)
                ddlSCSTSubGL.DataTextField = "Description"
                ddlSCSTSubGL.DataValueField = "gl_id"
                ddlSCSTSubGL.DataBind()
                ddlSCSTSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlSCSTSubGL.DataSource = dt
                ddlSCSTSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSCSTGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlSExciseGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSExciseGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSExciseGL.SelectedIndex > 0 Then
                ddlSExciseSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlSExciseGL.SelectedValue)
                ddlSExciseSubGL.DataTextField = "Description"
                ddlSExciseSubGL.DataValueField = "gl_id"
                ddlSExciseSubGL.DataBind()
                ddlSExciseSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlSExciseSubGL.DataSource = dt
                ddlSExciseSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSExciseGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlSPurchaseGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSPurchaseGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSPurchaseGL.SelectedIndex > 0 Then
                ddlSPurchaseSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlSPurchaseGL.SelectedValue)
                ddlSPurchaseSubGL.DataTextField = "Description"
                ddlSPurchaseSubGL.DataValueField = "gl_id"
                ddlSPurchaseSubGL.DataBind()
                ddlSPurchaseSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlSPurchaseSubGL.DataSource = dt
                ddlSPurchaseSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSPurchaseGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCashGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCashGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlCashGL.SelectedIndex > 0 Then
                ddlCashSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlCashGL.SelectedValue)
                ddlCashSubGL.DataTextField = "Description"
                ddlCashSubGL.DataValueField = "gl_id"
                ddlCashSubGL.DataBind()
                ddlCashSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlCashSubGL.DataSource = dt
                ddlCashSubGL.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCashGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlBankGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBankGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlBankGL.SelectedIndex > 0 Then
                ddlBankSubGL.DataSource = objSettings.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlBankGL.SelectedValue)
                ddlBankSubGL.DataTextField = "Description"
                ddlBankSubGL.DataValueField = "gl_id"
                ddlBankSubGL.DataBind()
                ddlBankSubGL.Items.Insert(0, "Select Sub General Ledger")
            Else
                ddlBankSubGL.DataSource = dt
                ddlBankSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBankGL_SelectedIndexChanged")
        End Try
    End Sub
End Class
