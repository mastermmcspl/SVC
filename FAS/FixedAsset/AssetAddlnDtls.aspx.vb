Imports System
Imports System.Data
Imports BusinesLayer
Partial Class FixedAsset_AssetAddlnDtls
    Inherits System.Web.UI.Page
    Private sFormName As String = "FixedAsset_AssetAdditionalDtls"
    Private objerrorclass As New BusinesLayer.Components.ErrorClass
    Dim objAsstAddnDtls As New ClsAssetAdditionalDtls
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASGeneral As New clsFASGeneral
    Private Shared sSession As AllSession
    Dim objGen As New clsFASGeneral
    Private objIndex As New clsIndexing
    Private Shared iDocID As Integer
    Dim dt As New DataTable
    Dim objclsEDICTGeneral As New clsEDICTGeneral

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtReferesh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                'AssetNo()
                LoadReferenceNo2()
                LoadDeviceNo()
                Binddevicetype()
                Bindploicytype()
                BindSoftware()
                loadAssetType()
                loademployee()
                LoadCurrency()
                Dim sAssetNo As String = "" : Dim sAssetRefNo As String = ""
                ' sAssetNo = Request.QueryString("AssetNo")
                sAssetRefNo = Request.QueryString("AssetRefNo")
                If sAssetRefNo <> "" Then
                    ddlRefno.SelectedValue = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("AssetRefNo"))))
                    sAssetRefNo = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("AssetRefNo"))))
                    ddlRefno_SelectedIndexChanged(sender, e)
                    ddlAssetNo.SelectedValue = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("AssetRefNo"))))
                    sAssetNo = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode((Request.QueryString("AssetRefNo"))))
                    ddlAssetNo_SelectedIndexChanged(sender, e)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub loadAssetType()
        Dim dt As New DataTable
        Try
            dt = objAsstAddnDtls.LoadFxdAssetType(sSession.AccessCode, sSession.AccessCodeID)
            ddlAssetType.DataTextField = "GL_Desc"
            ddlAssetType.DataValueField = "GL_ID"
            ddlAssetType.DataSource = dt
            ddlAssetType.DataBind()
            ddlAssetType.Items.Insert(0, "Select AssetType")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub Binddevicetype()
        Try
            ddldevicetype.Items.Insert(0, "Printer")
            ddldevicetype.Items.Insert(1, "Scanner")
            ddldevicetype.Items.Insert(2, "Reader")
            ddldevicetype.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub Bindploicytype()
        Try
            ddlploicytype.Items.Insert(0, "Insurance")
            ddlploicytype.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindSoftware()
        Try
            ddlSW.Items.Insert(0, "OS")
            ddlSW.Items.Insert(1, "Opensource")
            ddlSW.Items.Insert(2, "Application Software")
            ddlSW.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub AssetNo()
        Try
            ddlAssetNo.DataSource = objAsstAddnDtls.AssetNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlAssetNo.DataTextField = "AFAA_AssetNo"
            ddlAssetNo.DataValueField = "AFAA_ID"
            ddlAssetNo.DataBind()
            ddlAssetNo.Items.Insert(0, "Select AssetNo")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AssetNo")
        End Try
    End Sub
    Private Sub lnkbtnSupDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnSupDtls.Click
        Try
            lblTab.Text = 3
            lisupplier_Detls.Attributes.Add("class", "active")
            divSupDtls.Attributes.Add("class", "tab-pane active")
            liMaintence_Detls.Attributes.Remove("class")
            divMainteanceDtls.Attributes.Add("class", "tab-pane")
            lnkbtnInstlnDtls.Attributes.Remove("class")
            divInstallationDtls.Attributes.Add("class", "tab-pane")
            liInsurencedtls.Attributes.Remove("class")
            divinsurenceDtls.Attributes.Add("class", "tab-pane")
            liDeviceDtls.Attributes.Remove("class")
            divDevicedtls.Attributes.Add("class", "tab-pane")
            liCust_details.Attributes.Remove("class")
            divCustDtls.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSupDtls_Click")
        End Try
    End Sub
    Private Sub lnkbtnMainDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnMainDtls.Click
        Try
            lblTab.Text = 2
            liMaintence_Detls.Attributes.Add("class", "active")
            divMainteanceDtls.Attributes.Add("class", "tab-pane active")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")
            liinstaln_Detls.Attributes.Remove("class")
            divInstallationDtls.Attributes.Add("class", "tab-pane")
            liInsurencedtls.Attributes.Remove("class")
            divinsurenceDtls.Attributes.Add("class", "tab-pane")
            liDeviceDtls.Attributes.Remove("class")
            divDevicedtls.Attributes.Add("class", "tab-pane")
            liCust_details.Attributes.Remove("class")
            divCustDtls.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnMainDtls_Click")
        End Try
    End Sub

    Private Sub lnkbtnInstlnDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnInstlnDtls.Click
        Try
            lblTab.Text = 5
            liinstaln_Detls.Attributes.Add("class", "active")
            divInstallationDtls.Attributes.Add("class", "tab-pane active")
            liMaintence_Detls.Attributes.Remove("class")
            divMainteanceDtls.Attributes.Add("class", "tab-pane")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")
            liInsurencedtls.Attributes.Remove("class")
            divinsurenceDtls.Attributes.Add("class", "tab-pane")
            liDeviceDtls.Attributes.Remove("class")
            divDevicedtls.Attributes.Add("class", "tab-pane")
            liCust_details.Attributes.Remove("class")
            divCustDtls.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInstlnDtls_Click")
        End Try
    End Sub
    Private Sub lnkbtnInsurenceDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnInsurenceDtls.Click
        Try
            lblTab.Text = 4
            liInsurencedtls.Attributes.Add("class", "active")
            divinsurenceDtls.Attributes.Add("class", "tab-pane active")
            liinstaln_Detls.Attributes.Remove("class")
            divInstallationDtls.Attributes.Add("class", "tab-pane")
            liMaintence_Detls.Attributes.Remove("class")
            divMainteanceDtls.Attributes.Add("class", "tab-pane")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")
            liDeviceDtls.Attributes.Remove("class")
            divDevicedtls.Attributes.Add("class", "tab-pane")
            liCust_details.Attributes.Remove("class")
            divCustDtls.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInstlnDtls_Click")
        End Try
    End Sub
    Private Sub lnkbtnDeviceDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnDeviceDtls.Click
        Try
            lblTab.Text = 1
            liDeviceDtls.Attributes.Add("class", "active")
            divDevicedtls.Attributes.Add("class", "tab-pane active")
            liInsurencedtls.Attributes.Remove("class")
            divinsurenceDtls.Attributes.Add("class", "tab-pane")
            liinstaln_Detls.Attributes.Remove("class")
            divInstallationDtls.Attributes.Add("class", "tab-pane")
            liMaintence_Detls.Attributes.Remove("class")
            divMainteanceDtls.Attributes.Add("class", "tab-pane")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")
            liCust_details.Attributes.Remove("class")
            divCustDtls.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDeviceDtls_Click")
        End Try
    End Sub
    Private Sub lnkbtnCustodydtls_Click(sender As Object, e As EventArgs) Handles lnkbtnCustodydtls.Click
        Try
            lblTab.Text = 6
            liCust_details.Attributes.Add("class", "active")
            divCustDtls.Attributes.Add("class", "tab-pane active")
            liInsurencedtls.Attributes.Remove("class")
            divinsurenceDtls.Attributes.Add("class", "tab-pane")
            liinstaln_Detls.Attributes.Remove("class")
            divInstallationDtls.Attributes.Add("class", "tab-pane")
            liMaintence_Detls.Attributes.Remove("class")
            divMainteanceDtls.Attributes.Add("class", "tab-pane")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")
            liDeviceDtls.Attributes.Remove("class")
            divDevicedtls.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustodydtls_Click")
        End Try
    End Sub
    Private Sub lnkbtnLoanAsst_Click(sender As Object, e As EventArgs) Handles lnkbtnLoanAsst.Click
        Try
            lblTab.Text = 7
            liAsset_Loan.Attributes.Add("class", "active")
            divLoanAsst.Attributes.Add("class", "tab-pane active")
            liCust_details.Attributes.Remove("class")
            divCustDtls.Attributes.Add("class", "tab-pane")
            liInsurencedtls.Attributes.Remove("class")
            divinsurenceDtls.Attributes.Add("class", "tab-pane")
            liinstaln_Detls.Attributes.Remove("class")
            divInstallationDtls.Attributes.Add("class", "tab-pane")
            liMaintence_Detls.Attributes.Remove("class")
            divMainteanceDtls.Attributes.Add("class", "tab-pane")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")
            liDeviceDtls.Attributes.Remove("class")
            divDevicedtls.Attributes.Add("class", "tab-pane")

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLoanAsst_Click")
        End Try
    End Sub
    'Private Sub ddlAssetNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssetNo.SelectedIndexChanged
    '    Try
    '        If ddlAssetNo.SelectedIndex > 0 Then
    '            LoadReferenceNo(ddlAssetNo.SelectedValue)
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAssetNo_SelectedIndexChanged")
    '    End Try
    'End Sub
    'Public Sub LoadReferenceNo(ByVal iAssetNo As Integer)
    '    Try
    '        ddlRefno.DataSource = objAsstAddnDtls.LoadReferenceNo(sSession.AccessCode, sSession.AccessCodeID, iAssetNo)
    '        ddlRefno.DataTextField = "AFAA_AssetRefNo"
    '        ddlRefno.DataValueField = "AFAA_ID"
    '        ddlRefno.DataBind()
    '        ddlRefno.Items.Insert(0, "Select ReferenceNo")
    '    Catch ex As Exception
    '        lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadReferenceNo")
    '    End Try
    'End Sub

    Private Sub btnSearchRefNo_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearchRefNo.Click
        Dim dt As New DataTable
        Try
            If txtPartySearch.Text <> "" Then
                dt = objAsstAddnDtls.GetSearchReferanceNoList(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, txtPartySearch.Text)
                ddlRefno.DataSource = dt
                ddlRefno.DataTextField = "AFAA_AssetRefNo"
                ddlRefno.DataValueField = "AFAA_ID"
                ddlRefno.DataBind()
                ddlRefno.Items.Insert(0, "Select ReferenceNo")
            Else
                LoadReferenceNo2()
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSearchRefNo_Click")
        End Try
    End Sub
    Private Sub LoadReferenceNo2()
        Try
            ddlRefno.DataSource = objAsstAddnDtls.LoadReferenceNo2(sSession.AccessCode, sSession.AccessCodeID)
            ddlRefno.DataTextField = "AFAA_AssetRefNo"
            ddlRefno.DataValueField = "AFAA_ID"
            ddlRefno.DataBind()
            ddlRefno.Items.Insert(0, "Select ReferenceNo")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadReferenceNo2")
        End Try
    End Sub
    Private Sub LoadDeviceNo()
        Try
            ddlDeviceNo.DataSource = objAsstAddnDtls.LoadDeviceNo(sSession.AccessCode, sSession.AccessCodeID)
            ddlDeviceNo.DataTextField = "ADD_DeviceNo"
            ddlDeviceNo.DataValueField = "ADD_ID"
            ddlDeviceNo.DataBind()
            ddlDeviceNo.Items.Insert(0, "Select DeviceNo")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDeviceNo")
        End Try
    End Sub
    Private Sub ddlRefno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRefno.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlRefno.SelectedIndex > 0 Then
                AssetNo2(ddlRefno.SelectedValue)
            Else
                ddlRefno.Items.Clear()
                clear()
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRefno_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub AssetNo2(ByVal iRefn As Integer)
        Try
            ddlAssetNo.DataSource = objAsstAddnDtls.AssetNo2(sSession.AccessCode, sSession.AccessCodeID, iRefn)
            ddlAssetNo.DataTextField = "AFAA_AssetNo"
            ddlAssetNo.DataValueField = "AFAA_ID"
            ddlAssetNo.DataBind()
            ddlAssetNo.Items.Insert(0, "Select AssetNo")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AssetNo2")
        End Try
    End Sub
    Private Sub imgbtnDeviceDetails_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeviceDetails.Click
        Dim Arr As Array
        Dim objAsstAddnDtls1 As ClsAssetAdditionalDtls.DeviceDetails
        Try
            If ddlAssetNo.SelectedIndex > 0 Then
                objAsstAddnDtls1.ADD_ID = 0
                If ddlAssetNo.SelectedIndex > 0 Then
                    objAsstAddnDtls1.ADD_MasterID = ddlAssetNo.SelectedValue
                Else
                    objAsstAddnDtls1.ADD_MasterID = 0
                End If
                If ddldevicetype.SelectedIndex > 0 Then
                    objAsstAddnDtls1.ADD_DeviceType = ddldevicetype.SelectedIndex
                Else
                    objAsstAddnDtls1.ADD_DeviceType = 0
                End If
                If txtxDeviceNo.Text <> "" Then
                    objAsstAddnDtls1.ADD_DeviceNo = txtxDeviceNo.Text
                Else
                    objAsstAddnDtls1.ADD_DeviceNo = ""
                End If
                If txtModelname.Text <> "" Then
                    objAsstAddnDtls1.ADD_ModelName = txtModelname.Text
                Else
                    objAsstAddnDtls1.ADD_ModelName = ""
                End If
                If txtmanufacturedby.Text <> "" Then
                    objAsstAddnDtls1.ADD_ManufacturedBy = txtmanufacturedby.Text
                Else
                    objAsstAddnDtls1.ADD_ManufacturedBy = ""
                End If

                If txtdateofpurchase.Text <> "" Then
                    objAsstAddnDtls1.ADD_DateofPurchase = txtdateofpurchase.Text
                Else
                    objAsstAddnDtls1.ADD_DateofPurchase = "01/01/1991"
                End If
                If txtWarranty.Text <> "" Then
                    objAsstAddnDtls1.ADD_WarrantyExpireson = txtWarranty.Text
                Else
                    objAsstAddnDtls1.ADD_WarrantyExpireson = "01/01/1991"

                End If
                If txtempname.Text <> "" Then
                    objAsstAddnDtls1.ADD_Employeename = txtempname.Text
                Else
                    objAsstAddnDtls1.ADD_Employeename = ""
                End If
                If txtDetails.Text <> "" Then
                    objAsstAddnDtls1.ADD_Details = txtDetails.Text
                Else
                    objAsstAddnDtls1.ADD_Details = ""
                End If
                If rbtnstandalone.Checked = True Then
                    objAsstAddnDtls1.ADD_StandAloneServer = 1
                ElseIf rbtnServer.Checked = True Then
                    objAsstAddnDtls1.ADD_StandAloneServer = 2
                ElseIf rbtnAttachedServer.Checked = True Then
                    objAsstAddnDtls1.ADD_StandAloneServer = 3
                Else
                    objAsstAddnDtls1.ADD_StandAloneServer = 0
                End If

                If txtDescription.Text <> "" Then
                    objAsstAddnDtls1.ADD_DescriptionDev = txtDescription.Text
                Else
                    objAsstAddnDtls1.ADD_DescriptionDev = ""
                End If

                If txtbxSname.Text <> "" Then
                    objAsstAddnDtls1.ADD_SuplierName = txtbxSname.Text
                Else
                    objAsstAddnDtls1.ADD_SuplierName = ""
                End If
                If txtbxConPerson.Text <> "" Then
                    objAsstAddnDtls1.ADD_ContactPerson = txtbxConPerson.Text
                Else
                    objAsstAddnDtls1.ADD_ContactPerson = ""
                End If
                If txtbxPhoneNo.Text <> "" Then
                    objAsstAddnDtls1.ADD_Phone = txtbxPhoneNo.Text
                Else
                    objAsstAddnDtls1.ADD_Phone = ""
                End If
                If txtbxFax.Text <> "" Then
                    objAsstAddnDtls1.ADD_Fax = txtbxFax.Text
                Else
                    objAsstAddnDtls1.ADD_Fax = ""
                End If
                If txtbxAddress.Text <> "" Then
                    objAsstAddnDtls1.ADD_Address = txtbxAddress.Text
                Else
                    objAsstAddnDtls1.ADD_Address = ""
                End If
                If txtbxEmail.Text <> "" Then
                    objAsstAddnDtls1.ADD_EmailID = txtbxEmail.Text
                Else
                    objAsstAddnDtls1.ADD_EmailID = ""
                End If
                If txtbxwebsite.Text <> "" Then
                    objAsstAddnDtls1.ADD_Website = txtbxwebsite.Text
                Else
                    objAsstAddnDtls1.ADD_Website = ""
                End If
                objAsstAddnDtls1.ADD_CreatedBy = sSession.UserID
                objAsstAddnDtls1.ADD_CreatedOn = Date.Today
                objAsstAddnDtls1.ADD_UpdatedBy = sSession.UserID
                objAsstAddnDtls1.ADD_UpdatedOn = Date.Today
                objAsstAddnDtls1.ADD_DelFlag = "W"
                objAsstAddnDtls1.ADD_Status = "C"
                objAsstAddnDtls1.ADD_CompID = sSession.AccessCodeID
                objAsstAddnDtls1.ADD_Opeartion = "U"
                objAsstAddnDtls1.ADD_IPAddress = sSession.IPAddress
                objAsstAddnDtls1.ADD_YearID = sSession.YearID
                Arr = objAsstAddnDtls.SaveDeviceDetails(sSession.AccessCode, sSession.AccessCodeID, objAsstAddnDtls1)
                If Arr(0) = "2" Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                    imgbtnDeviceDetails.ImageUrl = "~/Images/Add24.png"
                ElseIf Arr(0) = "3" Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                    LoadDeviceNo()
                End If
            Else
                lblAssetAdditionDtlsMsg.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeviceDetails_Click")
        End Try
    End Sub

    Private Sub imgbtnmaintainance_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnmaintainance.Click
        Dim Arr As Array
        Dim objAsstAddnDtls2 As ClsAssetAdditionalDtls.MaintainanceDetails
        Try
            If ddlAssetNo.SelectedIndex > 0 Then
                objAsstAddnDtls2.AMD_ID = 0
                If ddlAssetNo.SelectedIndex > 0 Then
                    objAsstAddnDtls2.AMD_MasterID = ddlAssetNo.SelectedValue
                Else
                    objAsstAddnDtls2.AMD_MasterID = 0
                End If
                If txtMaintained.Text <> "" Then
                    objAsstAddnDtls2.AMD_MaintainedBy = txtMaintained.Text
                Else
                    objAsstAddnDtls2.AMD_MaintainedBy = ""
                End If
                If txtMainconPerson.Text <> "" Then
                    objAsstAddnDtls2.AMD_ContactPerson = txtMainconPerson.Text
                Else
                    objAsstAddnDtls2.AMD_ContactPerson = ""
                End If
                If txtManufacturedPhoneno.Text <> "" Then
                    objAsstAddnDtls2.AMD_Phone = txtManufacturedPhoneno.Text
                Else
                    objAsstAddnDtls2.AMD_Phone = ""
                End If
                If txtManufacturedFAX.Text <> "" Then
                    objAsstAddnDtls2.AMD_Fax = txtManufacturedFAX.Text
                Else
                    objAsstAddnDtls2.AMD_Fax = ""
                End If
                If txtManufacturedAddress.Text <> "" Then
                    objAsstAddnDtls2.AMD_Address = txtManufacturedAddress.Text
                Else
                    objAsstAddnDtls2.AMD_Address = ""
                End If
                If txtEmail.Text <> "" Then
                    objAsstAddnDtls2.AMD_EmailID = txtEmail.Text
                Else
                    objAsstAddnDtls2.AMD_EmailID = ""
                End If
                If txtWebsite.Text <> "" Then
                    objAsstAddnDtls2.AMD_Website = txtWebsite.Text
                Else
                    objAsstAddnDtls2.AMD_Website = ""
                End If
                If txtbxAMCompname.Text <> "" Then
                    objAsstAddnDtls2.AMD_Companyname = txtbxAMCompname.Text
                Else
                    objAsstAddnDtls2.AMD_Companyname = ""
                End If
                If txtAMCAmount.Text <> "" Then
                    objAsstAddnDtls2.AMD_AmcAmount = txtAMCAmount.Text
                Else
                    objAsstAddnDtls2.AMD_AmcAmount = "0.00"
                End If
                If txtbxAMCfrmDate.Text <> "" Then
                    objAsstAddnDtls2.AMD_AmcTermDate = txtbxAMCfrmDate.Text
                Else
                    objAsstAddnDtls2.AMD_AmcTermDate = "01/01/1991"
                End If
                If txtbxAMCtoDate.Text <> "" Then
                    objAsstAddnDtls2.AMD_AmcTo = txtbxAMCtoDate.Text
                Else
                    objAsstAddnDtls2.AMD_AmcTo = "01/01/1991"
                End If
                If rbtnOnetime.Checked = True Then
                    objAsstAddnDtls2.AMD_AmcPaymentterm = 1
                ElseIf rbtnInstlmnt.Checked = True Then
                    objAsstAddnDtls2.AMD_AmcPaymentterm = 2
                End If
                If txtNoInstlmt.Text <> "" Then
                    objAsstAddnDtls2.AMD_NoInstalment = txtNoInstlmt.Text
                Else
                    objAsstAddnDtls2.AMD_NoInstalment = 0
                End If
                If txtInstamount.Text <> "" Then
                    objAsstAddnDtls2.AMD_InstalmentAmnt = txtInstamount.Text
                Else
                    objAsstAddnDtls2.AMD_InstalmentAmnt = "0.00"
                End If
                If txtTotalPaidIstAmnt.Text <> "" Then
                    objAsstAddnDtls2.AMD_TotalPaidinstalment = txtTotalPaidIstAmnt.Text
                Else
                    objAsstAddnDtls2.AMD_TotalPaidinstalment = "0.00"
                End If
                If txtAmount.Text <> "" Then
                    objAsstAddnDtls2.AMD_TotalAmnt = txtAmount.Text
                Else
                    objAsstAddnDtls2.AMD_TotalAmnt = "0.00"
                End If

                objAsstAddnDtls2.AMD_CreatedBy = sSession.UserID
                objAsstAddnDtls2.AMD_CreatedOn = Date.Today
                objAsstAddnDtls2.AMD_UpdatedBy = sSession.UserID
                objAsstAddnDtls2.AMD_UpdatedOn = Date.Today
                objAsstAddnDtls2.AMD_DelFlag = "W"
                objAsstAddnDtls2.AMD_Status = "C"
                objAsstAddnDtls2.AMD_YearID = sSession.YearID
                objAsstAddnDtls2.AMD_CompID = sSession.AccessCodeID
                objAsstAddnDtls2.AMD_Opeartion = "U"
                objAsstAddnDtls2.AMD_IPAddress = sSession.IPAddress

                Arr = objAsstAddnDtls.SaveMaintenanceDetails(sSession.AccessCode, sSession.AccessCodeID, objAsstAddnDtls2)
                If Arr(0) = "2" Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                    imgbtnmaintainance.ImageUrl = "~/Images/Add24.png"
                ElseIf Arr(0) = "3" Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                End If
            Else
                lblAssetAdditionDtlsMsg.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnmaintainance_Click")
        End Try
    End Sub

    Private Sub imgbtninsurance_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtninsurance.Click
        Dim Arr As Array
        Dim objAsstAddnDtls3 As ClsAssetAdditionalDtls.InsuranceDetails
        Try

            If ddlAssetNo.SelectedIndex > 0 Then
                objAsstAddnDtls3.AID_ID = 0
                If ddlAssetNo.SelectedIndex > 0 Then
                    objAsstAddnDtls3.AID_MasterID = ddlAssetNo.SelectedValue
                Else
                    objAsstAddnDtls3.AID_MasterID = 0
                End If
                If txtinscomname.Text <> "" Then
                    objAsstAddnDtls3.AID_InsuranceComp = txtinscomname.Text
                Else
                    objAsstAddnDtls3.AID_InsuranceComp = ""
                End If
                If txtinsConPerns.Text <> "" Then
                    objAsstAddnDtls3.AID_ContactPerson = txtinsConPerns.Text
                Else
                    objAsstAddnDtls3.AID_ContactPerson = ""
                End If
                If txtInsPhno.Text <> "" Then
                    objAsstAddnDtls3.AID_Phone = txtInsPhno.Text
                Else
                    objAsstAddnDtls3.AID_Phone = ""

                End If
                If txtinsFAX.Text <> "" Then
                    objAsstAddnDtls3.AID_Fax = txtinsFAX.Text
                Else
                    objAsstAddnDtls3.AID_Fax = ""
                End If
                If txtInsAddress.Text <> "" Then
                    objAsstAddnDtls3.AID_Address = txtInsAddress.Text
                Else
                    objAsstAddnDtls3.AID_Address = ""
                End If
                If txtinsEmail.Text <> "" Then
                    objAsstAddnDtls3.AID_Email = txtinsEmail.Text
                Else
                    objAsstAddnDtls3.AID_Email = ""
                End If
                If txtinsWebsite.Text <> "" Then
                    objAsstAddnDtls3.AID_Website = txtinsWebsite.Text
                Else
                    objAsstAddnDtls3.AID_Website = ""
                End If
                If ddlploicytype.SelectedIndex > 0 Then
                    objAsstAddnDtls3.AID_PolicyType = ddlploicytype.SelectedIndex
                Else
                    objAsstAddnDtls3.AID_PolicyType = 0
                End If
                If txtPolcyno.Text <> "" Then
                    objAsstAddnDtls3.AID_PolicyNo = txtPolcyno.Text
                Else
                    objAsstAddnDtls3.AID_PolicyNo = ""
                End If
                If txtPolicyAmt.Text <> "" Then
                    objAsstAddnDtls3.AID_PolicyAmount = txtPolicyAmt.Text
                Else
                    objAsstAddnDtls3.AID_PolicyAmount = "0.00"
                End If
                If txtPremiumpaid.Text <> "" Then
                    objAsstAddnDtls3.AID_Premiumpaid = txtPremiumpaid.Text
                Else
                    objAsstAddnDtls3.AID_Premiumpaid = "0.00"
                End If
                If txtTermDate.Text <> "" Then
                    objAsstAddnDtls3.AID_TermDate = txtTermDate.Text
                Else
                    objAsstAddnDtls3.AID_TermDate = "01/01/1991"
                End If
                If txtTermToDate.Text <> "" Then
                    objAsstAddnDtls3.AID_ToDate = txtTermToDate.Text
                Else
                    objAsstAddnDtls3.AID_ToDate = "01/01/1991"
                End If
                objAsstAddnDtls3.AID_CreatedBy = sSession.UserID
                objAsstAddnDtls3.AID_CreatedOn = Date.Today
                objAsstAddnDtls3.AID_UpdatedBy = sSession.UserID
                objAsstAddnDtls3.AID_UpdatedOn = Date.Today
                objAsstAddnDtls3.AID_DelFlag = "W"
                objAsstAddnDtls3.AID_Status = "C"
                objAsstAddnDtls3.AID_YearID = sSession.YearID
                objAsstAddnDtls3.AID_CompID = sSession.AccessCodeID
                objAsstAddnDtls3.AID_Opeartion = "U"
                objAsstAddnDtls3.AID_IPAddress = sSession.IPAddress

                Arr = objAsstAddnDtls.SaveInsuranceDetails(sSession.AccessCode, sSession.AccessCodeID, objAsstAddnDtls3)
                If Arr(0) = "2" Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                    imgbtninsurance.ImageUrl = "~/Images/Add24.png"
                ElseIf Arr(0) = "3" Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                End If
            Else
                lblAssetAdditionDtlsMsg.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
            End If

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtninsurance_Click")
        End Try
    End Sub

    Private Sub imgbtnInstallation_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnInstallation.Click
        Dim objAsstAddnDtls4 As ClsAssetAdditionalDtls.InstallationDetails
        Dim Arr As Array
        Try
            If ddlAssetNo.SelectedIndex > 0 Then
                objAsstAddnDtls4.AIND_ID = 0
                If ddlAssetNo.SelectedIndex > 0 Then
                    objAsstAddnDtls4.AIND_MasterID = ddlAssetNo.SelectedValue
                Else
                    objAsstAddnDtls4.AIND_MasterID = 0
                End If
                If ddlDeviceNo.SelectedIndex > 0 Then
                    objAsstAddnDtls4.AIND_DeviceNo = ddlDeviceNo.SelectedValue
                Else
                    objAsstAddnDtls4.AIND_DeviceNo = 0
                End If
                If ddlSW.SelectedIndex > 0 Then
                    objAsstAddnDtls4.AIND_Software = ddlSW.SelectedIndex
                Else
                    objAsstAddnDtls4.AIND_Software = 0
                End If
                If txtversion.Text <> "" Then
                    objAsstAddnDtls4.AIND_Version = txtversion.Text
                Else
                    objAsstAddnDtls4.AIND_Version = ""
                End If
                If txtDateofInstlln.Text <> "" Then
                    objAsstAddnDtls4.AIND_DateofInstln = txtDateofInstlln.Text
                Else
                    objAsstAddnDtls4.AIND_DateofInstln = "01/01/1991"
                End If
                If txtUnintleddate.Text <> "" Then
                    objAsstAddnDtls4.AIND_UnInstlnOn = txtUnintleddate.Text
                Else
                    objAsstAddnDtls4.AIND_UnInstlnOn = "01/01/1991"
                End If
                If txtreintled.Text <> "" Then
                    objAsstAddnDtls4.AIND_ReInstlnOn = txtreintled.Text
                Else
                    objAsstAddnDtls4.AIND_ReInstlnOn = "01/01/1991"
                End If
                If txtinstedBy.Text <> "" Then
                    objAsstAddnDtls4.AIND_InstlnBy = txtinstedBy.Text
                Else
                    objAsstAddnDtls4.AIND_InstlnBy = ""
                End If
                If txtdbdetails.Text <> "" Then
                    objAsstAddnDtls4.AIND_DatabaseDtls = txtdbdetails.Text
                Else
                    objAsstAddnDtls4.AIND_DatabaseDtls = ""
                End If
                If txtInsDescription.Text <> "" Then
                    objAsstAddnDtls4.AIND_Description = txtInsDescription.Text
                Else
                    objAsstAddnDtls4.AIND_Description = ""
                End If
                If txtinsplace.Text <> "" Then
                    objAsstAddnDtls4.AIND_InstlnPlace = txtinsplace.Text
                Else
                    objAsstAddnDtls4.AIND_InstlnPlace = ""
                End If
                If txtConPerson.Text <> "" Then
                    objAsstAddnDtls4.AIND_ContactPerson = txtConPerson.Text
                Else
                    objAsstAddnDtls4.AIND_ContactPerson = ""
                End If
                If txtinsdtlsPhno.Text <> "" Then
                    objAsstAddnDtls4.AIND_Phone = txtinsdtlsPhno.Text
                Else
                    objAsstAddnDtls4.AIND_Phone = ""
                End If
                If txtInsdtlsFAX.Text <> "" Then
                    objAsstAddnDtls4.AIND_FAX = txtInsdtlsFAX.Text
                Else
                    objAsstAddnDtls4.AIND_FAX = ""
                End If
                If txtInsdtlsAddress.Text <> "" Then
                    objAsstAddnDtls4.AIND_Address = txtInsdtlsAddress.Text
                Else
                    objAsstAddnDtls4.AIND_Address = ""
                End If
                If txtInsdtlsEmail.Text <> "" Then
                    objAsstAddnDtls4.AIND_Email = txtInsdtlsEmail.Text
                Else
                    objAsstAddnDtls4.AIND_Email = ""
                End If
                If txtInsdtlsWebsite.Text <> "" Then
                    objAsstAddnDtls4.AIND_Website = txtInsdtlsWebsite.Text
                Else
                    objAsstAddnDtls4.AIND_Website = ""
                End If
                If txtinsdtlsMaintainedby.Text <> "" Then
                    objAsstAddnDtls4.AIND_Maintainedby = txtinsdtlsMaintainedby.Text
                Else
                    objAsstAddnDtls4.AIND_Maintainedby = ""
                End If
                If txtmainbyconperson.Text <> "" Then
                    objAsstAddnDtls4.AIND_MaintainedContactPerson = txtmainbyconperson.Text
                Else
                    objAsstAddnDtls4.AIND_MaintainedContactPerson = ""
                End If
                If txtMainaddress.Text <> "" Then
                    objAsstAddnDtls4.AIND_MaintainedAddress = txtMainaddress.Text
                Else
                    objAsstAddnDtls4.AIND_MaintainedAddress = ""
                End If
                If txtMainphno.Text <> "" Then
                    objAsstAddnDtls4.AIND_MaintainedPhone = txtMainphno.Text
                Else
                    objAsstAddnDtls4.AIND_MaintainedPhone = ""
                End If
                If txtmainFAX.Text <> "" Then
                    objAsstAddnDtls4.AIND_MaintainedFax = txtmainFAX.Text
                Else
                    objAsstAddnDtls4.AIND_MaintainedFax = ""
                End If
                If txtMainEmail.Text <> "" Then
                    objAsstAddnDtls4.AIND_MaintainedEmail = txtMainEmail.Text
                Else
                    objAsstAddnDtls4.AIND_MaintainedEmail = ""
                End If
                If txtMainwebsite.Text <> "" Then
                    objAsstAddnDtls4.AIND_MaintainedWebsite = txtMainwebsite.Text
                Else
                    objAsstAddnDtls4.AIND_MaintainedWebsite = ""
                End If
                objAsstAddnDtls4.AIND_CreatedBy = sSession.UserID
                objAsstAddnDtls4.AIND_CreatedOn = Date.Today
                objAsstAddnDtls4.AIND_UpdatedBy = sSession.UserID
                objAsstAddnDtls4.AIND_UpdatedOn = Date.Today
                objAsstAddnDtls4.AIND_DelFlag = "W"
                objAsstAddnDtls4.AIND_Status = "C"
                objAsstAddnDtls4.AIND_YearID = sSession.YearID
                objAsstAddnDtls4.AIND_CompID = sSession.AccessCodeID
                objAsstAddnDtls4.AIND_Opeartion = "U"
                objAsstAddnDtls4.AIND_IPAddress = sSession.IPAddress
                Arr = objAsstAddnDtls.SaveInstallationDetails(sSession.AccessCode, sSession.AccessCodeID, objAsstAddnDtls4)
                If Arr(0) = "2" Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                    imgbtnInstallation.ImageUrl = "~/Images/Add24.png"
                ElseIf Arr(0) = "3" Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                End If
            Else
                lblAssetAdditionDtlsMsg.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnInstallation_Click")
        End Try
    End Sub

    Private Sub ddlAssetNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssetNo.SelectedIndexChanged
        Dim dt, dt1, dt2, dt3, dt4, dt5 As New DataTable
        Try
            If ddlAssetNo.SelectedIndex > 0 Then
                dt = objAsstAddnDtls.LoadDeviceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssetNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If IsDBNull(dt.Rows(i)("ADD_DeviceType").ToString()) = False Then
                            If dt.Rows(i)("ADD_DeviceType").ToString() = "" Then
                                ddldevicetype.SelectedIndex = 0
                            Else
                                ddldevicetype.SelectedIndex = dt.Rows(i)("ADD_DeviceType").ToString()
                            End If
                        End If
                        If dt.Rows(i)("ADD_DeviceNo").ToString() = "" Then
                            txtxDeviceNo.Text = ""
                        Else
                            txtxDeviceNo.Text = dt.Rows(i)("ADD_DeviceNo").ToString()
                        End If
                        If dt.Rows(i)("ADD_ModelName").ToString() = "" Then
                            txtModelname.Text = ""
                        Else
                            txtModelname.Text = dt.Rows(i)("ADD_ModelName").ToString()
                        End If
                        If dt.Rows(i)("ADD_ManufacturedBy").ToString() = "" Then
                            txtmanufacturedby.Text = ""
                        Else
                            txtmanufacturedby.Text = dt.Rows(i)("ADD_ManufacturedBy").ToString()
                        End If
                        If dt.Rows(i)("ADD_DateofPurchase").ToString() = "" Then
                            txtdateofpurchase.Text = ""
                        Else
                            txtdateofpurchase.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("ADD_DateofPurchase").ToString(), "D")
                        End If
                        If dt.Rows(i)("ADD_Details").ToString() = "" Then
                            txtDetails.Text = ""
                        Else
                            txtDetails.Text = dt.Rows(i)("ADD_Details").ToString()
                        End If
                        If dt.Rows(i)("ADD_WarrantyExpireson").ToString() = "" Then
                            txtWarranty.Text = ""
                        Else
                            txtWarranty.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("ADD_WarrantyExpireson").ToString(), "D")
                        End If
                        If dt.Rows(i)("ADD_Employeename").ToString() = "" Then
                            txtempname.Text = ""
                        Else
                            txtempname.Text = dt.Rows(i)("ADD_Employeename").ToString()
                        End If

                        If IsDBNull(dt.Rows(i)("ADD_StandAloneServer").ToString()) = False Then
                            If dt.Rows(i)("ADD_StandAloneServer").ToString() = 1 Then
                                rbtnstandalone.Checked = True
                            ElseIf dt.Rows(i)("ADD_StandAloneServer").ToString() = 2 Then
                                rbtnServer.Checked = True
                            ElseIf dt.Rows(i)("ADD_StandAloneServer").ToString() = 3 Then
                                rbtnAttachedServer.Checked = True
                            End If
                        End If
                        If dt.Rows(i)("ADD_SuplierName").ToString() = "" Then
                            txtbxSname.Text = ""
                        Else
                            txtbxSname.Text = dt.Rows(i)("ADD_SuplierName").ToString()
                        End If
                        If dt.Rows(i)("ADD_ContactPerson").ToString() = "" Then
                            txtbxConPerson.Text = ""
                        Else
                            txtbxConPerson.Text = dt.Rows(i)("ADD_ContactPerson").ToString()
                        End If
                        If dt.Rows(i)("ADD_Address").ToString() = "" Then
                            txtbxAddress.Text = ""
                        Else
                            txtbxAddress.Text = dt.Rows(i)("ADD_Address").ToString()
                        End If
                        If dt.Rows(i)("ADD_Phone").ToString() = "" Then
                            txtbxPhoneNo.Text = ""
                        Else
                            txtbxPhoneNo.Text = dt.Rows(i)("ADD_Phone").ToString()
                        End If
                        If dt.Rows(i)("ADD_Fax").ToString() = "" Then
                            txtbxFax.Text = ""
                        Else
                            txtbxFax.Text = dt.Rows(i)("ADD_Fax").ToString()
                        End If
                        If dt.Rows(i)("ADD_EmailID").ToString() = "" Then
                            txtbxEmail.Text = ""
                        Else
                            txtbxEmail.Text = dt.Rows(i)("ADD_EmailID").ToString()
                        End If
                        If dt.Rows(i)("ADD_Website").ToString() = "" Then
                            txtbxwebsite.Text = ""
                        Else
                            txtbxwebsite.Text = dt.Rows(i)("ADD_Website").ToString()
                        End If
                        If dt.Rows(i)("ADD_DescriptionDev").ToString() = "" Then
                            txtDescription.Text = ""
                        Else
                            txtDescription.Text = dt.Rows(i)("ADD_DescriptionDev").ToString()
                        End If
                    Next
                Else
                    clear1()
                End If
                dt1 = objAsstAddnDtls.LoadInstallationDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssetNo.SelectedValue)
                If dt1.Rows.Count > 0 Then
                    For i = 0 To dt1.Rows.Count - 1
                        If dt1.Rows(i)("AIND_DeviceNo").ToString() = "" Then
                            ddlDeviceNo.SelectedIndex = 0
                        Else
                            ddlDeviceNo.SelectedValue = dt1.Rows(i)("AIND_DeviceNo").ToString()
                        End If
                        If dt1.Rows(i)("AIND_Software").ToString() = "" Then
                            ddlSW.SelectedIndex = 0
                        Else
                            ddlSW.SelectedIndex = dt1.Rows(i)("AIND_Software").ToString()
                        End If
                        If dt1.Rows(i)("AIND_Version").ToString() = "" Then
                            txtversion.Text = ""
                        Else
                            txtversion.Text = dt1.Rows(i)("AIND_Version").ToString()
                        End If
                        If dt1.Rows(i)("AIND_DateofInstln").ToString() = "" Then
                            txtDateofInstlln.Text = ""
                        Else
                            txtDateofInstlln.Text = dt1.Rows(i)("AIND_DateofInstln").ToString()
                        End If
                        If dt1.Rows(i)("AIND_UnInstlnOn").ToString() = "" Then
                            txtUnintleddate.Text = ""
                        Else
                            txtUnintleddate.Text = dt1.Rows(i)("AIND_UnInstlnOn").ToString()
                        End If
                        If dt1.Rows(i)("AIND_ReInstlnOn").ToString() = "" Then
                            txtreintled.Text = ""
                        Else
                            txtreintled.Text = dt1.Rows(i)("AIND_ReInstlnOn").ToString()
                        End If
                        If dt1.Rows(i)("AIND_InstlnBy").ToString() = "" Then
                            txtinstedBy.Text = ""
                        Else
                            txtinstedBy.Text = dt1.Rows(i)("AIND_InstlnBy").ToString()
                        End If
                        If dt1.Rows(i)("AIND_DatabaseDtls").ToString() = "" Then
                            txtdbdetails.Text = ""
                        Else
                            txtdbdetails.Text = dt1.Rows(i)("AIND_DatabaseDtls").ToString()
                        End If
                        If dt1.Rows(i)("AIND_Description").ToString() = "" Then
                            txtInsDescription.Text = ""
                        Else
                            txtInsDescription.Text = dt1.Rows(i)("AIND_Description").ToString()
                        End If
                        If dt1.Rows(i)("AIND_InstlnPlace").ToString() = "" Then
                            txtinsplace.Text = ""
                        Else
                            txtinsplace.Text = dt1.Rows(i)("AIND_InstlnPlace").ToString()
                        End If
                        If dt1.Rows(i)("AIND_ContactPerson").ToString() = "" Then
                            txtConPerson.Text = ""
                        Else
                            txtConPerson.Text = dt1.Rows(i)("AIND_ContactPerson").ToString()
                        End If
                        If dt1.Rows(i)("AIND_Address").ToString() = "" Then
                            txtInsdtlsAddress.Text = ""
                        Else
                            txtInsdtlsAddress.Text = dt1.Rows(i)("AIND_Address").ToString()
                        End If
                        If dt1.Rows(i)("AIND_Phone").ToString() = "" Then
                            txtinsdtlsPhno.Text = ""
                        Else
                            txtinsdtlsPhno.Text = dt1.Rows(i)("AIND_Phone").ToString()
                        End If
                        If dt1.Rows(i)("AIND_FAX").ToString() = "" Then
                            txtInsdtlsFAX.Text = ""
                        Else
                            txtInsdtlsFAX.Text = dt1.Rows(i)("AIND_FAX").ToString()
                        End If
                        If dt1.Rows(i)("AIND_Email").ToString() = "" Then
                            txtInsdtlsEmail.Text = ""
                        Else
                            txtInsdtlsEmail.Text = dt1.Rows(i)("AIND_Email").ToString()
                        End If
                        If dt1.Rows(i)("AIND_Website").ToString() = "" Then
                            txtInsdtlsWebsite.Text = ""
                        Else
                            txtInsdtlsWebsite.Text = dt1.Rows(i)("AIND_Website").ToString()
                        End If
                        If dt1.Rows(i)("AIND_Maintainedby").ToString() = "" Then
                            txtinsdtlsMaintainedby.Text = ""
                        Else
                            txtinsdtlsMaintainedby.Text = dt1.Rows(i)("AIND_Maintainedby").ToString()
                        End If
                        If dt1.Rows(i)("AIND_MaintainedContactPerson").ToString() = "" Then
                            txtmainbyconperson.Text = ""
                        Else
                            txtmainbyconperson.Text = dt1.Rows(i)("AIND_MaintainedContactPerson").ToString()
                        End If
                        If dt1.Rows(i)("AIND_MaintainedPhone").ToString() = "" Then
                            txtMainphno.Text = ""
                        Else
                            txtMainphno.Text = dt1.Rows(i)("AIND_MaintainedPhone").ToString()
                        End If
                        If dt1.Rows(i)("AIND_MaintainedFax").ToString() = "" Then
                            txtmainFAX.Text = ""
                        Else
                            txtmainFAX.Text = dt1.Rows(i)("AIND_MaintainedFax").ToString()
                        End If
                        If dt1.Rows(i)("AIND_MaintainedAddress").ToString() = "" Then
                            txtMainaddress.Text = ""
                        Else
                            txtMainaddress.Text = dt1.Rows(i)("AIND_MaintainedAddress").ToString()
                        End If
                        If dt1.Rows(i)("AIND_MaintainedAddress").ToString() = "" Then
                            txtMainaddress.Text = ""
                        Else
                            txtMainaddress.Text = dt1.Rows(i)("AIND_MaintainedAddress").ToString()
                        End If
                        If dt1.Rows(i)("AIND_MaintainedEmail").ToString() = "" Then
                            txtMainEmail.Text = ""
                        Else
                            txtMainEmail.Text = dt1.Rows(i)("AIND_MaintainedEmail").ToString()
                        End If
                        If dt1.Rows(i)("AIND_MaintainedWebsite").ToString() = "" Then
                            txtMainwebsite.Text = ""
                        Else
                            txtMainwebsite.Text = dt1.Rows(i)("AIND_MaintainedWebsite").ToString()
                        End If
                        If dt1.Rows(i)("AIND_MaintainedWebsite").ToString() = "" Then
                            txtMainwebsite.Text = ""
                        Else
                            txtMainwebsite.Text = dt1.Rows(i)("AIND_MaintainedWebsite").ToString()
                        End If
                    Next
                Else
                    clear2()
                End If
                dt2 = objAsstAddnDtls.LoadInsuranceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssetNo.SelectedValue)
                If dt2.Rows.Count > 0 Then
                    For i = 0 To dt2.Rows.Count - 1
                        If dt2.Rows(i)("AID_PolicyType").ToString() = "" Then
                            ddlploicytype.SelectedIndex = 0
                        Else
                            ddlploicytype.SelectedIndex = dt2.Rows(i)("AID_PolicyType").ToString()
                        End If
                        If dt2.Rows(i)("AID_PolicyNo").ToString() = "" Then
                            txtPolcyno.Text = ""
                        Else
                            txtPolcyno.Text = dt2.Rows(i)("AID_PolicyNo").ToString()
                        End If
                        If dt2.Rows(i)("AID_PolicyAmount").ToString() = "" Then
                            txtPolicyAmt.Text = ""
                        Else
                            txtPolicyAmt.Text = dt2.Rows(i)("AID_PolicyAmount").ToString()
                        End If
                        If dt2.Rows(i)("AID_Premiumpaid").ToString() = "" Then
                            txtPremiumpaid.Text = ""
                        Else
                            txtPremiumpaid.Text = dt2.Rows(i)("AID_Premiumpaid").ToString()
                        End If
                        If dt2.Rows(i)("AID_TermDate").ToString() = "" Then
                            txtTermDate.Text = ""
                        Else
                            txtTermDate.Text = dt2.Rows(i)("AID_TermDate").ToString()
                        End If
                        If dt2.Rows(i)("AID_ToDate").ToString() = "" Then
                            txtTermToDate.Text = ""
                        Else
                            txtTermToDate.Text = dt2.Rows(i)("AID_ToDate").ToString()
                        End If
                        If dt2.Rows(i)("AID_InsuranceComp").ToString() = "" Then
                            txtinscomname.Text = ""
                        Else
                            txtinscomname.Text = dt2.Rows(i)("AID_InsuranceComp").ToString()
                        End If
                        If dt2.Rows(i)("AID_ContactPerson").ToString() = "" Then
                            txtinsConPerns.Text = ""
                        Else
                            txtinsConPerns.Text = dt2.Rows(i)("AID_ContactPerson").ToString()
                        End If
                        If dt2.Rows(i)("AID_Phone").ToString() = "" Then
                            txtInsPhno.Text = ""
                        Else
                            txtInsPhno.Text = dt2.Rows(i)("AID_Phone").ToString()
                        End If
                        If dt2.Rows(i)("AID_Fax").ToString() = "" Then
                            txtinsFAX.Text = ""
                        Else
                            txtinsFAX.Text = dt2.Rows(i)("AID_Fax").ToString()
                        End If
                        If dt2.Rows(i)("AID_Address").ToString() = "" Then
                            txtInsAddress.Text = ""
                        Else
                            txtInsAddress.Text = dt2.Rows(i)("AID_Address").ToString()
                        End If
                        If dt2.Rows(i)("AID_Email").ToString() = "" Then
                            txtinsEmail.Text = ""
                        Else
                            txtinsEmail.Text = dt2.Rows(i)("AID_Email").ToString()
                        End If
                        If dt2.Rows(i)("AID_Website").ToString() = "" Then
                            txtinsWebsite.Text = ""
                        Else
                            txtinsWebsite.Text = dt2.Rows(i)("AID_Website").ToString()
                        End If
                    Next
                Else
                    clear3()
                End If

                dt3 = objAsstAddnDtls.LoadMaintananceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssetNo.SelectedValue)
                If dt3.Rows.Count > 0 Then
                    For i = 0 To dt3.Rows.Count - 1
                        If dt3.Rows(i)("AMD_MaintainedBy").ToString() = "" Then
                            txtMaintained.Text = ""
                        Else
                            txtMaintained.Text = dt3.Rows(i)("AMD_MaintainedBy").ToString()
                        End If
                        If dt3.Rows(i)("AMD_ContactPerson").ToString() = "" Then
                            txtMainconPerson.Text = ""
                        Else
                            txtMainconPerson.Text = dt3.Rows(i)("AMD_ContactPerson").ToString()
                        End If
                        If dt3.Rows(i)("AMD_Phone").ToString() = "" Then
                            txtManufacturedPhoneno.Text = ""
                        Else
                            txtManufacturedPhoneno.Text = dt3.Rows(i)("AMD_Phone").ToString()
                        End If
                        If dt3.Rows(i)("AMD_Fax").ToString() = "" Then
                            txtManufacturedFAX.Text = ""
                        Else
                            txtManufacturedFAX.Text = dt3.Rows(i)("AMD_Fax").ToString()
                        End If
                        If dt3.Rows(i)("AMD_Address").ToString() = "" Then
                            txtManufacturedAddress.Text = ""
                        Else
                            txtManufacturedAddress.Text = dt3.Rows(i)("AMD_Address").ToString()
                        End If
                        If dt3.Rows(i)("AMD_EmailID").ToString() = "" Then
                            txtEmail.Text = ""
                        Else
                            txtEmail.Text = dt3.Rows(i)("AMD_EmailID").ToString()
                        End If
                        If dt3.Rows(i)("AMD_Website").ToString() = "" Then
                            txtWebsite.Text = ""
                        Else
                            txtWebsite.Text = dt3.Rows(i)("AMD_Website").ToString()
                        End If
                        If dt3.Rows(i)("AMD_Companyname").ToString() = "" Then
                            txtbxAMCompname.Text = ""
                        Else
                            txtbxAMCompname.Text = dt3.Rows(i)("AMD_Companyname").ToString()
                        End If
                        If dt3.Rows(i)("AMD_AmcAmount").ToString() = "" Then
                            txtAMCAmount.Text = ""
                        Else
                            txtAMCAmount.Text = dt3.Rows(i)("AMD_AmcAmount").ToString()
                        End If
                        If dt3.Rows(i)("AMD_AmcTermDate").ToString() = "" Then
                            txtbxAMCfrmDate.Text = ""
                        Else
                            txtbxAMCfrmDate.Text = objGen.FormatDtForRDBMS(dt3.Rows(i)("AMD_AmcTermDate").ToString(), "D")
                        End If
                        If dt3.Rows(i)("AMD_AmcTo").ToString() = "" Then
                            txtbxAMCtoDate.Text = ""
                        Else
                            txtbxAMCtoDate.Text = objGen.FormatDtForRDBMS(dt3.Rows(i)("AMD_AmcTo").ToString(), "D")
                        End If
                        If IsDBNull(dt3.Rows(i)("AMD_AmcPaymentterm").ToString()) = False Then
                            If dt3.Rows(i)("AMD_AmcPaymentterm").ToString() = 1 Then
                                rbtnOnetime.Checked = True
                            ElseIf dt3.Rows(i)("AMD_AmcPaymentterm").ToString() = 2 Then
                                rbtnInstlmnt.Checked = True
                            End If
                        End If
                        If dt3.Rows(i)("AMD_NoInstalment").ToString() = "" Then
                            txtNoInstlmt.Text = ""
                        Else
                            txtNoInstlmt.Text = dt3.Rows(i)("AMD_NoInstalment").ToString()
                        End If
                        If dt3.Rows(i)("AMD_InstalmentAmnt").ToString() = "" Then
                            txtInstamount.Text = ""
                        Else
                            txtInstamount.Text = dt3.Rows(i)("AMD_InstalmentAmnt").ToString()
                        End If
                        If dt3.Rows(i)("AMD_TotalPaidinstalment").ToString() = "" Then
                            txtTotalPaidIstAmnt.Text = ""
                        Else
                            txtTotalPaidIstAmnt.Text = dt3.Rows(i)("AMD_TotalPaidinstalment").ToString()
                        End If

                        If dt3.Rows(i)("AMD_TotalAmnt").ToString() = "" Then
                            txtAmount.Text = ""
                        Else
                            txtAmount.Text = dt3.Rows(i)("AMD_TotalAmnt").ToString()
                        End If
                    Next
                Else
                    clear4()
                End If
                dt4 = objAsstAddnDtls.LoadAssetLoanDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssetNo.SelectedValue)
                If dt4.Rows.Count > 0 Then
                    For i = 0 To dt4.Rows.Count - 1
                        If dt4.Rows(i)("AOL_EmpID").ToString() = "" Then
                            ddlEmployee.SelectedValue = ""
                        Else
                            ddlEmployee.SelectedValue = dt4.Rows(i)("AOL_EmpID").ToString()
                        End If
                        If dt4.Rows(i)("AOL_EmpCode").ToString() = "" Then
                            txtEmpCOde.Text = ""
                        Else
                            txtEmpCOde.Text = dt4.Rows(i)("AOL_EmpCode").ToString()
                        End If
                        If dt4.Rows(i)("AOL_AssetID").ToString() = "" Then
                            ddlAssetType.SelectedValue = ""
                        Else
                            ddlAssetType.SelectedValue = dt4.Rows(i)("AOL_AssetID").ToString()
                        End If
                        If dt4.Rows(i)("AOL_SerNo").ToString() = "" Then
                            txtSerialNo.Text = ""
                        Else
                            txtSerialNo.Text = dt4.Rows(i)("AOL_SerNo").ToString()
                        End If
                        If dt4.Rows(i)("AOL_ApprxmateVal").ToString() = "" Then
                            txtApprxmiateVal.Text = ""
                        Else
                            txtApprxmiateVal.Text = dt4.Rows(i)("AOL_ApprxmateVal").ToString()
                        End If
                        If dt4.Rows(i)("AOL_IssueDate").ToString() = "" Then
                            txtCustIssueDate.Text = ""
                        Else
                            txtCustIssueDate.Text = objGen.FormatDtForRDBMS(dt4.Rows(i)("AOL_IssueDate").ToString(), "D")
                        End If
                        If dt4.Rows(i)("AOL_DueDate").ToString() = "" Then
                            txtCustDueDate.Text = ""
                        Else
                            txtCustDueDate.Text = objGen.FormatDtForRDBMS(dt4.Rows(i)("AOL_DueDate").ToString(), "D")
                        End If
                        If dt4.Rows(i)("AOL_RecvdDate").ToString() = "" Then
                            txtRecvdDate.Text = ""
                        Else
                            txtRecvdDate.Text = objGen.FormatDtForRDBMS(dt4.Rows(i)("AOL_RecvdDate").ToString(), "D")
                        End If
                        If dt4.Rows(i)("AOL_ReturnDate").ToString() = "" Then
                            txtretDate.Text = ""
                        Else
                            txtretDate.Text = objGen.FormatDtForRDBMS(dt4.Rows(i)("AOL_ReturnDate").ToString(), "D")
                        End If
                        If dt4.Rows(i)("AOL_CondWhenIssued").ToString() = "" Then
                            txtConditnIssued.Text = ""
                        Else
                            txtConditnIssued.Text = dt4.Rows(i)("AOL_CondWhenIssued").ToString()
                        End If
                        If dt4.Rows(i)("AOL_CondOnRecvd").ToString() = "" Then
                            txtCondOnrecvd.Text = ""
                        Else
                            txtCondOnrecvd.Text = dt4.Rows(i)("AOL_CondOnRecvd").ToString()
                        End If
                        If dt4.Rows(i)("AOL_Remarks").ToString() = "" Then
                            txtLnRemarks.Text = ""
                        Else
                            txtLnRemarks.Text = dt4.Rows(i)("AOL_Remarks").ToString()
                        End If

                    Next
                Else

                    clear5()
                End If
                dt5 = objAsstAddnDtls.LoadAsstTakenOnLoanDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssetNo.SelectedValue)
                If dt5.Rows.Count > 0 Then
                    For i = 0 To dt5.Rows.Count - 1
                        If dt5.Rows(i)("ATL_LoanTowhm").ToString() = "" Then
                            txtloanWhome.Text = ""
                        Else
                            txtloanWhome.Text = dt5.Rows(i)("ATL_LoanTowhm").ToString()
                        End If
                        If dt5.Rows(i)("ATL_LoanToAddrss").ToString() = "" Then
                            txtLoanAddress.Text = ""
                        Else
                            txtLoanAddress.Text = dt5.Rows(i)("ATL_LoanToAddrss").ToString()
                        End If
                        If dt5.Rows(i)("ATL_LoanAmt").ToString() = "" Then
                            txtloanAmount.Text = ""
                        Else
                            txtloanAmount.Text = dt5.Rows(i)("ATL_LoanAmt").ToString()
                        End If
                        If dt5.Rows(i)("ATL_LoanAggrmntNo").ToString() = "" Then
                            txtloanAgrmnt.Text = ""
                        Else
                            txtloanAgrmnt.Text = dt5.Rows(i)("ATL_LoanAggrmntNo").ToString()
                        End If
                        If dt5.Rows(i)("ATL_dLoanDate").ToString() = "" Then
                            txtloandate.Text = ""
                        Else
                            txtloandate.Text = objGen.FormatDtForRDBMS(dt5.Rows(i)("ATL_dLoanDate").ToString(), "D")
                        End If
                        If dt5.Rows(i)("ATL_ImpCrncyType").ToString() = "" Then
                            ddlCurrencytypeloan.SelectedValue = ""
                        Else
                            ddlCurrencytypeloan.SelectedValue = dt5.Rows(i)("ATL_ImpCrncyType").ToString()
                        End If
                        If dt5.Rows(i)("ATL_EcxhgDate").ToString() = "" Then
                            txtLoanExcngDate.Text = ""
                        Else
                            txtLoanExcngDate.Text = objGen.FormatDtForRDBMS(dt5.Rows(i)("ATL_EcxhgDate").ToString(), "D")
                        End If
                        If dt5.Rows(i)("ATL_ExchgAmt").ToString() = "" Then
                            txtLoanExchgeAmt.Text = ""
                        Else
                            txtLoanExchgeAmt.Text = dt5.Rows(i)("ATL_ExchgAmt").ToString()
                        End If
                        If dt5.Rows(i)("ATL_AmountRpees").ToString() = "" Then
                            txtLoanAmtRs.Text = ""
                        Else
                            txtLoanAmtRs.Text = dt5.Rows(i)("ATL_AmountRpees").ToString()
                        End If
                    Next
                Else
                    clear6()
                End If
                GetAttachFile(ddlAssetNo.SelectedItem.Text)
                lblBadgeCount.Text = Convert.ToString(objAsstAddnDtls.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, ddlAssetNo.SelectedItem.Text))
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAssetNo_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnSupplierSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSupplierSave.Click
        Dim iId As Integer
        Try
            If ddlAssetNo.SelectedIndex > 0 Then
                iId = objAsstAddnDtls.SaveSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlAssetNo.SelectedValue, txtbxSname.Text, txtbxConPerson.Text, txtbxAddress.Text, txtbxPhoneNo.Text, txtbxFax.Text, txtbxEmail.Text, txtbxwebsite.Text)
                If iId > 0 Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                End If
            Else
                lblAssetAdditionDtlsMsg.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSupplierSave_Click")
        End Try
    End Sub
    Public Sub clear()
        Try
            lblError.Text = ""
            txtbxSname.Text = "" : txtbxConPerson.Text = "" : txtbxPhoneNo.Text = "" : txtbxFax.Text = "" : txtbxEmail.Text = "" : txtbxwebsite.Text = "" : txtbxAddress.Text = ""
            ddlDeviceNo.SelectedIndex = 0 : ddlSW.SelectedIndex = 0 : txtversion.Text = "" : txtDateofInstlln.Text = "" : txtUnintleddate.Text = "" : txtreintled.Text = ""
            txtinstedBy.Text = "" : txtdbdetails.Text = "" : txtInsDescription.Text = "" : txtinsplace.Text = "" : txtConPerson.Text = "" : txtinsdtlsPhno.Text = ""
            txtInsdtlsFAX.Text = "" : txtInsdtlsAddress.Text = "" : txtInsdtlsEmail.Text = "" : txtInsdtlsWebsite.Text = "" : txtinsdtlsMaintainedby.Text = "" : txtmainbyconperson.Text = ""
            txtMainphno.Text = "" : txtmainFAX.Text = "" : txtMainaddress.Text = "" : txtMainEmail.Text = "" : txtMainwebsite.Text = ""
            ddlploicytype.SelectedIndex = 0 : txtPolcyno.Text = "" : txtPolicyAmt.Text = "" : txtPremiumpaid.Text = "" : txtTermDate.Text = "" : txtTermToDate.Text = ""
            txtinscomname.Text = "" : txtinsConPerns.Text = "" : txtInsPhno.Text = "" : txtinsFAX.Text = "" : txtInsAddress.Text = "" : txtinsEmail.Text = ""
            txtinsWebsite.Text = "" : txtMaintained.Text = "" : txtMainconPerson.Text = "" : txtManufacturedPhoneno.Text = "" : txtManufacturedFAX.Text = "" : txtManufacturedAddress.Text = ""
            txtEmail.Text = "" : txtWebsite.Text = "" : txtbxAMCompname.Text = "" : txtbxAMCfrmDate.Text = "" : txtbxAMCtoDate.Text = "" : txtAMCAmount.Text = ""
            rbtnOnetime.Checked = False : rbtnInstlmnt.Checked = False : txtNoInstlmt.Text = "" : txtInstamount.Text = "" : txtTotalPaidIstAmnt.Text = "" : txtAmount.Text = ""
            ddldevicetype.SelectedIndex = 0 : txtxDeviceNo.Text = "" : txtModelname.Text = "" : txtmanufacturedby.Text = "" : txtdateofpurchase.Text = "" : txtWarranty.Text = ""
            txtempname.Text = "" : txtDetails.Text = "" : rbtnstandalone.Checked = False : rbtnServer.Checked = False : rbtnAttachedServer.Checked = False
            txtDescription.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSupplierSave_Click")
        End Try
    End Sub

    Private Sub imgbtReferesh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtReferesh.Click
        Try
            clear()
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtReferesh_Click")
        End Try
    End Sub
    Public Sub clear1()
        Try
            ddldevicetype.SelectedIndex = 0 : txtxDeviceNo.Text = "" : txtModelname.Text = "" : txtmanufacturedby.Text = ""
            txtdateofpurchase.Text = "" : txtDetails.Text = "" : txtWarranty.Text = "" : txtempname.Text = "" : rbtnstandalone.Checked = False
            txtbxSname.Text = "" : txtbxConPerson.Text = "" : txtbxAddress.Text = "" : txtbxPhoneNo.Text = ""
            txtbxFax.Text = "" : txtbxEmail.Text = "" : txtbxwebsite.Text = "" : txtDescription.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "clear1")
        End Try
    End Sub
    Public Sub clear2()
        Try
            ddlDeviceNo.SelectedIndex = 0 : ddlSW.SelectedIndex = 0 : txtversion.Text = "" : txtDateofInstlln.Text = "" : txtUnintleddate.Text = ""
            txtreintled.Text = "" : txtinstedBy.Text = "" : txtdbdetails.Text = "" : txtInsDescription.Text = "" : txtinsplace.Text = ""
            txtConPerson.Text = "" : txtInsdtlsAddress.Text = "" : txtinsdtlsPhno.Text = "" : txtInsdtlsFAX.Text = "" : txtInsdtlsEmail.Text = ""
            txtInsdtlsWebsite.Text = "" : txtinsdtlsMaintainedby.Text = "" : txtmainbyconperson.Text = "" : txtMainphno.Text = ""
            txtmainFAX.Text = "" : txtMainaddress.Text = "" : txtMainaddress.Text = "" : txtMainEmail.Text = ""
            txtMainwebsite.Text = "" : txtMainwebsite.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "clear2")
        End Try
    End Sub
    Public Sub clear3()
        Try

            ddlploicytype.SelectedIndex = 0 : txtPolcyno.Text = "" : txtPolicyAmt.Text = "" : txtPremiumpaid.Text = "" : txtTermDate.Text = "" : txtTermToDate.Text = ""
            txtinscomname.Text = "" : txtinsConPerns.Text = "" : txtInsPhno.Text = "" : txtinsFAX.Text = "" : txtInsAddress.Text = ""
            txtinsEmail.Text = "" : txtinsWebsite.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "clear3")
        End Try
    End Sub
    Public Sub clear4()
        Try
            txtMaintained.Text = "" : txtMainconPerson.Text = "" : txtManufacturedPhoneno.Text = "" : txtManufacturedFAX.Text = ""
            txtManufacturedAddress.Text = "" : txtEmail.Text = "" : txtWebsite.Text = "" : txtbxAMCompname.Text = "" : txtAMCAmount.Text = ""
            txtbxAMCfrmDate.Text = "" : txtbxAMCtoDate.Text = "" : rbtnOnetime.Checked = False : txtNoInstlmt.Text = ""
            txtInstamount.Text = "" : txtTotalPaidIstAmnt.Text = "" : txtAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "clear4")
        End Try
    End Sub
    Public Sub clear5()
        Try
            ddlEmployee.SelectedIndex = 0 : txtEmpCOde.Text = "" : ddlAssetType.SelectedIndex = 0 : txtSerialNo.Text = ""
            txtApprxmiateVal.Text = "" : txtCustIssueDate.Text = "" : txtCustDueDate.Text = "" : txtRecvdDate.Text = "" : txtretDate.Text = ""
            txtConditnIssued.Text = "" : txtCondOnrecvd.Text = "" : txtLnRemarks.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "clear5")
        End Try
    End Sub
    Public Sub clear6()
        Try
            txtloanWhome.Text = "" : txtLoanAddress.Text = "" : txtloanAmount.Text = "" : txtloanAgrmnt.Text = ""
            txtloandate.Text = "" : ddlCurrencytypeloan.SelectedIndex = 0 : txtLoanExcngDate.Text = "" : txtLoanExchgeAmt.Text = "" : txtLoanAmtRs.Text = ""
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "clear6")
        End Try
    End Sub
    Private Sub ddlEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmployee.SelectedIndexChanged
        Try
            If ddlEmployee.SelectedIndex > 0 Then
                txtEmpCOde.Text = objAsstAddnDtls.GetEmpCode(sSession.AccessCode, sSession.AccessCodeID, ddlEmployee.SelectedValue)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub loademployee()
        Dim dt As New DataTable
        Try
            dt = objAsstAddnDtls.Loademployee(sSession.AccessCode, sSession.AccessCodeID)
            ddlEmployee.DataTextField = "usr_FullName"
            ddlEmployee.DataValueField = "usr_Id"
            ddlEmployee.DataSource = dt
            ddlEmployee.DataBind()
            ddlEmployee.Items.Insert(0, "Select Employee")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgBtnCustdtls_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnCustdtls.Click
        Try
            If ddlAssetNo.SelectedIndex > 0 Then
                Dim iId As Integer
                txtCustIssueDate.Text = Date.ParseExact(txtCustIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                txtCustDueDate.Text = Date.ParseExact(txtCustDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                txtRecvdDate.Text = Date.ParseExact(txtRecvdDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                txtretDate.Text = Date.ParseExact(txtretDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                iId = objAsstAddnDtls.SaveAssetLoanDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlAssetNo.SelectedValue, ddlEmployee.SelectedValue, txtEmpCOde.Text, ddlAssetType.SelectedValue, txtSerialNo.Text, txtApprxmiateVal.Text, txtCustIssueDate.Text, txtCustDueDate.Text, txtRecvdDate.Text, txtretDate.Text, txtConditnIssued.Text, txtCondOnrecvd.Text, txtLnRemarks.Text)
                If iId > 0 Then
                    lblAssetAdditionDtlsMsg.Text = "Successfully Saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                End If
            Else
                lblAssetAdditionDtlsMsg.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgBtnCustdtls_Click")
        End Try
    End Sub
    Private Sub LoadCurrency()
        Dim dCurrencyDt As New DataTable
        Try
            dCurrencyDt = objAsstAddnDtls.LoadCurrency(sSession.AccessCode, sSession.AccessCodeID)
            ddlCurrencytypeloan.DataSource = dCurrencyDt
            ddlCurrencytypeloan.DataTextField = "CUR_CountryName"
            ddlCurrencytypeloan.DataValueField = "CUR_ID"
            ddlCurrencytypeloan.DataBind()
            ddlCurrencytypeloan.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCurrency")
        End Try
    End Sub

    Private Sub imgBtnLoanAsst_Click(sender As Object, e As ImageClickEventArgs) Handles imgBtnLoanAsst.Click
        Try
            If ddlAssetNo.SelectedIndex > 0 Then
                If txtloanWhome.Text <> "" Then
                    txtloandate.Text = Date.ParseExact(txtloandate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    txtLoanExcngDate.Text = Date.ParseExact(txtLoanExcngDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objAsstAddnDtls.SaveTakenLoanAsstDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlAssetNo.SelectedValue, txtloanWhome.Text, txtLoanAddress.Text, txtloanAmount.Text, txtloanAgrmnt.Text, txtloandate.Text, ddlCurrencytypeloan.SelectedValue, txtLoanExcngDate.Text, txtLoanExchgeAmt.Text, txtLoanAmtRs.Text)
                    lblAssetAdditionDtlsMsg.Text = "Successfully Saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                Else
                    lblAssetAdditionDtlsMsg.Text = "Enter the Details"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
                End If
            Else
                lblAssetAdditionDtlsMsg.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionalDetails').modal('show');", True)
            End If

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgBtnLoanAsst_Click")
        End Try
    End Sub
    Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
        Dim objBatch As clsIndexing.BatchScan
        Dim Arr() As String
        Try
            If gvattach.Rows.Count > 0 Then
                AutomaticIndexing()
                GetAttachFile(ddlAssetNo.SelectedItem.Text)
                gvattach.Visible = True
                '  gvattach.DataBind()
                lblBadgeCount.Text = Convert.ToString(objAsstAddnDtls.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, ddlAssetNo.SelectedItem.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
            Else
                lblError.Text = "Add the files before index"
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub AutomaticIndexing()
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
        Dim Arr() As String
        Dim dDate As Date
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        'Dim iCabinet As Integer
        'Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable
        Dim bCheckCabinet As Boolean

        Try
            If ddlAssetNo.SelectedIndex = 0 Then
                lblError.Text = "Select Asset No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlAssetNo.Focus()
                Exit Sub
            Else
                icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlRefno.SelectedItem.Text)
            End If

            iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Asset Additional Details")

            If ddlRefno.SelectedIndex = 0 Then
                lblError.Text = "Select Asset Reference Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlRefno.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlAssetNo.SelectedItem.Text)
            End If

            iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

            'If ddlType.SelectedIndex = 0 Then
            '    lblModelError.Text = "Select Type."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            '    ddlType.Focus()
            '    Exit Sub
            'Else
            '    iType = ddlType.SelectedValue
            'End If

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                If gvattach.Rows.Count > 0 Then
                    For i = 0 To gvattach.Rows.Count - 1
                        iPageDetailsid = 0
                        chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                        lblPath = gvattach.Rows(i).FindControl("lblPath")
                        If chkSelect.Checked = True Then
                            sPageExt = UCase(gvattach.Rows(i).Cells(3).Text)
                            sFilePath = lblPath.Text
                            sFileName = gvattach.Rows(i).Cells(2).Text
                            objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objGen.SafeSQL(txtTitle.Text.Trim)
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt
                            If gvKeywords.Rows.Count > 0 Then

                                For k = 0 To gvKeywords.Rows.Count - 1
                                    txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
                                    If txtKeywords.Text <> "" Then
                                        sKeywords = sKeywords & "," & txtKeywords.Text
                                    End If
                                Next
                            End If
                            If sKeywords.StartsWith(",") = True Then
                                sKeywords = sKeywords.Remove(0, 1)
                            End If
                            If sKeywords.EndsWith(",") = True Then
                                sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
                            End If
                            objIndex.sPGEKeyWORD = objGen.SafeSQL(sKeywords)
                            objIndex.sPGEOCRText = ""
                            objIndex.iPGESIZE = 0
                            objIndex.iPGECURRENT_VER = 0
                            Select Case UCase(sPageExt)
                                Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                                    objIndex.sPGEOBJECT = "IMAGE"
                                Case Else
                                    objIndex.sPGEOBJECT = "OLE"
                            End Select
                            objIndex.sPGESTATUS = "A"
                            objIndex.iPGESubCabinet = iSubCabinet
                            objIndex.iPgeUpdatedBy = sSession.UserID

                            objIndex.spgeDelflag = "A"
                            objIndex.iPGEQCUsrGrpId = 0
                            objIndex.sPGEFTPStatus = "F"
                            objIndex.iPGEbatchname = objIndex.iPGEBASENAME
                            objIndex.spgeOrignalFileName = objGen.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
                            objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)

                            If gvDocumentType.Rows.Count > 0 Then
                                For j = 0 To gvDocumentType.Rows.Count - 1
                                    lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                                    txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                                    If objIndex.iPGEBASENAME = iPageDetailsid Then
                                        objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If Arr(0) = "3" Then
                        lblError.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            If sFileInDB = "FALSE" Then
                sImagePath = objIndex.GetImagePath(sSession.AccessCode)
                sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
                sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                If System.IO.File.Exists(sImagePath) = False Then
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub btnAttch_Click(sender As Object, e As EventArgs) Handles btnAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Try
            lblError.Text = "" : iDocID = 0

            If ddlAssetNo.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Asset Item Code."
                ddlAssetNo.Focus()
                Exit Sub
            End If

            Dim hfc As HttpFileCollection = Request.Files

            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dt.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        dt = Session("Attachment")
                        If dt.Rows.Count = 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)

                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        ElseIf dt.Rows.Count > 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        End If
                    End If
                Next
            End If

            If dt.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            End If

            Session("Attachment") = dt
            gvattach.DataSource = dt
            gvattach.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub gvattach_PreRender(sender As Object, e As EventArgs) Handles gvattach.PreRender
        Try
            If gvattach.Rows.Count > 0 Then
                gvattach.UseAccessibleHeader = True
                gvattach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvattach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
        End Try
    End Sub
    'Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
    '    Dim chkField As New CheckBox, chkAll As New CheckBox
    '    Dim iIndx As Integer
    '    Try
    '        chkAll = CType(sender, CheckBox)
    '        If chkAll.Checked = True Then
    '            For iIndx = 0 To gvattach.Rows.Count - 1
    '                chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = True
    '            Next
    '        Else
    '            For iIndx = 0 To gvattach.Rows.Count - 1
    '                chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = False
    '            Next
    '        End If
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Public Sub GetAttachFile(ByVal sTrNo As String)
        Dim dRow As DataRow
        Dim dt, dt1 As New DataTable
        Try
            dt.Columns.Add("FilePath")
            dt.Columns.Add("FileName")
            dt.Columns.Add("Extension")
            dt.Columns.Add("CreatedOn")

            dt1 = objAsstAddnDtls.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("FilePath") = ""
                    dRow("FileName") = dt1.Rows(i)("pge_Orignalfilename")
                    dRow("Extension") = dt1.Rows(i)("pge_ext")
                    dRow("CreatedOn") = objGen.FormatDtForRDBMS(dt1.Rows(i)("pge_createdon"), "D")
                    dt.Rows.Add(dRow)
                Next
            End If

            gvattach.DataSource = dt
            gvattach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim iCabinetID, iSubCabinetID, iFolderID As Integer
        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Dim dt As New DataTable
        Try
            If ddlAssetNo.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlAssetNo.SelectedItem.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Asset Additional Details")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, ddlRefno.SelectedItem.Text)

                    dt = objAsstAddnDtls.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCabinetID, iSubCabinetID, iFolderID)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            sSelectedChecksIDs = sSelectedChecksIDs & "," & dt.Rows(i)("PGE_BASENAME")
                        Next
                    End If

                    If sSelectedChecksIDs.StartsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                    End If
                    If sSelectedChecksIDs.EndsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(Len(sSelectedChecksIDs) - 1, 1)
                    End If

                    oSelectedCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iCabinetID))
                    oSelectedSubCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iSubCabinetID))
                    oSelectedFolID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iFolderID))
                    oSelectedChecksIDs = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedChecksIDs))
                    oSelectedIndexID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(0))

                    Response.Redirect(String.Format("~/Viewer/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={7}&SelectedDescID={8}&SelectedFrmtID={9}&SelectedCrByID={10}&SelectedIndexID={11}", "", "", oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, "", "", "", "", "", oSelectedIndexID), False)
                Else
                    lblError.Text = "No Attachments to view"
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Existing Asset Item COde No"
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
        End Try
    End Sub
End Class
